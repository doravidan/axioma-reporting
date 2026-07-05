const { chromium } = require('playwright');

const base = 'https://www.postybell.co.il';
const adminId = '910000101';
const adminPassword = '910000101';
const marker = `בדיקת מדיניות פרטיות ${Date.now()}`;
const htmlBody = `<p>${marker}</p><p>גרסה זו נוצרה בבדיקת ניהול גרסאות.</p>`;

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

  await page.goto(`${base}/Admin/PrivacyPolicy`, { waitUntil: 'networkidle', timeout: 30000 });
  const adminText = await page.locator('body').innerText({ timeout: 10000 });
  const hasAdminUi = adminText.includes('ניהול גרסאות מדיניות פרטיות')
    && adminText.includes('פרסום גרסה חדשה')
    && adminText.includes('היסטוריית גרסאות');

  await page.locator('#bodyHtml').fill(htmlBody);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'networkidle', timeout: 30000 }),
    page.locator('form[action="/Admin/PublishPrivacyPolicy"] button[type="submit"]').click(),
  ]);
  const afterPublish = await page.locator('body').innerText({ timeout: 10000 });

  await page.goto(`${base}/Home/Privacy`, { waitUntil: 'networkidle', timeout: 30000 });
  const publicText = await page.locator('body').innerText({ timeout: 10000 });

  const result = {
    hasAdminUi,
    publishSuccess: afterPublish.includes('מדיניות הפרטיות פורסמה'),
    publicShowsMarker: publicText.includes(marker),
  };
  console.log(JSON.stringify(result, null, 2));
  await browser.close();

  process.exit(Object.values(result).every(Boolean) ? 0 : 1);
})().catch((err) => {
  console.error(err);
  process.exit(1);
});
