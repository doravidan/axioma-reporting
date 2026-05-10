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
