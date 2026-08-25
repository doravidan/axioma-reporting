(function (global) {
  'use strict';

  function normalizeValues(values) {
    if (values === null || values === undefined) return [];
    return (Array.isArray(values) ? values : [values])
      .map(value => String(value))
      .filter(value => value.length > 0);
  }

  function createCatalog(selectElement) {
    if (!selectElement || !selectElement.options) return [];

    const seen = new Set();
    return Array.from(selectElement.options).reduce((catalog, option) => {
      const value = String(option.value);
      if (seen.has(value)) return catalog;
      seen.add(value);
      catalog.push({
        value,
        label: option.label || option.textContent || value,
        disabled: Boolean(option.disabled),
        placeholder: value.length === 0
      });
      return catalog;
    }, []);
  }

  function selectedValues(selectElement, choicesInstance) {
    if (choicesInstance && typeof choicesInstance.getValue === 'function') {
      return normalizeValues(choicesInstance.getValue(true));
    }

    return selectElement && selectElement.selectedOptions
      ? Array.from(selectElement.selectedOptions).map(option => String(option.value))
      : [];
  }

  function batchAddSelections(selectElement, ids, choicesInstance, catalog) {
    if (!selectElement) return { added: 0, selected: 0, rebuilt: false };

    const source = Array.isArray(catalog) && catalog.length > 0
      ? catalog
      : createCatalog(selectElement);
    const available = new Set(source.map(choice => choice.value));
    const selected = new Set(selectedValues(selectElement, choicesInstance));
    const before = selected.size;

    normalizeValues(ids).forEach(value => {
      if (available.has(value)) selected.add(value);
    });

    const added = selected.size - before;
    if (added === 0) return { added: 0, selected: selected.size, rebuilt: false };

    if (choicesInstance &&
        typeof choicesInstance.clearStore === 'function' &&
        typeof choicesInstance.setChoices === 'function') {
      const choices = source.map(choice => ({
        value: choice.value,
        label: choice.label,
        disabled: choice.disabled,
        placeholder: choice.placeholder,
        selected: selected.has(choice.value)
      }));

      // clearStore + setChoices performs one bulk rebuild. Calling
      // setChoiceByValue for every id causes a complete Choices render per id.
      choicesInstance.clearStore();
      choicesInstance.setChoices(choices, 'value', 'label', false);
      return { added, selected: selected.size, rebuilt: true };
    }

    Array.from(selectElement.options || []).forEach(option => {
      if (selected.has(String(option.value))) option.selected = true;
    });
    return { added, selected: selected.size, rebuilt: false };
  }

  global.AxiomaAllocationScope = Object.freeze({
    createCatalog,
    batchAddSelections
  });
})(window);
