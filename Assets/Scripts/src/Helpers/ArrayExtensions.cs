using UnityEngine;
using Random = UnityEngine.Random;

namespace src.Helpers
{
    public static class ArrayExtensions
    {
        public static T ChoseRandom<T>(this T[] arr)
        {
            var randomIndex = Mathf.FloorToInt(Random.Range(0, arr.Length));
            return arr[randomIndex];
        }
    }
}