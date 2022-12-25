using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AddImage : MonoBehaviour
{

    public GameObject prefab;

  
    
    
    // Start is called before the first frame update

    public List<Button> buttons;
    void Start()
    {
       
        buttons = FindObjectsOfType<Button>().ToList();
        for (int i = 0; i < buttons.Count; i++)
        {
            var TempImp = Instantiate(prefab, buttons[i].transform.position, Quaternion.identity);
            TempImp.GetComponent<RectTransform>().SetParent(buttons[i].GetComponent<RectTransform>());
            TempImp.GetComponent<Image>().color = Random.ColorHSV();
         


        }
    }

   
}
