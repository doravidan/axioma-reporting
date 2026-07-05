const { chromium } = require('playwright');

(async () => {
  const browser = await chromium.launch({
    headless: true,
    args: ['--host-resolver-rules=MAP www.postybell.co.il 192.168.1.1'],
  });
  const context = await browser.newContext({ ignoreHTTPSErrors: true, locale: 'he-IL', viewport: { width: 1440, height: 1000 } });
  const page = await context.newPage();
  await page.goto('https://www.postybell.co.il/Account/Login', { waitUntil: 'networkidle' });
  await page.locator('input[name="IdNumber"], #IdNumber').fill('910000101');
  await page.locator('input[name="Password"], input[type="password"]').fill('910000101');
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'networkidle' }).catch(() => null),
    page.locator('form button[type="submit"], button:has-text("כניסה")').first().click(),
  ]);

  const checks = [];
  for (const path of ['/Admin/SystemConstants', '/Admin/DataMigration', '/Admin/Frameworks?localityName=נת', '/Allocations']) {
    await page.goto(`https://www.postybell.co.il${path}`, { waitUntil: 'networkidle' });
    const body = await page.locator('body').innerText();
    checks.push({ path, ok: !body.includes('Not Found') && !body.includes('שגיאה'), sample: body.slice(0, 180).replace(/\s+/g, ' ') });
    await page.screenshot({ path: `test-results/extra-${path.replace(/[/?=&]/g, '_')}.png`, fullPage: true });
  }
  console.log(JSON.stringify(checks, null, 2));
  await browser.close();
  process.exit(checks.every((c) => c.ok) ? 0 : 1);
})();
