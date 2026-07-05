// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function () {
  function ready(fn) {
    if (document.readyState === 'loading') {
      document.addEventListener('DOMContentLoaded', fn);
    } else {
      fn();
    }
  }

  ready(function () {
    function htmlEscape(value) {
      return String(value == null ? '' : value)
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;');
    }

    function moveAllocationProgramsToHeader() {
      var programsSelect = document.getElementById('programIdsSelect');
      var projectSelect = document.querySelector('select[name="ProjectId"], select#ProjectId');
      if (!programsSelect || !projectSelect) return;

      var programsField = programsSelect.closest('.col-md-6, .col-md-4, .col-md-3, .col-12');
      var projectField = projectSelect.closest('.col-md-6, .col-md-4, .col-md-3, .col-12');
      var headerRow = projectField && projectField.closest('.row');
      if (!programsField || !projectField || !headerRow || programsField.parentElement === headerRow) return;

      programsField.className = 'col-md-6';
      headerRow.insertBefore(programsField, projectField.nextSibling);
    }

    moveAllocationProgramsToHeader();

    function attachPasswordToggle(password) {
      if (!password || password.dataset.visibilityToggle) return;
      var wrapper = document.createElement('div');
      wrapper.className = 'password-toggle-wrap';
      password.parentNode.insertBefore(wrapper, password);
      wrapper.appendChild(password);

      var button = document.createElement('button');
      button.type = 'button';
      button.className = 'password-toggle-btn';
      button.setAttribute('aria-label', 'הצג סיסמה');
      button.setAttribute('title', 'הצג סיסמה');
      button.innerHTML = '<svg viewBox="0 0 24 24" aria-hidden="true" focusable="false"><path d="M12 5c4.2 0 7.6 2.4 10 7-2.4 4.6-5.8 7-10 7s-7.6-2.4-10-7c2.4-4.6 5.8-7 10-7Zm0 2c-3.2 0-5.8 1.7-7.7 5 1.9 3.3 4.5 5 7.7 5s5.8-1.7 7.7-5C17.8 8.7 15.2 7 12 7Zm0 2.5A2.5 2.5 0 1 1 12 14a2.5 2.5 0 0 1 0-5Z" fill="currentColor"/></svg>';
      wrapper.appendChild(button);

      button.addEventListener('click', function () {
        var show = password.type === 'password';
        password.type = show ? 'text' : 'password';
        button.setAttribute('aria-label', show ? 'הסתר סיסמה' : 'הצג סיסמה');
        button.setAttribute('title', show ? 'הסתר סיסמה' : 'הצג סיסמה');
      });

      password.dataset.visibilityToggle = '1';
    }

    document.querySelectorAll('input[type="password"]').forEach(attachPasswordToggle);

    var mainNav = document.querySelector('.app-main-nav');
    if (!mainNav) return;

    if (!mainNav.querySelector('a[href*="/Home/Privacy"], a[href="/Privacy"]')) {
      var privacyItem = document.createElement('li');
      privacyItem.className = 'nav-item';
      var privacyLink = document.createElement('a');
      privacyLink.className = 'nav-link';
      privacyLink.href = '/Home/Privacy';
      privacyLink.textContent = 'מדיניות פרטיות';
      privacyItem.appendChild(privacyLink);
      mainNav.appendChild(privacyItem);
    }

    var items = Array.prototype.slice.call(mainNav.children);
    function score(item) {
      var link = item.querySelector('a');
      var href = ((link && link.getAttribute('href')) || '').toLowerCase();
      var text = (link && link.textContent || '').trim();
      if (item.querySelector('.dropdown-menu') || text.indexOf('ניהול') >= 0) return 10;
      if (href.indexOf('/home') >= 0 || href === '/' || text === 'ראשי') return 20;
      if (href.indexOf('/employee') >= 0 || text.indexOf('עובדים') >= 0) return 30;
      if (href.indexOf('/allocations') >= 0 || text.indexOf('הקצאות') >= 0) return 40;
      if (href.indexOf('/admin/reportingmonths') >= 0 || text.indexOf('חודשי') >= 0) return 50;
      if (href.indexOf('/dashboard') >= 0 || text.indexOf('דש') >= 0) return 60;
      if (href.indexOf('/report') >= 0 || href.indexOf('/myallocations') >= 0 || text.indexOf('פעילות') >= 0) return 70;
      if (href.indexOf('/privacy') >= 0 || text.indexOf('פרטיות') >= 0) return 80;
      return 90;
    }
    items.sort(function (a, b) { return score(a) - score(b); }).forEach(function (item) {
      mainNav.appendChild(item);
    });

    var allocationsPage = document.querySelector('.employee-allocations-list');
    var allocationFilterGrid = allocationsPage && allocationsPage.querySelector('.filter-grid');
    if (allocationFilterGrid) {
      [
        'f-project',
        'f-programs',
        'f-districts',
        'f-sectors',
        'f-id',
        'f-code',
        'f-first',
        'f-last',
        'f-annual',
        'f-monthly',
        'f-durations',
        'f-notes'
      ].forEach(function (id) {
        var field = allocationFilterGrid.querySelector('#' + id);
        if (field && field.closest('.filter-field')) {
          allocationFilterGrid.appendChild(field.closest('.filter-field'));
        }
      });
    }

    if (allocationsPage) {
      var notesLabel = allocationsPage.querySelector('label[for="f-notes"]');
      if (notesLabel) notesLabel.textContent = 'הערות הקצאה';
      Array.prototype.forEach.call(allocationsPage.querySelectorAll('th a'), function (link) {
        if ((link.textContent || '').trim() === 'הערות' && (link.getAttribute('href') || '').indexOf('notes') >= 0) {
          link.textContent = 'הערות הקצאה';
        }
      });
    }

    var dashboardFilterForm = document.getElementById('filterForm');
    if (dashboardFilterForm) {
      Array.prototype.forEach.call(dashboardFilterForm.querySelectorAll('select.form-select'), function (select) {
        if (select.dataset.dropdownScrollFix) return;
        select.dataset.dropdownScrollFix = '1';
        var ensureSpaceBelow = function () {
          var rect = select.getBoundingClientRect();
          if (rect.bottom > window.innerHeight * 0.55) {
            select.scrollIntoView({ block: 'start', inline: 'nearest' });
          }
        };
        select.addEventListener('mousedown', ensureSpaceBelow);
        select.addEventListener('focus', ensureSpaceBelow);
      });

      var includeArchived = dashboardFilterForm.querySelector('#includeArchived');
      if (includeArchived && !dashboardFilterForm.querySelector('.archive-filter-help')) {
        var archiveWrap = includeArchived.closest('.form-check') || includeArchived.parentElement;
        includeArchived.title = 'מציג גם דיווחים שסומנו כארכיון ואינם מוצגים כברירת מחדל.';
        var archiveHelp = document.createElement('div');
        archiveHelp.className = 'form-text archive-filter-help';
        archiveHelp.textContent = 'ארכיון = דיווחים ישנים/מוסתרים שאינם מוצגים כברירת מחדל; סימון השדה כולל אותם בתוצאות.';
        if (archiveWrap) archiveWrap.appendChild(archiveHelp);
      }
    }

    if (window.location.pathname.toLowerCase() === '/admin/frameworks') {
      var frameworksContainer = Array.prototype.find.call(document.querySelectorAll('.container-fluid'), function (container) {
        return container.querySelector('h3') && (container.querySelector('h3').textContent || '').indexOf('מסגרות') >= 0;
      }) || document.querySelector('main .container-fluid');
      if (frameworksContainer && !frameworksContainer.querySelector('.frameworks-explainer')) {
        var explainer = document.createElement('p');
        explainer.className = 'text-muted small frameworks-explainer';
        explainer.textContent = 'מסגרות הן רשומות השיוך והדיווח במערכת. מוסדות הם מאגר נתוני מוסד רחב יותר הכולל יישוב, מחוז, מגזר ושלב חינוך; חיפוש לפי יישוב מתבצע דרך סמל המוסד המקושר.';
        var title = frameworksContainer.querySelector('h3');
        var titleRow = title ? title.closest('.d-flex.justify-content-between') : null;
        if (titleRow) {
          titleRow.insertAdjacentElement('afterend', explainer);
        } else if (title) {
          title.insertAdjacentElement('afterend', explainer);
        }
      }

      var frameworksActions = document.querySelector('.container-fluid .d-flex.justify-content-between .d-flex.gap-2');
      if (frameworksActions && !frameworksActions.querySelector('a[href="/Admin/ExportFrameworks"]')) {
        var exportFrameworks = document.createElement('a');
        exportFrameworks.className = 'btn btn-success btn-sm';
        exportFrameworks.href = '/Admin/ExportFrameworks';
        exportFrameworks.textContent = 'ייצוא לאקסל';
        frameworksActions.insertBefore(exportFrameworks, frameworksActions.firstChild);

        var importFrameworks = document.createElement('a');
        importFrameworks.className = 'btn btn-outline-primary btn-sm';
        importFrameworks.href = '/Admin/DataMigration';
        importFrameworks.textContent = 'ייבוא אקסל';
        importFrameworks.title = 'ייבוא מסגרות דרך ייבוא טבלאות עזר';
        frameworksActions.insertBefore(importFrameworks, exportFrameworks.nextSibling);
      }

      if (frameworksContainer && !frameworksContainer.querySelector('.framework-filter-form')) {
        var params = new URLSearchParams(window.location.search);
        var token = document.querySelector('input[name="__RequestVerificationToken"]');
        var filterForm = document.createElement('form');
        filterForm.method = 'get';
        filterForm.className = 'card card-body mb-3 framework-filter-form';
        filterForm.innerHTML =
          '<div class="row g-2 align-items-end">' +
          '<div class="col-md-3"><label class="form-label">שם מסגרת</label><input name="frameworkName" class="form-control form-control-sm" value="' + htmlEscape(params.get('frameworkName') || '') + '"></div>' +
          '<div class="col-md-2"><label class="form-label">סמל מסגרת</label><input name="institutionSymbol" class="form-control form-control-sm" value="' + htmlEscape(params.get('institutionSymbol') || '') + '"></div>' +
          '<div class="col-md-2"><label class="form-label">שלב חינוך</label><select name="educationalStageId" class="form-select form-select-sm"><option value="">הכל</option></select></div>' +
          '<div class="col-md-2"><label class="form-label">יישוב</label><input name="localityName" class="form-control form-control-sm" value="' + htmlEscape(params.get('localityName') || '') + '"></div>' +
          '<div class="col-md-1"><label class="form-label">סטטוס</label><select name="isActive" class="form-select form-select-sm"><option value="">הכל</option><option value="true">פעיל</option><option value="false">לא פעיל</option></select></div>' +
          '<div class="col-md-2 d-flex gap-2"><button class="btn btn-primary btn-sm" type="submit">חפש</button><a class="btn btn-outline-secondary btn-sm" href="/Admin/Frameworks">נקה</a></div>' +
          '</div>';
        var stageSelect = filterForm.querySelector('select[name="educationalStageId"]');
        var modalStage = document.querySelector('#addModal select[name="educationalStageId"]');
        if (stageSelect && modalStage) {
          Array.prototype.forEach.call(modalStage.options, function (option) {
            if (!option.value) return;
            var copy = document.createElement('option');
            copy.value = option.value;
            copy.textContent = option.textContent;
            copy.selected = option.value === (params.get('educationalStageId') || '');
            stageSelect.appendChild(copy);
          });
        }
        var activeSelect = filterForm.querySelector('select[name="isActive"]');
        if (activeSelect) activeSelect.value = params.get('isActive') || '';
        var firstCard = frameworksContainer.querySelector('.card');
        var firstTable = frameworksContainer.querySelector('table');
        if (firstCard) {
          firstCard.insertAdjacentElement('beforebegin', filterForm);
        } else if (firstTable) {
          firstTable.insertAdjacentElement('beforebegin', filterForm);
        } else {
          frameworksContainer.appendChild(filterForm);
        }

        var bulk = document.createElement('div');
        bulk.className = 'd-flex gap-2 mb-3 framework-bulk-actions';
        function bulkForm(label, active, cls) {
          var form = document.createElement('form');
          form.method = 'post';
          form.action = '/Admin/BulkSetFrameworksActive';
          form.className = 'd-inline';
          if (token) {
            var tokenInput = document.createElement('input');
            tokenInput.type = 'hidden';
            tokenInput.name = '__RequestVerificationToken';
            tokenInput.value = token.value;
            form.appendChild(tokenInput);
          }
          ['frameworkName', 'institutionSymbol', 'educationalStageId', 'localityName'].forEach(function (name) {
            var input = document.createElement('input');
            input.type = 'hidden';
            input.name = name;
            input.value = params.get(name) || '';
            form.appendChild(input);
          });
          var activeInput = document.createElement('input');
          activeInput.type = 'hidden';
          activeInput.name = 'isActive';
          activeInput.value = active ? 'true' : 'false';
          form.appendChild(activeInput);
          var btn = document.createElement('button');
          btn.type = 'submit';
          btn.className = cls;
          btn.textContent = label;
          btn.onclick = function () { return confirm('לעדכן את כל המסגרות לפי הסינון הנוכחי?'); };
          form.appendChild(btn);
          return form;
        }
        bulk.appendChild(bulkForm('הפוך לפעיל (מסוננים)', true, 'btn btn-sm btn-success'));
        bulk.appendChild(bulkForm('הפוך ללא פעיל (מסוננים)', false, 'btn btn-sm btn-outline-danger'));
        filterForm.insertAdjacentElement('afterend', bulk);
      }
    }

    if (window.location.pathname.toLowerCase() === '/admin/inspectorassignments') {
      var inspectorSelect = document.querySelector('select[name="inspectorUserId"]');
      var inspectorMap = {};
      if (inspectorSelect) {
        Array.prototype.forEach.call(inspectorSelect.options, function (option) {
          var text = (option.textContent || '').trim();
          var match = text.match(/^(.+?)\s*\((.*?)(?:,\s*([^)]+))?\)$/);
          if (!match) return;
          inspectorMap[match[1].trim()] = {
            idNumber: (match[3] || '').trim(),
            firstName: match[1].trim().split(/\s+/)[0] || '',
            lastName: match[1].trim().split(/\s+/).slice(1).join(' ')
          };
        });
      }

      var inspectorTable = document.querySelector('.table-responsive table');
      if (inspectorTable && inspectorTable.tHead && !inspectorTable.dataset.identityColumns) {
        inspectorTable.dataset.identityColumns = '1';
        var headerRow = inspectorTable.tHead.rows[0];
        if (headerRow && headerRow.cells.length >= 1) {
          ['ת.ז', 'שם פרטי', 'שם משפחה'].reverse().forEach(function (title) {
            var th = document.createElement('th');
            th.textContent = title;
            headerRow.insertBefore(th, headerRow.cells[1]);
          });
        }
        Array.prototype.forEach.call(inspectorTable.tBodies[0] ? inspectorTable.tBodies[0].rows : [], function (row) {
          if (!row.cells.length || row.cells[0].colSpan > 1) {
            if (row.cells[0]) row.cells[0].colSpan = 8;
            return;
          }
          var fullName = (row.cells[0].textContent || '').trim();
          var data = inspectorMap[fullName] || { idNumber: '', firstName: fullName.split(/\s+/)[0] || '', lastName: fullName.split(/\s+/).slice(1).join(' ') };
          [data.idNumber, data.firstName, data.lastName].reverse().forEach(function (value) {
            var td = document.createElement('td');
            td.textContent = value || '-';
            row.insertBefore(td, row.cells[1]);
          });
        });
      }
    }

    var allocationReportTypeHidden = document.getElementById('allocationReportTypeId');
    if (allocationReportTypeHidden && !document.getElementById('allocationReportTypeSelect')) {
      var projectSelect = document.querySelector('select[name="ProjectId"], select#ProjectId');
      var projectField = projectSelect && projectSelect.closest('.col-md-6, .col-md-4, .col-md-3, div');
      var reportTypeField = document.createElement('div');
      reportTypeField.className = projectField && projectField.className ? projectField.className : 'col-md-6';
      reportTypeField.innerHTML =
        '<label class="form-label" for="allocationReportTypeSelect">סוג דיווח</label>' +
        '<select id="allocationReportTypeSelect" class="form-select"><option value="">ללא</option></select>';
      if (projectField && projectField.parentElement) {
        var programsSelect = document.getElementById('programIdsSelect');
        var programsField = programsSelect && programsSelect.closest('.col-md-6, .col-md-4, .col-md-3, .col-12');
        if (programsField && programsField.parentElement === projectField.parentElement) {
          programsField.insertAdjacentElement('afterend', reportTypeField);
        } else {
          projectField.insertAdjacentElement('afterend', reportTypeField);
        }
      }
      var reportTypeSelect = reportTypeField.querySelector('select');
      fetch('/Employee/AllocationReportTypes')
        .then(function (response) { return response.ok ? response.json() : []; })
        .then(function (items) {
          items.forEach(function (item) {
            var option = document.createElement('option');
            option.value = item.id;
            option.textContent = item.text;
            option.selected = String(item.id) === String(allocationReportTypeHidden.value || '');
            reportTypeSelect.appendChild(option);
          });
        })
        .catch(function () { });
      reportTypeSelect.addEventListener('change', function () {
        allocationReportTypeHidden.value = reportTypeSelect.value;
      });
    }
  });
})();

