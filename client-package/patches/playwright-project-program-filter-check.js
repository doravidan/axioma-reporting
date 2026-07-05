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

  await page.goto(`${base}/Account/Login`, { waitUntil: 'networkidle', timeout: 30000 });
  await page.locator('input[name="IdNumber"], #IdNumber').fill(adminId);
  await page.locator('input[name="Password"], input[type="password"]').fill(adminPassword);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'networkidle', timeout: 30000 }).catch(() => null),
    page.locator('form button[type="submit"]').first().click(),
  ]);

  await page.goto(`${base}/Employee/2/Allocations/Create`, { waitUntil: 'networkidle', timeout: 30000 });
  await page.waitForSelector('#programIdsSelect', { state: 'attached', timeout: 10000 });

  const initialState = await page.evaluate(() => {
    const programs = document.getElementById('programIdsSelect');
    const scopedNames = ['SubjectIds', 'DomainIds', 'FrameworkIds', 'EducationalProgramIds', 'DiscussionCodeIds', 'GradeLevelIds', 'ClassIds'];
    return {
      programOptions: programs.options.length,
      selectedPrograms: programs.selectedOptions.length,
      scopedSelected: Object.fromEntries(scopedNames.map((name) => {
        const select = document.querySelector(`select[name="${name}"]`);
        return [name, select ? select.selectedOptions.length : null];
      })),
    };
  });

  if (initialState.programOptions !== 0 || initialState.selectedPrograms !== 0) {
    fail('Program dropdown should start empty on create allocation page.', initialState);
  }

  const projectValue = await page.evaluate(() => {
    const project = document.querySelector('select[name="ProjectId"], #projectIdSelect, #ProjectId');
    if (!project) return null;
    const option = Array.from(project.options).find((item) => item.value && !item.disabled);
    return option ? option.value : null;
  });
  if (!projectValue) fail('No selectable project was found on create allocation page.');

  const endpointResult = await page.evaluate(async (selectedProjectId) => {
    const response = await fetch(`/Employee/ProgramsForProject?projectId=${encodeURIComponent(selectedProjectId)}`, {
      credentials: 'same-origin',
    });
    return {
      ok: response.ok,
      status: response.status,
      json: response.ok ? await response.json() : null,
    };
  }, projectValue);
  if (!endpointResult.ok) fail('ProgramsForProject endpoint failed.', { status: endpointResult.status });
  const endpointPrograms = endpointResult.json;

  await page.locator('select[name="ProjectId"], #projectIdSelect, #ProjectId').first().selectOption(projectValue);
  await page.waitForTimeout(1500);

  const afterProjectState = await page.evaluate(() => {
    const programs = document.getElementById('programIdsSelect');
    return {
      programOptions: programs.options.length,
      selectedPrograms: programs.selectedOptions.length,
      values: Array.from(programs.options).map((option) => option.value),
    };
  });

  if (afterProjectState.programOptions !== endpointPrograms.length || afterProjectState.selectedPrograms !== 0) {
    fail('Program dropdown should show only the selected project plans and keep none selected.', {
      projectValue,
      endpointCount: endpointPrograms.length,
      afterProjectState,
    });
  }

  await page.evaluate(() => {
    const programs = document.getElementById('programIdsSelect');
    const option = Array.from(programs.options).find((item) => item.value && !item.disabled);
    if (option) {
      option.selected = true;
      programs.dispatchEvent(new Event('change', { bubbles: true }));
    }
  });
  await page.waitForTimeout(1500);

  const afterProgramState = await page.evaluate(() => {
    const programs = document.getElementById('programIdsSelect');
    const scopedNames = ['SubjectIds', 'DomainIds', 'FrameworkIds', 'EducationalProgramIds', 'DiscussionCodeIds', 'GradeLevelIds', 'ClassIds'];
    return {
      selectedPrograms: programs.selectedOptions.length,
      scoped: Object.fromEntries(scopedNames.map((name) => {
        const select = document.querySelector(`select[name="${name}"]`);
        return [name, {
          options: select ? select.options.length : 0,
          selected: select ? select.selectedOptions.length : 0,
        }];
      })),
    };
  });

  const scopedHasSelection = Object.values(afterProgramState.scoped).some((state) => state.selected > 0);
  if (afterProjectState.programOptions > 0 && (afterProgramState.selectedPrograms !== 1 || !scopedHasSelection)) {
    fail('Selecting one plan should select its configured assignment values.', afterProgramState);
  }

  await page.goto(`${base}/Admin/ProjectPrograms`, { waitUntil: 'networkidle', timeout: 30000 });
  const adminState = await page.evaluate(() => {
    const cards = Array.from(document.querySelectorAll('.project-program-card'));
    const first = cards[0];
    return {
      cardCount: cards.length,
      firstText: first ? first.innerText.slice(0, 300) : '',
      firstProjectId: first ? first.getAttribute('data-project-id') : null,
      hasProjectLabel: first ? first.innerText.includes('פרויקט:') : false,
      hasProjectCode: first ? first.innerText.includes('קוד פרויקט') : false,
    };
  });

  if (!adminState.cardCount || !adminState.hasProjectLabel || !adminState.hasProjectCode || !adminState.firstProjectId) {
    fail('Project-program admin page should visibly list project sections.', adminState);
  }

  console.log(JSON.stringify({
    initialState,
    selectedProjectId: projectValue,
    endpointProgramCount: endpointPrograms.length,
    afterProjectState,
    afterProgramState,
    adminState,
  }, null, 2));

  await browser.close();
})().catch((err) => {
  console.error(err);
  process.exit(1);
});
