using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class ProblemLoader : MonoBehaviour
{
    private string[] keyArr = {"Maths", "Maths_input_field"/*, "PuzzleRotate", "RotatingImage"*/};

    private int number { get; set; }


    private string key { get; set; }


    AsyncOperationHandle<GameObject> problemAddresable;

    private Question answeredQuestion { get; set; }


    float timeRemaining { get; set; }


    private float maxTimeRemaining { get; set; }

    private Button button { get; set; }

    CheckTimeAndCompletion btnVal { get; set; }

    private GameObject currentPuzzle { get; set; }


    private void Start()
    {
        btnVal = FindObjectOfType<CheckTimeAndCompletion>();
    }

    public void StartProblem()
    {
        number = Random.Range(0, keyArr.Length);
        key = keyArr[number];
        StartCoroutine(ProblemInstantiate());
    }

    void Update()
    {
        answeredQuestion = FindObjectOfType<Question>();

        switch (answeredQuestion)
        {
            case null:
                switch (btnVal.puzzleIsDone)
                {
                    case true:
                        timeRemaining = 0;
                       
                        break;
                    default:
                        timeRemaining -= Time.deltaTime;
                        break;
                }

                break;

            default:
                switch (answeredQuestion.HasAnswered)
                {
                    case true:
                        timeRemaining = 0;
                        break;
                    default:
                        timeRemaining -= Time.deltaTime;
                        break;
                }

                break;
        }

        if (currentPuzzle != null)
        {
            currentPuzzle.GetComponentInChildren<Slider>().maxValue = maxTimeRemaining;
            currentPuzzle.GetComponentInChildren<Slider>().value = timeRemaining;
        }

       
        
    }


    public IEnumerator ProblemInstantiate()
    {
        button = GetComponent<Button>();

        button.enabled = false;
        button.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);

        problemAddresable = Addressables.LoadAssetAsync<GameObject>(key);

        yield return problemAddresable;

        if (problemAddresable.Status == AsyncOperationStatus.Succeeded)
        {
            var gameObject = Addressables.InstantiateAsync(key);

            currentPuzzle = gameObject.Result.gameObject;

            timeRemaining = maxTimeRemaining = gameObject.Result.GetComponent<SetTimeComponents>().maxTime;

            yield return new WaitUntil(() => timeRemaining <= 0);

            Addressables.Release(gameObject);

            timeRemaining = maxTimeRemaining;

            button.enabled = true;

            currentPuzzle = null;

            button.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
        }
    }
}