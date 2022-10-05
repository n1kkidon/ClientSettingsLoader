using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSettingsLoader
{
    public static class Extensions
    {
        /// <summary>
        /// Joins a collection of strings into a single string with a specified symbol as a separator
        /// </summary>
        /// <param name="values"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static string JoinColletion(this IEnumerable<string> values, string symbol)
        {
            string final = String.Empty;
            for (int i = 0; i < values.Count(); i++)
            {
                final += values.ElementAt(i);
                if (i < values.Count() - 1)
                    final += symbol;
            }
            return final;
        }
    }
}
