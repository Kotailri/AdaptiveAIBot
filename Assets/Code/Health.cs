using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public string nameString;

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
        print(nameString + "'s hp is now " + health);
        CheckDead();
    }
}
