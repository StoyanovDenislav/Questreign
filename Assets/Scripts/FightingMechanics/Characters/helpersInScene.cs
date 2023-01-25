using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class helpersInScene : MonoBehaviour
{
    public List<GameObject> helpersAvailableInScene;

    private string str = "player";

    [SerializeField] GameObject triangle;

    [SerializeField] List<GameObject> spawnPos;

    [SerializeField] List<Sprite> sprites;
    
    float max = 0.5f;
    float min = 0.2f;


    public int currentlySelectedCharacter;

    public void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject go = new GameObject();
            go.AddComponent<SpriteRenderer>().sprite = sprites[i];
            go.AddComponent<helperID>().ID = i;
            go.AddComponent<Health>().MaxHP = 100;
            go.GetComponent<Health>().HP = 100;
            go.AddComponent<enemyTakeDamage>();
            go.AddComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            go.AddComponent<BoxCollider2D>();
            go.name = str + i;
            go.transform.position = spawnPos[i].transform.position;
            helpersAvailableInScene.Add(go);
        }

        var firstHelper = helpersAvailableInScene[0].transform;

        triangle.transform.parent = firstHelper;

        triangle.transform.position = new Vector3(firstHelper.position.x, firstHelper.position.y + 1, 0);
    }

    private void Update()
    {
        triangle.transform.position = new Vector3(triangle.transform.position.x, Mathf.PingPong(Time.time*2,max-min)+min, triangle.transform.position.z);
    }


    public void CheckNewCharacter()
    {
        if (currentlySelectedCharacter >= helpersAvailableInScene.Count)
        {
            currentlySelectedCharacter = 0;
            ChangeSelectedCharacter();
        }
        else
        {
            ChangeSelectedCharacter();
        }
    }

   public void ChangeSelectedCharacter()
    {
        var firstHelper = helpersAvailableInScene[currentlySelectedCharacter].transform;

        triangle.transform.parent = firstHelper;

        triangle.transform.position = new Vector3(firstHelper.position.x,
            firstHelper.GetComponent<SpriteRenderer>().bounds.size.y / 2 - 2, 0);
    }
   
   
}