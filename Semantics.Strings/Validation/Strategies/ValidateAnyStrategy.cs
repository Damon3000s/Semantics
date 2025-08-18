// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Semantics.Strings;

/// <summary>
/// Validation strategy that requires any validation attribute to pass (OR logic).
/// </summary>
public sealed class ValidateAnyStrategy : IValidationStrategy
{
	/// <inheritdoc/>
	public bool Validate(ISemanticString semanticString, Type type)
	{
#if NET6_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(type);
#else
		if (type is null)
		{
			throw new ArgumentNullException(nameof(type));
		}
#endif
		SemanticStringValidationAttribute[] validationAttributes = [.. type.GetCustomAttributes(typeof(SemanticStringValidationAttribute), true)
			.Cast<SemanticStringValidationAttribute>()];
		return validationAttributes.Length == 0 || validationAttributes.Any(attr => attr.Validate(semanticString));
	}
}
