const { chromium } = require('playwright');

(async () => {
  const browser = await chromium.launch({
    headless: true,
    args: ['--host-resolver-rules=MAP www.postybell.co.il 192.168.1.1'],
  });
  const context = await browser.newContext({ ignoreHTTPSErrors: true, locale: 'he-IL', viewport: { width: 1440, height: 1000 } });
  const page = await context.newPage();
  page.on('console', (msg) => console.log('console:', msg.type(), msg.text()));
  page.on('pageerror', (err) => console.log('pageerror:', err.message));
  await page.goto('https://www.postybell.co.il/Account/Login', { waitUntil: 'networkidle' });
  await page.locator('input[name="IdNumber"], #IdNumber').fill('910000101');
  await page.locator('input[name="Password"], input[type="password"]').fill('910000101');
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'networkidle' }).catch(() => null),
    page.locator('form button[type="submit"], button:has-text("כניסה")').first().click(),
  ]);
  await page.goto('https://www.postybell.co.il/Admin/Frameworks', { waitUntil: 'networkidle' });
  await page.waitForTimeout(1000);
  console.log(await page.locator('body').innerText());
  console.log('forms=', await page.locator('form').count());
  console.log('buttons=', await page.locator('button,a.btn').evaluateAll((els) => els.map((e) => e.textContent.trim()).filter(Boolean)));
  await page.screenshot({ path: 'test-results/frameworks-dump.png', fullPage: true });
  await browser.close();
})();