(function () {
  if (!window.jQuery || !jQuery.validator) return;

  function isValidIsraeliId(value) {
    if (!value) return false;
    var trimmed = String(value).trim();
    if (!/^\d{1,9}$/.test(trimmed)) return false;

    var id = trimmed.padStart(9, '0');
    var sum = 0;
    for (var i = 0; i < id.length; i++) {
      var digit = Number(id.charAt(i));
      var product = digit * ((i % 2) + 1);
      if (product > 9) product = Math.floor(product / 10) + (product % 10);
      sum += product;
    }
    return sum % 10 === 0;
  }

  jQuery.validator.addMethod('israeliid', function (value, element) {
    return this.optional(element) || isValidIsraeliId(value);
  });

  jQuery.validator.addMethod('israeliphone', function (value, element) {
    return this.optional(element) || /^0(2|3|4|8|9|5[02-9]|7[2-9])\d{7}$/.test(value);
  });

  jQuery.validator.addMethod('wholenumber', function (value, element) {
    return this.optional(element) || /^\d+$/.test(value);
  });

  if (jQuery.validator.unobtrusive) {
    jQuery.validator.unobtrusive.adapters.addBool('israeliid');
    jQuery.validator.unobtrusive.adapters.addBool('israeliphone');
    jQuery.validator.unobtrusive.adapters.addBool('wholenumber');
  }
})();

