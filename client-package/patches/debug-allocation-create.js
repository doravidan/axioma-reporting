const { chromium } = require('playwright');

const loginId = process.env.POSTYBELL_LOGIN_ID || 'axioma.pm.test';
const password = process.env.POSTYBELL_PASSWORD || 'Axioma2026!';
const employeeId = process.env.POSTYBELL_EMPLOYEE_ID || '1591';
const origin = 'https://www.postybell.co.il';

(async () => {
  const browser = await chromium.launch({
    args: ['--host-resolver-rules=MAP postybell.co.il 192.168.1.1,MAP www.postybell.co.il 192.168.1.1'],
  });
  const page = await browser.newPage({ ignoreHTTPSErrors: true });
  page.on('console', msg => console.log(`CONSOLE ${msg.type()} ${msg.text()}`));
  page.on('response', async response => {
    const url = response.url();
    if (url.includes('/Employee/ProgramsForProject') || url.includes('/allocations/ScopedLookups')) {
      let body = '';
      try { body = await response.text(); } catch {}
      console.log(`RESPONSE ${response.status()} ${url} ${body.slice(0, 300)}`);
    }
  });

  await page.goto(`${origin}/Account/Login`, { waitUntil: 'domcontentloaded', timeout: 30000 });
  await page.fill('input[name="IdNumber"]', loginId);
  await page.fill('input[name="Password"]', password);
  await Promise.all([
    page.waitForLoadState('domcontentloaded', { timeout: 30000 }).catch(() => {}),
    page.click('button[type="submit"]'),
  ]);
  await page.waitForTimeout(1500);
  console.log(`AFTER_LOGIN ${page.url()} ${await page.title()}`);
  if (page.url().includes('/Account/TermsOfUse')) {
    const buttons = await page.locator('button, input[type="submit"], a.btn').evaluateAll(nodes => nodes.map((node, index) => ({
      index,
      text: node.innerText || node.value || node.getAttribute('aria-label') || '',
      tag: node.tagName,
      type: node.getAttribute('type') || '',
      href: node.getAttribute('href') || ''
    })));
    console.log(`TERMS_ACTIONS ${JSON.stringify(buttons)}`);
    const checkbox = page.locator('input[type="checkbox"]').first();
    if (await checkbox.count()) {
      await checkbox.check({ force: true });
    }
    const accept = page.locator('button[type="submit"], input[type="submit"]').first();
    if (await accept.count()) {
      await Promise.all([
        page.waitForLoadState('domcontentloaded', { timeout: 30000 }).catch(() => {}),
        accept.click(),
      ]);
      await page.waitForTimeout(1500);
      console.log(`AFTER_TERMS ${page.url()} ${await page.title()}`);
    }
  }

  const response = await page.goto(`${origin}/Employee/${employeeId}/Allocations/Create`, {
    waitUntil: 'domcontentloaded',
    timeout: 30000,
  });
  await page.waitForTimeout(2000);
  console.log(`FORM_STATUS ${response ? response.status() : 'none'}`);
  console.log(`FORM_URL ${page.url()}`);
  console.log(`FORM_TITLE ${await page.title()}`);
  const state = await page.evaluate(() => {
    const project = document.getElementById('projectIdSelect');
    const programs = document.getElementById('programIdsSelect');
    return {
      bodyText: document.body.innerText.slice(0, 1000),
      hasProject: !!project,
      hasPrograms: !!programs,
      projectOptions: project ? Array.from(project.options).map(o => ({ value: o.value, text: o.textContent })) : [],
      programOptions: programs ? Array.from(programs.options).map(o => ({ value: o.value, text: o.textContent })) : [],
      programChoices: !!programs?.choicesInstance,
      choicesContainers: document.querySelectorAll('.choices').length,
    };
  });
  console.log(JSON.stringify(state, null, 2));
  if (state.hasProject) {
    await page.selectOption('#projectIdSelect', '16');
    await page.waitForTimeout(2500);
    const afterSelect = await page.evaluate(() => {
      const programs = document.getElementById('programIdsSelect');
      const programField = programs?.closest('.col-md-6, .col-md-4, .col-md-3, .col-12') || programs?.parentElement;
      return {
        nativeProgramOptions: programs ? Array.from(programs.options).map(o => ({ value: o.value, text: o.textContent, selected: o.selected })) : [],
        programChoices: !!programs?.choicesInstance,
        programFieldText: programField ? programField.innerText : '',
        choicesText: programField ? Array.from(programField.querySelectorAll('.choices')).map(x => x.innerText) : [],
      };
    });
    console.log(`AFTER_SELECT ${JSON.stringify(afterSelect, null, 2)}`);
    await page.locator('#programIdsSelect').locator('..').locator('.choices').first().click({ timeout: 5000 }).catch(() => {});
    await page.waitForTimeout(500);
    const openState = await page.evaluate(() => {
      const programs = document.getElementById('programIdsSelect');
      const programField = programs?.closest('.col-md-6, .col-md-4, .col-md-3, .col-12') || programs?.parentElement;
      return {
        dropdownItems: programField ? Array.from(programField.querySelectorAll('.choices__list--dropdown .choices__item--choice')).map(x => x.textContent.trim()) : [],
        visibleText: programField ? programField.innerText : ''
      };
    });
    console.log(`AFTER_OPEN ${JSON.stringify(openState, null, 2)}`);
  }
  await page.screenshot({ path: 'debug-allocation-create.png', fullPage: true });
  await browser.close();
})();
