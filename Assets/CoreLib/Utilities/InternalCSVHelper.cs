using System.Collections.Generic;
using System.IO;
using System.Text;
using CoreLib.Complex_Types;

namespace CoreLib.Utilities
{
    public static class InternalCSVHelper
    {
        public static void Write(List<Pair<string, string>> pairs, string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var pair in pairs)
            {
                sb.AppendLine($"{pair.Item1},{pair.Item2}");
            }

            File.WriteAllText(path, sb.ToString());
        }
    }
}