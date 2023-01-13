using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class selectCharacter : MonoBehaviour
{
    GameObject activeGo;
    private helpersInScene HelpersInScene;
    private float pressDelay = 0;

    private void Awake()
    {
        HelpersInScene = FindObjectOfType<helpersInScene>();
    }

    void Update()
    {
        GameObject[] go = FindObjectsOfType<GameObject>();

        foreach (var gameobject in go)
        {
            if (gameobject.transform.childCount > 0 && gameobject.GetComponent<helperID>())
            {
                activeGo = gameobject.gameObject;
            }
        }


        /*if (Input.GetMouseButtonDown(0) && Time.time > pressDelay)
        {
            GoToCharacter();
        }


        if (Input.GetTouch(0).phase == TouchPhase.Began && Time.time > pressDelay)
        {
            GoToCharacter();
        }*/
    }

    public void GoToCharacter()
    {
        GameObject go2 = GameObject.Find("enemy");
        
        GameObject goz = activeGo;

        goz.transform.position = activeGo.transform.position;

        LeanTween.move(activeGo, go2.transform.position, 0.2f);

        LeanTween.move(activeGo, goz.transform.position, 0.2f).delay = 0.3f;

        HelpersInScene.currentlySelectedCharacter++;

        HelpersInScene.CheckNewCharacter();

        pressDelay = Time.time + 1f;

       /* RaycastHit2D raycastHit =
            Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);


        if (raycastHit.transform.gameObject != null)
        {
          
        }*/
    }
}