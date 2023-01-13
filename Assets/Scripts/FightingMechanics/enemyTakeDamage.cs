using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTakeDamage : MonoBehaviour
{
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
       
        if (other.transform.tag == "enemy")
        {
            other.transform.GetComponent<Health>().TakeHp(20);
        }
    }
}