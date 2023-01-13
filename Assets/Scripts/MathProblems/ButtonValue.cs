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

        if (question.answer == value)
        {
            gltext.text = "Correct!";

            SelectCharacter.GoToCharacter();
        }
        else
        {
            
            gltext.text = "Wrong!";
            AlphaChange();
           
            
        }

        question.HasAnswered = true;

        //  Invoke("HasAnswered", 1);
    }

    void AlphaChange()
    {





        LeanTween.alpha(HelpersInScene.helpersAvailableInScene[HelpersInScene.currentlySelectedCharacter], 0,
            0.5f).setOnComplete(() =>
        {
            for (int i = 0; i < HelpersInScene.helpersAvailableInScene.Count; i++)
            {
                if (HelpersInScene.helpersAvailableInScene[i].gameObject.GetComponent<helperID>().ID ==
                    HelpersInScene.currentlySelectedCharacter - 1)
                {
                    HelpersInScene.helpersAvailableInScene.RemoveAt(HelpersInScene.currentlySelectedCharacter - 1);
                    Debug.Log("Removed from list");
                   // Destroy(HelpersInScene.helpersAvailableInScene[i].gameObject);
                    Debug.Log("Destroyed");
                    break;
                }
            }
        });

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