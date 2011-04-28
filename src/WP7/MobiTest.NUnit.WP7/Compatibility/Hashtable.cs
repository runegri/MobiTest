// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;

namespace System.Collections
{
    public class Hashtable : System.Collections.Generic.Dictionary<object, object>
    {
        public new object this[object key]
        {
            get
            {
                if (!ContainsKey(key))
                    return null;
                else return base[key];
            }
            set
            {
                base[key] = value;
            }
        }
    }
}