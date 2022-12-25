using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HP;
    public int MaxHP;
    public int MinHP;

    public void TakeHp(int damage)
    {
        HP -= damage;
    }

    void Die()
    {
        if (HP <= MinHP)
        {
            Destroy(gameObject);
        }
    }
}
