const { chromium } = require('playwright');

(async () => {
  const url = process.argv[2] || 'https://postybell.co.il/Report?userId=27&allocationId=26&reportId=21';
  const browser = await chromium.launch();
  const page = await browser.newPage();

  page.on('requestfailed', request => {
    console.log(`REQUEST_FAILED ${request.url()} ${request.failure()?.errorText || ''}`);
  });

  const response = await page.goto(url, { waitUntil: 'domcontentloaded', timeout: 30000 });
  console.log(`FINAL_URL ${page.url()}`);
  console.log(`STATUS ${response ? response.status() : 'no-response'}`);
  console.log(`TITLE ${await page.title()}`);
  await page.screenshot({ path: 'postybell-report-playwright.png', fullPage: true });
  await browser.close();
})();
