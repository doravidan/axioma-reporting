const { chromium } = require('playwright');

const loginId = process.env.POSTYBELL_LOGIN_ID || 'axioma.pm.test';
const password = process.env.POSTYBELL_PASSWORD || 'Axioma2026!';
const origin = 'https://www.postybell.co.il';

(async () => {
  const browser = await chromium.launch({
    args: ['--host-resolver-rules=MAP postybell.co.il 192.168.1.1,MAP www.postybell.co.il 192.168.1.1'],
  });
  const page = await browser.newPage({ ignoreHTTPSErrors: true });

  await page.goto(`${origin}/Account/Login`, { waitUntil: 'domcontentloaded', timeout: 30000 });
  await page.fill('input[name="IdNumber"]', loginId);
  await page.fill('input[name="Password"]', password);
  await Promise.all([
    page.waitForNavigation({ waitUntil: 'domcontentloaded', timeout: 30000 }),
    page.click('button[type="submit"]'),
  ]);

  const employeeId = process.env.POSTYBELL_EMPLOYEE_ID || '1591';
  const response = await page.goto(`${origin}/Employee/${employeeId}/Allocations/Create`, {
    waitUntil: 'domcontentloaded',
    timeout: 30000,
  });

  await page.waitForSelector('#projectIdSelect option:not([value=""])', { state: 'attached', timeout: 30000 });
  const projectValue = await page.evaluate(async () => {
    const options = Array.from(document.querySelectorAll('#projectIdSelect option:not([value=""])'));
    const counts = await Promise.all(options.map(async option => {
      const projectId = encodeURIComponent(option.value);
      const programsResp = await fetch(`/Employee/ProgramsForProject?projectId=${projectId}`);
      const programs = programsResp.ok ? await programsResp.json() : [];
      const scopedResp = await fetch(`/allocations/ScopedLookups?projectId=${projectId}`);
      const scoped = scopedResp.ok ? await scopedResp.json() : {};
      const scopedTotal = ['subjects', 'domains', 'frameworks', 'educationalPrograms', 'discussionCodes', 'gradeLevels', 'classes']
        .reduce((sum, key) => sum + ((scoped[key] || []).length), 0);
      return { value: option.value, programs: programs.length, scopedTotal };
    }));
    counts.sort((a, b) => (b.scopedTotal - a.scopedTotal) || (b.programs - a.programs));
    return counts[0].value;
  });
  await page.selectOption('#projectIdSelect', projectValue);

  await page.waitForFunction(() => {
    const programs = document.querySelector('select[name="ProgramIds"]');
    return programs && programs.options.length > 0 && Array.from(programs.options).every(o => o.selected);
  }, { timeout: 30000 });

  await page.waitForTimeout(1500);

  const state = await page.evaluate(() => {
    return Array.from(document.querySelectorAll('select[multiple]')).map(select => {
      const options = Array.from(select.options);
      return {
        name: select.name,
        total: options.length,
        selected: options.filter(o => o.selected).length,
      };
    });
  });

  const failed = state.filter(select => select.total > 0 && select.selected !== select.total);

  console.log(`STATUS ${response ? response.status() : 'none'}`);
  console.log(`URL ${page.url()}`);
  console.log(`TITLE ${await page.title()}`);
  console.log(JSON.stringify(state, null, 2));

  await page.screenshot({ path: 'postybell-allocation-create-autoselect.png', fullPage: true });
  await browser.close();

  if (failed.length > 0) {
    console.error(`NOT_ALL_SELECTED ${JSON.stringify(failed)}`);
    process.exitCode = 2;
  }
})();
