const { chromium } = require('playwright');

const attempts = [
  ['admin', 'admin1234'],
  ['admin', 'Password123'],
  ['910000101', '910000101'],
  ['910000101', 'Password123'],
  ['axioma.pm.test', 'axioma.pm.test'],
  ['029345400', '029345400'],
];

(async () => {
  const browser = await chromium.launch({
    headless: true,
    args: ['--host-resolver-rules=MAP www.postybell.co.il 192.168.1.1'],
  });
  for (const [id, password] of attempts) {
    const context = await browser.newContext({ ignoreHTTPSErrors: true, locale: 'he-IL' });
    const page = await context.newPage();
    await page.goto('https://www.postybell.co.il/Account/Login', { waitUntil: 'networkidle' });
    await page.locator('input[name="IdNumber"], #IdNumber').fill(id);
    await page.locator('input[name="Password"], input[type="password"]').fill(password);
    await Promise.all([
      page.waitForNavigation({ waitUntil: 'networkidle', timeout: 15000 }).catch(() => null),
      page.locator('form button[type="submit"], button:has-text("כניסה")').first().click(),
    ]);
    const body = await page.locator('body').innerText().catch(() => '');
    console.log(JSON.stringify({
      id,
      password,
      url: page.url(),
      title: await page.title(),
      body: body.slice(0, 300).replace(/\s+/g, ' '),
    }, null, 2));
    await context.close();
  }
  await browser.close();
})();
