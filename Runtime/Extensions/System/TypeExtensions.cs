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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Type extensions. </summary>
  public static class TypeExtensions
  {
    /// <summary> Finds all non-abstract, non-interface implementations of a type in loaded assemblies. </summary>
    /// <param name="type"> The base type or interface to search for implementations of. </param>
    /// <returns> A collection of concrete types that inherit from or implement the given type. </returns>
    public static IEnumerable<Type> FindImplementations(this Type type)
    {
      return AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(assembly =>
        {
          try
          {
            return assembly.GetTypes();
          }
          catch (ReflectionTypeLoadException)
          {
            return Array.Empty<Type>();
          }
        })
        .Where(t => !t.IsAbstract && !t.IsInterface && type.IsAssignableFrom(t));
    }

    /// <summary> Returns true if the type implements the specified interface. </summary>
    /// <typeparam name="T"> The interface type to check for. </typeparam>
    /// <param name="type"> The type to inspect. </param>
    /// <returns> True if the type implements the interface; otherwise, false. </returns>
    public static bool ImplementsInterface<T>(this Type type) where T : class
    {
      if (!typeof(T).IsInterface)
        return false;

      return type.GetInterfaces().Contains(typeof(T));
    }

    /// <summary> Returns the friendly name of the type (generic types show type parameters). </summary>
    /// <param name="type"> The type to get the friendly name for. </param>
    /// <returns> A human-readable string representation of the type. </returns>
    public static string GetFriendlyName(this Type type)
    {
      if (type == null)
        return "null";

      if (type == typeof(void))
        return "void";

      if (!type.IsGenericType)
        return type.Name;

      string baseName = type.Name.Substring(0, type.Name.IndexOf('`'));
      string[] genericArgs = type.GetGenericArguments().Select(arg => GetFriendlyName(arg)).ToArray();
      return $"{baseName}<{string.Join(", ", genericArgs)}>";
    }
  }
}
