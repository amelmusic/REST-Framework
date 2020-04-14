using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace X.Core.Extensions
{
    public static class ListExt
    {
        public static async Task ForEachAsync<T>(this IList<T> list, Func<T, Task> func)
        {
            foreach (var value in list)
            {
                await func(value);
            }
        }

        public static void ForEach<T>(this IList<T> list, Action<T> action)
        {
            foreach (var value in list)
            {
                action(value);
            }
        }
    }
}
