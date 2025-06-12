// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Semantics;

using System.Numerics;

/// <summary>
/// Represents an entropy quantity with compile-time dimensional safety.
/// Entropy is a measure of the disorder or randomness in a thermodynamic system.
/// </summary>
public sealed record Entropy<T> : PhysicalQuantity<Entropy<T>, T>
	where T : struct, INumber<T>
{
	/// <summary>Gets the physical dimension of entropy [M L² T⁻² Θ⁻¹].</summary>
	public override PhysicalDimension Dimension => PhysicalDimensions.Entropy;

	/// <summary>
	/// Initializes a new instance of the Entropy class.
	/// </summary>
	public Entropy() : base() { }

	/// <summary>
	/// Creates a new Entropy from a value in joules per kelvin.
	/// </summary>
	/// <param name="joulesPerKelvin">The entropy value in J/K.</param>
	/// <returns>A new Entropy instance.</returns>
	public static Entropy<T> FromJoulesPerKelvin(T joulesPerKelvin) => Create(joulesPerKelvin);

	/// <summary>
	/// Creates a new Entropy from a value in calories per kelvin.
	/// </summary>
	/// <param name="caloriesPerKelvin">The entropy value in cal/K.</param>
	/// <returns>A new Entropy instance.</returns>
	public static Entropy<T> FromCaloriesPerKelvin(T caloriesPerKelvin) =>
		Create(caloriesPerKelvin * T.CreateChecked(4.184));

	/// <summary>
	/// Creates a new Entropy from a value in BTU per Rankine.
	/// </summary>
	/// <param name="btuPerRankine">The entropy value in BTU/°R.</param>
	/// <returns>A new Entropy instance.</returns>
	public static Entropy<T> FromBtuPerRankine(T btuPerRankine) =>
		Create(btuPerRankine * T.CreateChecked(1899.1));

	/// <summary>Gets the entropy in joules per kelvin.</summary>
	/// <returns>The entropy in J/K.</returns>
	public T JoulesPerKelvin => Value;

	/// <summary>Gets the entropy in calories per kelvin.</summary>
	/// <returns>The entropy in cal/K.</returns>
	public T CaloriesPerKelvin => Value / T.CreateChecked(4.184);

	/// <summary>Gets the entropy in BTU per Rankine.</summary>
	/// <returns>The entropy in BTU/°R.</returns>
	public T BtuPerRankine => Value / T.CreateChecked(1899.1);

	/// <summary>
	/// Calculates entropy change using ΔS = Q/T for reversible processes.
	/// </summary>
	/// <param name="heat">The heat transferred.</param>
	/// <param name="temperature">The absolute temperature.</param>
	/// <returns>The entropy change.</returns>
	public static Entropy<T> CalculateEntropyChange(Heat<T> heat, Temperature<T> temperature)
	{
		ArgumentNullException.ThrowIfNull(heat);
		ArgumentNullException.ThrowIfNull(temperature);
		return Create(heat.Value / temperature.Value);
	}

	/// <summary>
	/// Calculates the Gibbs free energy change: ΔG = ΔH - T·ΔS.
	/// </summary>
	/// <param name="enthalpy">The enthalpy change.</param>
	/// <param name="temperature">The absolute temperature.</param>
	/// <returns>The Gibbs free energy change.</returns>
	public Energy<T> CalculateGibbsFreeEnergy(Energy<T> enthalpy, Temperature<T> temperature)
	{
		ArgumentNullException.ThrowIfNull(enthalpy);
		ArgumentNullException.ThrowIfNull(temperature);
		return Energy<T>.Create(enthalpy.Value - (temperature.Value * Value));
	}
}
