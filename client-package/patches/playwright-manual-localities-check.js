const { chromium } = require('playwright');

const base = 'https://www.postybell.co.il';
const adminId = '910000101';
const adminPassword = '910000101';

function fail(message, details) {
  console.error(message);
  if (details) console.error(JSON.stringify(details, null, 2));
  process.exit(1);
}

(async () => {
  const browser = await chromium.launch({
    headless: true,
    args: ['--host-resolver-rules=MAP postybell.co.il 192.168.1.1,MAP www.postybell.co.il 192.168.1.1'],
  });
  const context = await browser.newContext({
    ignoreHTTPSErrors: true,
    locale: 'he-IL',
    viewport: { width: 1360, height: 1000 },
  });
  const page = await context.newPage();

  await page.goto(`${base}/Account/Login`, { waitUntil: 'domcontentloaded', timeout: 60000 });
  await page.locator('input[name="IdNumber"], #IdNumber').fill(adminId);
  await page.locator('input[name="Password"], input[type="password"]').fill(adminPassword);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 60000 }).catch(() => null),
    page.locator('form button[type="submit"]').first().click(),
  ]);

  await page.goto(`${base}/Report/Manual`, { waitUntil: 'domcontentloaded', timeout: 60000 });
  await page.waitForSelector('#manualUserSelect, #manualAllocationSelect', { timeout: 15000 });
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 60000 }),
    page.locator('#manualOpenButton').click(),
  ]);

  if (!new URL(page.url()).searchParams.get('manual')) {
    fail('Manual report page did not include manual=true flag.', { url: page.url() });
  }

  await page.locator('button[onclick="addRow()"], button:has-text("הוסף שורה")').first().click();
  await page.waitForSelector('#rowModal.show, #rowModal[style*="display: block"], #fieldLocality', { timeout: 15000 });

  const state = await page.evaluate(() => {
    const locality = document.getElementById('fieldLocality');
    const options = locality ? Array.from(locality.options).filter((option) => option.value) : [];
    return {
      url: location.href,
      exists: !!locality,
      optionCount: options.length,
      firstOptions: options.slice(0, 5).map((option) => option.textContent.trim()),
    };
  });

  if (!state.exists || state.optionCount === 0) {
    fail('Manual report row locality dropdown is empty.', state);
  }

  console.log(JSON.stringify(state, null, 2));
  await browser.close();
})().catch((err) => {
  console.error(err);
  process.exit(1);
});
