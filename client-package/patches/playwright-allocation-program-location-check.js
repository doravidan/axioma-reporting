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
    viewport: { width: 1360, height: 1000 },
  });
  const page = await context.newPage();

  await page.goto(`${base}/Account/Login`, { waitUntil: 'networkidle', timeout: 30000 });
  await page.locator('input[name="IdNumber"], #IdNumber').fill(adminId);
  await page.locator('input[name="Password"], input[type="password"]').fill(adminPassword);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'networkidle', timeout: 30000 }).catch(() => null),
    page.locator('form button[type="submit"]').first().click(),
  ]);

  await page.goto(`${base}/Employee/2/Allocations/Create`, { waitUntil: 'networkidle', timeout: 30000 });
  await page.waitForSelector('#programIdsSelect', { state: 'attached', timeout: 10000 });

  const location = await page.evaluate(() => {
    const select = document.getElementById('programIdsSelect');
    const field = select.closest('.col-md-6, .col-md-4, .col-md-3, .col-12');
    const card = select.closest('.card');
    const cardHeader = card && card.querySelector('.card-header');
    const project = document.querySelector('select[name="ProjectId"], select#ProjectId');
    const projectField = project.closest('.col-md-6, .col-md-4, .col-md-3, .col-12');
    const headerRow = projectField.closest('.row');
    const assignmentsCard = Array.from(document.querySelectorAll('.card')).find((c) => (c.querySelector('.card-header')?.textContent || '').includes('שיוכים'));
    return {
      cardHeader: cardHeader ? cardHeader.textContent.trim() : '',
      sameTopRowAsProject: field.parentElement === headerRow,
      inAssignmentsCard: assignmentsCard ? assignmentsCard.contains(select) : false,
      projectIndex: Array.from(headerRow.children).indexOf(projectField),
      programIndex: Array.from(headerRow.children).indexOf(field),
    };
  });

  const projectSelect = page.locator('select[name="ProjectId"], select#ProjectId').first();
  const firstProject = await projectSelect.locator('option').evaluateAll((options) => {
    const item = options.find((option) => option.value);
    return item && item.value;
  });
  if (firstProject) {
    await projectSelect.selectOption(firstProject);
    await page.waitForTimeout(1500);
  }

  const programState = await page.evaluate(() => {
    const programs = document.getElementById('programIdsSelect');
    const enabledProgramOptions = Array.from(programs.options).filter((option) => option.value && !option.disabled);
    if (enabledProgramOptions.length) {
      enabledProgramOptions[0].selected = true;
      programs.dispatchEvent(new Event('change', { bubbles: true }));
    }
    return { optionCount: enabledProgramOptions.length, selectedCount: Array.from(programs.selectedOptions).length };
  });
  await page.waitForTimeout(1500);

  const scopedState = await page.evaluate(() => {
    const names = ['SubjectIds', 'DomainIds', 'FrameworkIds', 'EducationalProgramIds', 'DiscussionCodeIds', 'GradeLevelIds', 'ClassIds'];
    return Object.fromEntries(names.map((name) => {
      const select = document.querySelector(`select[name="${name}"]`);
      return [name, {
        options: select ? select.options.length : 0,
        selected: select ? select.selectedOptions.length : 0,
      }];
    }));
  });

  const result = { location, programState, scopedState };
  console.log(JSON.stringify(result, null, 2));
  await browser.close();

  const scopedHasValues = Object.values(scopedState).some((state) => state.options > 0);
  const ok = location.cardHeader.includes('פרטי הקצאה')
    && location.sameTopRowAsProject
    && !location.inAssignmentsCard
    && location.programIndex === location.projectIndex + 1
    && scopedHasValues;
  process.exit(ok ? 0 : 1);
})().catch((err) => {
  console.error(err);
  process.exit(1);
});
