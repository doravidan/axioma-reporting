const { chromium } = require('playwright');

const base = 'https://www.postybell.co.il';
const loginId = process.env.POSTYBELL_LOGIN_ID || '029345400';
const password = process.env.POSTYBELL_PASSWORD || loginId;

(async () => {
  const browser = await chromium.launch({
    args: ['--host-resolver-rules=MAP postybell.co.il 192.168.1.1,MAP www.postybell.co.il 192.168.1.1'],
  });
  const page = await browser.newPage({
    viewport: { width: 1366, height: 900 },
    ignoreHTTPSErrors: true,
  });
  const events = [];
  page.on('requestfailed', req => events.push({ type: 'failed', url: req.url(), error: req.failure()?.errorText }));
  page.on('response', res => {
    const url = res.url();
    if (url.includes('/Report') || url.includes('/MyAllocations') || url.includes('/Account')) {
      events.push({ type: 'response', status: res.status(), url });
    }
  });
  try {
    await page.goto(`${base}/Account/Login`, { waitUntil: 'domcontentloaded', timeout: 60000 });
    await page.locator('input[name="IdNumber"], #IdNumber').fill(loginId);
    await page.locator('input[name="Password"], input[type="password"]').fill(password);
    await page.locator('button[type="submit"], input[type="submit"]').first().click();
    await page.waitForLoadState('networkidle', { timeout: 60000 }).catch(() => {});

    const startUrl = page.url();
    const navLink = page.getByRole('link', { name: /פעילות חודשית/ }).first();
    const linkCount = await navLink.count();
    if (!linkCount) {
      console.log(JSON.stringify({
        ok: false,
        reason: 'monthly activity link not found after login',
        startUrl,
        title: await page.title(),
        links: await page.locator('a').evaluateAll(els => els.map(a => ({ text: a.innerText.trim(), href: a.getAttribute('href') })).slice(0, 30)),
        bodyText: (await page.locator('body').innerText()).slice(0, 2000),
        events,
      }, null, 2));
      return;
    }
    const href = await navLink.getAttribute('href');
    await navLink.click();
    await page.waitForLoadState('domcontentloaded', { timeout: 30000 }).catch(() => {});
    await page.waitForTimeout(5000);

    console.log(JSON.stringify({
      ok: true,
      startUrl,
      href,
      finalUrl: page.url(),
      title: await page.title(),
      bodyText: (await page.locator('body').innerText()).slice(0, 1200),
      events,
    }, null, 2));
  } finally {
    await browser.close();
  }
})();
