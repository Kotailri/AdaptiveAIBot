using System.Collections;
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
                Instantiate(spawnItems[i].item, GetSpawnLocation(), Quaternion.identity);
                return;
            }
        }
    }

    private Vector2 GetSpawnLocation()
    {
        float minDistanceToPlayer = 5.0f;
        Bounds worldBounds = GameConfig.c_WorldBounds;

        while (true)
        {
            Vector2 position = worldBounds.GenerateRandomPositionInBounds();

            bool isSpawnOnBot = Math.IsInRadius(Global.playertracker.GetBotPosition(), minDistanceToPlayer, position);
            bool isSpawnOnPlayer = Math.IsInRadius(Global.playertracker.GetPlayerPosition(), minDistanceToPlayer, position);

            bool isSpawnOnItem = false;
            foreach (GameObject obj in currentItems)
            {
                if (Math.IsInRadius(obj.transform.position, 0.4f, position))
                    isSpawnOnItem = true;
            }

            Collider2D WallCollision = Physics2D.OverlapCircle(position, 0.4f, LayerMask.GetMask("Walls"));
            if (WallCollision == null && !isSpawnOnBot && !isSpawnOnPlayer && !isSpawnOnItem)
            {
                return new Vector2(position.x, position.y);
            }
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
