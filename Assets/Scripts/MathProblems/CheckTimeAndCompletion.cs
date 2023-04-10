using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckTimeAndCompletion : MonoBehaviour
{
    private selectCharacter SelectCharacter { get; set; }
    private helpersInScene HelpersInScene { get; set; }
    public bool puzzleIsDone = false;
    private Question question { get; set; }
    public float value { get; set; }
    public float currentPuzzleID { get; set; }

    public string NumberString = "";

    public bool puzzleHasBeenUnloaded = false;

    // Start is called before the first frame update
    void Start()
    {
        HelpersInScene = FindObjectOfType<helpersInScene>();

        SelectCharacter = FindObjectOfType<selectCharacter>();
        
        

        
    }


    public void PuzzleHasFinished()
    {
        puzzleHasBeenUnloaded = false;
        
        if (puzzleIsDone)
        {
            SelectCharacter.GoToCharacter();

            StartCoroutine(setToFalse());
        }
        else AlphaChange();
    }


    public void AlphaChange()
    {
       
       int removedCharacterIndex = HelpersInScene.currentlySelectedCharacter;
       HelpersInScene.currentlySelectedCharacter++;
       HelpersInScene.CheckNewCharacter();

       switch (removedCharacterIndex)
       {
           case 0:
               LeanTween.alpha(HelpersInScene.helpersAvailableInScene[0], 0, 0.5f).setOnComplete(() =>
               {
                   HelpersInScene.helpersAvailableInScene.RemoveAt(0);
                   if (HelpersInScene.helpersAvailableInScene.Count > 0)
                   {
                       HelpersInScene.currentlySelectedCharacter = 0;
                   }
               });
               break;

           default:
               if (HelpersInScene.helpersAvailableInScene.Count > removedCharacterIndex)
               {
                   LeanTween.alpha(HelpersInScene.helpersAvailableInScene[removedCharacterIndex], 0, 0.5f).setOnComplete(() =>
                   {
                       HelpersInScene.helpersAvailableInScene.RemoveAt(removedCharacterIndex);
                       HelpersInScene.currentlySelectedCharacter = removedCharacterIndex;
                   });
               }
               break;
       }
    
    }

    IEnumerator setToFalse()
    {
        yield return new WaitUntil(() => puzzleHasBeenUnloaded);

        puzzleIsDone = false;
    }
}


