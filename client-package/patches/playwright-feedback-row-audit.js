const { chromium } = require('playwright');

const base = 'https://www.postybell.co.il';
const adminId = '910000101';
const adminPassword = '910000101';

async function login(page) {
  await page.goto(`${base}/Account/Login`, { waitUntil: 'networkidle', timeout: 30000 });
  await page.locator('input[name="IdNumber"], #IdNumber').fill(adminId);
  await page.locator('input[name="Password"], input[type="password"]').fill(adminPassword);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'networkidle', timeout: 30000 }).catch(() => null),
    page.locator('form button[type="submit"]').first().click(),
  ]);
}

async function text(page) {
  return page.locator('body').innerText({ timeout: 10000 });
}

function hasAll(body, items) {
  return items.every((item) => body.includes(item));
}

function row(row, ok, evidence) {
  return { row, ok, evidence };
}

(async () => {
  const browser = await chromium.launch({
    headless: true,
    args: ['--host-resolver-rules=MAP www.postybell.co.il 192.168.1.1'],
  });
  const context = await browser.newContext({
    acceptDownloads: true,
    ignoreHTTPSErrors: true,
    locale: 'he-IL',
    viewport: { width: 1440, height: 1000 },
  });
  const page = await context.newPage();
  const results = [];

  await page.goto(`${base}/Account/Login`, { waitUntil: 'networkidle', timeout: 30000 });
  const passwordInput = page.locator('input[name="Password"], input[type="password"]').first();
  const beforeType = await passwordInput.getAttribute('type');
  const eye = page.locator('button:has-text("הצג"), button:has-text("הסתר"), [aria-label*="סיס"], [title*="סיס"], .password-toggle, #togglePassword').first();
  const eyeCount = await eye.count();
  if (eyeCount) await eye.click();
  const afterType = await passwordInput.getAttribute('type');
  results.push(row(5, beforeType === 'password' && eyeCount > 0 && afterType === 'text', { beforeType, afterType, eyeCount }));

  await page.locator('input[name="IdNumber"], #IdNumber').fill(adminId);
  await passwordInput.fill(adminPassword);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'networkidle', timeout: 30000 }).catch(() => null),
    page.locator('form button[type="submit"]').first().click(),
  ]);

  await page.goto(`${base}/`, { waitUntil: 'networkidle', timeout: 30000 });
  const homeBody = await text(page);
  const navState = await page.evaluate(() => {
    const nav = document.querySelector('nav, header');
    const links = Array.from((nav || document).querySelectorAll('a, button'))
      .map((el) => (el.textContent || '').replace(/\s+/g, ' ').trim())
      .filter(Boolean);
    const findBox = (needle) => {
      const el = Array.from(document.querySelectorAll('a, button, span, div')).find((item) => (item.textContent || '').includes(needle));
      if (!el) return null;
      const rect = el.getBoundingClientRect();
      return { x: rect.x, width: rect.width };
    };
    return {
      links,
      helloBox: findBox('שלום'),
      logoutBox: findBox('התנתק'),
      viewportWidth: window.innerWidth,
      hasPrivacyLink: !!document.querySelector('a[href="/Home/Privacy"], a[href*="/Home/Privacy"]'),
    };
  });
  const desiredNav = ['ניהול', 'ראשי', 'עובדים', 'הקצאות', 'חודשי דיווח', 'דש בורד דיווחים'];
  const navPositions = desiredNav.map((label) => navState.links.findIndex((link) => link.includes(label)));
  results.push(row(9, homeBody.includes('מדיניות פרטיות') && navState.hasPrivacyLink, { hasPrivacyText: homeBody.includes('מדיניות פרטיות'), hasPrivacyLink: navState.hasPrivacyLink }));
  results.push(row(10, !!navState.helloBox && navState.helloBox.x < navState.viewportWidth / 2, navState.helloBox));
  results.push(row(11, !!navState.logoutBox && navState.logoutBox.x < navState.viewportWidth / 2, navState.logoutBox));
  results.push(row(12, navPositions.every((index) => index >= 0) && navPositions.every((index, i, arr) => i === 0 || arr[i - 1] < index), { desiredNav, navPositions, links: navState.links }));

  await page.goto(`${base}/Home/Privacy`, { waitUntil: 'networkidle', timeout: 30000 });
  const privacyBody = await text(page);
  results.push(row(4, privacyBody.includes('מדיניות פרטיות') && privacyBody.trim().length > 50, { length: privacyBody.trim().length }));

  await page.goto(`${base}/Admin/PrivacyPolicy`, { waitUntil: 'networkidle', timeout: 30000 });
  const privacyAdmin = await page.evaluate(() => ({
    hasEditor: !!document.querySelector('#bodyHtml'),
    hasPublishForm: !!document.querySelector('form[action="/Admin/PublishPrivacyPolicy"]'),
    versionRows: document.querySelectorAll('table tbody tr').length,
  }));
  results.push(row(4.1, privacyAdmin.hasEditor && privacyAdmin.hasPublishForm && privacyAdmin.versionRows > 0, privacyAdmin));

  await page.goto(`${base}/Allocations`, { waitUntil: 'networkidle', timeout: 30000 });
  const allocationsBody = await text(page);
  const allocationsState = await page.evaluate(() => {
    const labels = Array.from(document.querySelectorAll('form label, form .form-label, thead th'))
      .map((el) => (el.textContent || '').replace(/\s+/g, ' ').trim())
      .filter(Boolean);
    return {
      hasAdd: !!Array.from(document.querySelectorAll('a, button')).find((el) => (el.textContent || '').includes('הוסף הקצאה')),
      labels,
      hasAllocationNotes: labels.some((label) => label.includes('הערות הקצאה')),
    };
  });
  const filterOrder = ['פרויקט', 'תוכנית', 'מחוז', 'מגזר', 'ת.ז', 'קוד עובד', 'שם פרטי', 'שם משפחה'];
  const filterPositions = filterOrder.map((label) => allocationsState.labels.findIndex((item) => item.includes(label)));
  results.push(row(8, allocationsState.hasAdd, { hasAdd: allocationsState.hasAdd }));
  results.push(row(13, filterPositions.every((index) => index >= 0) && filterPositions.every((index, i, arr) => i === 0 || arr[i - 1] < index), { filterOrder, filterPositions }));
  results.push(row(14, allocationsState.hasAllocationNotes && allocationsBody.includes('הערות הקצאה'), { hasAllocationNotes: allocationsState.hasAllocationNotes }));

  await page.goto(`${base}/Employee/2/Allocations/Create`, { waitUntil: 'networkidle', timeout: 30000 });
  await page.waitForSelector('#programIdsSelect', { state: 'attached', timeout: 10000 });
  const initialAllocationCreate = await page.evaluate(() => {
    const programs = document.getElementById('programIdsSelect');
    const project = document.querySelector('select[name="ProjectId"], select#ProjectId');
    const programField = programs.closest('.col-md-6, .col-md-4, .col-md-3, .col-12');
    const projectField = project.closest('.col-md-6, .col-md-4, .col-md-3, .col-12');
    const headerRow = projectField.closest('.row');
    const assignmentsCard = Array.from(document.querySelectorAll('.card')).find((card) => (card.querySelector('.card-header')?.textContent || '').includes('שיוכים'));
    const labels = Array.from(document.querySelectorAll('label')).map((el) => (el.textContent || '').replace(/\s+/g, ' ').trim());
    return {
      sameRow: programField.parentElement === headerRow,
      programIndex: Array.from(headerRow.children).indexOf(programField),
      projectIndex: Array.from(headerRow.children).indexOf(projectField),
      inAssignments: assignmentsCard ? assignmentsCard.contains(programs) : false,
      hasReportType: labels.some((label) => label.includes('סוג דיווח')),
      programOptions: Array.from(programs.options).filter((option) => option.value && !option.disabled).length,
    };
  });
  results.push(row(7, initialAllocationCreate.sameRow && initialAllocationCreate.programIndex === initialAllocationCreate.projectIndex + 1 && !initialAllocationCreate.inAssignments, initialAllocationCreate));
  results.push(row(19, initialAllocationCreate.sameRow && initialAllocationCreate.programIndex === initialAllocationCreate.projectIndex + 1, initialAllocationCreate));
  results.push(row(20, initialAllocationCreate.sameRow, initialAllocationCreate));
  results.push(row(32, initialAllocationCreate.hasReportType, initialAllocationCreate));

  const projectValue = await page.evaluate(() => {
    const select = document.querySelector('select[name="ProjectId"], select#ProjectId');
    const option = Array.from(select.options).find((item) => item.value);
    return option && option.value;
  });
  if (projectValue) await page.locator('select[name="ProjectId"], select#ProjectId').selectOption(projectValue);
  await page.waitForTimeout(1000);
  const allocationCreateAfterProject = await page.evaluate(() => ({
    programOptions: Array.from(document.getElementById('programIdsSelect').options).filter((option) => option.value && !option.disabled).length,
  }));
  await page.evaluate(() => {
    const programs = document.getElementById('programIdsSelect');
    const option = Array.from(programs.options).find((item) => item.value && !item.disabled);
    if (option) {
      option.selected = true;
      programs.dispatchEvent(new Event('change', { bubbles: true }));
    }
  });
  await page.waitForTimeout(1500);
  const scopedCounts = await page.evaluate(() => Object.fromEntries(['SubjectIds', 'DomainIds', 'FrameworkIds', 'EducationalProgramIds', 'DiscussionCodeIds', 'GradeLevelIds', 'ClassIds'].map((name) => {
    const select = document.querySelector(`select[name="${name}"]`);
    return [name, { options: select ? select.options.length : 0, selected: select ? select.selectedOptions.length : 0 }];
  })));
  results.push(row(6, allocationCreateAfterProject.programOptions > 0 && Object.values(scopedCounts).some((state) => state.selected > 0), { allocationCreateAfterProject, scopedCounts }));
  results.push(row(21, allocationCreateAfterProject.programOptions > 0 && Object.values(scopedCounts).some((state) => state.selected > 0), { allocationCreateAfterProject, scopedCounts }));

  await page.goto(`${base}/Admin/Frameworks`, { waitUntil: 'networkidle', timeout: 30000 });
  const frameworksState = await page.evaluate(() => {
    const body = document.body.innerText;
    const labels = Array.from(document.querySelectorAll('label, th, a, button')).map((el) => (el.textContent || '').replace(/\s+/g, ' ').trim());
    return {
      hasExport: !!document.querySelector('a[href="/Admin/ExportFrameworks"]'),
      hasImport: !!document.querySelector('a[href="/Admin/DataMigration"]'),
      hasHelp: body.includes('מסגרות') && body.includes('מוסדות'),
      hasSearchFields: ['שם מסגרת', 'סמל מסגרת', 'שלב חינוך', 'יישוב'].every((label) => labels.some((item) => item.includes(label))),
      hasBulkActive: body.includes('הפוך לפעיל') && body.includes('הפוך ללא פעיל'),
    };
  });
  results.push(row(16, frameworksState.hasExport, frameworksState));
  results.push(row(17, frameworksState.hasImport, frameworksState));
  results.push(row(28, frameworksState.hasHelp, frameworksState));
  results.push(row(29, frameworksState.hasSearchFields, frameworksState));
  results.push(row(30, frameworksState.hasBulkActive, frameworksState));
  results.push(row(34, frameworksState.hasSearchFields, frameworksState));

  const frameworkDownload = await Promise.all([
    page.waitForEvent('download', { timeout: 30000 }),
    page.locator('a[href="/Admin/ExportFrameworks"]').first().click(),
  ]).then(async ([download]) => {
    const path = 'test-results/frameworks-export.xlsx';
    await download.saveAs(path);
    return { suggestedFilename: download.suggestedFilename(), path };
  }).catch((err) => ({ error: String(err) }));
  results.push(row(16.1, !frameworkDownload.error && frameworkDownload.suggestedFilename.endsWith('.xlsx'), frameworkDownload));

  await page.goto(`${base}/Admin/InspectorAssignments`, { waitUntil: 'networkidle', timeout: 30000 });
  const inspectorBody = await text(page);
  results.push(row(18, hasAll(inspectorBody, ['ת.ז', 'שם פרטי', 'שם משפחה']), { hasAll: hasAll(inspectorBody, ['ת.ז', 'שם פרטי', 'שם משפחה']) }));

  await page.goto(`${base}/Employee`, { waitUntil: 'networkidle', timeout: 30000 });
  const employeeBody = await text(page);
  results.push(row(22, employeeBody.includes('לא פעיל') && !employeeBody.includes('השבתה'), {
    hasInactiveText: employeeBody.includes('לא פעיל'),
    hasOldDisableText: employeeBody.includes('השבתה'),
  }));

  await page.goto(`${base}/Admin/SystemConstants`, { waitUntil: 'networkidle', timeout: 30000 });
  const constantsBody = await text(page);
  results.push(row(27, constantsBody.includes('NotesSimilarityThresholdPercent') && !constantsBody.includes('NotesSimilarityThreshold '), {
    percentVisible: constantsBody.includes('NotesSimilarityThresholdPercent'),
    oldVisible: constantsBody.includes('NotesSimilarityThreshold '),
  }));

  await page.goto(`${base}/Dashboard`, { waitUntil: 'networkidle', timeout: 30000 });
  const dashboardBody = await text(page);
  const dashboardDropdownFix = await page.evaluate(() => {
    const selects = Array.from(document.querySelectorAll('#filterForm select.form-select'));
    return {
      selectCount: selects.length,
      fixedCount: selects.filter((select) => select.dataset.dropdownScrollFix === '1').length,
    };
  });
  results.push(row(15, dashboardDropdownFix.selectCount > 0 && dashboardDropdownFix.fixedCount === dashboardDropdownFix.selectCount, dashboardDropdownFix));
  results.push(row(23, dashboardBody.includes('קיום דיון'), { hasDiscussionLabel: dashboardBody.includes('קיום דיון') }));
  results.push(row(31, dashboardBody.includes('הצג דיווחים בארכיון') && dashboardBody.includes('דיווחים ישנים'), { archive: dashboardBody.includes('הצג דיווחים בארכיון'), help: dashboardBody.includes('דיווחים ישנים') }));
  results.push(row(32.1, dashboardBody.includes('סוג דיווח'), { hasReportType: dashboardBody.includes('סוג דיווח') }));

  await page.goto(`${base}/Dashboard/Summary`, { waitUntil: 'networkidle', timeout: 30000 });
  const summaryBody = await text(page);
  results.push(row(33, summaryBody.includes('מסמכים'), { hasDocuments: summaryBody.includes('מסמכים') }));
  results.push(row(40, summaryBody.includes('מסמכים'), { hasDocuments: summaryBody.includes('מסמכים') }));

  await page.goto(`${base}/Report/Manual`, { waitUntil: 'networkidle', timeout: 30000 });
  const manualBody = await text(page);
  results.push(row(3, hasAll(manualBody, ['הוספת דיווח ידני', 'עובד', 'הקצאה', 'חודש דיווח']), { url: page.url() }));
  results.push(row(36, hasAll(manualBody, ['חודש דיווח', 'הקצאה']), { manualBody: manualBody.slice(0, 200) }));
  results.push(row(37, hasAll(manualBody, ['עובד', 'חודש דיווח']), { manualBody: manualBody.slice(0, 200) }));

  await page.goto(`${base}/Report/ManualOpen?userId=87&allocationId=57&reportingMonthId=4`, { waitUntil: 'networkidle', timeout: 30000 });
  const reportBody = await text(page);
  await page.waitForTimeout(1500);
  const reportState = await page.evaluate(() => ({
    hasFrameworkSelect: !!document.querySelector('#fieldFramework, select[name="row.FrameworkId"]'),
    hasFrameworkAutocomplete: !!document.querySelector('.choices-framework-autocomplete, #fieldFramework[data-subject-autocomplete-init="1"], select[name="row.FrameworkId"][data-subject-autocomplete-init="1"]'),
    frameworkOptionsWithSymbol: Array.from(document.querySelectorAll('#fieldFramework option, select[name="row.FrameworkId"] option')).some((option) => /\d/.test(option.textContent || '') && (option.textContent || '').trim().length > 10),
    hasDiscussionLabel: document.body.innerText.includes('קיום דיון'),
    hasDocumentsArea: document.body.innerText.includes('מסמכי') || document.body.innerText.includes('מסמכים'),
    hasReportType: document.body.innerText.includes('סוג דיווח'),
  }));
  results.push(row(23.1, reportState.hasDiscussionLabel, reportState));
  results.push(row(32.2, reportState.hasReportType, reportState));
  results.push(row(33.1, reportState.hasDocumentsArea, reportState));
  results.push(row(35, reportState.frameworkOptionsWithSymbol || reportBody.includes('|'), reportState));
  results.push(row(38, reportState.frameworkOptionsWithSymbol || reportBody.includes('|'), reportState));
  results.push(row(39, reportState.hasFrameworkSelect && reportState.hasFrameworkAutocomplete, reportState));

  const reportId = new URL(page.url()).searchParams.get('reportId');
  if (reportId) {
    const reportDownload = await Promise.all([
      page.waitForEvent('download', { timeout: 30000 }),
      page.locator(`a[href*="/Report/ExportReportMonth?reportId=${reportId}"]`).first().click(),
    ]).then(async ([download]) => {
      const path = 'test-results/report-export.xlsx';
      await download.saveAs(path);
      return { suggestedFilename: download.suggestedFilename(), path };
    }).catch((err) => ({ error: String(err), reportId }));
    results.push(row(24, !reportDownload.error && reportDownload.suggestedFilename.endsWith('.xlsx'), reportDownload));
    results.push(row(25, !reportDownload.error && reportDownload.suggestedFilename.endsWith('.xlsx'), reportDownload));
  } else {
    results.push(row(24, false, { error: 'no reportId' }));
    results.push(row(25, false, { error: 'no reportId' }));
  }

  console.log(JSON.stringify(results, null, 2));
  await page.screenshot({ path: 'test-results/feedback-row-audit-final.png', fullPage: true });
  await browser.close();
  process.exit(results.every((item) => item.ok) ? 0 : 1);
})().catch((err) => {
  console.error(err);
  process.exit(1);
});
