const { chromium } = require('playwright');

const base = 'https://www.postybell.co.il';
const adminId = '910000101';
const adminPassword = '910000101';

(async () => {
  const browser = await chromium.launch({
    headless: true,
    args: ['--host-resolver-rules=MAP www.postybell.co.il 192.168.1.1'],
  });
  const context = await browser.newContext({
    ignoreHTTPSErrors: true,
    locale: 'he-IL',
    viewport: { width: 1280, height: 900 },
  });
  const page = await context.newPage();

  await page.goto(`${base}/Account/Login`, { waitUntil: 'networkidle', timeout: 30000 });
  await page.locator('input[name="IdNumber"], #IdNumber').fill(adminId);
  await page.locator('input[name="Password"], input[type="password"]').fill(adminPassword);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'networkidle', timeout: 30000 }).catch(() => null),
    page.locator('form button[type="submit"]').first().click(),
  ]);

  await page.goto(`${base}/Report/Manual`, { waitUntil: 'networkidle', timeout: 30000 });
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'networkidle', timeout: 30000 }),
    page.locator('form button[type="submit"]').first().click(),
  ]);

  const result = await page.evaluate(() => {
    const host = document.createElement('div');
    host.innerHTML = `
      <select id="fieldFramework"><option value="1">A</option></select>
      <select id="fieldConclusionFramework"><option value="1">A</option></select>
      <select data-name="row.FrameworkId"><option value="1">A</option></select>
      <table><tr><td data-edit-field="FrameworkId"><select class="cell-field"><option value="1">A</option></select></td></tr></table>
    `;
    document.body.appendChild(host);
    window.initSubjectAutocomplete(host);
    const selects = Array.from(host.querySelectorAll('select'));
    return {
      choicesAvailable: typeof window.Choices !== 'undefined',
      initialized: selects.map((select) => select.dataset.subjectAutocompleteInit === '1'),
      frameworkContainers: host.querySelectorAll('.choices-framework-autocomplete').length,
    };
  });

  console.log(JSON.stringify(result, null, 2));
  await browser.close();

  const ok = result.choicesAvailable
    && result.initialized.every(Boolean)
    && result.frameworkContainers === 4;
  process.exit(ok ? 0 : 1);
})().catch((err) => {
  console.error(err);
  process.exit(1);
});
