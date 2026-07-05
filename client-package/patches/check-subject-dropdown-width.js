const { chromium } = require('playwright');

const origin = 'https://www.postybell.co.il';
const loginId = process.env.POSTYBELL_LOGIN_ID || 'axioma.pm.test';
const password = process.env.POSTYBELL_PASSWORD || 'Axioma2026!';

(async () => {
  const browser = await chromium.launch({
    args: ['--host-resolver-rules=MAP postybell.co.il 192.168.1.1,MAP www.postybell.co.il 192.168.1.1'],
  });
  const page = await browser.newPage({
    ignoreHTTPSErrors: true,
    viewport: { width: 1440, height: 1000 },
  });

  await page.goto(`${origin}/Account/Login`, { waitUntil: 'domcontentloaded', timeout: 30000 });
  await page.fill('input[name="IdNumber"]', loginId);
  await page.fill('input[name="Password"]', password);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 30000 }),
    page.click('button[type="submit"]'),
  ]);

  await page.goto(`${origin}/Dashboard`, { waitUntil: 'domcontentloaded', timeout: 30000 });
  await page.waitForTimeout(1200);
  await page.locator('.choices-subject-autocomplete').first().click();
  await page.waitForTimeout(300);

  const result = await page.evaluate(() => {
    const select = document.querySelector('select[name="Subject1Id"]');
    const choices = select && (select.closest('.choices') || document.querySelector('.choices-subject-autocomplete'));
    const dropdown = choices && choices.querySelector('.choices__list--dropdown, .choices__list[aria-expanded]');
    const item = dropdown && dropdown.querySelector('.choices__item');
    const rect = el => {
      if (!el) return null;
      const r = el.getBoundingClientRect();
      return { x: r.x, y: r.y, width: r.width, height: r.height };
    };

    return {
      classes: choices ? choices.className : '',
      control: rect(choices),
      dropdown: rect(dropdown),
      itemWhiteSpace: item ? getComputedStyle(item).whiteSpace : '',
      itemWordBreak: item ? getComputedStyle(item).wordBreak : '',
      itemText: item ? item.textContent.trim().slice(0, 80) : '',
    };
  });

  console.log(JSON.stringify(result, null, 2));
  await page.screenshot({ path: 'postybell-subject-autocomplete-dashboard.png', fullPage: true });
  await browser.close();

  if (!result.dropdown || result.dropdown.width < 300 || !result.classes.includes('choices-subject-autocomplete')) {
    process.exitCode = 2;
  }
})();
