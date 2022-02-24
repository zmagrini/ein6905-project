using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    public static class MoreMath
    {
        // This method only exists for consistency, so you can *always* call
        // MoreMath.Max instead of alternating between MoreMath.Max and Math.Max
        // depending on your argument count.
        public static int Max(int x, int y)
        {
            return Math.Max(x, y);
        }

        public static int Max(int x, int y, int z)
        {
            // Or inline it as x < y ? (y < z ? z : y) : (x < z ? z : x);
            // Time it before micro-optimizing though!
            return Math.Max(x, Math.Max(y, z));
        }

        public static int Max(int w, int x, int y, int z)
        {
            return Math.Max(w, Math.Max(x, Math.Max(y, z)));
        }

        public static int Max(int v, int w, int x, int y, int z)
        {
            return Math.Max(v, Math.Max(w, Math.Max(x, Math.Max(y, z))));
        }

        public static int Max(int u, int v, int w, int x, int y, int z)
        {
            return Math.Max(u, Math.Max(v, Math.Max(w, Math.Max(x, Math.Max(y, z)))));
        }

        public static int Max(int t, int u, int v, int w, int x, int y, int z)
        {
            return Math.Max(t, Math.Max(u, Math.Max(v, Math.Max(w, Math.Max(x, Math.Max(y, z))))));
        }

        public static int Max(params int[] values)
        {
            return Enumerable.Max(values);
        }
    }
}
