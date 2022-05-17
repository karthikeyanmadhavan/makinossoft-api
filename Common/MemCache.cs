using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Common
{
    public static class MemCache
    {
        public static Dictionary<int, string> AppMessages { get; set; }

        public static string GetApiMessage(int messageCode)
        {
            if (AppMessages.ContainsKey(messageCode))
            {
                return AppMessages[messageCode];
            }
            return string.Empty;
        }
    }
}
