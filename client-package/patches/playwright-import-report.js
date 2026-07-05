const path = require('path');
const { chromium } = require('playwright');

const baseUrl = 'https://postybell.co.il';
const reportUrl = `${baseUrl}/Report?userId=27&allocationId=26&reportId=21`;
const excelPath = path.resolve('postybell-valid-import.xlsx');
const loginId = process.env.POSTYBELL_LOGIN_ID || '029345400';
const password = process.env.POSTYBELL_PASSWORD || loginId;

(async () => {
  const browser = await chromium.launch();
  const page = await browser.newPage();

  page.on('requestfailed', request => {
    console.log(`REQUEST_FAILED ${request.method()} ${request.url()} ${request.failure()?.errorText || ''}`);
  });
  page.on('response', response => {
    if (response.status() >= 400) {
      console.log(`HTTP_${response.status()} ${response.url()}`);
    }
  });
  page.on('console', msg => {
    console.log(`CONSOLE_${msg.type()} ${msg.text()}`);
  });
  page.on('dialog', async dialog => {
    console.log(`DIALOG ${dialog.type()} ${dialog.message()}`);
    await dialog.accept();
  });

  console.log(`GOTO ${reportUrl}`);
  await page.goto(reportUrl, { waitUntil: 'domcontentloaded', timeout: 30000 });

  if (page.url().includes('/Account/Login')) {
    console.log('LOGIN');
    await page.fill('input[name="IdNumber"]', loginId);
    await page.fill('input[name="Password"]', password);
    await Promise.all([
      page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 30000 }),
      page.click('button[type="submit"]')
    ]);
  }

  if (!page.url().startsWith(reportUrl)) {
    console.log(`AFTER_LOGIN_URL ${page.url()}`);
    await page.goto(reportUrl, { waitUntil: 'domcontentloaded', timeout: 30000 });
  }

  console.log(`REPORT_URL ${page.url()}`);
  await page.screenshot({ path: 'postybell-before-import.png', fullPage: true });

  const excelForm = page.locator('form[action$="/Report/UploadExcel"]').first();
  const fileInput = excelForm.locator('input[type="file"]').first();
  const fileCount = await excelForm.locator('input[type="file"]').count();
  console.log(`FILE_INPUTS ${fileCount}`);
  if (fileCount < 1) {
    throw new Error('No file input found on report page');
  }

  await fileInput.setInputFiles(excelPath);

  const submit = excelForm.locator('button[type="submit"], input[type="submit"]').first();
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 30000 }).catch(error => {
      console.log(`NAVIGATION_ERROR ${error.message}`);
    }),
    submit.click()
  ]);

  console.log(`AFTER_IMPORT_URL ${page.url()}`);
  console.log(`TITLE ${await page.title()}`);
  await page.screenshot({ path: 'postybell-after-import.png', fullPage: true });
  await browser.close();
})();
