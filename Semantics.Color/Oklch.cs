// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Semantics.Color;

using System;

/// <summary>
/// A color in the polar (lightness, chroma, hue) form of Oklab. Hue is in degrees, 0..360.
/// </summary>
/// <param name="L">Perceived lightness.</param>
/// <param name="C">Chroma (colourfulness).</param>
/// <param name="H">Hue angle in degrees, 0..360.</param>
public readonly record struct Oklch(double L, double C, double H)
{
	/// <summary>Converts this polar color back to Cartesian <see cref="Oklab"/>.</summary>
	/// <returns>The Oklab equivalent.</returns>
	public Oklab ToOklab()
	{
		double hRad = H * (Math.PI / 180.0);
		return new Oklab(L, C * Math.Cos(hRad), C * Math.Sin(hRad));
	}

	// Conversions to the other color-space types. Oklch shares the perceptual base with Oklab, so
	// every hop routes through Oklab; Oklab in turn crosses the gamma boundary once (through the
	// linear Color hub) to reach the sRGB family.

	/// <summary>Creates an Oklch color from a Cartesian <see cref="Oklab"/> value.</summary>
	/// <param name="oklab">The Oklab color.</param>
	/// <returns>The Oklch equivalent.</returns>
	public static Oklch FromOklab(Oklab oklab) => oklab.ToOklch();

	/// <summary>Converts this Oklch color to a linear <see cref="Color"/> (via <see cref="Oklab"/>).</summary>
	/// <param name="a">Straight alpha for the result (default 1.0).</param>
	/// <returns>The linear-RGB equivalent.</returns>
	public Color ToColor(double a = 1.0) => ToOklab().ToColor(a);

	/// <summary>Creates an Oklch color from a linear <see cref="Color"/> (via <see cref="Oklab"/>).</summary>
	/// <param name="color">The linear color.</param>
	/// <returns>The Oklch equivalent.</returns>
	public static Oklch FromColor(Color color) => FromOklab(Oklab.FromColor(color));

	/// <summary>Converts this Oklch color to <see cref="Srgb"/> (via <see cref="Oklab"/>).</summary>
	/// <returns>The sRGB equivalent.</returns>
	public Srgb ToSrgb() => ToOklab().ToSrgb();

	/// <summary>Creates an Oklch color from an <see cref="Srgb"/> value (via <see cref="Oklab"/>).</summary>
	/// <param name="srgb">The sRGB color.</param>
	/// <returns>The Oklch equivalent.</returns>
	public static Oklch FromSrgb(Srgb srgb) => FromOklab(Oklab.FromSrgb(srgb));

	/// <summary>Converts this Oklch color to <see cref="Hsl"/> (via <see cref="Oklab"/>).</summary>
	/// <returns>The HSL equivalent.</returns>
	public Hsl ToHsl() => ToOklab().ToHsl();

	/// <summary>Creates an Oklch color from an <see cref="Hsl"/> value (via <see cref="Oklab"/>).</summary>
	/// <param name="hsl">The HSL color.</param>
	/// <returns>The Oklch equivalent.</returns>
	public static Oklch FromHsl(Hsl hsl) => FromOklab(Oklab.FromHsl(hsl));

	/// <summary>Converts this Oklch color to <see cref="Hsv"/> (via <see cref="Oklab"/>).</summary>
	/// <returns>The HSV equivalent.</returns>
	public Hsv ToHsv() => ToOklab().ToHsv();

	/// <summary>Creates an Oklch color from an <see cref="Hsv"/> value (via <see cref="Oklab"/>).</summary>
	/// <param name="hsv">The HSV color.</param>
	/// <returns>The Oklch equivalent.</returns>
	public static Oklch FromHsv(Hsv hsv) => FromOklab(Oklab.FromHsv(hsv));
}
