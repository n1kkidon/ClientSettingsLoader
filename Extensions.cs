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
            if (!values.Any())
                return "";
            string final = String.Empty;
            for (int i = 0; i < values.Count()-1; i++)
                final += values.ElementAt(i)+symbol;
            final += values.ElementAt(^1);
            return final;
        }
        public static string JoinColletion2(this IEnumerable<string> values)
        {
            if (!values.Any())
                return "";
            string final = String.Empty;
            for (int i = 0; i < values.Count() - 1; i++)
                final += values.ElementAt(i) + new string(' ', 27-values.ElementAt(i).Length/4); //wip, a bit scuffed atm
            final += values.ElementAt(^1);
            return final;
        }
    }
}
