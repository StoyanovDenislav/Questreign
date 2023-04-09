using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRotation : MonoBehaviour
{
    private Button _button;
    private float rotation;

    private RotationKeeper RotationKeeper;

    CheckTimeAndCompletion checkIfSolved;

    void Start()
    {
        checkIfSolved = FindObjectOfType<CheckTimeAndCompletion>();
    }

    public void Rotation()
    {
        _button = gameObject.GetComponent<Button>();

        rotation = _button.transform.rotation.z;

        switch (rotation)
        {
            case 90:
                rotation = 180;
                break;
            case 180:
                rotation = 270;
                break;
            case 270:
                rotation = 0;
                break;

            default:
                rotation = 90;
                break;
        }

        _button.transform.Rotate(0, 0, rotation);

        CheckAll();
    }

    public void CheckAll()
    {
        RotationKeeper = FindObjectOfType<RotationKeeper>();


        for (int i = 0; i < RotationKeeper.buttons.Count; i++)
        {
            RotationKeeper.rotations[i] = (int) RotationKeeper.buttons[i].GetComponent<Button>().transform.rotation
                .normalized.eulerAngles.z;
        }

        bool isAllEqual = RotationKeeper.rotations.Count > 0 && RotationKeeper.rotations.All(x => x == 0);


        if (isAllEqual)
        {
            checkIfSolved.puzzleIsDone = true;
            checkIfSolved.PuzzleHasFinished();
        }
    }

    /*IEnumerator waitBeforeRandom()
    {
        RandomRotationButton rotationButton = FindObjectOfType<RandomRotationButton>();

        yield return new WaitForSeconds(3);

        rotationButton.RandomRotation();
    }*/
}