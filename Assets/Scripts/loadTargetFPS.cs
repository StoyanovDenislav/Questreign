using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class loadTargetFPS : MonoBehaviour
{
    private string key = "targetFPS";
    
    AsyncOperationHandle<GameObject> targetFPSHandle;

    private void Awake()
    {
        StartCoroutine(loadFPS());
    }

    public IEnumerator loadFPS()
    {
        targetFPSHandle = Addressables.LoadAssetAsync<GameObject>(key);

        yield return targetFPSHandle;

        if (targetFPSHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Addressables.InstantiateAsync(key);
        }
    }
}