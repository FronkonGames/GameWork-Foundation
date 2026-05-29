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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Mediator interface for centralizing communication. </summary>
  /// <typeparam name="TMessage">The message type.</typeparam>
  public interface IMediator<in TMessage>
  {
    /// <summary> Send a message to all subscribers. </summary>
    void Send(TMessage message);
  }

  /// <summary> Mediator interface for request-response communication. </summary>
  /// <typeparam name="TRequest">The request type.</typeparam>
  /// <typeparam name="TResponse">The response type.</typeparam>
  public interface IMediator<in TRequest, TResponse>
  {
    /// <summary> Send a request and get a response. </summary>
    TResponse Send(TRequest request);
  }
}
