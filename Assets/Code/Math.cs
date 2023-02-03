using UnityEngine;
public static class Math
{
    public static bool IsInRadius(Vector2 position, float radius, Vector2 toCheck)
    {
        float distance = Vector2.Distance(position, toCheck);
        return distance <= radius;
    }
    
    public static Vector2 RandomInRadius(Vector2 center, float radius)
    {
        float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float randomRadius = Random.Range(0, radius);
        float randomX = center.x + randomRadius * Mathf.Cos(randomAngle);
        float randomY = center.y + randomRadius * Mathf.Sin(randomAngle);
        return new Vector2(randomX, randomY);
    }
}