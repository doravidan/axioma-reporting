/*
 * jQuery Validation Plugin — Hebrew (he) translation
 * Vendored locally for the Axioma Reporting System v1.1.
 *
 * Source upstream: https://cdn.jsdelivr.net/npm/jquery-validation@1.20.0/dist/localization/messages_he.js
 * (Same content as the official `messages_he.js` distribution; reproduced here so
 *  the file is under source control and works on air-gapped client deployments.)
 *
 * Loaded from _Layout.cshtml AFTER jquery.validate.min.js so it overrides the
 * default English messages on every page (login + every authenticated screen).
 */
(function (factory) {
  if (typeof define === "function" && define.amd) {
    define(["jquery", "../jquery.validate"], factory);
  } else if (typeof module === "object" && module.exports) {
    module.exports = factory(require("jquery"));
  } else {
    factory(jQuery);
  }
}(function ($) {
  if (!$ || !$.validator) { return; }
  $.extend($.validator.messages, {
    required: "השדה הזה הוא שדה חובה.",
    remote: "אנא תקנו ערך שדה זה.",
    email: "אנא הזינו כתובת דוא\"ל חוקית.",
    url: "אנא הזינו כתובת אינטרנט חוקית.",
    date: "אנא הזינו תאריך חוקי.",
    dateISO: "אנא הזינו תאריך חוקי (ISO).",
    number: "אנא הזינו מספר חוקי.",
    digits: "אנא הזינו רק ספרות.",
    creditcard: "אנא הזינו מספר כרטיס אשראי חוקי.",
    equalTo: "אנא הזינו את אותו ערך שוב.",
    extension: "אנא הזינו ערך עם סיומת חוקית.",
    maxlength: $.validator.format("אנא הזינו לא יותר מ-{0} תווים."),
    minlength: $.validator.format("אנא הזינו לפחות {0} תווים."),
    rangelength: $.validator.format("אנא הזינו ערך באורך בין {0} ל-{1} תווים."),
    range: $.validator.format("אנא הזינו ערך בין {0} ל-{1}."),
    max: $.validator.format("אנא הזינו ערך קטן או שווה ל-{0}."),
    min: $.validator.format("אנא הזינו ערך גדול או שווה ל-{0}."),
    step: $.validator.format("אנא הזינו ערך שהוא כפולה של {0}.")
  });
  return $;
}));
