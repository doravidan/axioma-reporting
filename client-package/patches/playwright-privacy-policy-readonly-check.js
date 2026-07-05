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

  await page.goto(`${base}/`, { waitUntil: 'networkidle', timeout: 30000 });
  const navHasPrivacyAdmin = await page.locator('a[href="/Admin/PrivacyPolicy"]').count();

  await page.goto(`${base}/Admin/PrivacyPolicy`, { waitUntil: 'networkidle', timeout: 30000 });
  const adminState = await page.evaluate(() => ({
    hasEditor: !!document.querySelector('#bodyHtml'),
    hasPublishForm: !!document.querySelector('form[action="/Admin/PublishPrivacyPolicy"]'),
    versionRows: document.querySelectorAll('table tbody tr').length,
    hasViewLink: !!document.querySelector('a[href="/Home/Privacy"]'),
  }));

  await page.goto(`${base}/Home/Privacy`, { waitUntil: 'networkidle', timeout: 30000 });
  const publicState = await page.evaluate(() => ({
    title: document.title,
    bodyTextLength: document.body.innerText.trim().length,
    hasEmptyMessage: document.body.innerText.includes('לא הוגדרה מדיניות פרטיות'),
  }));

  const result = {
    navHasPrivacyAdmin: navHasPrivacyAdmin > 0,
    adminState,
    publicState,
  };
  console.log(JSON.stringify(result, null, 2));
  await browser.close();

  const ok = result.navHasPrivacyAdmin
    && adminState.hasEditor
    && adminState.hasPublishForm
    && adminState.versionRows > 0
    && adminState.hasViewLink
    && publicState.title.includes('מדיניות פרטיות')
    && publicState.bodyTextLength > 50
    && !publicState.hasEmptyMessage;
  process.exit(ok ? 0 : 1);
})().catch((err) => {
  console.error(err);
  process.exit(1);
});
