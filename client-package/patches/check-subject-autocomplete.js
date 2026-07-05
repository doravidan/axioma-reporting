const { chromium } = require('playwright');

const loginId = process.env.POSTYBELL_LOGIN_ID || 'axioma.pm.test';
const password = process.env.POSTYBELL_PASSWORD || 'Axioma2026!';
const origin = 'https://www.postybell.co.il';

async function login(page) {
  await page.goto(`${origin}/Account/Login`, { waitUntil: 'domcontentloaded', timeout: 30000 });
  await page.fill('input[name="IdNumber"]', loginId);
  await page.fill('input[name="Password"]', password);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 30000 }),
    page.click('button[type="submit"]'),
  ]);
}

async function subjectState(page) {
  return page.evaluate(() => {
    return Array.from(document.querySelectorAll([
      '#fieldSubject1',
      '#fieldSubject2',
      'select[name="Subject1Id"]',
      'select[name="Subject2Id"]',
      'select[name="row.Subject1Id"]',
      'select[name="row.Subject2Id"]',
      'select[data-name="row.Subject1Id"]',
      'select[data-name="row.Subject2Id"]',
      'select[data-subject-autocomplete="1"]'
    ].join(','))).map(select => {
      const wrapper = select.closest('.choices') || select.nextElementSibling;
      return {
        id: select.id,
        name: select.name,
        dataName: select.dataset.name || '',
        optionCount: select.options.length,
        hidden: select.hidden || select.style.display === 'none' || select.classList.contains('choices__input'),
        choicesInit: select.dataset.subjectAutocompleteInit || '',
        hasChoices: !!(wrapper && wrapper.classList && wrapper.classList.contains('choices')),
        hasSearchInput: !!(wrapper && wrapper.querySelector('input[type="search"], input.choices__input')),
      };
    });
  });
}

(async () => {
  const browser = await chromium.launch({
    args: ['--host-resolver-rules=MAP postybell.co.il 192.168.1.1,MAP www.postybell.co.il 192.168.1.1'],
  });
  const page = await browser.newPage({ ignoreHTTPSErrors: true });
  await login(page);

  const paths = process.argv.slice(2);
  if (!paths.length) paths.push('/Dashboard');

  let failed = false;
  for (const path of paths) {
    const response = await page.goto(`${origin}${path}`, { waitUntil: 'domcontentloaded', timeout: 30000 });
    await page.waitForTimeout(1200);
    const state = await subjectState(page);
    console.log(`PATH ${path}`);
    console.log(`STATUS ${response ? response.status() : 'none'}`);
    console.log(`URL ${page.url()}`);
    console.log(`TITLE ${await page.title()}`);
    console.log(JSON.stringify(state, null, 2));
    if (state.length === 0 || state.some(x => !x.choicesInit || !x.hasChoices || !x.hasSearchInput)) {
      failed = true;
    }
  }

  await browser.close();
  if (failed) process.exitCode = 2;
})();
