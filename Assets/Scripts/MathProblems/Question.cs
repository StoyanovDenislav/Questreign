using System;
using System.Collections.Generic;
using System.Linq;
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
    
    [SerializeField] TMP_InputField m_InputField;

    private ButtonValue btnVal;

    private selectCharacter SelectCharacter;

    public float InputFieldAnswer;

    private void Start()
    {
        btnVal = FindObjectOfType<ButtonValue>();
        SelectCharacter = FindObjectOfType<selectCharacter>();

        switch (buttons.Count > 0)
        {
            case false:
                numbers.Clear();
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = null;
                    buttons[i].GetComponent<ButtonValue>().value = 0;
                }


                break;
        }

        for (int i = 0; i < 2; i++)
        {
            numbers.Add(Random.Range(0, 6)); //choosing a number to do the addition with
        }

        AddOrDec();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && m_InputField.text.Length > 0)
        {
            InputFieldAnswer = int.Parse(m_InputField.text);

            switch (answer == InputFieldAnswer)
            {
                case true:

                    btnVal.gltext.text = "Correct!";

                    SelectCharacter.GoToCharacter();

                    break;

                case false:

                    btnVal.AlphaChange();

                    btnVal.gltext.text = "Wrong!";

                    break;
            }

            HasAnswered = true;
        }
    }


    // Update is called once per frame

    void AddOrDec()
    {
        var sortedNumbers = numbers.OrderByDescending(x => x).ToArray();

        string[] sign = new string[] {" + ", " - "};

        var chance = Random.Range(0, 2); //generating a number to determine addition or decrement

        answer = Operator(sign[chance], (int) sortedNumbers[0], (int) sortedNumbers[1]);

        text.text = sortedNumbers[0] + sign[chance] + sortedNumbers[1];

        if (buttons.Count > 0) GenerateAnswers();
    }

    public void GenerateAnswers()
    {
        List<float> numbers = new List<float> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}; // declaring numbers
        List<float> usedNums = new List<float>();

        numbers.Remove(answer); // removing the answer from all numbers
        usedNums.Add(answer); // adding the answer to all numbers


        for (int i = 0; i < buttons.Count - 1; i++)
        {
            float num = Random.Range(0, numbers.Count + 1);
            //choosing a random number between 0 and the count of the numbers + 1, because of the way Random.Range works
            if (usedNums.Contains(num))
            {
                while (usedNums.Contains(num))
                {
                    num = Random.Range(0, numbers.Count + 1);
                }
            }

            numbers.Remove(num);
            usedNums.Add(num);
        }

        Shuffle(usedNums);
        
        try
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = usedNums[i].ToString();
                buttons[i].GetComponent<ButtonValue>().value = usedNums[i];
            }
        }
        catch(Exception e)
        {
            throw e;
        }

       
    }

    void Shuffle<T>(IList<T> ts)
    {
        var count = ts.Count;

        var last = count - 1;

        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    int Operator(string logic, int x, int y)
    {
        switch (logic)
        {
            case " + ": return x + y;
            case " - ": return x - y;
            default: return 0;
        }
    }
}