const { chromium } = require('playwright');

const base = 'https://www.postybell.co.il';
const adminId = '910000101';
const adminPassword = '910000101';

const requiredTexts = {
  home: ['מדיניות פרטיות'],
  allocations: ['הוסף הקצאה', 'פרויקט', 'תוכנית', 'מחוז', 'מגזר', 'ת.ז', 'קוד עובד', 'שם פרטי', 'שם משפחה', 'הערות הקצאה'],
  frameworks: ['ייצוא לאקסל', 'ייבוא אקסל', 'שם מסגרת', 'סמל מסגרת', 'שלב חינוך', 'יישוב', 'הפוך לפעיל', 'הפוך ללא פעיל'],
  inspectorAssignments: ['ת.ז', 'שם פרטי', 'שם משפחה'],
  dashboard: ['הצג דיווחים בארכיון', 'דיווחים ישנים', 'מסמכים'],
  manual: ['הוספת דיווח ידני', 'עובד', 'הקצאה', 'חודש דיווח'],
};

async function expectText(page, label, texts) {
  const body = await page.locator('body').innerText({ timeout: 10000 });
  const missing = texts.filter((text) => !body.includes(text));
  return { label, ok: missing.length === 0, missing };
}

async function goto(page, path) {
  await page.goto(`${base}${path}`, { waitUntil: 'networkidle', timeout: 30000 });
}

(async () => {
  const browser = await chromium.launch({
    headless: true,
    args: ['--host-resolver-rules=MAP www.postybell.co.il 192.168.1.1'],
  });
  const context = await browser.newContext({
    ignoreHTTPSErrors: true,
    locale: 'he-IL',
    viewport: { width: 1440, height: 1000 },
  });
  const page = await context.newPage();
  const results = [];

  await goto(page, '/Account/Login');
  const passwordInput = page.locator('input[type="password"], input[name="Password"]').first();
  const beforeType = await passwordInput.getAttribute('type');
  const eyeButtonCount = await page.locator('button:has-text("הצג"), button:has-text("הסתר"), [aria-label*="סיס"], [title*="סיס"], .password-toggle, #togglePassword').count();
  await page.locator('input[name="IdNumber"], #IdNumber').fill(adminId);
  await passwordInput.fill(adminPassword);
  if (eyeButtonCount > 0) {
    await page.locator('button:has-text("הצג"), button:has-text("הסתר"), [aria-label*="סיס"], [title*="סיס"], .password-toggle, #togglePassword').first().click();
  }
  const afterType = await passwordInput.getAttribute('type');
  results.push({ label: 'login-password-eye', ok: beforeType === 'password' && (eyeButtonCount > 0 || afterType === 'text'), missing: eyeButtonCount > 0 ? [] : ['password eye button'] });

  await Promise.all([
    page.waitForNavigation({ waitUntil: 'networkidle', timeout: 30000 }).catch(() => null),
    page.locator('form button[type="submit"], button:has-text("כניסה")').first().click(),
  ]);

  if (page.url().includes('/Account/TwoFactor')) {
    results.push({ label: 'login', ok: false, missing: ['two factor required'] });
    console.log(JSON.stringify(results, null, 2));
    await browser.close();
    process.exit(1);
  }

  await goto(page, '/');
  results.push(await expectText(page, 'home-nav', requiredTexts.home));
  const navText = await page.locator('nav, header').first().innerText().catch(() => '');
  results.push({ label: 'nav-left-user', ok: navText.includes('שלום') && navText.includes('התנתק'), missing: navText.includes('שלום') && navText.includes('התנתק') ? [] : ['שלום/התנתק'] });

  await goto(page, '/Allocations');
  results.push(await expectText(page, 'allocations-list', requiredTexts.allocations));

  await goto(page, '/Admin/Frameworks');
  results.push(await expectText(page, 'frameworks-admin', requiredTexts.frameworks));
  const frameworkHelp = (await page.locator('body').innerText()).includes('מסגרות') && (await page.locator('body').innerText()).includes('מוסדות');
  results.push({ label: 'frameworks-explanation', ok: frameworkHelp, missing: frameworkHelp ? [] : ['מסגרות/מוסדות explanation'] });

  await goto(page, '/Admin/InspectorAssignments');
  results.push(await expectText(page, 'inspector-assignments', requiredTexts.inspectorAssignments));

  await goto(page, '/Dashboard');
  results.push(await expectText(page, 'dashboard-filters', requiredTexts.dashboard.slice(0, 2)));

  await goto(page, '/Dashboard/Summary');
  results.push(await expectText(page, 'dashboard-summary-documents', ['מסמכים']));

  await goto(page, '/Report/Manual');
  results.push(await expectText(page, 'manual-report', requiredTexts.manual));

  const labelCheck = await page.evaluate(async () => {
    const response = await fetch('/Report/FrameworkLabels?ids=1,2');
    const json = await response.json().catch(() => null);
    return { ok: response.ok, isArray: Array.isArray(json) };
  });
  results.push({ label: 'framework-label-endpoint', ok: labelCheck.ok && labelCheck.isArray, missing: labelCheck.ok ? [] : ['endpoint failed'] });

  const failed = results.filter((r) => !r.ok);
  console.log(JSON.stringify(results, null, 2));
  await page.screenshot({ path: 'test-results/feedback-verify-final.png', fullPage: true });
  await browser.close();
  process.exit(failed.length ? 1 : 0);
})().catch((err) => {
  console.error(err);
  process.exit(1);
});
