using System.Collections;
using System.Collections.Generic;
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

    public void UpdateHealth(int hp)
    {
        health += hp;
        bar.current = health;
        Global.gamemanager.UpdateGM();
    }
}
