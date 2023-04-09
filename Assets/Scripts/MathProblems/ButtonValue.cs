using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonValue : MonoBehaviour
{
    public float value; //the value of the button
    public TextMeshProUGUI text;
    public TextMeshProUGUI gltext;
    private Question question;
    public bool hasSelected;
    private selectCharacter SelectCharacter;
    private helpersInScene HelpersInScene;
    private CheckTimeAndCompletion _checkTimeAndCompletion;
    


    public void Start()
    {
        question = FindObjectOfType<Question>();
        HelpersInScene = FindObjectOfType<helpersInScene>();
        _checkTimeAndCompletion = FindObjectOfType<CheckTimeAndCompletion>();
       
    }

    public void SubmitAnswer()
    {
        SelectCharacter = FindObjectOfType<selectCharacter>();

        if (question.answer == value || question.answer == question.InputFieldAnswer)
        {
            SelectCharacter.GoToCharacter();
        }
        else
        {
            _checkTimeAndCompletion.AlphaChange();
            _checkTimeAndCompletion.NumberString +=  _checkTimeAndCompletion.currentPuzzleID;
            PlayerPrefs.SetString("MainString", _checkTimeAndCompletion.NumberString);
            
        }

        question.HasAnswered = true;
    }
}