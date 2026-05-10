#!/usr/bin/env python3
"""
extract_logo_colors.py
======================
Sample the dominant brand colours from the Axioma "Site & Sound חינוך" logo
and emit (a) the top three saturated hex codes to stdout, and (b) a 600x150
PNG palette swatch to scripts/logo-palette.png.

Filters out near-white (any channel > 240) and near-black (max channel < 30)
so we capture only meaningful brand pigments. Ranks remaining colours by
saturation*coverage, returns top 3.

Run:
    python scripts/extract_logo_colors.py

Dependencies:
    pip install Pillow
"""
from __future__ import annotations

import colorsys
import os
import sys
from collections import Counter

try:
    from PIL import Image, ImageDraw, ImageFont
except ImportError:
    print("ERROR: Pillow is required. Install with: pip install Pillow", file=sys.stderr)
    sys.exit(1)


HERE = os.path.dirname(os.path.abspath(__file__))
REPO_ROOT = os.path.dirname(HERE)
LOGO_PATH = os.path.join(REPO_ROOT, "src", "AxiomaReporting.Web", "wwwroot", "images", "logo.png")
PALETTE_OUT = os.path.join(HERE, "logo-palette.png")


def is_meaningful(rgb: tuple[int, int, int]) -> bool:
    """Drop near-white, near-black, and very low-saturation pixels."""
    r, g, b = rgb
    if r > 240 and g > 240 and b > 240:  # white-ish background
        return False
    if r < 30 and g < 30 and b < 30:     # near-black noise
        return False
    h, l, s = colorsys.rgb_to_hls(r / 255.0, g / 255.0, b / 255.0)
    if s < 0.15:                          # too grey to be brand
        return False
    return True


def quantize_image(img: Image.Image, n_colors: int = 16) -> list[tuple[int, tuple[int, int, int]]]:
    """Reduce to n_colors via Pillow quantize, return [(count, rgb), ...]."""
    rgba = img.convert("RGBA")
    # Composite onto white so transparent pixels become white (and get filtered).
    bg = Image.new("RGB", rgba.size, (255, 255, 255))
    bg.paste(rgba, mask=rgba.split()[3])
    quant = bg.quantize(colors=n_colors, method=Image.Quantize.MEDIANCUT)
    palette = quant.getpalette()  # flat list [r,g,b,r,g,b,...]
    counts = Counter(quant.getdata())
    out: list[tuple[int, tuple[int, int, int]]] = []
    for idx, count in counts.items():
        rgb = (palette[idx * 3], palette[idx * 3 + 1], palette[idx * 3 + 2])
        out.append((count, rgb))
    return out


def saturation(rgb: tuple[int, int, int]) -> float:
    r, g, b = rgb
    _, _, s = colorsys.rgb_to_hls(r / 255.0, g / 255.0, b / 255.0)
    return s


def hex_of(rgb: tuple[int, int, int]) -> str:
    return "#{:02X}{:02X}{:02X}".format(*rgb)


def pick_top_three(palette: list[tuple[int, tuple[int, int, int]]]) -> list[tuple[int, int, int]]:
    """Filter, then rank by saturation*sqrt(count). Take top 3 with hue spread."""
    filtered = [(cnt, rgb) for cnt, rgb in palette if is_meaningful(rgb)]
    if not filtered:
        return []
    # Score: saturation × log(count). Higher is better.
    import math
    scored = sorted(
        filtered,
        key=lambda x: saturation(x[1]) * math.log(x[0] + 2),
        reverse=True,
    )
    # De-duplicate by hue bucket (avoid 3 near-identical oranges).
    picked: list[tuple[int, int, int]] = []
    used_hues: list[float] = []
    for _, rgb in scored:
        h, _, _ = colorsys.rgb_to_hls(rgb[0] / 255.0, rgb[1] / 255.0, rgb[2] / 255.0)
        if any(abs(h - u) < 0.08 for u in used_hues):
            continue
        picked.append(rgb)
        used_hues.append(h)
        if len(picked) == 3:
            break
    # Fall-back: if hue de-dup left us short, top up from scored list.
    for _, rgb in scored:
        if len(picked) >= 3:
            break
        if rgb not in picked:
            picked.append(rgb)
    return picked[:3]


def render_palette(colors: list[tuple[int, int, int]], path: str) -> None:
    width, height = 600, 150
    swatch_w = width // max(len(colors), 1)
    img = Image.new("RGB", (width, height), (245, 245, 245))
    draw = ImageDraw.Draw(img)
    try:
        font = ImageFont.truetype("arial.ttf", 22)
    except IOError:
        font = ImageFont.load_default()
    for i, rgb in enumerate(colors):
        x0 = i * swatch_w
        x1 = x0 + swatch_w
        draw.rectangle([x0, 0, x1, height], fill=rgb)
        # Pick label colour for contrast.
        r, g, b = rgb
        luminance = 0.299 * r + 0.587 * g + 0.114 * b
        label_fill = (255, 255, 255) if luminance < 140 else (20, 20, 20)
        label = hex_of(rgb)
        bbox = draw.textbbox((0, 0), label, font=font)
        tw, th = bbox[2] - bbox[0], bbox[3] - bbox[1]
        tx = x0 + (swatch_w - tw) // 2
        ty = (height - th) // 2
        draw.text((tx, ty), label, fill=label_fill, font=font)
    img.save(path, "PNG")


def main() -> int:
    if not os.path.exists(LOGO_PATH):
        print(f"ERROR: logo not found at {LOGO_PATH}", file=sys.stderr)
        return 1
    with Image.open(LOGO_PATH) as src:
        palette = quantize_image(src, n_colors=24)
    top = pick_top_three(palette)
    if not top:
        print("ERROR: no meaningful colours found in logo", file=sys.stderr)
        return 2
    print("Top 3 dominant brand colours (most → least saturated):")
    for rgb in top:
        print(f"  {hex_of(rgb)}    rgb{rgb}")
    render_palette(top, PALETTE_OUT)
    print(f"\nPalette swatch saved → {PALETTE_OUT}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
