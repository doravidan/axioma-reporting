# Choices.js (vendored)

The shared `_Layout.cshtml` references:
- `~/lib/choices.js/choices.min.css`
- `~/lib/choices.js/choices.min.js`

If those files are not yet present in this directory, drop them in from the
official CDN before deploying. The layout's inline init IIFE is defensive —
it checks for `window.Choices` before calling it — so the page will still
render if the assets are missing, but `<select multiple>` controls will
fall back to the native browser widget.

## Source

Choices.js v10.2.0 (MIT licensed):
- https://cdn.jsdelivr.net/npm/choices.js@10.2.0/public/assets/scripts/choices.min.js
- https://cdn.jsdelivr.net/npm/choices.js@10.2.0/public/assets/styles/choices.min.css

## Quick install (PowerShell, from repo root)

```powershell
$dir = "src\AxiomaReporting.Web\wwwroot\lib\choices.js"
New-Item -ItemType Directory -Force -Path $dir | Out-Null
Invoke-WebRequest "https://cdn.jsdelivr.net/npm/choices.js@10.2.0/public/assets/scripts/choices.min.js" -OutFile "$dir\choices.min.js"
Invoke-WebRequest "https://cdn.jsdelivr.net/npm/choices.js@10.2.0/public/assets/styles/choices.min.css" -OutFile "$dir\choices.min.css"
```

## Quick install (bash + curl)

```bash
mkdir -p src/AxiomaReporting.Web/wwwroot/lib/choices.js
curl -L -o src/AxiomaReporting.Web/wwwroot/lib/choices.js/choices.min.js \
  https://cdn.jsdelivr.net/npm/choices.js@10.2.0/public/assets/scripts/choices.min.js
curl -L -o src/AxiomaReporting.Web/wwwroot/lib/choices.js/choices.min.css \
  https://cdn.jsdelivr.net/npm/choices.js@10.2.0/public/assets/styles/choices.min.css
```

## Init

The layout runs this IIFE at the bottom of every page so any `<select multiple>`
gets enriched with chips, search, and `removeItemButton`:

```html
<script>
  (function () {
    if (typeof window.Choices === 'undefined') return;
    document.querySelectorAll('select[multiple]').forEach(function (el) {
      if (!el.dataset.choicesInit) {
        new Choices(el, {
          removeItemButton: true,
          searchPlaceholderValue: 'חיפוש…',
          noResultsText: 'לא נמצאו תוצאות',
          shouldSort: false
        });
        el.dataset.choicesInit = '1';
      }
    });
  })();
</script>
```

RTL CSS overrides for the Choices container live in `wwwroot/css/theme.css`.
