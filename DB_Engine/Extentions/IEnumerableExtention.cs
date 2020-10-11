using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB_Engine.Extentions
{
    public static class IEnumerableExtention
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static IEnumerable<Tuple<T1, T2>> CrossJoin<T1, T2>(this IEnumerable<T1> sequence1, IEnumerable<T2> sequence2)
        {
            return sequence1.SelectMany(t1 => sequence2.Select(t2 => Tuple.Create(t1, t2)));
        }

    }
}
