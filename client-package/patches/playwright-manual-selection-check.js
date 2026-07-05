const { chromium } = require('playwright');

const base = 'https://www.postybell.co.il';
const adminId = '910000101';
const adminPassword = '910000101';

(async () => {
  const browser = await chromium.launch({
    headless: true,
    args: ['--host-resolver-rules=MAP www.postybell.co.il 192.168.1.1'],
  });
  const context = await browser.newContext({
    ignoreHTTPSErrors: true,
    locale: 'he-IL',
    viewport: { width: 1360, height: 900 },
  });
  const page = await context.newPage();

  await page.goto(`${base}/Account/Login`, { waitUntil: 'networkidle', timeout: 30000 });
  await page.locator('input[name="IdNumber"], #IdNumber').fill(adminId);
  await page.locator('input[name="Password"], input[type="password"]').fill(adminPassword);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'networkidle', timeout: 30000 }).catch(() => null),
    page.locator('form button[type="submit"]').first().click(),
  ]);

  await page.goto(`${base}/Report/Manual`, { waitUntil: 'networkidle', timeout: 30000 });
  await page.locator('#manualUserSelect').selectOption('13');
  const filtered = await page.evaluate(() => {
    const select = document.getElementById('manualAllocationSelect');
    return {
      value: select.value,
      enabledUserIds: Array.from(select.options)
        .filter((option) => !option.disabled && !option.hidden)
        .map((option) => option.getAttribute('data-user-id')),
    };
  });

  const badResponse = await page.goto(`${base}/Report/ManualOpen?userId=13&allocationId=37&reportingMonthId=6`, {
    waitUntil: 'networkidle',
    timeout: 30000,
  });
  const badBody = await page.locator('body').innerText({ timeout: 10000 });
  const badUrl = page.url();

  await page.goto(`${base}/Report/ManualOpen?userId=13&allocationId=12&reportingMonthId=6`, {
    waitUntil: 'networkidle',
    timeout: 30000,
  });
  const goodUrl = page.url();

  const result = {
    filtered,
    badStatus: badResponse && badResponse.status(),
    badUrl,
    badShowsValidation: badBody.includes('יש לבחור הקצאה ששייכת לעובד שנבחר'),
    goodUrl,
    goodReachedReport: goodUrl.includes('/Report?') || goodUrl.includes('/Report/Index'),
  };
  console.log(JSON.stringify(result, null, 2));

  await browser.close();
  const ok = filtered.value === '12'
    && filtered.enabledUserIds.length > 0
    && filtered.enabledUserIds.every((id) => id === '13')
    && result.badStatus !== 404
    && result.badShowsValidation
    && result.goodReachedReport;
  process.exit(ok ? 0 : 1);
})().catch((err) => {
  console.error(err);
  process.exit(1);
});
