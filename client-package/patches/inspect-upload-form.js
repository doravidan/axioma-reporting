const { chromium } = require('playwright');

(async () => {
  const browser = await chromium.launch();
  const page = await browser.newPage();
  await page.goto('https://postybell.co.il/Account/Login', { waitUntil: 'domcontentloaded' });
  await page.fill('input[name="IdNumber"]', '029345400');
  await page.fill('input[name="Password"]', '029345400');
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'domcontentloaded' }),
    page.click('button[type="submit"]')
  ]);
  await page.goto('https://postybell.co.il/Report?userId=27&allocationId=26&reportId=21', { waitUntil: 'domcontentloaded' });
  const form = await page.locator('form[action$="/Report/UploadExcel"]').evaluate(f => ({
    action: f.action,
    method: f.method,
    enctype: f.enctype,
    html: f.outerHTML,
    inputs: [...f.querySelectorAll('input')].map(i => ({ name: i.name, type: i.type, value: i.value }))
  }));
  console.log(JSON.stringify(form, null, 2));
  await browser.close();
})();
