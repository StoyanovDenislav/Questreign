using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InstantiateTutorials : MonoBehaviour
{
    private string[] keyArr = {"Addition"};

    private string key;

    private float clipLength;

    private float currentTime;

    private bool hasBeenInitialized;

    AsyncOperationHandle<GameObject> tutorialAddresable;

    private Animator _animator;

    private bool HasIntiated;


    public void StartAdditionTutorial()
    {
        key = keyArr[0];    
        StartCoroutine(TutorialInstantiate());
        
    }

    public void Update()
    {
        if (hasBeenInitialized)
        {
            if (!HasIntiated)
            {
                AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;

                foreach (var clip in clips)
                {
                    clipLength += clip.length;
                }

                HasIntiated = true;
            }

            currentTime += Time.deltaTime;
        }
    }


    public IEnumerator TutorialInstantiate()
    {
        tutorialAddresable = Addressables.LoadAssetAsync<GameObject>(key);

        yield return tutorialAddresable;

        if (tutorialAddresable.Status == AsyncOperationStatus.Succeeded)
        {
            var gameObjectHandle = Addressables.InstantiateAsync(key);

            _animator = gameObjectHandle.Result.gameObject.GetComponent<Animator>();

            hasBeenInitialized = true;

            yield return new WaitWhile(() => currentTime <= clipLength + 1);

            Addressables.Release(gameObjectHandle);

            hasBeenInitialized = false;

            currentTime = 0;

            clipLength = 0;

            HasIntiated = false;
        }
    }
}