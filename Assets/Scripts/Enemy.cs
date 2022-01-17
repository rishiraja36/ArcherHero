using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health;
    public bool isDead;
    public CharacterController controller;

    public void TakeDamage(int damage)
    {
        if(Health>0)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Dead();
            }
        }
        
    }

    public void Dead()
    {
        isDead = true;
        controller.Collapse();
    }
}