(function () {
  function showActionMessage(message, isError) {
    var live = document.getElementById('filterLiveRegion');
    if (live) live.textContent = message;

    var toast = document.getElementById('filterToast');
    if (!toast) return;
    toast.textContent = message;
    toast.classList.remove('d-none', 'alert-success', 'alert-danger');
    toast.classList.add(isError ? 'alert-danger' : 'alert-success');
    setTimeout(function () { toast.classList.add('d-none'); }, 4000);
  }

  function markReportActionDone(reportId, text) {
    var cb = document.querySelector('.report-cb[value="' + reportId + '"]');
    var row = cb ? cb.closest('tr') : null;
    if (!row) return;

    var actionCell = row.lastElementChild;
    if (actionCell) actionCell.innerHTML = '<span class="badge bg-secondary">' + text + '</span>';
    cb.checked = false;
    cb.disabled = true;

    var selectedCount = document.getElementById('selectedCount');
    var bulkApproveBtn = document.getElementById('bulkApproveBtn');
    if (selectedCount && bulkApproveBtn) {
      var count = document.querySelectorAll('.report-cb:checked').length;
      selectedCount.textContent = count;
      bulkApproveBtn.disabled = count === 0;
    }
  }

  document.addEventListener('submit', function (event) {
    var form = event.target;
    if (!form || !form.action || form.action.indexOf('/Report/Approve') < 0) return;

    event.preventDefault();
    var button = form.querySelector('button[type="submit"]');
    if (button) button.disabled = true;

    fetch(form.action, {
      method: 'POST',
      body: new FormData(form),
      headers: { 'Accept': 'application/json', 'X-Requested-With': 'XMLHttpRequest' },
      credentials: 'same-origin'
    })
      .then(function (res) { return res.json(); })
      .then(function (data) {
        if (!data || !data.success) throw new Error((data && data.error) || 'שגיאה באישור הדיווח');
        markReportActionDone(data.reportId || form.action.split('/').pop(), 'אושר');
        showActionMessage('הדיווח אושר', false);
      })
      .catch(function (err) {
        if (button) button.disabled = false;
        showActionMessage(err.message || 'שגיאה באישור הדיווח', true);
      });
  }, true);

  document.addEventListener('submit', function (event) {
    var form = event.target;
    if (!form || !form.action || form.action.indexOf('/Report/Reject') < 0) return;

    event.preventDefault();
    var button = form.querySelector('button[type="submit"]');
    if (button) button.disabled = true;

    fetch(form.action, {
      method: 'POST',
      body: new FormData(form),
      headers: { 'Accept': 'application/json', 'X-Requested-With': 'XMLHttpRequest' },
      credentials: 'same-origin'
    })
      .then(function (res) { return res.json(); })
      .then(function (data) {
        if (!data || !data.success) throw new Error((data && data.error) || 'שגיאה בדחיית הדיווח');
        var reportId = data.reportId || (document.getElementById('rejectReportId') || {}).value;
        markReportActionDone(reportId, 'הוחזר לתיקון');
        var modalEl = document.getElementById('rejectModal');
        var modal = modalEl && window.bootstrap ? bootstrap.Modal.getInstance(modalEl) : null;
        if (modal) modal.hide();
        form.reset();
        showActionMessage('הדיווח הוחזר לתיקון', false);
      })
      .catch(function (err) {
        showActionMessage(err.message || 'שגיאה בדחיית הדיווח', true);
      })
      .finally(function () {
        if (button) button.disabled = false;
      });
  }, true);
})();

