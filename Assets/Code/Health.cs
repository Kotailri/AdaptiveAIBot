using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public string nameString;

    [Space(5.0f)]
    public ProgressBar bar;

    private void CheckDead()
    {
        if (health <= 0)
        {
            print(nameString + " has perished");
        }
    }

    public void UpdateHealth(int hp)
    {
        health += hp;
        bar.current = health;
        print(nameString + "'s hp is now " + health);
        CheckDead();
    }
}
