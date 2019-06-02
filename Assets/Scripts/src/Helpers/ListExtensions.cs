using System.Collections;
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
                var randomPos = Mathf.FloorToInt(Random.Range(min, max));
                
                /* Swap elements in list */
                var aux = list[randomPos];
                list[randomPos] = list[i];
                list[i] = aux;
            }
        }
    }
}