using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class ProblemLoader : MonoBehaviour
{
    public Dictionary<string, int> keyArr = new Dictionary<string, int>()
    {
        {"Maths", 0},
        {"Maths_input_field", 1},
        {"RotatingImage", 2}
    };

    public List<int> puzzleOrder;

    //  public string[] keyArr = {"Maths"};

    private int number { get; set; }


    private string key { get; set; }


    AsyncOperationHandle<GameObject> problemAddresable;

    private Question answeredQuestion { get; set; }


    float timeRemaining { get; set; }


    private float maxTimeRemaining { get; set; }

    private Button button { get; set; }

    CheckTimeAndCompletion btnVal { get; set; }

    private GameObject currentPuzzle { get; set; }


    private CollectMistakes _collectMistakes = null;

    private int indexer = 0;

    private void Start()
    {
        btnVal = FindObjectOfType<CheckTimeAndCompletion>();
        currentPuzzle = null;
        _collectMistakes = FindObjectOfType<CollectMistakes>();

        
    }

    public void StartProblem()
    {
        if (File.Exists(Application.persistentDataPath + "/string.dat"))
        {
            btnVal.NumberString = SaveSystem.LoadNumberString().stringData;
        }
        else btnVal.NumberString = "";
        
        if (indexer >= puzzleOrder.Count) indexer = 0;

        puzzleOrder.Clear();

        switch (btnVal.NumberString.Length > 10)
        {
            case true:

                List<int> repeatCounts = new List<int>();

                for (int i = 0; i < keyArr.Count; i++)
                {
                    repeatCounts.Add(keyArr.Count - i);
                }

                int index = 0;

                foreach (var pair in _collectMistakes.sortedCounts)
                {
                    int repeatCount = repeatCounts[Mathf.Min(index, repeatCounts.Count - 1)];

                    for (int i = 0; i < repeatCount; i++)
                    {
                        puzzleOrder.Add(pair.Key);
                    }

                    index++;
                }

                break;

            default:

                for (int i = 0; i < keyArr.Count; i++)
                {
                    puzzleOrder.Add(i);
                }

                break;
        }

        key = keyArr.Keys.ElementAt(puzzleOrder[indexer]);


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

                if (timeRemaining <= 0 && !btnVal.puzzleIsDone && currentPuzzle != null)
                {
                    btnVal.AlphaChange();

                    btnVal.NumberString += btnVal.currentPuzzleID;
                    
                    SaveSystem.SaveNumberString(btnVal.NumberString);
                    _collectMistakes.GetMostMistakes();
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

                if (timeRemaining <= 0 && !answeredQuestion.HasAnswered && currentPuzzle != null)
                {
                    btnVal.AlphaChange();

                    btnVal.NumberString += btnVal.currentPuzzleID;

                    SaveSystem.SaveNumberString(btnVal.NumberString);

                    _collectMistakes.GetMostMistakes();
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

            timeRemaining = maxTimeRemaining = currentPuzzle.GetComponent<SetTimeComponents>().maxTime;

            btnVal.currentPuzzleID = keyArr.Values.ElementAt(puzzleOrder[indexer]);

            yield return new WaitUntil(() => timeRemaining <= 0);

            Addressables.Release(gameObject);

            button.enabled = true;

            currentPuzzle = null;

            btnVal.puzzleHasBeenUnloaded = true;

            button.GetComponent<Image>().color = new Color(255, 255, 255, 1f);

            indexer++;
        }
    }
}