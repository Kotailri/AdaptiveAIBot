using UnityEngine;

public class Health : MonoBehaviour, IResettable
{
    public int health;

    [Space(5.0f)]
    public ProgressBar bar;

    private void Start()
    {
        InitResettable();
    }

    private void OnDestroy()
    {
        OnDestroyAction();
    }

    public bool CheckDead()
    {
        return health <= 0;
    }

    public void InitResettable()
    {
        Global.resettables.Add(this);
    }

    public void ResetObject()
    {
        health = 100;
        bar.current = health;
    }

    public void Kill()
    {
        health = 0;
        bar.current = health;
        if (Global.gamemanager)
            Global.gamemanager.UpdateGM();
    }

    public void UpdateHealth(int hp)
    {
        health += hp;
        if (health > 100)
            health = 100;
        bar.current = health;
        if (Global.gamemanager)
            Global.gamemanager.UpdateGM();
    }

    public void OnDestroyAction()
    {
        Global.resettables.Remove(this);
    }
}
