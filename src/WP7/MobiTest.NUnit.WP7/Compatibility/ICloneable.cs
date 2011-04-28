using System;

namespace System
{
  /// <summary>
  /// Silverlight does not implement ICloneable
  /// </summary>
  public interface ICloneable
  {
    object Clone();
  }
}
