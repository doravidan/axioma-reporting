const { chromium } = require('playwright');

const origin = 'https://postybell.co.il';
const loginId = process.env.POSTYBELL_LOGIN_ID || '029345400';
const password = process.env.POSTYBELL_PASSWORD || loginId;
const maxPages = Number(process.env.POSTYBELL_MAX_PAGES || 180);

const startUrls = [
  '/',
  '/Dashboard',
  '/Dashboard/Summary',
  '/Employee',
  '/Allocations',
  '/Report?userId=22&allocationId=21&reportId=22',
  '/Report?userId=27&allocationId=26&reportId=21',
  '/Admin/ReportingMonths',
  '/Admin/SystemConstants',
  '/Admin/EmailTemplates',
  '/Admin/EmailServerSettings',
  '/Admin/NotificationLogs',
  '/Admin/TermsOfUse',
  '/Account/ForgotPassword'
].map(path => new URL(path, origin).href);

const seen = new Set();
const queue = [...startUrls];
const failures = [];

function normalizeUrl(href) {
  try {
    const url = new URL(href, origin);
    if (url.origin !== origin) return null;
    if (url.hash) url.hash = '';
    if (/\.(css|js|png|jpg|jpeg|gif|svg|ico|pdf|xlsx|xls)$/i.test(url.pathname)) return null;
    if (/\/Account\/Logout/i.test(url.pathname)) return null;
    return url.href;
  } catch {
    return null;
  }
}

(async () => {
  const browser = await chromium.launch();
  const page = await browser.newPage();

  page.on('requestfailed', request => {
    const url = request.url();
    if (url.startsWith(origin)) {
      failures.push({ url, status: 'requestfailed', detail: request.failure()?.errorText || '' });
      console.log(`REQUEST_FAILED ${url} ${request.failure()?.errorText || ''}`);
    }
  });

  await page.goto(`${origin}/Account/Login`, { waitUntil: 'domcontentloaded', timeout: 30000 });
  await page.fill('input[name="IdNumber"]', loginId);
  await page.fill('input[name="Password"]', password);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 30000 }),
    page.click('button[type="submit"]')
  ]);
  console.log(`LOGIN_URL ${page.url()}`);

  while (queue.length && seen.size < maxPages) {
    const url = queue.shift();
    if (!url || seen.has(url)) continue;
    seen.add(url);

    let response = null;
    try {
      response = await page.goto(url, { waitUntil: 'domcontentloaded', timeout: 30000 });
      const status = response ? response.status() : 0;
      console.log(`PAGE ${status} ${page.url()}`);
      if (status >= 400) {
        failures.push({ url, status, detail: page.url() });
      }
      if (status < 400) {
        const links = await page.locator('a[href]').evaluateAll(anchors => anchors.map(a => a.href));
        for (const href of links) {
          const normalized = normalizeUrl(href);
          if (normalized && !seen.has(normalized) && queue.length + seen.size < maxPages * 2) {
            queue.push(normalized);
          }
        }
      }
    } catch (error) {
      failures.push({ url, status: 'exception', detail: error.message });
      console.log(`EXCEPTION ${url} ${error.message}`);
    }
  }

  console.log(`CRAWLED ${seen.size}`);
  console.log(`FAILURES ${failures.length}`);
  for (const failure of failures) {
    console.log(`FAIL ${failure.status} ${failure.url} ${failure.detail}`);
  }
  await browser.close();
  if (failures.some(f => f.status === 400 || String(f.detail).includes('400'))) {
    process.exitCode = 1;
  }
})();
