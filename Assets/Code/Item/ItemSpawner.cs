using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour, IResettable
{
    [System.Serializable]
    public struct SpawnItem
    {
        public GameObject item;
        public int chance;
    }

    public LayerMask noSpawn;
    public List<SpawnItem> spawnItems;
    public List<GameObject> currentItems;
    private int totalChance = 0;

    private float currItemSpawnTimer = 0f;

    private void Awake()
    {
        Global.itemSpawner = this;
        foreach (SpawnItem i in spawnItems)
        {
            totalChance += i.chance;
        }
    }

    private void OnEnable()
    {
        InitResettable();
    }

    private void OnDestroy()
    {
        OnDestroyAction();
    }

    private void Update()
    {
        currItemSpawnTimer += Time.deltaTime;

        if (currItemSpawnTimer >= GameConfig.c_ItemSpawnTime)
        {
            currItemSpawnTimer = 0f;
            InstantiateItem();
        }
    }

    /// <summary>
    /// Spawns an item on the map.
    /// </summary>
    private void InstantiateItem()
    {
        if (currentItems.Count >= GameConfig.c_MaxItemCount)
            return;

        int randomResult = Random.Range(0, totalChance);

        int cumulativeChance = 0;
        for (int i = 0; i < spawnItems.Count; i++)
        {
            cumulativeChance += spawnItems[i].chance;
            if (randomResult < cumulativeChance)
            {
                Vector2 loc = GetSpawnLocation();
                if (loc != Vector2.zero)
                    Instantiate(spawnItems[i].item, loc, Quaternion.identity);
                return;
            }
        }
    }

    /// <summary>
    /// Gets location that item can spawn in.
    /// </summary>
    /// <returns></returns>
    private Vector2 GetSpawnLocation()
    {
        Bounds worldBounds = GameConfig.c_WorldBounds;

        int maxIterations = 50;
        while (true)
        {
            maxIterations--;
            Vector2 position = worldBounds.GenerateRandomPositionInBounds();

            Collider2D[] WallCollision = Physics2D.OverlapCircleAll(position, 1.0f, noSpawn);
            if (WallCollision.Length == 0)
            {
                return new Vector2(position.x, position.y);
            }
            if (maxIterations <= 0)
                return Vector2.zero;
        }
    }

    public void ResetObject()
    {
        currentItems.Clear();
        currItemSpawnTimer = 0.0f;
    }

    public void InitResettable()
    {
        Global.resettables.Add(this);
    }

    public void OnDestroyAction()
    {
        Global.resettables.Remove(this);
    }
}
