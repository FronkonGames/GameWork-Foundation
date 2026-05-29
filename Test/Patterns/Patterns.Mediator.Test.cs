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
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary> Patterns tests. </summary>
public partial class PatternsTests
{
  private class TestMediator : IMediator<string>
  {
    public string LastMessage;

    public void Send(string message) => LastMessage = message;
  }

  private class TestRequestMediator : IMediator<int, string>
  {
    public string Send(int request) => $"Response:{request}";
  }

  /// <summary> Mediator test. </summary>
  [UnityTest]
  public IEnumerator Mediator()
  {
    TestMediator mediator = new();
    mediator.Send("Hello");
    Assert.AreEqual("Hello", mediator.LastMessage);

    mediator.Send("World");
    Assert.AreEqual("World", mediator.LastMessage);

    TestRequestMediator requestMediator = new();
    Assert.AreEqual("Response:42", requestMediator.Send(42));
    Assert.AreEqual("Response:0", requestMediator.Send(0));

    yield return null;
  }
}
