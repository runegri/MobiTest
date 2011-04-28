using System;
using System.Collections.Generic;

namespace System.Collections.Specialized
{
  /// <summary>
  /// Adds an ListDictionary type that behaves like the legacy ArrayList so
  /// NUnit can continue using this type.
  /// </summary>
  public class ListDictionary : Dictionary<object, object>
  {
    public new object this[ object key ]
    {
      get
      {
        if( !ContainsKey( key ) )
          return null;
        else
          return base[ key ];
      }
      set
      {
        base[ key ] = value;
      }
    }
  }
}