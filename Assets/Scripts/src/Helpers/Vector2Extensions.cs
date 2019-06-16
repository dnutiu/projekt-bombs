using UnityEngine;

namespace src.Helpers
{
    public static class Vector2Extensions
    {
        /*
         * Normalizes a vector2 to maximum speed and allow only movement in one axis at a time.
         */
        public static Vector2 NormalizeToCross(this Vector2 vector)
        {
            var x = Mathf.Round(vector.x);
            var y = Mathf.Round(vector.y);
            if (Mathf.Abs(y) > Mathf.Abs(x))
            {
                x = 0;
            }
            else
            {
                y = 0;
            }
            return new Vector2(x, y);
        }
    }
}