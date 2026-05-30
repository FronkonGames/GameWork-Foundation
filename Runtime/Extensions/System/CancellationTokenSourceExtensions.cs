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
using System.Threading;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> CancellationTokenSource extensions. </summary>
  public static class CancellationTokenSourceExtensions
  {
    /// <summary> Cancels and disposes the token source if not null. </summary>
    public static void CancelAndDispose(this CancellationTokenSource cancellation)
    {
      if (cancellation == null)
        return;

      if (cancellation.IsCancellationRequested == false)
        cancellation.Cancel();

      cancellation.Dispose();
    }

    /// <summary> Cancels and disposes the current source, then returns a new one. </summary>
    public static CancellationTokenSource Recreate(this CancellationTokenSource cancellation)
    {
      cancellation.CancelAndDispose();
      return new CancellationTokenSource();
    }

    /// <summary> Cancels and disposes the current source, then returns a linked one. </summary>
    public static CancellationTokenSource Recreate(this CancellationTokenSource cancellation, params CancellationToken[] tokens)
    {
      cancellation.CancelAndDispose();
      return CancellationTokenSource.CreateLinkedTokenSource(tokens);
    }
  }
}
