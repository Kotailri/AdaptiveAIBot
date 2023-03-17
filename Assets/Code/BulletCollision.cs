using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour, IResettable
{
    private float DistanceTravelled = 0;
    private Vector2 SpawnLocation;

    public float MaxDist;
    public bool isBig = false;
    public PlayerType playerType = PlayerType.None;

    public int damageBoost = 0;
    private GameObject projectileObstacle;
    private GameObject projectileObstacle2;

    private void Start()
    {
        InitResettable();
        if (TryGetComponent(out CreateProjectileObstacle proj) && playerType == PlayerType.Player)
        {
            projectileObstacle = proj.SpawnObstacle();
            projectileObstacle2 = proj.SpawnObstacle(hasNavMesh: false);
        }
    }

    private void OnEnable()
    {
        SpawnLocation = transform.position;
    }

    private void Update()
    {
        DistanceTravelled = Vector2.Distance(transform.position, SpawnLocation);
        if (DistanceTravelled > GameConfig.c_MaxProjectileDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (DistanceTravelled <= MaxDist)
            return;

        if (collision.gameObject.tag == "Wall")
        {
            if (playerType == PlayerType.Player)
            {
                Global.playertracker.PlayerHitsMissed++;
            }

            else if (playerType == PlayerType.Bot)
            {
                Global.playertracker.BotHitsMissed++;
            }

            Destroy(gameObject);
        }

        if ((collision.gameObject.tag == "Player" && playerType == PlayerType.Bot) ||
            (collision.gameObject.tag == "Bot" && playerType == PlayerType.Player))
        {
            if (playerType == PlayerType.Player)
            {
                if (collision.gameObject.TryGetComponent(out BotShoot botShoot))
                {
                    if (botShoot.detectHits)
                    {
                        Global.playertracker.PlayerCounterHits++;
                    }
                        
                }
                Global.playertracker.PlayerHitsLanded++;
            }

            else if (playerType == PlayerType.Bot)
            {
                Global.playertracker.BotHitsLanded++;
            }

            AudioManager.instance.PlaySound("punch");
            collision.gameObject.GetComponent<Health>().UpdateHealth(isBig? -GameConfig.c_BulletDamage_big : - GameConfig.c_BulletDamage);
            collision.gameObject.GetComponent<Health>().UpdateHealth(-damageBoost);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Destroy(projectileObstacle);
        Destroy(projectileObstacle2);
        OnDestroyAction();
    }

    public void ResetObject()
    {
        Destroy(gameObject);
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
