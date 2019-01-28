using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Persistence
{
    public static class CompareExtensions
    {
        private static bool DefaultEquality<T>(T left, T right) => left.Equals(right);

        public static bool CompareWithNull<T>(this T left, T right) => left.CompareWithNull(right, DefaultEquality);
        public static bool CompareWithNull<T>(this T left, T right, Func<T, T, bool> bothHaveValueDelegate)
        {
            if (left == null && right != null)
                return false;
            if (left != null)
            {
                if (right == null)
                    return false;
                if (!bothHaveValueDelegate(left, right))
                    return false;
            }
            return true;
        }
        
        public static bool CompareTwoLists<T>(this List<T> left, List<T> right) => CompareWithNull(left, right, DefaultListComparison);
        private static bool DefaultListComparison<T>(List<T> left, List<T> right)
        {
            if (left.Count != right.Count)
                return false;
            for (int i = 0; i < left.Count; i++)
                if (!left[i].CompareWithNull(right[i]))
                    return false;
            return true;
        }
    }
}
