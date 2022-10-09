using System;

namespace OrchestratoR.Core.Helpers
{
    public  static class ProcessUnitStringComparer
    {
        public static bool Equals(string a, string b) => string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
    }
}