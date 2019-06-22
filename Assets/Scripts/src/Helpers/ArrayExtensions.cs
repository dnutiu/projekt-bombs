using System.Collections.Generic;
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

        public static T ChoseRandomExcept<T>(this T[] arr, T exceptValue)
        {
            T value;
            do
            {
                var randomIndex = Mathf.FloorToInt(Random.Range(0, arr.Length));
                value = arr[randomIndex];

            } while (exceptValue.Equals(value));
            return value;
        }

        public static T ChoseRandomExcept<T>(this T[] arr, List<T> exceptValue)
        {
            T value;
            do
            {
                var randomIndex = Mathf.FloorToInt(Random.Range(0, arr.Length));
                value = arr[randomIndex];

            } while (exceptValue.Contains(value));
            return value;
        }
    }
}