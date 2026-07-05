const { chromium } = require('playwright');

const origin = 'https://postybell.co.il';
const loginId = process.env.POSTYBELL_LOGIN_ID || '029345400';
const password = process.env.POSTYBELL_PASSWORD || loginId;

(async () => {
  const browser = await chromium.launch();
  const page = await browser.newPage();
  const seen = [];

  page.on('response', response => {
    const url = response.url();
    if (url.startsWith(origin)) {
      seen.push({ url, status: response.status() });
    }
  });

  page.on('dialog', dialog => dialog.accept());

  await page.goto(`${origin}/Account/Login`, { waitUntil: 'domcontentloaded', timeout: 30000 });
  await page.fill('input[name="IdNumber"]', loginId);
  await page.fill('input[name="Password"]', password);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 30000 }),
    page.click('button[type="submit"]')
  ]);

  await page.goto(`${origin}/Employee`, { waitUntil: 'domcontentloaded', timeout: 30000 });
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 30000 }),
    page.locator('tr button.btn-outline-warning').first().click()
  ]);

  for (const item of seen.filter(x => /Employee\/(BulkAction|ResetPassword)/.test(x.url))) {
    console.log(`${item.status} ${item.url}`);
  }

  const bad = seen.find(x => x.url.includes('/Employee/BulkAction') || x.status === 400);
  await browser.close();
  if (bad) process.exitCode = 1;
})();
