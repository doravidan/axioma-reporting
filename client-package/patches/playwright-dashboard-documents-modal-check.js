const { chromium } = require('playwright');

const base = 'https://www.postybell.co.il';
const adminId = process.env.POSTYBELL_LOGIN_ID || '910000101';
const adminPassword = process.env.POSTYBELL_PASSWORD || '910000101';

(async () => {
  const browser = await chromium.launch({
    args: ['--host-resolver-rules=MAP postybell.co.il 192.168.1.1,MAP www.postybell.co.il 192.168.1.1'],
  });
  const page = await browser.newPage({
    viewport: { width: 1366, height: 900 },
    ignoreHTTPSErrors: true,
  });
  try {
    await page.goto(`${base}/Account/Login`, { waitUntil: 'domcontentloaded', timeout: 60000 });
    await page.locator('input[name="IdNumber"], #IdNumber').fill(adminId);
    await page.locator('input[name="Password"], input[type="password"]').fill(adminPassword);
    await page.locator('button[type="submit"], input[type="submit"]').first().click();
    await page.waitForLoadState('networkidle', { timeout: 60000 });

    await page.goto(`${base}/Dashboard/Summary`, { waitUntil: 'domcontentloaded', timeout: 60000 });
    await page.waitForLoadState('networkidle', { timeout: 60000 }).catch(() => {});

    const docsButton = page.locator('a.btn[href*="/Report?reportId="][href*="#documents"]').first();
    const count = await docsButton.count();
    if (!count) {
      throw new Error('No dashboard document button found');
    }

    const href = await docsButton.getAttribute('href');
    await docsButton.click();
    const modal = page.locator('#dashboardDocumentsModal.show');
    await modal.waitFor({ state: 'visible', timeout: 15000 });

    await page.locator('#dashboardDocumentsMeta').waitFor({ state: 'visible', timeout: 15000 });
    await page.locator('#dashboardDocumentsRows tr').first().waitFor({ state: 'visible', timeout: 15000 });

    const metaText = (await page.locator('#dashboardDocumentsMeta').innerText()).trim();
    const rowCount = await page.locator('#dashboardDocumentsRows tr').count();
    const viewCount = await page.locator('#dashboardDocumentsRows a[target="_blank"]').count();
    const downloadCount = await page.locator('#dashboardDocumentsRows a[href*="download=True"], #dashboardDocumentsRows a[href*="download=true"]').count();

    console.log(JSON.stringify({ ok: true, href, rowCount, viewCount, downloadCount, metaText }, null, 2));
  } finally {
    await browser.close();
  }
})();