(function () {
  var lookupIdToField = {
    fieldDistrict: 'DistrictId',
    fieldLocality: 'LocalityId',
    fieldFramework: 'FrameworkId',
    fieldEduProgram: 'EducationalProgramId',
    fieldDomain: 'DomainId',
    fieldSubject1: 'Subject1Id',
    fieldSubject2: 'Subject2Id',
    fieldDiscussion: 'DiscussionCodeId',
    fieldConclusionClass: 'ConclusionClassId',
    fieldConclusionFramework: 'ConclusionFrameworkId',
    fieldConclusionLocation: 'ConclusionLocationId',
    fieldGradeLevel: 'GradeLevelId',
    fieldClass: 'ClassId',
    manualAllocationSelect: 'ManualAllocationId',
    manualReportingMonth: 'ManualReportingMonthId'
  };

  var searchableLookupFields = [
    'DistrictId',
    'LocalityId',
    'FrameworkId',
    'EducationalProgramId',
    'DomainId',
    'Subject1Id',
    'Subject2Id',
    'DiscussionCodeId',
    'ConclusionClassId',
    'ConclusionFrameworkId',
    'ConclusionLocationId',
    'GradeLevelId',
    'ClassId',
    'ManualAllocationId',
    'ManualReportingMonthId'
  ];

  function getLookupAutocompleteField(select) {
    if (!select || select.tagName !== 'SELECT' || select.multiple) return '';
    var name = select.dataset.name || select.name || '';
    if (name.indexOf('row.') === 0) name = name.substring(4);
    var editCell = select.closest ? select.closest('td[data-edit-field]') : null;
    var editField = editCell ? editCell.dataset.editField : '';

    var field = lookupIdToField[select.id] || editField || name;
    if (select.dataset.subjectAutocomplete === '1' && !field) field = 'Subject1Id';
    if (searchableLookupFields.indexOf(field) < 0) return '';
    if (field === 'Subject1Id' || field === 'Subject2Id') return 'subject';
    if (field === 'FrameworkId' || field === 'ConclusionFrameworkId' || field === 'ManualAllocationId') return 'framework';
    if (field.indexOf('Manual') === 0) return 'manual';
    return 'lookup';
  }

  function isLookupAutocompleteSelect(select) {
    return !!getLookupAutocompleteField(select);
  }

  function isSubjectAutocompleteSelect(select) {
    return getLookupAutocompleteField(select) === 'subject';
  }

  function initSubjectAutocomplete(root) {
    if (typeof window.Choices === 'undefined') return;
    var scope = root || document;
    var selects = [];
    if (scope.matches && isLookupAutocompleteSelect(scope)) {
      selects.push(scope);
    }
    scope.querySelectorAll?.('select').forEach(function (select) {
      if (isLookupAutocompleteSelect(select)) selects.push(select);
    });

    selects.forEach(function (select) {
      if (select.dataset.subjectAutocompleteInit) return;
      try {
        var fieldType = getLookupAutocompleteField(select);
        var isFramework = fieldType === 'framework';
        select.subjectChoicesInstance = new window.Choices(select, {
          searchEnabled: true,
          searchChoices: true,
          shouldSort: false,
          itemSelectText: '',
          placeholderValue: isFramework ? 'חפש לפי יישוב, סמל מוסד או שם מסגרת' : 'חיפוש...',
          searchPlaceholderValue: isFramework ? 'חפש לפי יישוב, סמל מוסד או שם מסגרת' : 'חיפוש...',
          noResultsText: 'לא נמצאו תוצאות',
          noChoicesText: 'אין אפשרויות',
          position: 'auto',
          allowHTML: false
        });
        var container = select.subjectChoicesInstance.containerOuter && select.subjectChoicesInstance.containerOuter.element;
        if (container) {
          container.classList.add('choices-subject-autocomplete');
          if (fieldType === 'framework') container.classList.add('choices-framework-autocomplete');
        }
        select.dataset.subjectAutocompleteInit = '1';
      } catch (e) { /* native select fallback */ }
    });
  }

  window.initSubjectAutocomplete = initSubjectAutocomplete;
  window.refreshSubjectAutocomplete = function (select) {
    if (!isLookupAutocompleteSelect(select)) return;
    var html = select.innerHTML;
    var disabled = select.disabled;
    var selectedValues = Array.prototype.map.call(select.selectedOptions || [], function (option) {
      return option.value;
    });
    if (select.subjectChoicesInstance && typeof select.subjectChoicesInstance.destroy === 'function') {
      try { select.subjectChoicesInstance.destroy(); } catch (e) { /* ignore */ }
    }
    select.innerHTML = html;
    select.disabled = disabled;
    selectedValues.forEach(function (value) {
      var option = Array.prototype.find.call(select.options, function (candidate) {
        return candidate.value === value;
      });
      if (option) option.selected = true;
    });
    delete select.dataset.subjectAutocompleteInit;
    initSubjectAutocomplete(select);
  };

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', function () { initSubjectAutocomplete(document); });
  } else {
    initSubjectAutocomplete(document);
  }

  if (window.MutationObserver) {
    new MutationObserver(function (mutations) {
      mutations.forEach(function (mutation) {
        mutation.addedNodes.forEach(function (node) {
          if (node.nodeType === 1) initSubjectAutocomplete(node);
        });
      });
    }).observe(document.documentElement, { childList: true, subtree: true });
  }

  document.addEventListener('shown.bs.modal', function (event) {
    event.target.querySelectorAll?.('select').forEach(function (select) {
      window.refreshSubjectAutocomplete(select);
    });
  });

  document.addEventListener('reset', function (event) {
    setTimeout(function () {
      event.target.querySelectorAll?.('select').forEach(function (select) {
        window.refreshSubjectAutocomplete(select);
      });
    }, 0);
  }, true);
})();

