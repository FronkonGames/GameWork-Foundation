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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Type extensions. </summary>
  public static class TypeExtensions
  {
    private static readonly HashSet<Type> UnitySerializablePrimitiveTypes = new()
    {
      typeof(bool), typeof(byte), typeof(sbyte), typeof(char), typeof(double), typeof(float), typeof(int),
      typeof(uint), typeof(long), typeof(ulong), typeof(short), typeof(ushort), typeof(string)
    };

    private static readonly HashSet<Type> UnitySerializableBuiltinTypes = new()
    {
      typeof(Vector2), typeof(Vector3), typeof(Vector4), typeof(Rect), typeof(Quaternion), typeof(Matrix4x4),
      typeof(Color), typeof(Color32), typeof(LayerMask), typeof(AnimationCurve), typeof(Gradient),
      typeof(RectOffset), typeof(GUIStyle)
    };

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

    /// <summary> Finds a field recursively in the type hierarchy using a dot-separated path. </summary>
    /// <param name="parentType"> The type to start searching from. </param>
    /// <param name="path"> Dot-separated field path (for example, "parentField.nestedField"). </param>
    /// <param name="flags"> Binding flags used for the search. </param>
    /// <returns> The field info if found; otherwise, null. </returns>
    public static FieldInfo GetFieldAtPath(this Type parentType, string path,
      BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
    {
      FieldInfo field = null;

      foreach (string part in path.Split('.'))
      {
        field = parentType.GetFieldRecursive(part, flags);

        if (field == null)
          return null;

        parentType = field.FieldType;
      }

      return field;
    }

    /// <summary> Searches for a field in the type and all base types. </summary>
    /// <param name="type"> The type to search. </param>
    /// <param name="name"> The field name. </param>
    /// <param name="flags"> Binding flags used for the search. </param>
    /// <returns> The field if found; otherwise, null. </returns>
    public static FieldInfo GetFieldRecursive(this Type type, string name, BindingFlags flags)
    {
      while (true)
      {
        FieldInfo field = type.GetField(name, flags);

        if (field != null)
          return field;

        Type baseType = type.BaseType;

        if (baseType == null)
          return null;

        type = baseType;
      }
    }

    /// <summary> Returns public and SerializeField-marked instance fields. </summary>
    /// <param name="type"> The type to inspect. </param>
    /// <returns> Serializable fields declared on the type. </returns>
    public static IEnumerable<FieldInfo> GetSerializedFields(this Type type)
    {
      const BindingFlags instanceFilter = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
      FieldInfo[] instanceFields = type.GetFields(instanceFilter);

      return instanceFields.Where(field => field.IsPublic == true || field.HasAttribute<SerializeField>());
    }

    /// <summary> Returns true if the type is nullable. </summary>
    /// <param name="type"> The type to check. </param>
    /// <returns> True when the type accepts null. </returns>
    public static bool IsNullable(this Type type)
      => type.IsValueType == false || Nullable.GetUnderlyingType(type) != null;

    /// <summary> Returns true when the type inherits from an open generic base type. </summary>
    /// <param name="typeToCheck"> The type to inspect. </param>
    /// <param name="generic"> The open generic base type (for example, typeof(Base&lt;&gt;)). </param>
    /// <returns> True when the type derives from the generic definition. </returns>
    public static bool IsSubclassOfRawGeneric(this Type typeToCheck, Type generic)
    {
      while (typeToCheck != null && typeToCheck != typeof(object))
      {
        Type current = typeToCheck.IsGenericType == true ? typeToCheck.GetGenericTypeDefinition() : typeToCheck;

        if (generic == current)
          return true;

        typeToCheck = typeToCheck.BaseType;
      }

      return false;
    }

    /// <summary> Returns true when the type inherits from the given base type, including open generics. </summary>
    /// <param name="typeToCheck"> The type to inspect. </param>
    /// <param name="baseType"> The base type or open generic base type. </param>
    /// <returns> True when inheritance is detected. </returns>
    public static bool InheritsFrom(this Type typeToCheck, Type baseType)
    {
      bool subClassOfRawGeneric = baseType.IsGenericType == true && typeToCheck.IsSubclassOfRawGeneric(baseType);

      return baseType.IsAssignableFrom(typeToCheck) == true || subClassOfRawGeneric == true;
    }

    /// <summary> Returns true when Unity can serialize the type in the inspector. </summary>
    /// <param name="type"> The type to check. </param>
    /// <returns> True when the type is Unity-serializable. </returns>
    public static bool IsUnitySerializable(this Type type)
    {
      static bool IsSystemType(Type typeToCheck) =>
        typeToCheck.Namespace != null && typeToCheck.Namespace.StartsWith("System");

      static bool IsCustomSerializableType(Type typeToCheck) =>
        typeToCheck.IsSerializable == true && IsSystemType(typeToCheck) == false;

      if (type.IsInterface == true || (type.IsAbstract == true && type.IsSealed == true))
        return false;

      if (IsCustomSerializableType(type) == true)
        return true;

      if (type.InheritsFrom(typeof(UnityEngine.Object)) == true && type.IsGenericTypeDefinition == false)
        return true;

      if (type.IsEnum == true)
        return true;

      return UnitySerializablePrimitiveTypes.Contains(type) == true || UnitySerializableBuiltinTypes.Contains(type) == true;
    }

    /// <summary> Returns true when the type declares no members other than the implicit default constructor. </summary>
    /// <param name="type"> The type to check. </param>
    /// <returns> True when the type is empty. </returns>
    public static bool IsEmpty(this Type type)
      => type.GetMembers((BindingFlags)(-1)).Length == 1;

    /// <summary> Returns the assembly name without version or culture metadata. </summary>
    /// <param name="type"> The type whose assembly name is returned. </param>
    /// <returns> Short assembly name (for example, "mscorlib"). </returns>
    public static string GetShortAssemblyName(this Type type)
    {
      string assemblyFullName = type.Assembly.FullName;
      int commaIndex = assemblyFullName.IndexOf(',');

      return assemblyFullName.Substring(0, commaIndex);
    }
  }
}
