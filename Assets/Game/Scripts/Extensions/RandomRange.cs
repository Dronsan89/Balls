using UnityEngine;

namespace Extensions
{
    public static class RandomRange
    {
        public static float GetRandomRange(this Vector2 rage) => Random.Range(rage.x, rage.y);
    }
}
