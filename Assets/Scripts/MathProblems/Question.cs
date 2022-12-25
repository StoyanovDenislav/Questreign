using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Question : MonoBehaviour
{
    public TextMeshProUGUI text;
    public List<float> numbers = new List<float>();
    public float answer;
    private float timeToNextAddition;
    public bool HasAnswered = false;
    public List<Button> buttons = new List<Button>();
    
    

    private void Start()
    {
        
        numbers.Clear();
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = null;
            buttons[i].GetComponent<ButtonValue>().value = 0;
        }

        for (int i = 0; i < 2; i++)
        {
            numbers.Add(Random.Range(0, 6)); //choosing a number to do the addition with
        }

        AddOrDec();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddOrDec()
    {
        var chance = Random.Range(0, 101); //generating a number to determine addition or decrement

        if (chance > 50) // addition 
        {
            answer = numbers[0] + numbers[1]; // calculating the answer

            text.text = numbers[0] + " + " + numbers[1]; // outputting text
        }
        else
        {
            if (numbers[0] > numbers[1] || numbers[0] == numbers[1])
            {
                answer = numbers[0] - numbers[1]; // calculating the answer
                text.text = numbers[0] + " - " + numbers[1]; // outputting text
            }

            else if (numbers[0] < numbers[1])
            {
                answer = numbers[1] - numbers[0]; // calculating the answer
                text.text = numbers[1] + " - " + numbers[0]; // outputting text
            }
        }

        GenerateAnswers();
    }

    public void GenerateAnswers()
    {
        List<float> numbers = new List<float> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        List<float> usedNums = new List<float>();

        numbers.Remove(answer);
        usedNums.Add(answer);


        for (int i = 0; i < buttons.Count - 1; i++)
        {
            float num = Random.Range(0, numbers.Count + 1);
            if (usedNums.Contains(num))
            {
                do
                {
                    num = Random.Range(0, numbers.Count + 1);
                } while (usedNums.Contains(num));
            }

            numbers.Remove(num);
            usedNums.Add(num);
        }
        
        Shuffle(usedNums);

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = usedNums[i].ToString();
            buttons[i].GetComponent<ButtonValue>().value = usedNums[i];
        }
    }
    
    public static void Shuffle<T>(IList<T> ts) {
        var count = ts.Count;
        
        var last = count - 1;
        
        for (var i = 0; i < last; ++i) {
            
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
            
        }
    }
    
    
}