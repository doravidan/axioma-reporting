# Hebrew Fonts

This folder bundles Hebrew-capable TrueType fonts for PDF generation
(`PdfReportService`, based on QuestPDF) and, optionally, for static web use.

## Required file

| Filename | License | Source |
|----------|---------|--------|
| `NotoSansHebrew-Regular.ttf` | Apache License 2.0 | https://github.com/googlefonts/noto-fonts/raw/main/hinted/ttf/NotoSansHebrew/NotoSansHebrew-Regular.ttf |

### Alternatives (any of these will work — rename to the filename above or edit `PdfReportService`)

| Filename | License | Source |
|----------|---------|--------|
| `Alef-Regular.ttf` | SIL Open Font License 1.1 | https://fonts.google.com/specimen/Alef |
| `FrankRuehlCLM-Medium.ttf` | GPL/OFL | https://culmus.sourceforge.io/ |

## Re-install

Download the TTF file from the source above and place it in this folder:

```
src/AxiomaReporting.Web/wwwroot/fonts/NotoSansHebrew-Regular.ttf
```

The font is registered once at startup in `Program.cs` via
`QuestPDF.Infrastructure.FontManager.RegisterFont(...)`. If the file is
missing the service falls back to the default QuestPDF font and logs a
warning; Hebrew glyphs may not render correctly in that state.
