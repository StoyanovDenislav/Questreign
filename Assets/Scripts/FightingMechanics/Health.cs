using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int HP;
    public int MaxHP;
    public int MinHP;


    public void TakeHp(int damage)
    {
        HP -= damage;

        if (HP <= MinHP)
        {
           AlphaChange();
        }
    }

    void AlphaChange()
    {
        //LeanTween.alpha(gameObject, 0, 0.2f);
        LeanTween.alpha(gameObject, 0, 1.5f).setOnComplete(() => { gameObject.SetActive(false);  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);});
    }
}