const { chromium } = require('playwright');

const base = 'https://www.postybell.co.il';
const adminId = '910000101';
const adminPassword = '910000101';

(async () => {
  const browser = await chromium.launch({
    headless: true,
    args: ['--host-resolver-rules=MAP postybell.co.il 192.168.1.1,MAP www.postybell.co.il 192.168.1.1'],
  });
  const context = await browser.newContext({
    ignoreHTTPSErrors: true,
    locale: 'he-IL',
    viewport: { width: 1360, height: 900 },
  });
  const page = await context.newPage();

  await page.goto(`${base}/Account/Login`, { waitUntil: 'domcontentloaded', timeout: 60000 });
  await page.locator('input[name="IdNumber"], #IdNumber').fill(adminId);
  await page.locator('input[name="Password"], input[type="password"]').fill(adminPassword);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 60000 }).catch(() => null),
    page.locator('form button[type="submit"]').first().click(),
  ]);

  await page.goto(`${base}/Admin/Frameworks`, { waitUntil: 'domcontentloaded', timeout: 60000 });
  await page.waitForSelector('.framework-filter-form', { timeout: 15000 });
  await page.screenshot({ path: 'patches/frameworks-ui-before.png', fullPage: true });

  const state = await page.evaluate(() => {
    function rect(selector) {
      const el = document.querySelector(selector);
      if (!el) return null;
      const r = el.getBoundingClientRect();
      return { top: r.top, bottom: r.bottom, left: r.left, right: r.right, width: r.width, height: r.height };
    }
    const nav = document.querySelector('nav, header, .navbar');
    const filter = document.querySelector('.framework-filter-form');
    const overlap = nav && filter ? !(filter.getBoundingClientRect().top >= nav.getBoundingClientRect().bottom) : false;
    return {
      url: location.href,
      bodyClass: document.body.className,
      nav: rect('nav, header, .navbar'),
      container: rect('.container-fluid'),
      filter: rect('.framework-filter-form'),
      title: rect('.container-fluid h3'),
      firstCard: rect('.container-fluid .card'),
      overlap,
      filterParent: filter?.parentElement?.className || '',
      filterPrevious: filter?.previousElementSibling?.outerHTML?.slice(0, 250) || '',
      filterNext: filter?.nextElementSibling?.outerHTML?.slice(0, 250) || '',
      labels: Array.from(document.querySelectorAll('.framework-filter-form label')).map((label) => label.textContent.trim()),
    };
  });

  console.log(JSON.stringify(state, null, 2));
  await browser.close();
})().catch((err) => {
  console.error(err);
  process.exit(1);
});
