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
        public static int MaxTwoLab(int x, int y, int labOnePotential, int labTwoPotential)
        {
            int max = Math.Max(x, y);
            if (x == y)
            {
                int minPotential = Math.Min(labOnePotential, labTwoPotential);
                if (x == minPotential)
                    return 1;
                else
                    return 2;
            }
            else
            {
                if (x == max)
                    return 1;
                else
                    return 2;
            }

        }

        public static int MaxThreeLab(int x, int y, int z, int labOnePotential, int labTwoPotential, int labThreePotential)
        {
            
            List<int> labList = new List<int>();
            List<int> maxList = new List<int>();
            List<int> minList = new List<int>();
            List<int> potentialList = new List<int>();
            int max = MoreMath.Max(x, y, z);
            
            if (max == x && max != y && max != z)
                return 1;
            if (max == y && max != x && max != z)
                return 2;
            if (max == z && max != x && max != y)
                return 3;
            
            labList.AddRange(new List<int>{ x, y, z });
            potentialList.AddRange(new List<int> { labOnePotential, labTwoPotential, labThreePotential });
            
            for (int i = 0; i < labList.Count; i++)
            {
                if (labList[i] == max)
                    maxList.Add(i);
            }
  
            for (int i = 0; i < maxList.Count; i++)
                minList.Add(potentialList[maxList.ElementAt(i)]);

            int minimum = minList.Min(); //finds the minimum of potential work for all tied max lists
            int index = minList.IndexOf(minimum);
            return maxList[index] + 1;
        }



        public static int MaxFourLab(int w, int x, int y, int z, int labOnePotential, int labTwoPotential, int labThreePotential, int labFourPotential)
        {

            List<int> labList = new List<int>();
            List<int> maxList = new List<int>();
            List<int> minList = new List<int>();
            List<int> potentialList = new List<int>();
            int max = MoreMath.Max(w, x, y, z);
            if (max == w && max != x && max != y && max != z)
                return 1;
            if (max == x && max != w && max != y && max != z)
                return 2;
            if (max == y && max != w && max != x && max != z)
                return 3;
            if (max == z && max != w && max != x && max != y)
                return 4;

            labList.AddRange(new List<int> { w, x, y, z });
            potentialList.AddRange(new List<int> { labOnePotential, labTwoPotential, labThreePotential, labFourPotential });

            for (int i = 0; i < labList.Count; i++)
            {
                if (labList[i] == max)
                    maxList.Add(i);
            }

            for (int i = 0; i < maxList.Count; i++)
            {
                minList.Add(potentialList[maxList.ElementAt(i)]);
            }
            int minimum = minList.Min(); //finds the minimum of potential work for all tied max lists
            int index = minList.IndexOf(minimum);
            return maxList[index] + 1;
        }


        public static int MaxFiveLab(int v, int w, int x, int y, int z, int labOnePotential, int labTwoPotential, int labThreePotential, int labFourPotential, int labFivePotential)
        {

            List<int> labList = new List<int>();
            List<int> maxList = new List<int>();
            List<int> minList = new List<int>();
            List<int> potentialList = new List<int>();
            int max = MoreMath.Max(v, w, x, y, z);
            if (max == v && max != w && max != x && max != y && max != z)
                return 1;
            if (max == w && max != v && max != x && max != y && max != z)
                return 2;
            if (max == x && max != v && max != w && max != y && max != z)
                return 3;
            if (max == y && max != v && max != w && max != x && max != z)
                return 4;
            if (max == z && max != v && max != w && max != x && max != y)
                return 5;

            labList.AddRange(new List<int> { v, w, x, y, z });
            potentialList.AddRange(new List<int> { labOnePotential, labTwoPotential, labThreePotential, labFourPotential, labFivePotential });

            for (int i = 0; i < labList.Count; i++)
            {
                if (labList[i] == max)
                    maxList.Add(i);
            }

            for (int i = 0; i < maxList.Count; i++)
            {
                minList.Add(potentialList[maxList.ElementAt(i)]);
            }
            int minimum = minList.Min(); //finds the minimum of potential work for all tied max lists
            int index = minList.IndexOf(minimum);
            return maxList[index] + 1;
        }

        public static int MaxSixLab(int u, int v, int w, int x, int y, int z, int labOnePotential, int labTwoPotential, int labThreePotential, int labFourPotential, int labFivePotential, int labSixPotential)
        {

            List<int> labList = new List<int>();
            List<int> maxList = new List<int>();
            List<int> minList = new List<int>();
            List<int> potentialList = new List<int>();
            int max = MoreMath.Max(u, v, w, x, y, z);
            if (max == u && max != v && max != w && max != x && max != y && max != z)
                return 1;
            if (max == v && max != u && max != w && max != x && max != y && max != z)
                return 2;
            if (max == w && max != u && max != v && max != x && max != y && max != z)
                return 3;
            if (max == x && max != u && max != v && max != w && max != y && max != z)
                return 4;
            if (max == y && max != u && max != v && max != w && max != x && max != z)
                return 5;
            if (max == z && max != u && max != v && max != w && max != x && max != y)
                return 6;

            labList.AddRange(new List<int> { u, v, w, x, y, z });
            potentialList.AddRange(new List<int> { labOnePotential, labTwoPotential, labThreePotential, labFourPotential, labFivePotential, labSixPotential });

            for (int i = 0; i < labList.Count; i++)
            {
                if (labList[i] == max)
                    maxList.Add(i);
            }

            for (int i = 0; i < maxList.Count; i++)
            {
                minList.Add(potentialList[maxList.ElementAt(i)]);
            }
            int minimum = minList.Min(); //finds the minimum of potential work for all tied max lists
            int index = minList.IndexOf(minimum);
            return maxList[index] + 1;
        }

        public static int MaxSevenLab(List<int> shiftsRemaining, List<int> labPotentials)
        {
            List<int> maxList = new List<int>();
            List<int> minList = new List<int>();
            List<int> potentialList = new List<int>();
            int max = shiftsRemaining.Max();
            
            for (int i = 0; i < shiftsRemaining.Count; i++)
            {
                if (shiftsRemaining[i] == max)
                    maxList.Add(i);
            }
            if (maxList.Count == 1)
            {
                int labToChoose = maxList[0];
                return labToChoose + 1;
            }
                
            for (int i = 0; i < maxList.Count; i++)
            {
                minList.Add(labPotentials[maxList.ElementAt(i)]);
            }
            //FIX: if minimums are also equal, pick lab with least amount of items in the build order
                    //if minlist.count > 1, put in the one with the lowest in buildorder
            int minimum = minList.Min(); //finds the minimum of potential work for all tied max lists
            int index = minList.IndexOf(minimum);
            return maxList[index] + 1;
            
        }

        public static int Max(int x, int y, int z)
        {
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

        public static int Min(int x, int y, int z)
        {
            return Math.Min(x, Math.Min(y, z));
        }

        public static int Min(int w, int x, int y, int z)
        {
            return Math.Min(w, Math.Min(x, Math.Min(y, z)));
        }

        public static int Min(int v, int w, int x, int y, int z)
        {
            return Math.Min(v, Math.Min(w, Math.Min(x, Math.Min(y, z))));
        }

        public static int Min(int u, int v, int w, int x, int y, int z)
        {
            return Math.Min(u, Math.Min(v, Math.Min(w, Math.Min(x, Math.Min(y, z)))));
        }

        public static int Min(int t, int u, int v, int w, int x, int y, int z)
        {
            return Math.Min(t, Math.Min(u, Math.Min(v, Math.Min(w, Math.Min(x, Math.Min(y, z))))));
        }
    }
}