(function () {
  if (window.location.pathname.toLowerCase() !== '/report') return;

  var rowModalSelects = {
    fieldDistrict: { key: 'districts', placeholder: 'בחר...' },
    fieldLocality: { key: 'localities', placeholder: 'בחר...' },
    fieldFramework: { key: 'frameworks', placeholder: 'בחר...' },
    fieldEduProgram: { key: 'educationalPrograms', placeholder: 'בחר...' },
    fieldDomain: { key: 'domains', placeholder: 'בחר...' },
    fieldSubject1: { key: 'subjects', placeholder: 'בחר...' },
    fieldSubject2: { key: 'subjects', placeholder: '---' },
    fieldDiscussion: { key: 'discussionCodes', placeholder: '---' },
    fieldConclusionClass: { key: 'conclusionClasses', placeholder: '---' },
    fieldConclusionFramework: { key: 'conclusionFrameworks', placeholder: '---' },
    fieldConclusionLocation: { key: 'locations', placeholder: '---' },
    fieldGradeLevel: { key: 'gradeLevels', placeholder: '---' },
    fieldClass: { key: 'classes', placeholder: '---' }
  };
  var lookupCache = {};

  function getRowAllocationId() {
    var input = document.querySelector('#rowForm input[name="allocationId"]');
    if (input && input.value) return input.value;
    return new URLSearchParams(window.location.search).get('allocationId');
  }

  function itemText(item) {
    return item.text || item.description || item.name || '';
  }

  function destroyChoices(select) {
    if (select.subjectChoicesInstance && typeof select.subjectChoicesInstance.destroy === 'function') {
      try { select.subjectChoicesInstance.destroy(); } catch (e) { /* native select fallback */ }
    }
    delete select.dataset.subjectAutocompleteInit;
  }

  function rebuildRowSelect(select, config, items) {
    if (!select) return;
    var selected = select.value || '';
    var disabled = select.disabled;
    destroyChoices(select);
    select.innerHTML = '';
    var empty = document.createElement('option');
    empty.value = '';
    empty.textContent = config.placeholder;
    select.appendChild(empty);
    (items || []).forEach(function (item) {
      var option = document.createElement('option');
      option.value = String(item.id);
      option.textContent = itemText(item);
      if (selected && String(item.id) === String(selected)) option.selected = true;
      select.appendChild(option);
    });
    select.disabled = disabled;
    if (window.refreshSubjectAutocomplete) window.refreshSubjectAutocomplete(select);
  }

  function applyRowLookups(data) {
    Object.keys(rowModalSelects).forEach(function (id) {
      var config = rowModalSelects[id];
      rebuildRowSelect(document.getElementById(id), config, data[config.key] || []);
    });
  }

  function loadRowLookups() {
    var allocationId = getRowAllocationId();
    if (!allocationId) return Promise.resolve(null);
    var query = new URLSearchParams(window.location.search);
    var manual = (query.get('manual') || '').toLowerCase() === 'true';
    var cacheKey = allocationId + ':' + manual;
    if (!lookupCache[cacheKey]) {
      lookupCache[cacheKey] = fetch('/Report/AllocationLookups?allocationId=' + encodeURIComponent(allocationId) + '&manual=' + encodeURIComponent(manual), {
        headers: { 'Accept': 'application/json' },
        credentials: 'same-origin'
      }).then(function (res) {
        if (!res.ok) throw new Error('lookup load failed');
        return res.json();
      });
    }
    return lookupCache[cacheKey].then(function (data) {
      applyRowLookups(data);
      return data;
    }).catch(function () {
      delete lookupCache[cacheKey];
      return null;
    });
  }

  document.addEventListener('shown.bs.modal', function (event) {
    if (event.target && event.target.id === 'rowModal') loadRowLookups();
  });

  window.reloadReportRowLookups = loadRowLookups;
})();

