using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drop : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update

    public void OnDrop(PointerEventData eventData)
    {
        
        
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        eventData.pointerDrag.GetComponent<RectTransform>().SetParent(GetComponent<RectTransform>().transform.parent, false);
     
        /*if (eventData.pointerDrag != null)
        {
            
            
          
          
        }*/
    }
}
