// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Semantics.Color;

using System;

/// <summary>
/// A color in the gamma-encoded sRGB space, each channel 0..1. This is the only color space that
/// crosses the gamma boundary to and from the linear <see cref="Color"/>.
/// </summary>
/// <param name="R">Gamma-encoded red channel.</param>
/// <param name="G">Gamma-encoded green channel.</param>
/// <param name="B">Gamma-encoded blue channel.</param>
public readonly record struct Srgb(double R, double G, double B)
{
	/// <summary>Converts this sRGB color to a linear <see cref="Color"/>.</summary>
	/// <param name="a">Straight alpha for the resulting color (default 1.0).</param>
	/// <returns>The linear-RGB equivalent.</returns>
	public Color ToLinear(double a = 1.0) => new(DecodeChannel(R), DecodeChannel(G), DecodeChannel(B), a);

	/// <summary>Converts a linear <see cref="Color"/> to gamma-encoded sRGB (alpha dropped).</summary>
	/// <param name="color">The linear color.</param>
	/// <returns>The sRGB equivalent.</returns>
	public static Srgb FromLinear(Color color) =>
		new(EncodeChannel(color.R), EncodeChannel(color.G), EncodeChannel(color.B));

	// Conversions to the other color-space types. sRGB, HSL, and HSV share the gamma-encoded
	// sRGB base, so those hops stay within the family (no gamma round-trip). Reaching the Oklab
	// family crosses the gamma boundary once, through the linear Color hub.

	/// <summary>Converts this sRGB color to a linear <see cref="Color"/> (alias for <see cref="ToLinear"/>).</summary>
	/// <param name="a">Straight alpha for the result (default 1.0).</param>
	/// <returns>The linear-RGB equivalent.</returns>
	public Color ToColor(double a = 1.0) => ToLinear(a);

	/// <summary>Creates an sRGB color from a linear <see cref="Color"/> (alias for <see cref="FromLinear"/>).</summary>
	/// <param name="color">The linear color.</param>
	/// <returns>The sRGB equivalent.</returns>
	public static Srgb FromColor(Color color) => FromLinear(color);

	/// <summary>Converts this sRGB color to <see cref="Hsl"/>.</summary>
	/// <returns>The HSL equivalent.</returns>
	public Hsl ToHsl() => Hsl.FromSrgb(this);

	/// <summary>Creates an sRGB color from an <see cref="Hsl"/> value.</summary>
	/// <param name="hsl">The HSL color.</param>
	/// <returns>The sRGB equivalent.</returns>
	public static Srgb FromHsl(Hsl hsl) => hsl.ToSrgb();

	/// <summary>Converts this sRGB color to <see cref="Hsv"/>.</summary>
	/// <returns>The HSV equivalent.</returns>
	public Hsv ToHsv() => Hsv.FromSrgb(this);

	/// <summary>Creates an sRGB color from an <see cref="Hsv"/> value.</summary>
	/// <param name="hsv">The HSV color.</param>
	/// <returns>The sRGB equivalent.</returns>
	public static Srgb FromHsv(Hsv hsv) => hsv.ToSrgb();

	/// <summary>Converts this sRGB color to <see cref="Oklab"/> (via the linear <see cref="Color"/> hub).</summary>
	/// <returns>The Oklab equivalent.</returns>
	public Oklab ToOklab() => Oklab.FromColor(ToColor());

	/// <summary>Creates an sRGB color from an <see cref="Oklab"/> value (via the linear <see cref="Color"/> hub).</summary>
	/// <param name="oklab">The Oklab color.</param>
	/// <returns>The sRGB equivalent.</returns>
	public static Srgb FromOklab(Oklab oklab) => FromColor(oklab.ToColor());

	/// <summary>Converts this sRGB color to <see cref="Oklch"/> (via <see cref="Oklab"/>).</summary>
	/// <returns>The Oklch equivalent.</returns>
	public Oklch ToOklch() => ToOklab().ToOklch();

	/// <summary>Creates an sRGB color from an <see cref="Oklch"/> value (via <see cref="Oklab"/>).</summary>
	/// <param name="oklch">The Oklch color.</param>
	/// <returns>The sRGB equivalent.</returns>
	public static Srgb FromOklch(Oklch oklch) => FromOklab(oklch.ToOklab());

	private static double DecodeChannel(double s) =>
		s <= 0.04045 ? s / 12.92 : Math.Pow((s + 0.055) / 1.055, 2.4);

	private static double EncodeChannel(double linear) =>
		linear <= 0.0031308 ? 12.92 * linear : (1.055 * Math.Pow(linear, 1.0 / 2.4)) - 0.055;
}
