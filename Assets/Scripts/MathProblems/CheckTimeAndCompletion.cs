using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTimeAndCompletion : MonoBehaviour
{
    private selectCharacter SelectCharacter;
    private helpersInScene HelpersInScene;
    public bool puzzleIsDone = false;
    private Question question;
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        HelpersInScene = FindObjectOfType<helpersInScene>();

        SelectCharacter = FindObjectOfType<selectCharacter>();
    }
    

    public void PuzzleHasFinished()
    {
        if (puzzleIsDone) SelectCharacter.GoToCharacter();
        else AlphaChange();
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