const { chromium } = require('playwright');

const idNumber = process.env.POSTYBELL_RESET_ID || 'admin';

(async () => {
  const browser = await chromium.launch();
  const page = await browser.newPage();
  page.on('requestfailed', request => {
    console.log(`REQUEST_FAILED ${request.url()} ${request.failure()?.errorText || ''}`);
  });
  page.on('response', response => {
    if (response.status() >= 400) {
      console.log(`HTTP_${response.status()} ${response.url()}`);
    }
  });
  await page.goto('https://postybell.co.il/Account/ForgotPassword', { waitUntil: 'domcontentloaded', timeout: 30000 });
  await page.fill('input[name="idNumber"]', idNumber);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 30000 }),
    page.click('button[type="submit"]')
  ]);
  console.log(`AFTER_FORGOT ${page.url()} ${await page.title()}`);
  await browser.close();
})();