(function () {
  if (window.location.pathname.toLowerCase() !== '/report/manual') return;

  function ready(fn) {
    if (document.readyState === 'loading') {
      document.addEventListener('DOMContentLoaded', fn);
    } else {
      fn();
    }
  }

  ready(function () {
    var id = document.getElementById('manualIdNumber');
    var code = document.getElementById('manualEmployeeCode');
    var first = document.getElementById('manualFirstName');
    var last = document.getElementById('manualLastName');
    var results = document.getElementById('manualEmployeeResults');
    var userId = document.getElementById('manualUserId');
    var alloc = document.getElementById('manualAllocationSelect');
    var button = document.getElementById('manualOpenButton');
    var empty = document.getElementById('manualNoAllocations');
    var timer = null;
    if (!id || !code || !first || !last || !results || !userId || !alloc || !button || !empty) return;

    function clearSelection() {
      userId.value = '';
      alloc.innerHTML = '';
      alloc.disabled = true;
      button.disabled = true;
      empty.style.display = 'none';
      if (window.refreshSubjectAutocomplete) window.refreshSubjectAutocomplete(alloc);
    }

    function textForEmployee(emp) {
      return [
        readJsonValue(emp, 'idNumber', 'IdNumber'),
        readJsonValue(emp, 'employeeCode', 'EmployeeCode'),
        readJsonValue(emp, 'firstName', 'FirstName'),
        readJsonValue(emp, 'lastName', 'LastName')
      ].filter(Boolean).join(' | ');
    }

    function readJsonValue(item, camelName, pascalName) {
      if (!item) return null;
      if (item[camelName] !== undefined && item[camelName] !== null) return item[camelName];
      return item[pascalName];
    }

    function render(data) {
      results.innerHTML = '';
      clearSelection();

      (data.employees || []).forEach(function (emp) {
        var item = document.createElement('button');
        item.type = 'button';
        item.className = 'list-group-item list-group-item-action';
        item.textContent = textForEmployee(emp);
        item.addEventListener('click', function () {
          var selectedEmployeeId = readJsonValue(emp, 'id', 'Id');
          userId.value = selectedEmployeeId;
          results.innerHTML = '';
          var selected = document.createElement('div');
          selected.className = 'list-group-item active';
          selected.textContent = item.textContent;
          results.appendChild(selected);

          alloc.innerHTML = '';
          (data.allocations || []).filter(function (allocation) {
            return String(readJsonValue(allocation, 'userId', 'UserId')) === String(selectedEmployeeId);
          }).forEach(function (allocation) {
            var allocationId = readJsonValue(allocation, 'id', 'Id');
            var opt = document.createElement('option');
            opt.value = allocationId;
            opt.textContent = readJsonValue(allocation, 'projectName', 'ProjectName') || ('Allocation ' + allocationId);
            alloc.appendChild(opt);
          });
          var hasAllocations = alloc.options.length > 0;
          alloc.disabled = !hasAllocations;
          button.disabled = !hasAllocations;
          if (window.refreshSubjectAutocomplete) window.refreshSubjectAutocomplete(alloc);
          empty.style.display = hasAllocations ? 'none' : 'block';
        });
        results.appendChild(item);
      });

      if ((data.employees || []).length === 0) {
        results.innerHTML = '<div class="list-group-item text-muted">לא נמצאו עובדים</div>';
      }
    }

    function search() {
      var params = new URLSearchParams({
        idNumber: id.value,
        employeeCode: code.value,
        firstName: first.value,
        lastName: last.value
      });
      fetch('/Report/ManualEmployeeSearch?' + params.toString(), { headers: { Accept: 'application/json' } })
        .then(function (resp) { return resp.ok ? resp.json() : null; })
        .then(function (data) { if (data) render(data); });
    }

    function schedule() {
      clearTimeout(timer);
      timer = setTimeout(search, 250);
    }

    document.querySelectorAll('.manual-employee-filter').forEach(function (el) {
      el.addEventListener('input', schedule);
    });
    search();
  });
})();

