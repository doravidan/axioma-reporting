const { chromium } = require('playwright');

const loginId = process.env.POSTYBELL_LOGIN_ID || '029345400';
const password = process.env.POSTYBELL_PASSWORD || loginId;
const targetPath = process.argv[2] || '/Employee/67/Allocations/53/Edit';
const origin = 'https://www.postybell.co.il';

(async () => {
  const browser = await chromium.launch({
    args: ['--host-resolver-rules=MAP postybell.co.il 192.168.1.1,MAP www.postybell.co.il 192.168.1.1'],
  });
  const page = await browser.newPage({ ignoreHTTPSErrors: true });

  await page.goto(`${origin}/Account/Login`, { waitUntil: 'domcontentloaded', timeout: 30000 });
  await page.fill('input[name="IdNumber"]', loginId);
  await page.fill('input[name="Password"]', password);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 30000 }),
    page.click('button[type="submit"]'),
  ]);

  const response = await page.goto(`${origin}${targetPath}`, { waitUntil: 'domcontentloaded', timeout: 30000 });
  const bodyText = await page.locator('body').innerText();
  const html = await page.content();
  const badMarkers = (bodyText.match(/[׳ֲײ]|ג€|Â|×[\u0080-\uFFFF]/g) || []).length;

  console.log(`STATUS ${response ? response.status() : 'none'}`);
  console.log(`URL ${page.url()}`);
  console.log(`TITLE ${await page.title()}`);
  console.log(`BAD_MARKERS ${badMarkers}`);
  console.log(bodyText.slice(0, 1600));

  await page.screenshot({ path: 'postybell-allocation-edit-53.png', fullPage: true });
  await browser.close();

  if (badMarkers > 0 || html.includes('׳³') || html.includes('ג€')) {
    process.exitCode = 2;
  }
})();
