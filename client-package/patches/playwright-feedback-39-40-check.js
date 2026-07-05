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

  await page.goto(`${base}/Employee/2/Allocations/Create`, { waitUntil: 'domcontentloaded', timeout: 60000 });
  await page.waitForSelector('select[name="FrameworkIds"]', { state: 'attached', timeout: 10000 });
  const allocationFrameworkState = await page.evaluate(() => {
    const select = document.querySelector('select[name="FrameworkIds"]');
    const container = select?.closest('.choices');
    return {
      choicesInit: select?.dataset.choicesInit || '',
      hasStoredInstance: !!select?.choicesInstance,
      inputPlaceholder: container?.querySelector('input.choices__input')?.getAttribute('placeholder') || '',
      visibleText: container?.innerText || '',
    };
  });
  if (!allocationFrameworkState.choicesInit || !allocationFrameworkState.hasStoredInstance || !allocationFrameworkState.inputPlaceholder.includes('יישוב')) {
    fail('Allocation framework selector is not clearly searchable by locality/symbol/name.', allocationFrameworkState);
  }

  await page.goto(`${base}/Dashboard/Summary`, { waitUntil: 'domcontentloaded', timeout: 60000 });
  const dashboardState = await page.evaluate(() => {
    const headers = Array.from(document.querySelectorAll('table thead th')).map((th) => th.innerText.trim());
    const documentLinks = Array.from(document.querySelectorAll('a[href*="#documents"]')).map((a) => ({
      text: a.innerText.trim(),
      title: a.getAttribute('title') || '',
      href: a.getAttribute('href') || '',
    }));
    return {
      headers,
      hasDocumentsHeader: headers.includes('מסמכים'),
      documentLinks,
    };
  });
  if (!dashboardState.hasDocumentsHeader) {
    fail('Dashboard summary is missing a visible documents column header.', dashboardState);
  }
  const unlabeledDocumentLink = dashboardState.documentLinks.find((link) => !link.text.includes('מסמכים'));
  if (unlabeledDocumentLink) {
    fail('Dashboard document link should include a clear label, not only a number.', dashboardState);
  }

  let reportFrameworkState = null;
  const firstReportLink = dashboardState.documentLinks[0]?.href;
  if (firstReportLink) {
    await page.goto(new URL(firstReportLink, base).toString(), { waitUntil: 'domcontentloaded', timeout: 60000 });
    await page.waitForSelector('#fieldFramework', { state: 'attached', timeout: 10000 }).catch(() => null);
    reportFrameworkState = await page.evaluate(() => {
      const select = document.querySelector('#fieldFramework');
      const container = select?.closest('.choices');
      return {
        exists: !!select,
        inputPlaceholder: container?.querySelector('input.choices__input')?.getAttribute('placeholder') || '',
        firstFrameworkText: select?.options?.[1]?.textContent || '',
      };
    });
    if (reportFrameworkState.exists && !reportFrameworkState.inputPlaceholder.includes('יישוב')) {
      fail('Report framework selector is not clearly searchable by locality/symbol/name.', reportFrameworkState);
    }
  }

  console.log(JSON.stringify({ allocationFrameworkState, dashboardState, reportFrameworkState }, null, 2));
  await browser.close();
})().catch((err) => {
  console.error(err);
  process.exit(1);
});
