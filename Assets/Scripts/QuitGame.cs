using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{

    [SerializeField] GameObject panel;
        // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            OpenClosePanel();

        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void OpenClosePanel()
    {
        if (panel.activeInHierarchy)
        {
            choiceNo();
        }
        else
        {
            panel.SetActive(true);
        }
        
    }

    public void choiceNo()
    {
        panel.SetActive(false);
    }
}
