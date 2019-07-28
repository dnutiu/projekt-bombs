using UnityEngine;

namespace src.Helpers
{
    public class Count
    {
        private readonly int _min;
        private readonly int _max;

        public Count(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public int RandomIntRange()
        {
            return Mathf.FloorToInt(Random.Range(_min, _max));
        }
    }
}