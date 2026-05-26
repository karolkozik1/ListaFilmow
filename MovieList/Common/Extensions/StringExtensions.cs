using Common.CommonData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNumeric(this string value)
        {
            return int.TryParse(value, out _);
        }
    }
}
