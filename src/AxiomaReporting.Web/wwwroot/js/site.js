// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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

// ── הצגת סיסמה (עין) — משוב לקוח: "להוסיף אפשרות לראות את הסיסמא שהקלדתי" ──
// עוטף אוטומטית כל שדה סיסמה בכפתור הצגה/הסתרה, בכל מסכי המערכת.
(function () {
  function attachToggle(input) {
    if (input.dataset.pwToggle) return;
    input.dataset.pwToggle = '1';

    var wrapper = document.createElement('div');
    wrapper.className = 'position-relative';
    input.parentNode.insertBefore(wrapper, input);
    wrapper.appendChild(input);

    var btn = document.createElement('button');
    btn.type = 'button';
    btn.className = 'btn btn-link p-0 position-absolute top-50 translate-middle-y';
    btn.style.left = '0.6rem';
    btn.style.lineHeight = '1';
    btn.style.textDecoration = 'none';
    btn.setAttribute('aria-label', 'הצג סיסמה');
    btn.setAttribute('title', 'הצג/הסתר סיסמה');
    btn.textContent = '👁';
    btn.addEventListener('click', function () {
      var show = input.type === 'password';
      input.type = show ? 'text' : 'password';
      btn.setAttribute('aria-label', show ? 'הסתר סיסמה' : 'הצג סיסמה');
      btn.style.opacity = show ? '1' : '0.55';
    });
    btn.style.opacity = '0.55';
    // מרווח פנימי כדי שהטקסט לא יוסתר ע"י הכפתור (RTL: הכפתור בצד שמאל)
    input.style.paddingLeft = '2.2rem';
    wrapper.appendChild(btn);
  }

  function init() {
    document.querySelectorAll('input[type="password"]').forEach(attachToggle);
  }
  if (document.readyState === 'loading') document.addEventListener('DOMContentLoaded', init);
  else init();
})();
