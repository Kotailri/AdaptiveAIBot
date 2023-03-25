using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Burst : MonoBehaviour, IResettable
{
    public PlayerType owner;

    [Space(5.0f)]
    public GameObject botBullet;
    public GameObject botBullet_big;
    [Space(5.0f)]
    public GameObject playerBullet;
    public GameObject playerBullet_big;

    private bool maxSizeReached = false;
    private float currSize = 1.0f;
    private Vector2 originalScale;

    private GameObject follow;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    /// <summary>
    /// Set object for burst object to attach to and follow.
    /// </summary>
    /// <param name="toFollow"></param>
    public void SetFollow(GameObject toFollow)
    {
        follow = toFollow;
        AudioManager.instance.PlaySound("zap");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletCollision collidingBullet;
        if (collision.TryGetComponent<BulletCollision>(out collidingBullet))
        {
            AudioManager.instance.PlaySound("zap2");
            if (collidingBullet.playerType == PlayerType.Bot && owner == PlayerType.Player)
                DeflectBullet(collidingBullet.gameObject, collidingBullet.isBig? playerBullet_big : playerBullet);

            if (collidingBullet.playerType == PlayerType.Player && owner == PlayerType.Bot)
                DeflectBullet(collidingBullet.gameObject, collidingBullet.isBig ? botBullet_big : botBullet);
        }

        if (collision.gameObject.CompareTag("Bot") && owner == PlayerType.Player)
            collision.gameObject.GetComponent<Health>().UpdateHealth(-GameConfig.c_BurstDamage);

        if (collision.gameObject.CompareTag("Player") && owner == PlayerType.Bot)
            collision.gameObject.GetComponent<Health>().UpdateHealth(-GameConfig.c_BurstDamage);
    }

    /// <summary>
    /// Change bullet's owner and reverses bullet velocity.
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="newBullet"></param>
    private void DeflectBullet(GameObject bullet, GameObject newBullet)
    {
        GameObject proj = Instantiate(newBullet, bullet.transform.position, bullet.transform.rotation);

        proj.GetComponent<BulletCollision>().playerType = owner;
        proj.GetComponent<BulletCollision>().isBig = bullet.GetComponent<BulletCollision>().isBig;
        proj.GetComponent<Rigidbody2D>().velocity = -bullet.GetComponent<Rigidbody2D>().velocity;
        
        Destroy(bullet);
    }

    void Update()
    {
        transform.position = follow.transform.position;

        if (maxSizeReached)
            currSize -= (Time.deltaTime * GameConfig.c_BurstGrowSpeedMultiplier);
        else
            currSize += (Time.deltaTime * GameConfig.c_BurstGrowSpeedMultiplier); 

        if (currSize >= GameConfig.c_BurstMaxSize)
            maxSizeReached = true;

        transform.localScale = originalScale * currSize;

        if (currSize <= 0)
            Destroy(gameObject);
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