(function () {
  if (window.location.pathname.toLowerCase() !== '/report') return;

  function relabelFramework(root) {
    var scope = root || document;
    var elements = [];
    if (scope.matches && (scope.matches('label') || scope.matches('th'))) {
      elements.push(scope);
    }
    scope.querySelectorAll?.('label, th').forEach(function (element) {
      elements.push(element);
    });
    elements.forEach(function (element) {
      if ((element.textContent || '').trim() === 'מסגרת') {
        element.textContent = 'מסגרת חינוכית';
      }
    });
  }

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', function () { relabelFramework(document); });
  } else {
    relabelFramework(document);
  }

  if (window.MutationObserver) {
    new MutationObserver(function (mutations) {
      mutations.forEach(function (mutation) {
        mutation.addedNodes.forEach(function (node) {
          if (node.nodeType === 1) relabelFramework(node);
        });
      });
    }).observe(document.documentElement, { childList: true, subtree: true });
  }
})();

(function () {
  var scopedMap = {
    FrameworkId: 'frameworks',
    DomainId: 'domains',
    Subject1Id: 'subjects',
    Subject2Id: 'subjects',
    DiscussionCodeId: 'discussionCodes'
  };

  function getAllocationId() {
    var input = document.querySelector('input[name="allocationId"]');
    if (input && input.value) return input.value;
    return new URLSearchParams(window.location.search).get('allocationId');
  }

  function optionText(item) {
    return item.description || item.text || item.name || '';
  }

  function rebuildSelect(select, items) {
    if (!select) return;
    var oldValue = select.value;
    var first = select.options[0];
    var placeholder = first && first.value === '' ? first.cloneNode(true) : null;
    select.innerHTML = '';
    if (placeholder) select.appendChild(placeholder);
    (items || []).forEach(function (item) {
      var opt = document.createElement('option');
      opt.value = String(item.id);
      opt.textContent = optionText(item);
      if (String(item.id) === String(oldValue)) opt.selected = true;
      select.appendChild(opt);
    });
    if (window.refreshSubjectAutocomplete) window.refreshSubjectAutocomplete(select);
  }

  function applyScopedLists(container, data) {
    Object.keys(scopedMap).forEach(function (field) {
      var selector = container.id === 'rowForm'
        ? '#field' + (field === 'Subject1Id' ? 'Subject1' : field === 'Subject2Id' ? 'Subject2' : field === 'DiscussionCodeId' ? 'Discussion' : field.replace('Id', ''))
        : '[data-name="row.' + field + '"]';
      rebuildSelect(container.querySelector(selector), data[scopedMap[field]]);
    });
  }

  function reloadReportScope(container, selectedValue) {
    var query = new URLSearchParams(window.location.search);
    if ((query.get('manual') || '').toLowerCase() === 'true') return;

    var allocationId = getAllocationId();
    if (!allocationId || !selectedValue) return;

    fetch('/Report/ScopedForProgram?allocationId=' + encodeURIComponent(allocationId) + '&programId=' + encodeURIComponent(selectedValue), {
      headers: { 'Accept': 'application/json' },
      credentials: 'same-origin'
    })
      .then(function (res) { return res.ok ? res.json() : null; })
      .then(function (data) {
        if (data) applyScopedLists(container, data);
      })
      .catch(function () { /* keep current row values on failure */ });
  }

  document.addEventListener('change', function (event) {
    var target = event.target;
    if (!target) return;

    if (target.id === 'fieldEduProgram') {
      var rowForm = document.getElementById('rowForm');
      if (rowForm) reloadReportScope(rowForm, target.value);
      return;
    }

    if (target.matches && target.matches('[data-name="row.EducationalProgramId"]')) {
      var detail = target.closest('tr[data-detail-for]');
      if (detail) reloadReportScope(detail, target.value);
    }
  });
})();

