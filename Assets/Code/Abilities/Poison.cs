using UnityEngine;
using UnityEngine.AI;

public class Poison : MonoBehaviour, IResettable
{
    public PlayerType owner;
    private Collider2D playerInLava;

    private void OnEnable()
    {
        Destroy(gameObject, GameConfig.c_PoisonExpireTime);
        if (owner == PlayerType.Player)
        {
            if (Global.difficultyLevel < Random.Range(1, 10))
            {
                GetComponent<NavMeshObstacle>().enabled = false;
            }
        }
        AudioManager.instance.PlaySound("bubbles");
    }

    private void Start()
    {
        InitResettable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (owner == PlayerType.Player && collision.gameObject.CompareTag("Bot") 
            || owner == PlayerType.Bot && collision.gameObject.CompareTag("Player"))
        {
            playerInLava = collision;
            collision.gameObject.GetComponent<Health>().UpdateHealth(-GameConfig.c_PoisonEnterDamage);
            InvokeRepeating(nameof(DamagePlayer), GameConfig.c_PoisonTimer, GameConfig.c_PoisonTimer);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (owner == PlayerType.Player && collision.gameObject.CompareTag("Bot")
            || owner == PlayerType.Bot && collision.gameObject.CompareTag("Player"))
        {
            playerInLava = null;
            CancelInvoke(nameof(DamagePlayer));
        }
    }

    private void DamagePlayer()
    {
        if (playerInLava)
            playerInLava.gameObject.GetComponent<Health>().UpdateHealth(-GameConfig.c_PoisonTickDamage);
    }
    public void ResetObject()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnDestroyAction();
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
