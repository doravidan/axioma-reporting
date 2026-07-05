const { chromium } = require('playwright');
const loginId = process.env.POSTYBELL_LOGIN_ID || 'admin';
const password = process.env.POSTYBELL_PASSWORD || 'admin1234';
const origin = 'https://www.postybell.co.il';
(async () => {
  const browser = await chromium.launch({ args: ['--host-resolver-rules=MAP postybell.co.il 192.168.1.1,MAP www.postybell.co.il 192.168.1.1'] });
  const page = await browser.newPage({ ignoreHTTPSErrors: true });
  await page.goto(`${origin}/Account/Login`, { waitUntil: 'domcontentloaded', timeout: 30000 });
  await page.fill('input[name="IdNumber"]', loginId);
  await page.fill('input[name="Password"]', password);
  await Promise.allSettled([
    page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 30000 }),
    page.click('button[type="submit"]'),
  ]);
  console.log('AFTER_LOGIN_URL', page.url());
  console.log('AFTER_LOGIN_TITLE', await page.title());
  console.log((await page.locator('body').innerText()).slice(0, 800));
  const response = await page.goto(`${origin}/Employee`, { waitUntil: 'domcontentloaded', timeout: 60000 });
  const text = await page.locator('body').innerText();
  const rows = await page.locator('tbody tr').count().catch(() => -1);
  console.log('STATUS', response && response.status());
  console.log('URL', page.url());
  console.log('TITLE', await page.title());
  console.log('ROWS', rows);
  console.log(text.slice(0, 2000));
  await browser.close();
})();
