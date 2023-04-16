using UnityEngine;

namespace Siege.Utility
{
    public static class Utility
    {
        public static class NameComparisons
        {
            public delegate bool Comparison(string a, string b);
            
            public static bool Literal(string a, string b) => a == b;
            public static bool Simple(string a, string b) => SimplifyName(a) == SimplifyName(b);
        }

        public static string SimplifyName(string name) => name.Trim().ToLower().Replace(" ", "");
    }
}
