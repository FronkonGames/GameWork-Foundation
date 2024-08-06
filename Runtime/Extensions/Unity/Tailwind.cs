////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Martin Bustos @FronkonGames <fronkongames@gmail.com>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of
// the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// An expertly-crafted color palette out-of-the-box that is a great starting point if you don’t have your
  /// own specific branding in mind. Based on https://tailwindcss.com/docs/customizing-colors
  /// 50 is light, 950 is dark.
  /// </summary>
  public static class Tailwind
  {
    public static Color Slate_50 = FromHTML("#f8fafc");
    public static Color Slate_100 = FromHTML("#f1f5f9");
    public static Color Slate_200 = FromHTML("#e2e8f0");
    public static Color Slate_300 = FromHTML("#cbd5e1");
    public static Color Slate_400 = FromHTML("#94a3b8");
    public static Color Slate_500 = FromHTML("#64748b");
    public static Color Slate_600 = FromHTML("#475569");
    public static Color Slate_700 = FromHTML("#334155");
    public static Color Slate_800 = FromHTML("#1e293b");
    public static Color Slate_900 = FromHTML("#0f172a");
    public static Color Slate_950 = FromHTML("#020617");

    public static Color Gray_50 = FromHTML("#f9fafb");
    public static Color Gray_100 = FromHTML("#f3f4f6");
    public static Color Gray_200 = FromHTML("#e5e7eb");
    public static Color Gray_300 = FromHTML("#d1d5db");
    public static Color Gray_400 = FromHTML("#9ca3af");
    public static Color Gray_500 = FromHTML("#6b7280");
    public static Color Gray_600 = FromHTML("#4b5563");
    public static Color Gray_700 = FromHTML("#374151");
    public static Color Gray_800 = FromHTML("#1f2937");
    public static Color Gray_900 = FromHTML("#111827");
    public static Color Gray_950 = FromHTML("#030712");

    public static Color Zinc_50 = FromHTML("#fafafa");
    public static Color Zinc_100 = FromHTML("#f4f4f5");
    public static Color Zinc_200 = FromHTML("#e4e4e7");
    public static Color Zinc_300 = FromHTML("#d4d4d8");
    public static Color Zinc_400 = FromHTML("#a1a1aa");
    public static Color Zinc_500 = FromHTML("#71717a");
    public static Color Zinc_600 = FromHTML("#52525b");
    public static Color Zinc_700 = FromHTML("#3f3f46");
    public static Color Zinc_800 = FromHTML("#27272a");
    public static Color Zinc_900 = FromHTML("#18181b");
    public static Color Zinc_950 = FromHTML("#09090b");

    public static Color Neutral_50 = FromHTML("#fafafa");
    public static Color Neutral_100 = FromHTML("#f5f5f5");
    public static Color Neutral_200 = FromHTML("#e5e5e5");
    public static Color Neutral_300 = FromHTML("#d4d4d4");
    public static Color Neutral_400 = FromHTML("#a3a3a3");
    public static Color Neutral_500 = FromHTML("#737373");
    public static Color Neutral_600 = FromHTML("#525252");
    public static Color Neutral_700 = FromHTML("#404040");
    public static Color Neutral_800 = FromHTML("#262626");
    public static Color Neutral_900 = FromHTML("#171717");
    public static Color Neutral_950 = FromHTML("#0a0a0a");

    public static Color Stone_50 = FromHTML("#fafaf9");
    public static Color Stone_100 = FromHTML("#f5f5f4");
    public static Color Stone_200 = FromHTML("#e7e5e4");
    public static Color Stone_300 = FromHTML("#d6d3d1");
    public static Color Stone_400 = FromHTML("#a8a29e");
    public static Color Stone_500 = FromHTML("#78716c");
    public static Color Stone_600 = FromHTML("#57534e");
    public static Color Stone_700 = FromHTML("#44403c");
    public static Color Stone_800 = FromHTML("#292524");
    public static Color Stone_900 = FromHTML("#1c1917");
    public static Color Stone_950 = FromHTML("#0c0a09");

    public static Color Red_50 = FromHTML("#fef2f2");
    public static Color Red_100 = FromHTML("#fee2e2");
    public static Color Red_200 = FromHTML("#fecaca");
    public static Color Red_300 = FromHTML("#fca5a5");
    public static Color Red_400 = FromHTML("#f87171");
    public static Color Red_500 = FromHTML("#ef4444");
    public static Color Red_600 = FromHTML("#dc2626");
    public static Color Red_700 = FromHTML("#b91c1c");
    public static Color Red_800 = FromHTML("#991b1b");
    public static Color Red_900 = FromHTML("#7f1d1d");
    public static Color Red_950 = FromHTML("#450a0a");

    public static Color Orange_50 = FromHTML("#fff7ed");
    public static Color Orange_100 = FromHTML("#ffedd5");
    public static Color Orange_200 = FromHTML("#fed7aa");
    public static Color Orange_300 = FromHTML("#fdba74");
    public static Color Orange_400 = FromHTML("#fb923c");
    public static Color Orange_500 = FromHTML("#f97316");
    public static Color Orange_600 = FromHTML("#ea580c");
    public static Color Orange_700 = FromHTML("#c2410c");
    public static Color Orange_800 = FromHTML("#9a3412");
    public static Color Orange_900 = FromHTML("#7c2d12");
    public static Color Orange_950 = FromHTML("#431407");

    public static Color Amber_50 = FromHTML("#fffbeb");
    public static Color Amber_100 = FromHTML("#fef3c7");
    public static Color Amber_200 = FromHTML("#fde68a");
    public static Color Amber_300 = FromHTML("#fcd34d");
    public static Color Amber_400 = FromHTML("#fbbf24");
    public static Color Amber_500 = FromHTML("#f59e0b");
    public static Color Amber_600 = FromHTML("#d97706");
    public static Color Amber_700 = FromHTML("#b45309");
    public static Color Amber_800 = FromHTML("#92400e");
    public static Color Amber_900 = FromHTML("#78350f");
    public static Color Amber_950 = FromHTML("#451a03");

    public static Color Yellow_50 = FromHTML("#fefce8");
    public static Color Yellow_100 = FromHTML("#fef9c3");
    public static Color Yellow_200 = FromHTML("#fef08a");
    public static Color Yellow_300 = FromHTML("#fde047");
    public static Color Yellow_400 = FromHTML("#facc15");
    public static Color Yellow_500 = FromHTML("#eab308");
    public static Color Yellow_600 = FromHTML("#ca8a04");
    public static Color Yellow_700 = FromHTML("#a16207");
    public static Color Yellow_800 = FromHTML("#854d0e");
    public static Color Yellow_900 = FromHTML("#713f12");
    public static Color Yellow_950 = FromHTML("#422006");

    public static Color Lime_50 = FromHTML("#f7fee7");
    public static Color Lime_100 = FromHTML("#ecfccb");
    public static Color Lime_200 = FromHTML("#d9f99d");
    public static Color Lime_300 = FromHTML("#bef264");
    public static Color Lime_400 = FromHTML("#a3e635");
    public static Color Lime_500 = FromHTML("#84cc16");
    public static Color Lime_600 = FromHTML("#65a30d");
    public static Color Lime_700 = FromHTML("#4d7c0f");
    public static Color Lime_800 = FromHTML("#3f6212");
    public static Color Lime_900 = FromHTML("#365314");
    public static Color Lime_950 = FromHTML("#1a2e05");

    public static Color Green_50 = FromHTML("#f0fdf4");
    public static Color Green_100 = FromHTML("#dcfce7");
    public static Color Green_200 = FromHTML("#bbf7d0");
    public static Color Green_300 = FromHTML("#86efac");
    public static Color Green_400 = FromHTML("#4ade80");
    public static Color Green_500 = FromHTML("#22c55e");
    public static Color Green_600 = FromHTML("#16a34a");
    public static Color Green_700 = FromHTML("#15803d");
    public static Color Green_800 = FromHTML("#166534");
    public static Color Green_900 = FromHTML("#14532d");
    public static Color Green_950 = FromHTML("#052e16");

    public static Color Emerald_50 = FromHTML("#ecfdf5");
    public static Color Emerald_100 = FromHTML("#d1fae5");
    public static Color Emerald_200 = FromHTML("#a7f3d0");
    public static Color Emerald_300 = FromHTML("#6ee7b7");
    public static Color Emerald_400 = FromHTML("#34d399");
    public static Color Emerald_500 = FromHTML("#10b981");
    public static Color Emerald_600 = FromHTML("#059669");
    public static Color Emerald_700 = FromHTML("#047857");
    public static Color Emerald_800 = FromHTML("#065f46");
    public static Color Emerald_900 = FromHTML("#064e3b");
    public static Color Emerald_950 = FromHTML("#022c22");

    public static Color Teal_50 = FromHTML("#f0fdfa");
    public static Color Teal_100 = FromHTML("#ccfbf1");
    public static Color Teal_200 = FromHTML("#99f6e4");
    public static Color Teal_300 = FromHTML("#5eead4");
    public static Color Teal_400 = FromHTML("#2dd4bf");
    public static Color Teal_500 = FromHTML("#14b8a6");
    public static Color Teal_600 = FromHTML("#0d9488");
    public static Color Teal_700 = FromHTML("#0f766e");
    public static Color Teal_800 = FromHTML("#115e59");
    public static Color Teal_900 = FromHTML("#134e4a");
    public static Color Teal_950 = FromHTML("#042f2e");

    public static Color Cyan_50 = FromHTML("#ecfeff");
    public static Color Cyan_100 = FromHTML("#cffafe");
    public static Color Cyan_200 = FromHTML("#a5f3fc");
    public static Color Cyan_300 = FromHTML("#67e8f9");
    public static Color Cyan_400 = FromHTML("#22d3ee");
    public static Color Cyan_500 = FromHTML("#06b6d4");
    public static Color Cyan_600 = FromHTML("#0891b2");
    public static Color Cyan_700 = FromHTML("#0e7490");
    public static Color Cyan_800 = FromHTML("#155e75");
    public static Color Cyan_900 = FromHTML("#164e63");
    public static Color Cyan_950 = FromHTML("#083344");

    public static Color Sky_50 = FromHTML("#f0f9ff");
    public static Color Sky_100 = FromHTML("#e0f2fe");
    public static Color Sky_200 = FromHTML("#bae6fd");
    public static Color Sky_300 = FromHTML("#7dd3fc");
    public static Color Sky_400 = FromHTML("#38bdf8");
    public static Color Sky_500 = FromHTML("#0ea5e9");
    public static Color Sky_600 = FromHTML("#0284c7");
    public static Color Sky_700 = FromHTML("#0369a1");
    public static Color Sky_800 = FromHTML("#075985");
    public static Color Sky_900 = FromHTML("#0c4a6e");
    public static Color Sky_950 = FromHTML("#082f49");

    public static Color Blue_50 = FromHTML("#eff6ff");
    public static Color Blue_100 = FromHTML("#dbeafe");
    public static Color Blue_200 = FromHTML("#bfdbfe");
    public static Color Blue_300 = FromHTML("#93c5fd");
    public static Color Blue_400 = FromHTML("#60a5fa");
    public static Color Blue_500 = FromHTML("#3b82f6");
    public static Color Blue_600 = FromHTML("#2563eb");
    public static Color Blue_700 = FromHTML("#1d4ed8");
    public static Color Blue_800 = FromHTML("#1e40af");
    public static Color Blue_900 = FromHTML("#1e3a8a");
    public static Color Blue_950 = FromHTML("#172554");

    public static Color Indigo_50 = FromHTML("#eef2ff");
    public static Color Indigo_100 = FromHTML("#e0e7ff");
    public static Color Indigo_200 = FromHTML("#c7d2fe");
    public static Color Indigo_300 = FromHTML("#a5b4fc");
    public static Color Indigo_400 = FromHTML("#818cf8");
    public static Color Indigo_500 = FromHTML("#6366f1");
    public static Color Indigo_600 = FromHTML("#4f46e5");
    public static Color Indigo_700 = FromHTML("#4338ca");
    public static Color Indigo_800 = FromHTML("#3730a3");
    public static Color Indigo_900 = FromHTML("#312e81");
    public static Color Indigo_950 = FromHTML("#1e1b4b");

    public static Color Violet_50 = FromHTML("#f5f3ff");
    public static Color Violet_100 = FromHTML("#ede9fe");
    public static Color Violet_200 = FromHTML("#ddd6fe");
    public static Color Violet_300 = FromHTML("#c4b5fd");
    public static Color Violet_400 = FromHTML("#a78bfa");
    public static Color Violet_500 = FromHTML("#8b5cf6");
    public static Color Violet_600 = FromHTML("#7c3aed");
    public static Color Violet_700 = FromHTML("#6d28d9");
    public static Color Violet_800 = FromHTML("#5b21b6");
    public static Color Violet_900 = FromHTML("#4c1d95");
    public static Color Violet_950 = FromHTML("#2e1065");

    public static Color Purple_50 = FromHTML("#faf5ff");
    public static Color Purple_100 = FromHTML("#f3e8ff");
    public static Color Purple_200 = FromHTML("#e9d5ff");
    public static Color Purple_300 = FromHTML("#d8b4fe");
    public static Color Purple_400 = FromHTML("#c084fc");
    public static Color Purple_500 = FromHTML("#a855f7");
    public static Color Purple_600 = FromHTML("#9333ea");
    public static Color Purple_700 = FromHTML("#7e22ce");
    public static Color Purple_800 = FromHTML("#6b21a8");
    public static Color Purple_900 = FromHTML("#581c87");
    public static Color Purple_950 = FromHTML("#3b0764");

    public static Color Fuchsia_50 = FromHTML("#fdf4ff");
    public static Color Fuchsia_100 = FromHTML("#fae8ff");
    public static Color Fuchsia_200 = FromHTML("#f5d0fe");
    public static Color Fuchsia_300 = FromHTML("#f0abfc");
    public static Color Fuchsia_400 = FromHTML("#e879f9");
    public static Color Fuchsia_500 = FromHTML("#d946ef");
    public static Color Fuchsia_600 = FromHTML("#c026d3");
    public static Color Fuchsia_700 = FromHTML("#a21caf");
    public static Color Fuchsia_800 = FromHTML("#86198f");
    public static Color Fuchsia_900 = FromHTML("#701a75");
    public static Color Fuchsia_950 = FromHTML("#4a044e");

    public static Color Pink_50 = FromHTML("#fdf2f8");
    public static Color Pink_100 = FromHTML("#fce7f3");
    public static Color Pink_200 = FromHTML("#fbcfe8");
    public static Color Pink_300 = FromHTML("#f9a8d4");
    public static Color Pink_400 = FromHTML("#f472b6");
    public static Color Pink_500 = FromHTML("#ec4899");
    public static Color Pink_600 = FromHTML("#db2777");
    public static Color Pink_700 = FromHTML("#be185d");
    public static Color Pink_800 = FromHTML("#9d174d");
    public static Color Pink_900 = FromHTML("#831843");
    public static Color Pink_950 = FromHTML("#500724");

    public static Color Rose_50 = FromHTML("#fff1f2");
    public static Color Rose_100 = FromHTML("#ffe4e6");
    public static Color Rose_200 = FromHTML("#fecdd3");
    public static Color Rose_300 = FromHTML("#fda4af");
    public static Color Rose_400 = FromHTML("#fb7185");
    public static Color Rose_500 = FromHTML("#f43f5e");
    public static Color Rose_600 = FromHTML("#e11d48");
    public static Color Rose_700 = FromHTML("#be123c");
    public static Color Rose_800 = FromHTML("#9f1239");
    public static Color Rose_900 = FromHTML("#881337");
    public static Color Rose_950 = FromHTML("#4c0519");

    private static Color FromHTML(string hex) => new(Convert.ToByte(hex[1..3], 16) / 255.0f,
                                                     Convert.ToByte(hex[3..5], 16) / 255.0f,
                                                     Convert.ToByte(hex[5..7], 16) / 255.0f);
  }
}