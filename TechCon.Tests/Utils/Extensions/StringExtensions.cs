using System;
using System.Collections.Generic;
using System.Text;

namespace TechCon.Tests.Utils.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }
        public static bool ToBool(this string str)
        {
            return bool.Parse(str);
        }

    }
}
