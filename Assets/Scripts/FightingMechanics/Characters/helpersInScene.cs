using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class helpersInScene : MonoBehaviour
{
    [SerializeField] List<GameObject> helpersAvailableInScene;

    private string str = "player";

    [SerializeField] GameObject triangle;

    [SerializeField] Sprite txr2D;

    [SerializeField] List<GameObject> spawnPos;

    [SerializeField] List<Sprite> sprites;


    public int currentlySelectedCharacter;

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject go = new GameObject();
            go.AddComponent<SpriteRenderer>().sprite = sprites[i];
            go.AddComponent<helperID>().ID = i;
            go.name = str + i;
            go.transform.position = spawnPos[i].transform.position;
            helpersAvailableInScene.Add(go);
        }

        var firstHelper = helpersAvailableInScene[0].transform;

        triangle.transform.parent = firstHelper;

        triangle.transform.position = new Vector3(firstHelper.position.x, firstHelper.position.y + 1, 0);
    }


    public void CheckNewCharacter()
    {
        if (currentlySelectedCharacter > 3)
        {
            currentlySelectedCharacter = 0;
            ChangeSelectedCharacter();
        }
        else
        {
            ChangeSelectedCharacter();
        }
    }

    void ChangeSelectedCharacter()
    {
        var firstHelper = helpersAvailableInScene[currentlySelectedCharacter].transform;

        triangle.transform.parent = firstHelper;

        triangle.transform.position = new Vector3(firstHelper.position.x,
            firstHelper.GetComponent<SpriteRenderer>().bounds.size.y / 2 - 2, 0);
    }
}