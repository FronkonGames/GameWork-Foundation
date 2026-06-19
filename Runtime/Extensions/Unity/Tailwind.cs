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
    public static readonly Color State_50  = new(0.973f, 0.980f, 0.988f);
    public static readonly Color State_100 = new(0.945f, 0.961f, 0.976f);
    public static readonly Color State_200 = new(0.886f, 0.910f, 0.941f);
    public static readonly Color State_300 = new(0.796f, 0.835f, 0.882f);
    public static readonly Color State_400 = new(0.580f, 0.639f, 0.722f);
    public static readonly Color State_500 = new(0.392f, 0.455f, 0.545f);
    public static readonly Color State_600 = new(0.278f, 0.333f, 0.412f);
    public static readonly Color State_700 = new(0.200f, 0.255f, 0.333f);
    public static readonly Color State_800 = new(0.118f, 0.161f, 0.231f);
    public static readonly Color State_900 = new(0.059f, 0.090f, 0.165f);
    public static readonly Color State_950 = new(0.008f, 0.024f, 0.090f);

    public static readonly Color Gray_50  = new(0.976f, 0.980f, 0.984f);
    public static readonly Color Gray_100 = new(0.953f, 0.957f, 0.965f);
    public static readonly Color Gray_200 = new(0.898f, 0.906f, 0.922f);
    public static readonly Color Gray_300 = new(0.820f, 0.835f, 0.859f);
    public static readonly Color Gray_400 = new(0.612f, 0.639f, 0.686f);
    public static readonly Color Gray_500 = new(0.420f, 0.447f, 0.502f);
    public static readonly Color Gray_600 = new(0.294f, 0.333f, 0.388f);
    public static readonly Color Gray_700 = new(0.216f, 0.255f, 0.318f);
    public static readonly Color Gray_800 = new(0.122f, 0.161f, 0.216f);
    public static readonly Color Gray_900 = new(0.067f, 0.094f, 0.153f);
    public static readonly Color Gray_950 = new(0.012f, 0.027f, 0.071f);

    public static readonly Color Zinc_50  = new(0.980f, 0.980f, 0.980f);
    public static readonly Color Zinc_100 = new(0.957f, 0.957f, 0.961f);
    public static readonly Color Zinc_200 = new(0.894f, 0.894f, 0.906f);
    public static readonly Color Zinc_300 = new(0.831f, 0.831f, 0.847f);
    public static readonly Color Zinc_400 = new(0.631f, 0.631f, 0.667f);
    public static readonly Color Zinc_500 = new(0.443f, 0.443f, 0.478f);
    public static readonly Color Zinc_600 = new(0.322f, 0.322f, 0.357f);
    public static readonly Color Zinc_700 = new(0.247f, 0.247f, 0.275f);
    public static readonly Color Zinc_800 = new(0.153f, 0.153f, 0.165f);
    public static readonly Color Zinc_900 = new(0.094f, 0.094f, 0.106f);
    public static readonly Color Zinc_950 = new(0.035f, 0.035f, 0.043f);

    public static readonly Color Neutral_50 =  new(0.980f, 0.980f, 0.980f);
    public static readonly Color Neutral_100 = new(0.961f, 0.961f, 0.961f);
    public static readonly Color Neutral_200 = new(0.898f, 0.898f, 0.898f);
    public static readonly Color Neutral_300 = new(0.831f, 0.831f, 0.831f);
    public static readonly Color Neutral_400 = new(0.639f, 0.639f, 0.639f);
    public static readonly Color Neutral_500 = new(0.451f, 0.451f, 0.451f);
    public static readonly Color Neutral_600 = new(0.322f, 0.322f, 0.322f);
    public static readonly Color Neutral_700 = new(0.251f, 0.251f, 0.251f);
    public static readonly Color Neutral_800 = new(0.149f, 0.149f, 0.149f);
    public static readonly Color Neutral_900 = new(0.090f, 0.090f, 0.090f);
    public static readonly Color Neutral_950 = new(0.039f, 0.039f, 0.039f);

    public static readonly Color Stone_50 =  new(0.980f, 0.980f, 0.976f);
    public static readonly Color Stone_100 = new(0.961f, 0.961f, 0.957f);
    public static readonly Color Stone_200 = new(0.906f, 0.898f, 0.894f);
    public static readonly Color Stone_300 = new(0.839f, 0.827f, 0.820f);
    public static readonly Color Stone_400 = new(0.659f, 0.635f, 0.620f);
    public static readonly Color Stone_500 = new(0.471f, 0.443f, 0.424f);
    public static readonly Color Stone_600 = new(0.341f, 0.325f, 0.306f);
    public static readonly Color Stone_700 = new(0.267f, 0.251f, 0.235f);
    public static readonly Color Stone_800 = new(0.161f, 0.145f, 0.141f);
    public static readonly Color Stone_900 = new(0.110f, 0.098f, 0.090f);
    public static readonly Color Stone_950 = new(0.047f, 0.039f, 0.035f);

    public static readonly Color Red_50 =  new(0.996f, 0.949f, 0.949f);
    public static readonly Color Red_100 = new(0.996f, 0.886f, 0.886f);
    public static readonly Color Red_200 = new(0.996f, 0.792f, 0.792f);
    public static readonly Color Red_300 = new(0.988f, 0.647f, 0.647f);
    public static readonly Color Red_400 = new(0.973f, 0.443f, 0.443f);
    public static readonly Color Red_500 = new(0.937f, 0.267f, 0.267f);
    public static readonly Color Red_600 = new(0.863f, 0.149f, 0.149f);
    public static readonly Color Red_700 = new(0.725f, 0.110f, 0.110f);
    public static readonly Color Red_800 = new(0.600f, 0.106f, 0.106f);
    public static readonly Color Red_900 = new(0.498f, 0.114f, 0.114f);
    public static readonly Color Red_950 = new(0.271f, 0.039f, 0.039f);

    public static readonly Color Orange_50 =  new(1.000f, 0.969f, 0.929f);
    public static readonly Color Orange_100 = new(1.000f, 0.929f, 0.835f);
    public static readonly Color Orange_200 = new(0.996f, 0.843f, 0.667f);
    public static readonly Color Orange_300 = new(0.992f, 0.729f, 0.455f);
    public static readonly Color Orange_400 = new(0.984f, 0.573f, 0.235f);
    public static readonly Color Orange_500 = new(0.976f, 0.451f, 0.086f);
    public static readonly Color Orange_600 = new(0.918f, 0.345f, 0.047f);
    public static readonly Color Orange_700 = new(0.761f, 0.255f, 0.047f);
    public static readonly Color Orange_800 = new(0.604f, 0.204f, 0.071f);
    public static readonly Color Orange_900 = new(0.486f, 0.176f, 0.071f);
    public static readonly Color Orange_950 = new(0.263f, 0.078f, 0.027f);

    public static readonly Color Amber_50  = new(1.000f, 0.984f, 0.922f);
    public static readonly Color Amber_100 = new(0.996f, 0.953f, 0.780f);
    public static readonly Color Amber_200 = new(0.992f, 0.902f, 0.541f);
    public static readonly Color Amber_300 = new(0.988f, 0.827f, 0.302f);
    public static readonly Color Amber_400 = new(0.984f, 0.749f, 0.141f);
    public static readonly Color Amber_500 = new(0.961f, 0.620f, 0.043f);
    public static readonly Color Amber_600 = new(0.851f, 0.467f, 0.024f);
    public static readonly Color Amber_700 = new(0.706f, 0.325f, 0.035f);
    public static readonly Color Amber_800 = new(0.573f, 0.251f, 0.055f);
    public static readonly Color Amber_900 = new(0.471f, 0.208f, 0.059f);
    public static readonly Color Amber_950 = new(0.271f, 0.102f, 0.012f);

    public static readonly Color Yellow_50  = new(0.996f, 0.988f, 0.910f);
    public static readonly Color Yellow_100 = new(0.996f, 0.976f, 0.765f);
    public static readonly Color Yellow_200 = new(0.996f, 0.941f, 0.541f);
    public static readonly Color Yellow_300 = new(0.992f, 0.878f, 0.278f);
    public static readonly Color Yellow_400 = new(0.980f, 0.800f, 0.082f);
    public static readonly Color Yellow_500 = new(0.918f, 0.702f, 0.031f);
    public static readonly Color Yellow_600 = new(0.792f, 0.541f, 0.016f);
    public static readonly Color Yellow_700 = new(0.631f, 0.384f, 0.027f);
    public static readonly Color Yellow_800 = new(0.522f, 0.302f, 0.055f);
    public static readonly Color Yellow_900 = new(0.443f, 0.247f, 0.071f);
    public static readonly Color Yellow_950 = new(0.259f, 0.125f, 0.024f);

    public static readonly Color Lime_50  = new(0.969f, 0.996f, 0.906f);
    public static readonly Color Lime_100 = new(0.925f, 0.988f, 0.796f);
    public static readonly Color Lime_200 = new(0.851f, 0.976f, 0.616f);
    public static readonly Color Lime_300 = new(0.745f, 0.949f, 0.392f);
    public static readonly Color Lime_400 = new(0.639f, 0.902f, 0.208f);
    public static readonly Color Lime_500 = new(0.518f, 0.800f, 0.086f);
    public static readonly Color Lime_600 = new(0.396f, 0.639f, 0.051f);
    public static readonly Color Lime_700 = new(0.302f, 0.486f, 0.059f);
    public static readonly Color Lime_800 = new(0.247f, 0.384f, 0.071f);
    public static readonly Color Lime_900 = new(0.212f, 0.325f, 0.078f);
    public static readonly Color Lime_950 = new(0.102f, 0.180f, 0.020f);

    public static readonly Color Green_50  = new(0.941f, 0.992f, 0.957f);
    public static readonly Color Green_100 = new(0.863f, 0.988f, 0.906f);
    public static readonly Color Green_200 = new(0.733f, 0.969f, 0.816f);
    public static readonly Color Green_300 = new(0.525f, 0.937f, 0.675f);
    public static readonly Color Green_400 = new(0.290f, 0.871f, 0.502f);
    public static readonly Color Green_500 = new(0.133f, 0.773f, 0.369f);
    public static readonly Color Green_600 = new(0.086f, 0.639f, 0.290f);
    public static readonly Color Green_700 = new(0.082f, 0.502f, 0.239f);
    public static readonly Color Green_800 = new(0.086f, 0.396f, 0.204f);
    public static readonly Color Green_900 = new(0.078f, 0.325f, 0.176f);
    public static readonly Color Green_950 = new(0.020f, 0.180f, 0.086f);

    public static readonly Color Emerald_50  = new(0.925f, 0.992f, 0.961f);
    public static readonly Color Emerald_100 = new(0.820f, 0.980f, 0.898f);
    public static readonly Color Emerald_200 = new(0.655f, 0.953f, 0.816f);
    public static readonly Color Emerald_300 = new(0.431f, 0.906f, 0.718f);
    public static readonly Color Emerald_400 = new(0.204f, 0.827f, 0.600f);
    public static readonly Color Emerald_500 = new(0.063f, 0.725f, 0.506f);
    public static readonly Color Emerald_600 = new(0.020f, 0.588f, 0.412f);
    public static readonly Color Emerald_700 = new(0.016f, 0.471f, 0.341f);
    public static readonly Color Emerald_800 = new(0.024f, 0.373f, 0.275f);
    public static readonly Color Emerald_900 = new(0.024f, 0.306f, 0.231f);
    public static readonly Color Emerald_950 = new(0.008f, 0.173f, 0.133f);

    public static readonly Color Teal_50  = new(0.941f, 0.992f, 0.980f);
    public static readonly Color Teal_100 = new(0.800f, 0.984f, 0.945f);
    public static readonly Color Teal_200 = new(0.600f, 0.965f, 0.894f);
    public static readonly Color Teal_300 = new(0.369f, 0.918f, 0.831f);
    public static readonly Color Teal_400 = new(0.176f, 0.831f, 0.749f);
    public static readonly Color Teal_500 = new(0.078f, 0.722f, 0.651f);
    public static readonly Color Teal_600 = new(0.051f, 0.580f, 0.533f);
    public static readonly Color Teal_700 = new(0.059f, 0.463f, 0.431f);
    public static readonly Color Teal_800 = new(0.067f, 0.369f, 0.349f);
    public static readonly Color Teal_900 = new(0.075f, 0.306f, 0.290f);
    public static readonly Color Teal_950 = new(0.016f, 0.184f, 0.180f);

    public static readonly Color Cyan_50  = new(0.925f, 0.996f, 1.000f);
    public static readonly Color Cyan_100 = new(0.812f, 0.980f, 0.996f);
    public static readonly Color Cyan_200 = new(0.647f, 0.953f, 0.988f);
    public static readonly Color Cyan_300 = new(0.404f, 0.910f, 0.976f);
    public static readonly Color Cyan_400 = new(0.133f, 0.827f, 0.933f);
    public static readonly Color Cyan_500 = new(0.024f, 0.714f, 0.831f);
    public static readonly Color Cyan_600 = new(0.031f, 0.569f, 0.698f);
    public static readonly Color Cyan_700 = new(0.055f, 0.455f, 0.565f);
    public static readonly Color Cyan_800 = new(0.082f, 0.369f, 0.459f);
    public static readonly Color Cyan_900 = new(0.086f, 0.306f, 0.388f);
    public static readonly Color Cyan_950 = new(0.031f, 0.200f, 0.267f);

    public static readonly Color Sky_50  = new(0.941f, 0.976f, 1.000f);
    public static readonly Color Sky_100 = new(0.878f, 0.949f, 0.996f);
    public static readonly Color Sky_200 = new(0.729f, 0.902f, 0.992f);
    public static readonly Color Sky_300 = new(0.490f, 0.827f, 0.988f);
    public static readonly Color Sky_400 = new(0.220f, 0.741f, 0.973f);
    public static readonly Color Sky_500 = new(0.055f, 0.647f, 0.914f);
    public static readonly Color Sky_600 = new(0.008f, 0.518f, 0.780f);
    public static readonly Color Sky_700 = new(0.012f, 0.412f, 0.631f);
    public static readonly Color Sky_800 = new(0.027f, 0.349f, 0.522f);
    public static readonly Color Sky_900 = new(0.047f, 0.290f, 0.431f);
    public static readonly Color Sky_950 = new(0.031f, 0.184f, 0.286f);

    public static readonly Color Blue_50  = new(0.937f, 0.965f, 1.000f);
    public static readonly Color Blue_100 = new(0.859f, 0.918f, 0.996f);
    public static readonly Color Blue_200 = new(0.749f, 0.859f, 0.996f);
    public static readonly Color Blue_300 = new(0.576f, 0.773f, 0.992f);
    public static readonly Color Blue_400 = new(0.376f, 0.647f, 0.980f);
    public static readonly Color Blue_500 = new(0.231f, 0.510f, 0.965f);
    public static readonly Color Blue_600 = new(0.145f, 0.388f, 0.922f);
    public static readonly Color Blue_700 = new(0.114f, 0.306f, 0.847f);
    public static readonly Color Blue_800 = new(0.118f, 0.251f, 0.686f);
    public static readonly Color Blue_900 = new(0.118f, 0.227f, 0.541f);
    public static readonly Color Blue_950 = new(0.090f, 0.145f, 0.329f);

    public static readonly Color Indigo_50  = new(0.933f, 0.949f, 1.000f);
    public static readonly Color Indigo_100 = new(0.878f, 0.906f, 1.000f);
    public static readonly Color Indigo_200 = new(0.780f, 0.824f, 0.996f);
    public static readonly Color Indigo_300 = new(0.647f, 0.706f, 0.988f);
    public static readonly Color Indigo_400 = new(0.506f, 0.549f, 0.973f);
    public static readonly Color Indigo_500 = new(0.388f, 0.400f, 0.945f);
    public static readonly Color Indigo_600 = new(0.310f, 0.275f, 0.898f);
    public static readonly Color Indigo_700 = new(0.263f, 0.220f, 0.792f);
    public static readonly Color Indigo_800 = new(0.216f, 0.188f, 0.639f);
    public static readonly Color Indigo_900 = new(0.192f, 0.180f, 0.506f);
    public static readonly Color Indigo_950 = new(0.118f, 0.106f, 0.294f);

    public static readonly Color Violet_50  = new(0.961f, 0.953f, 1.000f);
    public static readonly Color Violet_100 = new(0.929f, 0.914f, 0.996f);
    public static readonly Color Violet_200 = new(0.867f, 0.839f, 0.996f);
    public static readonly Color Violet_300 = new(0.769f, 0.710f, 0.992f);
    public static readonly Color Violet_400 = new(0.655f, 0.545f, 0.980f);
    public static readonly Color Violet_500 = new(0.545f, 0.361f, 0.965f);
    public static readonly Color Violet_600 = new(0.486f, 0.227f, 0.929f);
    public static readonly Color Violet_700 = new(0.427f, 0.157f, 0.851f);
    public static readonly Color Violet_800 = new(0.357f, 0.129f, 0.714f);
    public static readonly Color Violet_900 = new(0.298f, 0.114f, 0.584f);
    public static readonly Color Violet_950 = new(0.180f, 0.063f, 0.396f);

    public static readonly Color Purple_50  = new(0.980f, 0.961f, 1.000f);
    public static readonly Color Purple_100 = new(0.953f, 0.910f, 1.000f);
    public static readonly Color Purple_200 = new(0.914f, 0.835f, 1.000f);
    public static readonly Color Purple_300 = new(0.847f, 0.706f, 0.996f);
    public static readonly Color Purple_400 = new(0.753f, 0.518f, 0.988f);
    public static readonly Color Purple_500 = new(0.659f, 0.333f, 0.969f);
    public static readonly Color Purple_600 = new(0.576f, 0.200f, 0.918f);
    public static readonly Color Purple_700 = new(0.494f, 0.133f, 0.808f);
    public static readonly Color Purple_800 = new(0.420f, 0.129f, 0.659f);
    public static readonly Color Purple_900 = new(0.345f, 0.110f, 0.529f);
    public static readonly Color Purple_950 = new(0.231f, 0.027f, 0.392f);

    public static readonly Color Fuchsia_50  = new(0.992f, 0.957f, 1.000f);
    public static readonly Color Fuchsia_100 = new(0.980f, 0.910f, 1.000f);
    public static readonly Color Fuchsia_200 = new(0.961f, 0.816f, 0.996f);
    public static readonly Color Fuchsia_300 = new(0.941f, 0.671f, 0.988f);
    public static readonly Color Fuchsia_400 = new(0.910f, 0.475f, 0.976f);
    public static readonly Color Fuchsia_500 = new(0.851f, 0.275f, 0.937f);
    public static readonly Color Fuchsia_600 = new(0.753f, 0.149f, 0.827f);
    public static readonly Color Fuchsia_700 = new(0.635f, 0.110f, 0.686f);
    public static readonly Color Fuchsia_800 = new(0.525f, 0.098f, 0.561f);
    public static readonly Color Fuchsia_900 = new(0.439f, 0.102f, 0.459f);
    public static readonly Color Fuchsia_950 = new(0.290f, 0.016f, 0.306f);

    public static readonly Color Pink_50  = new(0.992f, 0.949f, 0.973f);
    public static readonly Color Pink_100 = new(0.988f, 0.906f, 0.953f);
    public static readonly Color Pink_200 = new(0.984f, 0.812f, 0.910f);
    public static readonly Color Pink_300 = new(0.976f, 0.659f, 0.831f);
    public static readonly Color Pink_400 = new(0.957f, 0.447f, 0.714f);
    public static readonly Color Pink_500 = new(0.925f, 0.282f, 0.600f);
    public static readonly Color Pink_600 = new(0.859f, 0.153f, 0.467f);
    public static readonly Color Pink_700 = new(0.745f, 0.094f, 0.365f);
    public static readonly Color Pink_800 = new(0.616f, 0.090f, 0.302f);
    public static readonly Color Pink_900 = new(0.514f, 0.094f, 0.263f);
    public static readonly Color Pink_950 = new(0.314f, 0.027f, 0.141f);

    public static readonly Color Rose_50  = new(1.000f, 0.945f, 0.949f);
    public static readonly Color Rose_100 = new(1.000f, 0.894f, 0.902f);
    public static readonly Color Rose_200 = new(0.996f, 0.804f, 0.827f);
    public static readonly Color Rose_300 = new(0.992f, 0.643f, 0.686f);
    public static readonly Color Rose_400 = new(0.984f, 0.443f, 0.522f);
    public static readonly Color Rose_500 = new(0.957f, 0.247f, 0.369f);
    public static readonly Color Rose_600 = new(0.882f, 0.114f, 0.282f);
    public static readonly Color Rose_700 = new(0.745f, 0.071f, 0.235f);
    public static readonly Color Rose_800 = new(0.624f, 0.071f, 0.224f);
    public static readonly Color Rose_900 = new(0.533f, 0.075f, 0.216f);
    public static readonly Color Rose_950 = new(0.298f, 0.020f, 0.098f);
    
    #region Non-standard Tailwind colors

    // Warm, radiant metallic from pale cream to deep amber-brown
    public static readonly Color Gold_50  = new(1.000f, 0.992f, 0.941f);
    public static readonly Color Gold_100 = new(0.996f, 0.976f, 0.851f);
    public static readonly Color Gold_200 = new(0.992f, 0.941f, 0.702f);
    public static readonly Color Gold_300 = new(0.992f, 0.902f, 0.553f);
    public static readonly Color Gold_400 = new(0.988f, 0.863f, 0.404f);
    public static readonly Color Gold_500 = new(0.984f, 0.820f, 0.255f);
    public static readonly Color Gold_600 = new(0.898f, 0.718f, 0.180f);
    public static readonly Color Gold_700 = new(0.812f, 0.616f, 0.106f);
    public static readonly Color Gold_800 = new(0.722f, 0.510f, 0.035f);
    public static readonly Color Gold_900 = new(0.635f, 0.408f, 0.000f);
    public static readonly Color Gold_950 = new(0.478f, 0.302f, 0.000f);

    // Cool-toned metallic from icy near-white to charcoal gray
    public static readonly Color Silver_50  = new(0.980f, 0.980f, 0.988f);
    public static readonly Color Silver_100 = new(0.941f, 0.941f, 0.953f);
    public static readonly Color Silver_200 = new(0.878f, 0.878f, 0.898f);
    public static readonly Color Silver_300 = new(0.820f, 0.820f, 0.847f);
    public static readonly Color Silver_400 = new(0.761f, 0.761f, 0.796f);
    public static readonly Color Silver_500 = new(0.702f, 0.702f, 0.745f);
    public static readonly Color Silver_600 = new(0.541f, 0.541f, 0.588f);
    public static readonly Color Silver_700 = new(0.384f, 0.384f, 0.427f);
    public static readonly Color Silver_800 = new(0.224f, 0.224f, 0.267f);
    public static readonly Color Silver_900 = new(0.122f, 0.122f, 0.165f);
    public static readonly Color Silver_950 = new(0.039f, 0.039f, 0.082f);

    // Earthy reddish-brown metallics from light beige to deep terracotta
    public static readonly Color Bronze_50  = new(0.992f, 0.969f, 0.941f);
    public static readonly Color Bronze_100 = new(0.969f, 0.910f, 0.835f);
    public static readonly Color Bronze_200 = new(0.937f, 0.831f, 0.690f);
    public static readonly Color Bronze_300 = new(0.906f, 0.753f, 0.545f);
    public static readonly Color Bronze_400 = new(0.875f, 0.675f, 0.400f);
    public static readonly Color Bronze_500 = new(0.843f, 0.596f, 0.255f);
    public static readonly Color Bronze_600 = new(0.702f, 0.467f, 0.165f);
    public static readonly Color Bronze_700 = new(0.561f, 0.353f, 0.114f);
    public static readonly Color Bronze_800 = new(0.420f, 0.255f, 0.075f);
    public static readonly Color Bronze_900 = new(0.278f, 0.176f, 0.039f);
    public static readonly Color Bronze_950 = new(0.165f, 0.102f, 0.012f);

    // Fresh green-cyan progression from icy pastel to deep teal
    public static readonly Color Mint_50  = new(0.941f, 0.992f, 0.980f);
    public static readonly Color Mint_100 = new(0.776f, 0.980f, 0.933f);
    public static readonly Color Mint_200 = new(0.616f, 0.965f, 0.886f);
    public static readonly Color Mint_300 = new(0.451f, 0.953f, 0.839f);
    public static readonly Color Mint_400 = new(0.290f, 0.937f, 0.792f);
    public static readonly Color Mint_500 = new(0.129f, 0.922f, 0.745f);
    public static readonly Color Mint_600 = new(0.090f, 0.761f, 0.608f);
    public static readonly Color Mint_700 = new(0.059f, 0.600f, 0.471f);
    public static readonly Color Mint_800 = new(0.035f, 0.439f, 0.333f);
    public static readonly Color Mint_900 = new(0.020f, 0.278f, 0.200f);
    public static readonly Color Mint_950 = new(0.008f, 0.157f, 0.118f);
    
    // Cool metallic spectrum from arctic white to deep graphite
    public static readonly Color Platinum_50  = new(0.976f, 0.984f, 0.992f);
    public static readonly Color Platinum_100 = new(0.945f, 0.961f, 0.976f);
    public static readonly Color Platinum_200 = new(0.894f, 0.914f, 0.945f);
    public static readonly Color Platinum_300 = new(0.820f, 0.855f, 0.902f);
    public static readonly Color Platinum_400 = new(0.722f, 0.769f, 0.839f);
    public static readonly Color Platinum_500 = new(0.620f, 0.682f, 0.776f);
    public static readonly Color Platinum_600 = new(0.478f, 0.545f, 0.663f);
    public static readonly Color Platinum_700 = new(0.369f, 0.431f, 0.549f);
    public static readonly Color Platinum_800 = new(0.247f, 0.298f, 0.404f);
    public static readonly Color Platinum_900 = new(0.141f, 0.180f, 0.271f);
    public static readonly Color Platinum_950 = new(0.071f, 0.094f, 0.165f);

    // Warm terra-cotta metallics ranging from pale peach to molten rust
    public static readonly Color Copper_50  = new(0.996f, 0.969f, 0.949f);
    public static readonly Color Copper_100 = new(0.988f, 0.914f, 0.867f);
    public static readonly Color Copper_200 = new(0.973f, 0.824f, 0.733f);
    public static readonly Color Copper_300 = new(0.953f, 0.725f, 0.600f);
    public static readonly Color Copper_400 = new(0.929f, 0.612f, 0.467f);
    public static readonly Color Copper_500 = new(0.902f, 0.490f, 0.333f);
    public static readonly Color Copper_600 = new(0.780f, 0.369f, 0.227f);
    public static readonly Color Copper_700 = new(0.639f, 0.267f, 0.149f);
    public static readonly Color Copper_800 = new(0.494f, 0.180f, 0.090f);
    public static readonly Color Copper_900 = new(0.349f, 0.114f, 0.051f);
    public static readonly Color Copper_950 = new(0.220f, 0.055f, 0.016f);    

    // Organic greens from dewy moss to shadowed pine
    public static readonly Color Forest_50  = new(0.945f, 0.976f, 0.953f);
    public static readonly Color Forest_100 = new(0.871f, 0.941f, 0.882f);
    public static readonly Color Forest_200 = new(0.749f, 0.886f, 0.776f);
    public static readonly Color Forest_300 = new(0.612f, 0.831f, 0.663f);
    public static readonly Color Forest_400 = new(0.467f, 0.776f, 0.549f);
    public static readonly Color Forest_500 = new(0.314f, 0.722f, 0.435f);
    public static readonly Color Forest_600 = new(0.220f, 0.608f, 0.341f);
    public static readonly Color Forest_700 = new(0.157f, 0.490f, 0.263f);
    public static readonly Color Forest_800 = new(0.110f, 0.373f, 0.192f);
    public static readonly Color Forest_900 = new(0.071f, 0.259f, 0.129f);
    public static readonly Color Forest_950 = new(0.035f, 0.157f, 0.075f);

    // Aquatic blues spanning pale surf to abyssal navy
    public static readonly Color Ocean_50  = new(0.937f, 0.969f, 0.996f);
    public static readonly Color Ocean_100 = new(0.855f, 0.929f, 0.988f);
    public static readonly Color Ocean_200 = new(0.722f, 0.859f, 0.973f);
    public static readonly Color Ocean_300 = new(0.561f, 0.776f, 0.957f);
    public static readonly Color Ocean_400 = new(0.384f, 0.690f, 0.937f);
    public static readonly Color Ocean_500 = new(0.204f, 0.596f, 0.918f);
    public static readonly Color Ocean_600 = new(0.102f, 0.482f, 0.773f);
    public static readonly Color Ocean_700 = new(0.071f, 0.380f, 0.627f);
    public static readonly Color Ocean_800 = new(0.051f, 0.286f, 0.482f);
    public static readonly Color Ocean_900 = new(0.031f, 0.196f, 0.337f);
    public static readonly Color Ocean_950 = new(0.016f, 0.114f, 0.200f);

    // Vibrant sky-to-cobalt progression with energetic luminosity
    public static readonly Color Electric_50  = new(0.941f, 0.976f, 1.000f);
    public static readonly Color Electric_100 = new(0.878f, 0.949f, 0.996f);
    public static readonly Color Electric_200 = new(0.729f, 0.902f, 0.992f);
    public static readonly Color Electric_300 = new(0.490f, 0.827f, 0.988f);
    public static readonly Color Electric_400 = new(0.220f, 0.741f, 0.973f);
    public static readonly Color Electric_500 = new(0.055f, 0.647f, 0.914f);
    public static readonly Color Electric_600 = new(0.008f, 0.518f, 0.780f);
    public static readonly Color Electric_700 = new(0.012f, 0.412f, 0.631f);
    public static readonly Color Electric_800 = new(0.027f, 0.349f, 0.522f);
    public static readonly Color Electric_900 = new(0.047f, 0.290f, 0.431f);
    public static readonly Color Electric_950 = new(0.031f, 0.184f, 0.286f);

    // Electric magenta-to-fuchsia spectrum
    public static readonly Color NeonPink_50  = new(1.000f, 0.941f, 0.984f);
    public static readonly Color NeonPink_100 = new(1.000f, 0.839f, 0.969f);
    public static readonly Color NeonPink_200 = new(1.000f, 0.678f, 0.937f);
    public static readonly Color NeonPink_300 = new(1.000f, 0.502f, 0.906f);
    public static readonly Color NeonPink_400 = new(1.000f, 0.310f, 0.871f);
    public static readonly Color NeonPink_500 = new(1.000f, 0.122f, 0.839f);
    public static readonly Color NeonPink_600 = new(0.902f, 0.000f, 0.741f);
    public static readonly Color NeonPink_700 = new(0.722f, 0.000f, 0.588f);
    public static readonly Color NeonPink_800 = new(0.541f, 0.000f, 0.435f);
    public static readonly Color NeonPink_900 = new(0.361f, 0.000f, 0.286f);
    public static readonly Color NeonPink_950 = new(0.200f, 0.000f, 0.161f);

    // Antique parchment browns from faded ivory to aged leather
    public static readonly Color Sepia_50  = new(0.992f, 0.980f, 0.969f);
    public static readonly Color Sepia_100 = new(0.980f, 0.949f, 0.914f);
    public static readonly Color Sepia_200 = new(0.957f, 0.894f, 0.827f);
    public static readonly Color Sepia_300 = new(0.922f, 0.835f, 0.725f);
    public static readonly Color Sepia_400 = new(0.875f, 0.761f, 0.612f);
    public static readonly Color Sepia_500 = new(0.816f, 0.678f, 0.498f);
    public static readonly Color Sepia_600 = new(0.698f, 0.557f, 0.380f);
    public static readonly Color Sepia_700 = new(0.561f, 0.427f, 0.282f);
    public static readonly Color Sepia_800 = new(0.424f, 0.310f, 0.200f);
    public static readonly Color Sepia_900 = new(0.290f, 0.208f, 0.133f);
    public static readonly Color Sepia_950 = new(0.165f, 0.118f, 0.075f);

    // Soft historical lavenders from dawn haze to twilight amethyst
    public static readonly Color Periwinkle_50  = new(0.969f, 0.976f, 0.996f);
    public static readonly Color Periwinkle_100 = new(0.929f, 0.945f, 0.988f);
    public static readonly Color Periwinkle_200 = new(0.859f, 0.878f, 0.973f);
    public static readonly Color Periwinkle_300 = new(0.776f, 0.804f, 0.957f);
    public static readonly Color Periwinkle_400 = new(0.678f, 0.718f, 0.937f);
    public static readonly Color Periwinkle_500 = new(0.573f, 0.624f, 0.918f);
    public static readonly Color Periwinkle_600 = new(0.455f, 0.514f, 0.835f);
    public static readonly Color Periwinkle_700 = new(0.353f, 0.404f, 0.690f);
    public static readonly Color Periwinkle_800 = new(0.263f, 0.302f, 0.541f);
    public static readonly Color Periwinkle_900 = new(0.180f, 0.212f, 0.392f);
    public static readonly Color Periwinkle_950 = new(0.106f, 0.129f, 0.251f);

    // Earthy green tea tones from whipped froth to concentrated brew
    public static readonly Color Matcha_50  = new(0.965f, 0.984f, 0.941f);
    public static readonly Color Matcha_100 = new(0.922f, 0.969f, 0.867f);
    public static readonly Color Matcha_200 = new(0.843f, 0.937f, 0.729f);
    public static readonly Color Matcha_300 = new(0.753f, 0.902f, 0.584f);
    public static readonly Color Matcha_400 = new(0.659f, 0.863f, 0.439f);
    public static readonly Color Matcha_500 = new(0.561f, 0.820f, 0.298f);
    public static readonly Color Matcha_600 = new(0.435f, 0.702f, 0.212f);
    public static readonly Color Matcha_700 = new(0.337f, 0.561f, 0.161f);
    public static readonly Color Matcha_800 = new(0.251f, 0.420f, 0.122f);
    public static readonly Color Matcha_900 = new(0.176f, 0.282f, 0.082f);
    public static readonly Color Matcha_950 = new(0.110f, 0.161f, 0.043f);

    // Juicy purple-reds spanning petal blush to blackberry jam
    public static readonly Color Berry_50  = new(0.992f, 0.949f, 0.973f);
    public static readonly Color Berry_100 = new(0.988f, 0.894f, 0.945f);
    public static readonly Color Berry_200 = new(0.976f, 0.784f, 0.890f);
    public static readonly Color Berry_300 = new(0.965f, 0.659f, 0.827f);
    public static readonly Color Berry_400 = new(0.949f, 0.529f, 0.761f);
    public static readonly Color Berry_500 = new(0.933f, 0.380f, 0.686f);
    public static readonly Color Berry_600 = new(0.831f, 0.259f, 0.569f);
    public static readonly Color Berry_700 = new(0.678f, 0.173f, 0.451f);
    public static readonly Color Berry_800 = new(0.522f, 0.110f, 0.337f);
    public static readonly Color Berry_900 = new(0.365f, 0.063f, 0.227f);
    public static readonly Color Berry_950 = new(0.216f, 0.031f, 0.133f);
    #endregion
  }
}