(function () {
  function isFrameworkSelect(select) {
    if (!select || select.tagName !== 'SELECT') return false;
    var name = (select.getAttribute('name') || select.id || '').toLowerCase();
    return name.indexOf('framework') >= 0;
  }

  function relabelFrameworkSelects(root) {
    var selects = Array.prototype.slice.call((root || document).querySelectorAll('select')).filter(isFrameworkSelect);
    var ids = [];
    selects.forEach(function (select) {
      Array.prototype.forEach.call(select.options, function (option) {
        if (option.value && ids.indexOf(option.value) < 0) ids.push(option.value);
      });
    });
    if (!ids.length) return;

    fetch('/Report/FrameworkLabels?ids=' + encodeURIComponent(ids.join(',')), {
      headers: { 'Accept': 'application/json' },
      credentials: 'same-origin'
    })
      .then(function (res) { return res.ok ? res.json() : []; })
      .then(function (items) {
        var labels = {};
        (items || []).forEach(function (item) { labels[String(item.id)] = item.text; });
        selects.forEach(function (select) {
          Array.prototype.forEach.call(select.options, function (option) {
            if (labels[option.value]) option.textContent = labels[option.value];
          });
        });
      })
      .catch(function () { /* keep original labels */ });
  }

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', function () { relabelFrameworkSelects(document); });
  } else {
    relabelFrameworkSelects(document);
  }

  if (window.MutationObserver) {
    new MutationObserver(function (mutations) {
      mutations.forEach(function (mutation) {
        mutation.addedNodes.forEach(function (node) {
          if (node.nodeType === 1) relabelFrameworkSelects(node);
        });
      });
    }).observe(document.documentElement, { childList: true, subtree: true });
  }
})();

(function () {
  var path = window.location.pathname.toLowerCase();
  var query = new URLSearchParams(window.location.search);
  var forced = (query.get('forced') || '').toLowerCase();
  if (path !== '/account/changepassword' || forced !== 'true') return;

  document.querySelectorAll('.app-main-nav, .app-user-nav .dropdown').forEach(function (element) {
    element.classList.add('d-none');
  });

  document.querySelectorAll('.app-navbar-brand').forEach(function (element) {
    element.removeAttribute('href');
    element.setAttribute('aria-disabled', 'true');
    element.style.pointerEvents = 'none';
  });
})();

(function () {
  if (window.location.pathname.toLowerCase() !== '/dashboard/summary') return;
  document.querySelectorAll('table thead tr').forEach(function (row) {
    if (Array.prototype.some.call(row.children, function (cell) { return cell.textContent.indexOf('מסמכים') >= 0; })) return;
    var th = document.createElement('th');
    th.textContent = 'מסמכים';
    if (row.children.length > 9) {
      row.insertBefore(th, row.lastElementChild);
    } else {
      row.appendChild(th);
    }
  });
})();

(function () {
  if (window.location.pathname.toLowerCase() !== '/report') return;
  var upload = document.getElementById('report-file-upload');
  var card = upload ? upload.closest('.card') : null;
  if (!card) {
    var heading = Array.prototype.find.call(document.querySelectorAll('.card h5'), function (h) {
      return h.textContent.indexOf('מסמכי') >= 0 || h.textContent.indexOf('מסמכים') >= 0;
    });
    card = heading ? heading.closest('.card') : null;
  }
  if (card) card.id = 'documents';
})();

(function () {
  if (window.location.pathname.toLowerCase() !== '/employee') return;

  var actions = [
    { className: 'btn-outline-warning', action: 'ResetPassword', confirmText: 'לאפס סיסמה למספר הזהות?' },
    { className: 'btn-outline-success', action: 'UnlockAccount', confirmText: 'לשחרר את נעילת החשבון?' },
    { className: 'btn-outline-danger', action: 'DeleteEmployee', confirmText: 'להשבית עובד זה?' }
  ];

  document.addEventListener('click', function (event) {
    var button = event.target.closest('button[type="submit"]');
    if (!button) return;

    var row = button.closest('tr');
    if (!row) return;

    var matched = actions.find(function (item) {
      return button.classList.contains(item.className);
    });
    if (!matched) return;

    var idInput = row.querySelector('input.row-check[name="selectedIds"]');
    var tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
    if (!idInput || !idInput.value || !tokenInput || !tokenInput.value) return;

    event.preventDefault();
    event.stopPropagation();

    if (!window.confirm(matched.confirmText)) return;

    var form = document.createElement('form');
    form.method = 'post';
    form.action = '/Employee/' + matched.action + '/' + encodeURIComponent(idInput.value) + window.location.search;
    form.style.display = 'none';

    var token = document.createElement('input');
    token.type = 'hidden';
    token.name = '__RequestVerificationToken';
    token.value = tokenInput.value;
    form.appendChild(token);

    document.body.appendChild(form);
    form.submit();
  }, true);
})();
