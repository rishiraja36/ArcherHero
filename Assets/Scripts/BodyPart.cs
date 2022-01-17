using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public Enemy enemy;


    public void TakeDamage(int damage)
    {
        enemy.TakeDamage(damage);
    }
}
