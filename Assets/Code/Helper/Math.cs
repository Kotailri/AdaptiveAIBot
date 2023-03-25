using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for extra math utility
/// </summary>
public static class Math
{
    /// <summary>
    /// Returns true if toCheck is within the radius of a position.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    /// <param name="toCheck"></param>
    /// <returns></returns>
    public static bool IsInRadius(Vector2 position, float radius, Vector2 toCheck)
    {
        float distance = Vector2.Distance(position, toCheck);
        return distance <= radius;
    }
    
    /// <summary>
    /// Returns a random vector within a radius of a specified center point.
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static Vector2 RandomInRadius(Vector2 center, float radius)
    {
        float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float randomRadius = Random.Range(0, radius);
        float randomX = center.x + randomRadius * Mathf.Cos(randomAngle);
        float randomY = center.y + randomRadius * Mathf.Sin(randomAngle);
        return new Vector2(randomX, randomY);
    }

    /// <summary>
    /// Returns the average of a list of floats, returns 0 if list is empty.
    /// </summary>
    /// <param name="floats"></param>
    /// <returns></returns>
    public static float AverageFloat(List<float> floats)
    {
        if (floats.Count == 0)
            return 0.0f;

        float num = 0;
        foreach (float f in floats)
        {
            num += f;
        }
        return num / (float)floats.Count;
    }
}
