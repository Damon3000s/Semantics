// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.Semantics.Test;

using System.Collections;
using System.Globalization;
using System.Text;

public record MySemanticString : SemanticString<MySemanticString> { }

[TestClass]
public class StringTests
{

	[TestMethod]
	public void ImplicitCastToString()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		string systemString = semanticString;
		Assert.AreEqual("test", systemString);
	}

	[TestMethod]
	public void ExplicitCastFromString()
	{
		string systemString = "test";
		MySemanticString semanticString = (MySemanticString)systemString;
		Assert.AreEqual("test", semanticString.WeakString);
	}

	[TestMethod]
	public void ToStringMethod()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		Assert.AreEqual("test", semanticString.ToString());
	}

	private static readonly char[] TestCharArray = ['t', 'e', 's', 't'];
	private static readonly string[] Expected1 = ["hello", "world", "test"];
	private static readonly string[] Expected2 = ["a", "b,c,d"];

	[TestMethod]
	public void ToCharArrayMethod()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		char[] chars = semanticString.ToCharArray();
		CollectionAssert.AreEqual(TestCharArray, chars);
	}

	[TestMethod]
	public void IsEmptyMethod()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>(string.Empty);
		Assert.IsTrue(semanticString.IsEmpty());
	}

	[TestMethod]
	public void IsValidMethod()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		Assert.IsTrue(semanticString.IsValid());
	}

	[TestMethod]
	public void WithPrefixMethod()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		MySemanticString result = semanticString.WithPrefix("pre-");
		Assert.AreEqual("pre-test", result.ToString());
	}

	[TestMethod]
	public void WithSuffixMethod()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		MySemanticString result = semanticString.WithSuffix("-post");
		Assert.AreEqual("test-post", result.ToString());
	}
	[TestMethod]
	public void AsStringExtensionMethod()
	{
		string systemString = "test";
		MySemanticString semanticString = systemString.As<MySemanticString>();
		Assert.AreEqual("test", semanticString.WeakString);
	}

	[TestMethod]
	public void AsCharArrayExtensionMethod()
	{
		char[] charArray = ['t', 'e', 's', 't'];
		MySemanticString semanticString = charArray.As<MySemanticString>();
		CollectionAssert.AreEqual(charArray, semanticString.ToCharArray());
	}

	[TestMethod]
	public void AsReadOnlySpanExtensionMethod()
	{
		ReadOnlySpan<char> span = "test".AsSpan();
		MySemanticString semanticString = span.As<MySemanticString>();
		Assert.AreEqual("test", semanticString.WeakString);
	}

	// New comprehensive tests for missing functionality

	[TestMethod]
	public void ExplicitCastFromCharArray()
	{
		char[] chars = ['t', 'e', 's', 't'];
		MySemanticString semanticString = (MySemanticString)chars;
		Assert.AreEqual("test", semanticString.WeakString);
	}

	[TestMethod]
	public void ImplicitCastToCharArray()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		char[] result = semanticString;
		CollectionAssert.AreEqual(TestCharArray, result);
	}

	[TestMethod]
	public void ImplicitCastToReadOnlySpan()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		ReadOnlySpan<char> result = semanticString;
		Assert.IsTrue(result.SequenceEqual("test".AsSpan()));
	}

	[TestMethod]
	public void LengthProperty()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		Assert.AreEqual(4, semanticString.Length);
	}

	[TestMethod]
	public void IndexerAccess()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		Assert.AreEqual('t', semanticString[0]);
		Assert.AreEqual('e', semanticString[1]);
		Assert.AreEqual('s', semanticString[2]);
		Assert.AreEqual('t', semanticString[3]);
	}

	[TestMethod]
	public void CompareTo_String()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		Assert.AreEqual(0, semanticString.CompareTo("test"));
		Assert.IsTrue(semanticString.CompareTo("apple") > 0);
		Assert.IsTrue(semanticString.CompareTo("zebra") < 0);
	}

	[TestMethod]
	public void CompareTo_SemanticString()
	{
		MySemanticString semanticString1 = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		MySemanticString semanticString2 = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		MySemanticString semanticString3 = SemanticString<MySemanticString>.Create<MySemanticString>("apple");

		Assert.AreEqual(0, semanticString1.CompareTo(semanticString2));
		Assert.IsTrue(semanticString1.CompareTo(semanticString3) > 0);
	}

	[TestMethod]
	public void Contains_String_ReturnsCorrectResult()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		Assert.IsTrue(semanticString.Contains("world"));
		Assert.IsTrue(semanticString.Contains("hello"));
		Assert.IsFalse(semanticString.Contains("test"));
	}

	[TestMethod]
	public void Contains_WithStringComparison_ReturnsCorrectResult()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("Hello World");
		Assert.IsTrue(semanticString.Contains("WORLD", StringComparison.OrdinalIgnoreCase));
		Assert.IsFalse(semanticString.Contains("WORLD", StringComparison.Ordinal));
	}

	[TestMethod]
	public void EndsWith_ReturnsCorrectResult()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		Assert.IsTrue(semanticString.EndsWith("world"));
		Assert.IsFalse(semanticString.EndsWith("hello"));
	}

	[TestMethod]
	public void StartsWith_ReturnsCorrectResult()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		Assert.IsTrue(semanticString.StartsWith("hello"));
		Assert.IsFalse(semanticString.StartsWith("world"));
	}

	[TestMethod]
	public void IndexOf_Char_ReturnsCorrectIndex()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		Assert.AreEqual(0, semanticString.IndexOf('h'));
		Assert.AreEqual(2, semanticString.IndexOf('l'));
		Assert.AreEqual(-1, semanticString.IndexOf('z'));
	}

	[TestMethod]
	public void IndexOf_String_ReturnsCorrectIndex()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		Assert.AreEqual(0, semanticString.IndexOf("hello"));
		Assert.AreEqual(6, semanticString.IndexOf("world"));
		Assert.AreEqual(-1, semanticString.IndexOf("test"));
	}

	[TestMethod]
	public void LastIndexOf_Char_ReturnsCorrectIndex()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		Assert.AreEqual(3, semanticString.LastIndexOf('l'));
		Assert.AreEqual(4, semanticString.LastIndexOf('o'));
		Assert.AreEqual(-1, semanticString.LastIndexOf('z'));
	}

	[TestMethod]
	public void ToCharArray_WithStartIndexAndLength()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		char[] result = semanticString.ToCharArray(1, 3);
		char[] expected = ['e', 'l', 'l'];
		CollectionAssert.AreEqual(expected, result);
	}

	[TestMethod]
	public void ComparisonOperators()
	{
		MySemanticString semanticString1 = SemanticString<MySemanticString>.Create<MySemanticString>("apple");
		MySemanticString semanticString2 = SemanticString<MySemanticString>.Create<MySemanticString>("banana");
		MySemanticString semanticString3 = SemanticString<MySemanticString>.Create<MySemanticString>("apple");

		Assert.IsTrue(semanticString1 < semanticString2);
		Assert.IsTrue(semanticString1 <= semanticString2);
		Assert.IsTrue(semanticString1 <= semanticString3);
		Assert.IsTrue(semanticString2 > semanticString1);
		Assert.IsTrue(semanticString2 >= semanticString1);
		Assert.IsTrue(semanticString1 >= semanticString3);
	}

	[TestMethod]
	public void ComparisonOperators_WithNull()
	{
		MySemanticString? nullSemanticString = null;
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");

		Assert.IsTrue(nullSemanticString < semanticString);
		Assert.IsTrue(nullSemanticString <= semanticString);
		Assert.IsFalse(nullSemanticString > semanticString);
		Assert.IsFalse(nullSemanticString >= semanticString);
		Assert.IsFalse(semanticString < nullSemanticString);
		Assert.IsTrue(semanticString > nullSemanticString);
	}

	[TestMethod]
	public void FromString_NullInput_ThrowsArgumentNullException()
	{
		Assert.ThrowsExactly<ArgumentNullException>(() =>
			SemanticString<MySemanticString>.Create<MySemanticString>((string)null!));
	}

	[TestMethod]
	public void FromCharArray_NullInput_ThrowsArgumentNullException()
	{
		Assert.ThrowsExactly<ArgumentNullException>(() =>
			SemanticString<MySemanticString>.Create<MySemanticString>((char[])null!));
	}

	[TestMethod]
	public void FromReadOnlySpan_EmptySpan_CreatesEmptySemanticString()
	{
		ReadOnlySpan<char> emptySpan = [];
		MySemanticString result = SemanticString<MySemanticString>.Create<MySemanticString>(emptySpan);
		Assert.AreEqual(string.Empty, result.WeakString);
	}

	[TestMethod]
	public void As_ConversionBetweenSemanticStringTypes()
	{
		MySemanticString original = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		AnotherSemanticString converted = original.As<AnotherSemanticString>();
		Assert.AreEqual("test", converted.WeakString);
	}

	[TestMethod]
	public void StaticToString_WithNullSemanticString()
	{
		MySemanticString? nullSemanticString = null;
		string result = SemanticString<MySemanticString>.ToString(nullSemanticString);
		Assert.AreEqual(string.Empty, result);
	}

	[TestMethod]
	public void StaticToCharArray_WithNullSemanticString()
	{
		MySemanticString? nullSemanticString = null;
		char[] result = SemanticString<MySemanticString>.ToCharArray(nullSemanticString);
		Assert.AreEqual(0, result.Length);
	}

	[TestMethod]
	public void StaticToReadOnlySpan_WithNullSemanticString()
	{
		MySemanticString? nullSemanticString = null;
		ReadOnlySpan<char> result = SemanticString<MySemanticString>.ToReadOnlySpan(nullSemanticString);
		Assert.IsTrue(result.IsEmpty);
	}

	[TestMethod]
	public void IsEmpty_WithNonEmptyString_ReturnsFalse()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		Assert.IsFalse(semanticString.IsEmpty());
	}

	[TestMethod]
	public void ImplicitConversions_WithNull()
	{
		MySemanticString? nullSemanticString = null;
		string stringResult = nullSemanticString;
		char[] charArrayResult = nullSemanticString;
		ReadOnlySpan<char> spanResult = nullSemanticString;

		Assert.AreEqual(string.Empty, stringResult);
		Assert.AreEqual(0, charArrayResult.Length);
		Assert.IsTrue(spanResult.IsEmpty);
	}

	[TestMethod]
	public void Equals_WithString_ReturnsCorrectResult()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		Assert.IsTrue(semanticString.Equals("test"));
		Assert.IsFalse(semanticString.Equals("other"));
	}

	[TestMethod]
	public void Equals_WithStringComparison_ReturnsCorrectResult()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("Test");
		Assert.IsTrue(semanticString.Equals("TEST", StringComparison.OrdinalIgnoreCase));
		Assert.IsFalse(semanticString.Equals("TEST", StringComparison.Ordinal));
	}

	[TestMethod]
	public void GetEnumerator_IteratesOverCharacters()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		using IEnumerator<char> enumerator = ((IEnumerable<char>)semanticString).GetEnumerator();

		Assert.IsTrue(enumerator.MoveNext());
		Assert.AreEqual('t', enumerator.Current);
		Assert.IsTrue(enumerator.MoveNext());
		Assert.AreEqual('e', enumerator.Current);
		Assert.IsTrue(enumerator.MoveNext());
		Assert.AreEqual('s', enumerator.Current);
		Assert.IsTrue(enumerator.MoveNext());
		Assert.AreEqual('t', enumerator.Current);
		Assert.IsFalse(enumerator.MoveNext());
	}

	[TestMethod]
	public void GetEnumerator_NonGeneric_IteratesOverCharacters()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("test");
		string result = "";

		foreach (char c in (IEnumerable)semanticString)
		{
			result += c;
		}

		Assert.AreEqual("test", result);
	}

	// New tests for additional String manipulation methods
	[TestMethod]
	public void IndexOfAny_WithCharArray_ReturnsCorrectIndex()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		char[] chars = ['o', 'w'];
		Assert.AreEqual(4, semanticString.IndexOfAny(chars)); // First 'o' in "hello"
	}

	[TestMethod]
	public void IndexOfAny_WithStartIndex_ReturnsCorrectIndex()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		char[] chars = ['o', 'w'];
		Assert.AreEqual(6, semanticString.IndexOfAny(chars, 5)); // 'w' in "world"
	}

	[TestMethod]
	public void IndexOfAny_WithStartIndexAndCount_ReturnsCorrectIndex()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		char[] chars = ['o', 'w'];
		Assert.AreEqual(6, semanticString.IndexOfAny(chars, 6, 3)); // 'w' in "world" (first match)
	}

	[TestMethod]
	public void LastIndexOfAny_WithCharArray_ReturnsCorrectIndex()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		char[] chars = ['o', 'l'];
		Assert.AreEqual(9, semanticString.LastIndexOfAny(chars)); // Last 'l' in "world"
	}

	[TestMethod]
	public void LastIndexOfAny_WithStartIndex_ReturnsCorrectIndex()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		char[] chars = ['o', 'l'];
		Assert.AreEqual(4, semanticString.LastIndexOfAny(chars, 5)); // 'o' at index 4 in "hello"
	}

	[TestMethod]
	public void LastIndexOfAny_WithStartIndexAndCount_ReturnsCorrectIndex()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		char[] chars = ['o', 'l'];
		Assert.AreEqual(3, semanticString.LastIndexOfAny(chars, 3, 2)); // 'l' at index 3 in "hello"
	}

	[TestMethod]
	public void CopyTo_CopiesCharactersCorrectly()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		char[] destination = new char[10];
		semanticString.CopyTo(1, destination, 2, 3); // Copy "ell" to index 2

		char[] expected = ['\0', '\0', 'e', 'l', 'l', '\0', '\0', '\0', '\0', '\0'];
		CollectionAssert.AreEqual(expected, destination);
	}

	[TestMethod]
	public void Insert_InsertsStringCorrectly()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		string result = semanticString.Insert(5, " beautiful");
		Assert.AreEqual("hello beautiful world", result);
	}

	[TestMethod]
	public void Remove_SingleParameter_RemovesFromIndex()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		string result = semanticString.Remove(5);
		Assert.AreEqual("hello", result);
	}

	[TestMethod]
	public void Remove_TwoParameters_RemovesSpecifiedLength()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		string result = semanticString.Remove(5, 6);
		Assert.AreEqual("hello", result);
	}

	[TestMethod]
	public void Replace_Char_ReplacesCorrectly()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		string result = semanticString.Replace('l', 'x');
		Assert.AreEqual("hexxo", result);
	}

	[TestMethod]
	public void Replace_String_ReplacesCorrectly()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		string result = semanticString.Replace("world", "universe");
		Assert.AreEqual("hello universe", result);
	}

	[TestMethod]
	public void Split_WithCharArray_SplitsCorrectly()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello,world;test");
		string[] result = semanticString.Split(',', ';');
		CollectionAssert.AreEqual(Expected1, result);
	}

	[TestMethod]
	public void Split_WithCharArrayAndCount_SplitsCorrectly()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("a,b,c,d");
		string[] result = semanticString.Split([','], 2);
		CollectionAssert.AreEqual(Expected2, result);
	}

	[TestMethod]
	public void Split_WithStringArray_SplitsCorrectly()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello::world::test");
		string[] result = semanticString.Split(["::"], StringSplitOptions.None);
		CollectionAssert.AreEqual(Expected1, result);
	}

	[TestMethod]
	public void Substring_SingleParameter_ReturnsCorrectSubstring()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		string result = semanticString.Substring(6);
		Assert.AreEqual("world", result);
	}

	[TestMethod]
	public void Substring_TwoParameters_ReturnsCorrectSubstring()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world");
		string result = semanticString.Substring(0, 5);
		Assert.AreEqual("hello", result);
	}

	[TestMethod]
	public void ToLower_ReturnsLowercaseString()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("HELLO World");
		string result = semanticString.ToLower();
		Assert.AreEqual("hello world", result);
	}

	[TestMethod]
	public void ToUpper_ReturnsUppercaseString()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello World");
		string result = semanticString.ToUpper();
		Assert.AreEqual("HELLO WORLD", result);
	}

	[TestMethod]
	public void ToLowerInvariant_ReturnsLowercaseString()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("HELLO World");
		string result = semanticString.ToLowerInvariant();
		Assert.AreEqual("hello world", result);
	}

	[TestMethod]
	public void ToUpperInvariant_ReturnsUppercaseString()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello World");
		string result = semanticString.ToUpperInvariant();
		Assert.AreEqual("HELLO WORLD", result);
	}

	[TestMethod]
	public void Trim_RemovesWhitespace()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("  hello world  ");
		string result = semanticString.Trim();
		Assert.AreEqual("hello world", result);
	}

	[TestMethod]
	public void Trim_WithCharArray_RemovesSpecifiedChars()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("xxhello worldxx");
		string result = semanticString.Trim('x');
		Assert.AreEqual("hello world", result);
	}

	[TestMethod]
	public void TrimStart_RemovesLeadingChars()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("xxhello worldxx");
		string result = semanticString.TrimStart('x');
		Assert.AreEqual("hello worldxx", result);
	}

	[TestMethod]
	public void TrimEnd_RemovesTrailingChars()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("xxhello worldxx");
		string result = semanticString.TrimEnd('x');
		Assert.AreEqual("xxhello world", result);
	}

	[TestMethod]
	public void PadLeft_PadsStringCorrectly()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		string result = semanticString.PadLeft(10);
		Assert.AreEqual("     hello", result);
	}

	[TestMethod]
	public void PadLeft_WithChar_PadsStringCorrectly()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		string result = semanticString.PadLeft(10, '*');
		Assert.AreEqual("*****hello", result);
	}

	[TestMethod]
	public void PadRight_PadsStringCorrectly()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		string result = semanticString.PadRight(10);
		Assert.AreEqual("hello     ", result);
	}

	[TestMethod]
	public void PadRight_WithChar_PadsStringCorrectly()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		string result = semanticString.PadRight(10, '*');
		Assert.AreEqual("hello*****", result);
	}

	[TestMethod]
	public void GetTypeCode_ReturnsString()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		TypeCode result = semanticString.GetTypeCode();
		Assert.AreEqual(TypeCode.String, result);
	}

	[TestMethod]
	public void IsNormalized_WithDefaultForm_ReturnsTrue()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		bool result = semanticString.IsNormalized();
		Assert.IsTrue(result);
	}

	[TestMethod]
	public void IsNormalized_WithSpecificForm_ReturnsTrue()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		bool result = semanticString.IsNormalized(NormalizationForm.FormC);
		Assert.IsTrue(result);
	}

	[TestMethod]
	public void Normalize_WithDefaultForm_ReturnsNormalizedString()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		string result = semanticString.Normalize();
		Assert.AreEqual("hello", result);
	}

	[TestMethod]
	public void Normalize_WithSpecificForm_ReturnsNormalizedString()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		string result = semanticString.Normalize(NormalizationForm.FormC);
		Assert.AreEqual("hello", result);
	}

	[TestMethod]
	public void StartsWith_WithCultureAndIgnoreCase_ReturnsCorrectResult()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("Hello World");
		bool result = semanticString.StartsWith("HELLO", true, CultureInfo.InvariantCulture);
		Assert.IsTrue(result);
	}

	[TestMethod]
	public void StartsWith_WithStringComparison_ReturnsCorrectResult()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("Hello World");
		Assert.IsTrue(semanticString.StartsWith("HELLO", StringComparison.OrdinalIgnoreCase));
		Assert.IsFalse(semanticString.StartsWith("HELLO", StringComparison.Ordinal));
	}

	[TestMethod]
	public void EndsWith_WithCultureAndIgnoreCase_ReturnsCorrectResult()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("Hello World");
		bool result = semanticString.EndsWith("WORLD", true, CultureInfo.InvariantCulture);
		Assert.IsTrue(result);
	}

	[TestMethod]
	public void EndsWith_WithStringComparison_ReturnsCorrectResult()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("Hello World");
		Assert.IsTrue(semanticString.EndsWith("WORLD", StringComparison.OrdinalIgnoreCase));
		Assert.IsFalse(semanticString.EndsWith("WORLD", StringComparison.Ordinal));
	}

	[TestMethod]
	public void ToLower_WithCulture_ReturnsCorrectResult()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("HELLO");
		string result = semanticString.ToLower(CultureInfo.InvariantCulture);
		Assert.AreEqual("hello", result);
	}

	[TestMethod]
	public void ToUpper_WithCulture_ReturnsCorrectResult()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		string result = semanticString.ToUpper(CultureInfo.InvariantCulture);
		Assert.AreEqual("HELLO", result);
	}

	[TestMethod]
	public void ToString_WithFormatProvider_ReturnsCorrectResult()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		string result = semanticString.ToString(CultureInfo.InvariantCulture);
		Assert.AreEqual("hello", result);
	}

	[TestMethod]
	public void IndexOf_WithStringComparisonVariants_ReturnsCorrectResults()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("Hello World Hello");

		// Test different overloads with StringComparison
		Assert.AreEqual(0, semanticString.IndexOf("HELLO", StringComparison.OrdinalIgnoreCase));
		Assert.AreEqual(-1, semanticString.IndexOf("HELLO", StringComparison.Ordinal));
		Assert.AreEqual(12, semanticString.IndexOf("HELLO", 1, StringComparison.OrdinalIgnoreCase));
		Assert.AreEqual(0, semanticString.IndexOf("HELLO", 0, 10, StringComparison.OrdinalIgnoreCase));
	}

	[TestMethod]
	public void LastIndexOf_WithStringComparisonVariants_ReturnsCorrectResults()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("Hello World Hello");

		// Test different overloads with StringComparison
		Assert.AreEqual(12, semanticString.LastIndexOf("HELLO", StringComparison.OrdinalIgnoreCase));
		Assert.AreEqual(-1, semanticString.LastIndexOf("HELLO", StringComparison.Ordinal)); // Case sensitive - should not find "HELLO" in "Hello"
		Assert.AreEqual(0, semanticString.LastIndexOf("HELLO", 10, StringComparison.OrdinalIgnoreCase)); // Correct behavior
		Assert.AreEqual(-1, semanticString.LastIndexOf("HELLO", 10, 5, StringComparison.OrdinalIgnoreCase));
	}

	[TestMethod]
	public void IndexOf_WithStartIndexVariants_ReturnsCorrectResults()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world hello");

		// Test char-based IndexOf with start index
		Assert.AreEqual(2, semanticString.IndexOf('l', 1));
		Assert.AreEqual(14, semanticString.IndexOf('l', 10));
		Assert.AreEqual(14, semanticString.IndexOf('l', 10, 5));

		// Test string-based IndexOf with start index
		Assert.AreEqual(12, semanticString.IndexOf("hello", 5)); // Correct behavior
		Assert.AreEqual(-1, semanticString.IndexOf("hello", 5, 8));
	}

	[TestMethod]
	public void LastIndexOf_WithStartIndexVariants_ReturnsCorrectResults()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello world hello");

		// Test char-based LastIndexOf with start index
		Assert.AreEqual(9, semanticString.LastIndexOf('l', 10)); // 'l' at position 9 in "world"
		Assert.AreEqual(9, semanticString.LastIndexOf('l', 10, 5)); // Actual result is 9

		// Test string-based LastIndexOf with start index
		Assert.AreEqual(0, semanticString.LastIndexOf("hello", 10)); // Correct behavior
		Assert.AreEqual(-1, semanticString.LastIndexOf("hello", 10, 8));
	}

	// Tests for edge cases and error conditions
	[TestMethod]
	public void IndexOfAny_WithNullArray_ThrowsArgumentNullException()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		Assert.ThrowsExactly<ArgumentNullException>(() => semanticString.IndexOfAny(null!));
	}

	[TestMethod]
	public void LastIndexOfAny_WithNullArray_ThrowsArgumentNullException()
	{
		MySemanticString semanticString = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		Assert.ThrowsExactly<ArgumentNullException>(() => semanticString.LastIndexOfAny(null!));
	}

	[TestMethod]
	public void GetHashCode_ConsistentForSameContent()
	{
		MySemanticString semanticString1 = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		MySemanticString semanticString2 = SemanticString<MySemanticString>.Create<MySemanticString>("hello");
		Assert.AreEqual(semanticString1.GetHashCode(), semanticString2.GetHashCode());
	}
}

public record AnotherSemanticString : SemanticString<AnotherSemanticString> { }
