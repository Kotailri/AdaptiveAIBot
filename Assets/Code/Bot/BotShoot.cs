using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShoot : MonoBehaviour, IAction
{
    public GameObject projectile;
    public GameObject projectile_big;

    private void ShootSmall()
    {
        GameObject proj = Instantiate(projectile, transform.position, transform.rotation);
        proj.GetComponent<BulletCollision>().playerType = PlayerType.Bot;
        proj.GetComponent<Rigidbody2D>().velocity = (proj.transform.up).normalized * -GameConfig.c_ProjectileSpeed;
    }

    private void ShootBig()
    {
        StartCoroutine(WindUpShoot());
    }

    private IEnumerator WindUpShoot()
    {
        GetComponent<SpriteRenderer>().color = Color.black;

        yield return new WaitForSecondsRealtime(GameConfig.c_WindupDelay);

        GetComponent<SpriteRenderer>().color = new Color(1,0.36f, 0.315f, 1);

        GameObject proj = Instantiate(projectile_big, transform.position, transform.rotation);
        proj.GetComponent<BulletCollision>().playerType = PlayerType.Bot;
        proj.GetComponent<Rigidbody2D>().velocity = (proj.transform.up).normalized * -GameConfig.c_ProjectileSpeed;
        proj.GetComponent<BulletCollision>().isBig = true;
    }

    private void Update()
    {
        // TESTING
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShootSmall();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ShootBig();
        }
    }

    bool IAction.CheckAction()
    {
        throw new System.NotImplementedException();
    }

    void IAction.ExecuteAction()
    {
        throw new System.NotImplementedException();
    }

    float IAction.GetActionChance()
    {
        throw new System.NotImplementedException();
    }
}
