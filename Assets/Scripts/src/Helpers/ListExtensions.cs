using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace src.Helpers
{
    public static class ListExtensions
    {
        public static void ShuffleList(this IList list)
        {
            const int min = 0;
            var max = list.Count - 1;
            for (var i = min; i < max; i++)
            {
                var randomPos = Random.Range(min, max);

                /* Swap elements in list */
                var aux = list[randomPos];
                list[randomPos] = list[i];
                list[i] = aux;
            }
        }

        public static T PopRandom<T>(this IList<T> list)
        {
            var randomIndex = Random.Range(0, list.Count - 1);
            var elem = list[randomIndex];
            list.RemoveAt(randomIndex);
            return elem;
        }
        
        public static T PeekRandom<T>(this IList<T> list)
        {
            var randomIndex = Random.Range(0, list.Count - 1);
            return list[randomIndex];
        }
    }
}