using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Experiments.Application.Helpers
{
    public static class IListExtensions
    {
        public static IEnumerable<IList<T>> SplitList<T>(this List<T> locations, int nSize = 300)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
