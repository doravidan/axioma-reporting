const { chromium } = require('playwright');

const base = 'https://www.postybell.co.il';
const adminId = '910000101';
const adminPassword = '910000101';
const manualButtonText = 'הוספת דיווח ידני';

(async () => {
  const browser = await chromium.launch({
    headless: true,
    args: ['--host-resolver-rules=MAP www.postybell.co.il 192.168.1.1'],
  });
  const context = await browser.newContext({
    ignoreHTTPSErrors: true,
    locale: 'he-IL',
    viewport: { width: 1360, height: 900 },
  });
  const page = await context.newPage();

  await page.goto(`${base}/Account/Login`, { waitUntil: 'networkidle', timeout: 30000 });
  await page.locator('input[name="IdNumber"], #IdNumber').fill(adminId);
  await page.locator('input[name="Password"], input[type="password"]').fill(adminPassword);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'networkidle', timeout: 30000 }).catch(() => null),
    page.locator('form button[type="submit"]').first().click(),
  ]);

  await page.goto(`${base}/Report`, { waitUntil: 'networkidle', timeout: 30000 });
  const button = page.locator(`a:has-text("${manualButtonText}")`).first();
  const href = await button.getAttribute('href', { timeout: 10000 });
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'networkidle', timeout: 30000 }),
    button.click(),
  ]);
  const body = await page.locator('body').innerText({ timeout: 10000 });

  const result = {
    href,
    url: page.url(),
    hasManualTitle: body.includes(manualButtonText),
    hasEmployeeSelect: body.includes('עובד'),
    hasAllocationSelect: body.includes('הקצאה'),
  };
  console.log(JSON.stringify(result, null, 2));
  await browser.close();

  const ok = href && href.toLowerCase() === '/report/manual'
    && page.url().toLowerCase().includes('/report/manual')
    && result.hasManualTitle
    && result.hasEmployeeSelect
    && result.hasAllocationSelect;
  process.exit(ok ? 0 : 1);
})().catch((err) => {
  console.error(err);
  process.exit(1);
});
