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


    public void Start()
    {
        question = FindObjectOfType<Question>();
        HelpersInScene = FindObjectOfType<helpersInScene>();
    }

    public void SubmitAnswer()
    {
        SelectCharacter = FindObjectOfType<selectCharacter>();

        if (question.answer == value || question.answer == question.InputFieldAnswer)
        {
            gltext.text = "Correct!";

            SelectCharacter.GoToCharacter();
        }
        else
        {
            AlphaChange();

            gltext.text = "Wrong!";
        }

        question.HasAnswered = true;

        //  Invoke("HasAnswered", 1);
    }

    public void AlphaChange()
    {
       
        HelpersInScene.currentlySelectedCharacter++;
        HelpersInScene.CheckNewCharacter();

        switch (HelpersInScene.helpersAvailableInScene.IndexOf(
            HelpersInScene.helpersAvailableInScene[HelpersInScene.currentlySelectedCharacter]) == 0)
        {


            case true:
                LeanTween.alpha(HelpersInScene.helpersAvailableInScene[HelpersInScene.currentlySelectedCharacter], 0,
                    0.5f).setOnComplete(() =>
                {
                    HelpersInScene.helpersAvailableInScene.RemoveAt(HelpersInScene.currentlySelectedCharacter);
                });
                break;
                
                case false:
            {
                LeanTween.alpha(HelpersInScene.helpersAvailableInScene[HelpersInScene.currentlySelectedCharacter - 1],
                    0,
                    0.5f).setOnComplete(() =>
                {
                    HelpersInScene.helpersAvailableInScene.RemoveAt(HelpersInScene.currentlySelectedCharacter - 1);
                });

                break;
            }
        }



        //  HelpersInScene.helpersAvailableInScene.Remove(HelpersInScene.helpersAvailableInScene[HelpersInScene.currentlySelectedCharacter - 1].gameObject);
        //  Destroy(HelpersInScene.helpersAvailableInScene[HelpersInScene.currentlySelectedCharacter - 1].gameObject);


        /*  else if (HelpersInScene.helpersAvailableInScene.Count <= 1)
          {
              for (int i = 0; i < HelpersInScene.helpersAvailableInScene.Count; i++)
              {
                  HelpersInScene.helpersAvailableInScene.RemoveAt(i);
              }
   
              yield return new WaitUntil(() => LeanTween.alpha(HelpersInScene.helpersAvailableInScene[HelpersInScene.currentlySelectedCharacter], 0, 0.5f).destroyOnComplete);
              
              Destroy(HelpersInScene.helpersAvailableInScene[HelpersInScene.currentlySelectedCharacter]);
          }*/
    }
}