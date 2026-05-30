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
using NUnit.Framework;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Option and Holder tests. </summary>
  public class OptionTests
  {
    /// <summary> Option disabled mode returns default value. </summary>
    [Test]
    public void Option_Disabled_ReturnsDefaultValue()
    {
      Option<int, IntHolder> option = new Option<int, IntHolder>();

      Assert.AreEqual(0, option.Value);
      Assert.AreEqual(default(bool), new Option<bool, BoolHolder>().Value);
      Assert.AreEqual(default(float), new Option<float, FloatHolder>().Value);
      Assert.AreEqual(default(string), new Option<string, StringHolder>().Value);
    }

    /// <summary> Option LocalValue mode returns local value. </summary>
    [Test]
    public void Option_LocalValue_ReturnsLocalValue()
    {
      Option<int, IntHolder> option = new Option<int, IntHolder>(42);

      Assert.AreEqual(42, option.Value);
      Assert.AreEqual(ValueHolderMode.LocalValue, option.Mode);
      Assert.IsTrue(option.Enabled);
    }

    /// <summary> Option SetDisabled sets mode to Disabled. </summary>
    [Test]
    public void Option_SetDisabled_SetsModeToDisabled()
    {
      Option<int, IntHolder> option = new Option<int, IntHolder>(42);

      Assert.IsTrue(option.Enabled);

      option.SetDisabled();

      Assert.IsFalse(option.Enabled);
      Assert.AreEqual(ValueHolderMode.Disabled, option.Mode);
      Assert.AreEqual(0, option.Value);
    }

    /// <summary> Option SetLocalValue sets local value and mode. </summary>
    [Test]
    public void Option_SetLocalValue_SetsLocalValueAndMode()
    {
      Option<int, IntHolder> option = new Option<int, IntHolder>();

      option.SetLocalValue(99);

      Assert.AreEqual(99, option.Value);
      Assert.AreEqual(99, option.LocalValue);
      Assert.AreEqual(ValueHolderMode.LocalValue, option.Mode);
      Assert.IsTrue(option.Enabled);
    }

    /// <summary> Option constructor with value sets LocalValue mode. </summary>
    [Test]
    public void Option_ConstructorWithValue_SetsLocalValueMode()
    {
      Option<int, IntHolder> optionInt = new Option<int, IntHolder>(123);
      Option<bool, BoolHolder> optionBool = new Option<bool, BoolHolder>(true);
      Option<float, FloatHolder> optionFloat = new Option<float, FloatHolder>(3.14f);
      Option<string, StringHolder> optionString = new Option<string, StringHolder>("hello");

      Assert.AreEqual(123, optionInt.Value);
      Assert.AreEqual(ValueHolderMode.LocalValue, optionInt.Mode);

      Assert.AreEqual(true, optionBool.Value);
      Assert.AreEqual(ValueHolderMode.LocalValue, optionBool.Mode);

      Assert.AreEqual(3.14f, optionFloat.Value);
      Assert.AreEqual(ValueHolderMode.LocalValue, optionFloat.Mode);

      Assert.AreEqual("hello", optionString.Value);
      Assert.AreEqual(ValueHolderMode.LocalValue, optionString.Mode);
    }

    /// <summary> Option TryGetValue when disabled returns false. </summary>
    [Test]
    public void Option_TryGetValue_Disabled_ReturnsFalse()
    {
      Option<int, IntHolder> option = new Option<int, IntHolder>();

      Assert.IsFalse(option.TryGetValue(out int value));
      Assert.AreEqual(0, value);
    }

    /// <summary> Option TryGetValue with local value returns true and value. </summary>
    [Test]
    public void Option_TryGetValue_LocalValue_ReturnsTrueAndValue()
    {
      Option<int, IntHolder> option = new Option<int, IntHolder>(55);

      Assert.IsTrue(option.TryGetValue(out int value));
      Assert.AreEqual(55, value);
    }

    /// <summary> Option implicit conversion returns value. </summary>
    [Test]
    public void Option_ImplicitConversion_ReturnsValue()
    {
      Option<int, IntHolder> option = new Option<int, IntHolder>(77);

      int implicitValue = option;

      Assert.AreEqual(77, implicitValue);
    }

    /// <summary> Option ToString when disabled returns "Disabled". </summary>
    [Test]
    public void Option_ToString_Disabled_ReturnsDisabled()
    {
      Option<int, IntHolder> option = new Option<int, IntHolder>();

      Assert.AreEqual("Disabled", option.ToString());
    }

    /// <summary> Option ToString with local value returns "Local: {value}". </summary>
    [Test]
    public void Option_ToString_LocalValue_ReturnsLocalFormat()
    {
      Option<int, IntHolder> optionInt = new Option<int, IntHolder>(10);
      Option<string, StringHolder> optionString = new Option<string, StringHolder>("test");

      Assert.AreEqual("Local: 10", optionInt.ToString());
      Assert.AreEqual("Local: test", optionString.ToString());
    }

    /// <summary> BoolHolder GetValue/SetValue works correctly. </summary>
    [Test]
    public void BoolHolder_GetSetValue_WorksCorrectly()
    {
      BoolHolder holder = ScriptableObject.CreateInstance<BoolHolder>();

      Assert.IsFalse(holder.GetValue());

      holder.SetValue(true);
      Assert.IsTrue(holder.GetValue());

      holder.SetValue(false);
      Assert.IsFalse(holder.GetValue());
    }

    /// <summary> IntHolder GetValue/SetValue works correctly. </summary>
    [Test]
    public void IntHolder_GetSetValue_WorksCorrectly()
    {
      IntHolder holder = ScriptableObject.CreateInstance<IntHolder>();

      Assert.AreEqual(0, holder.GetValue());

      holder.SetValue(42);
      Assert.AreEqual(42, holder.GetValue());

      holder.SetValue(-100);
      Assert.AreEqual(-100, holder.GetValue());
    }

    /// <summary> FloatHolder GetValue/SetValue works correctly. </summary>
    [Test]
    public void FloatHolder_GetSetValue_WorksCorrectly()
    {
      FloatHolder holder = ScriptableObject.CreateInstance<FloatHolder>();

      Assert.AreEqual(0f, holder.GetValue());

      holder.SetValue(3.14f);
      Assert.AreEqual(3.14f, holder.GetValue());

      holder.SetValue(-2.5f);
      Assert.AreEqual(-2.5f, holder.GetValue());
    }

    /// <summary> StringHolder GetValue/SetValue works correctly. </summary>
    [Test]
    public void StringHolder_GetSetValue_WorksCorrectly()
    {
      StringHolder holder = ScriptableObject.CreateInstance<StringHolder>();

      Assert.AreEqual(string.Empty, holder.GetValue());

      holder.SetValue("hello");
      Assert.AreEqual("hello", holder.GetValue());

      holder.SetValue(string.Empty);
      Assert.AreEqual(string.Empty, holder.GetValue());
    }
  }
}
