using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddresableThingy : MonoBehaviour
{
    Health healthSystem;

    private string key = "Maths";

    AsyncOperationHandle<GameObject> mathAddresable;

    private ButtonValue ButtonValue;

    public void MathProblem()
    {
        StartCoroutine(MathsProblemInstantiate());
        
    }

    public void Update()
    {
       
    }


    public IEnumerator MathsProblemInstantiate()
    {
        mathAddresable = Addressables.LoadAssetAsync<GameObject>(key);

        yield return mathAddresable;

        if (mathAddresable.Status == AsyncOperationStatus.Succeeded)
        {
            var gameObject = Addressables.InstantiateAsync(key);

            yield return new WaitForSeconds(5);


            Addressables.Release(gameObject);
        }
    }
}