// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;

namespace System.Collections
{
    public class ArrayList : List<object>
    {
        public ArrayList()
            : base()
        {
        }

        public ArrayList(int size)
            : base(size)
        {
        }

        public ArrayList(object[] s)
            : base()
        {
            foreach (object o in s) Add(o);
        }

        public ArrayList(int[] startingArray)
            : base()
        {
            foreach (int o in startingArray)
                Add(o);
        }

        public ArrayList(IEnumerable collection):base((IEnumerable<object>) collection)
        {

        }
    }
}