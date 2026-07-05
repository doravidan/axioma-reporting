const { chromium } = require('playwright');

const origin = 'https://postybell.co.il';
const loginId = process.env.POSTYBELL_LOGIN_ID || '029345400';
const password = process.env.POSTYBELL_PASSWORD || loginId;

const urls = [
  '/Dashboard/ExportExcel?pageSize=25',
  '/Dashboard/SummaryExportExcel?pageSize=25',
  '/Employee/ExportExcel?pageSize=25'
].map(path => new URL(path, origin).href);

(async () => {
  const browser = await chromium.launch();
  const context = await browser.newContext({ acceptDownloads: true });
  const page = await context.newPage();

  await page.goto(`${origin}/Account/Login`, { waitUntil: 'domcontentloaded', timeout: 30000 });
  await page.fill('input[name="IdNumber"]', loginId);
  await page.fill('input[name="Password"]', password);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 30000 }),
    page.click('button[type="submit"]')
  ]);

  let failed = false;
  for (const url of urls) {
    const response = await context.request.get(url, { maxRedirects: 0, timeout: 30000 });
    const status = response.status();
    const contentType = response.headers()['content-type'] || '';
    const disposition = response.headers()['content-disposition'] || '';
    console.log(`EXPORT ${status} ${url}`);
    console.log(`  content-type: ${contentType}`);
    console.log(`  content-disposition: ${disposition}`);
    if (status >= 400) failed = true;
  }

  await browser.close();
  if (failed) process.exitCode = 1;
})();
