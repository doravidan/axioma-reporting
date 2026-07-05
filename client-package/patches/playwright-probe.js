const { chromium } = require('playwright');

(async () => {
  const browser = await chromium.launch({
    headless: true,
    args: ['--host-resolver-rules=MAP www.postybell.co.il 192.168.1.1'],
  });
  const context = await browser.newContext({ ignoreHTTPSErrors: true });
  const page = await context.newPage();
  await page.goto('https://www.postybell.co.il/Account/Login', { waitUntil: 'domcontentloaded', timeout: 30000 });
  console.log('url=', page.url());
  console.log('title=', await page.title());
  console.log((await page.locator('body').innerText().catch(() => '')).slice(0, 1000));
  await page.screenshot({ path: 'test-results/feedback-probe-login.png', fullPage: true });
  await browser.close();
})();
