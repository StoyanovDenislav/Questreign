using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drop : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    private Drag _drag;

    Button[] allButton;
    List<Button> buttonsTag = new List<Button>();

    private RectTransform gameObjectToSwap;

    public void Awake()
    {
        allButton = FindObjectsOfType<Button>();


        for (int i = 0; i < allButton.Length; i++)
        {
            if (allButton[i].tag == "GridDragAndDropPuzzle")
            {
                buttonsTag.Add(allButton[i]);
            }
        }
    }

    void Update()
    {
        _drag = FindObjectOfType<Drag>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Transform testForChild = eventData.pointerCurrentRaycast.gameObject.transform;

        switch (testForChild.childCount > 0)
        {
            case true:
                RectTransform previousChild = testForChild.GetChild(0).gameObject.GetComponent<RectTransform>();

                previousChild.anchoredPosition = Vector2.zero;
                previousChild.SetParent(eventData.pointerDrag.GetComponent<Drag>().starterParent, false);
                previousChild.GetComponent<Drag>().starterParent =
                    eventData.pointerDrag.GetComponent<Drag>().starterParent;

                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                eventData.pointerDrag.GetComponent<RectTransform>()
                    .SetParent(eventData.pointerCurrentRaycast.gameObject.GetComponent<RectTransform>(), false);
                eventData.pointerDrag.GetComponent<Drag>().starterParent =
                    eventData.pointerCurrentRaycast.gameObject.GetComponent<RectTransform>();
                break;
            default:
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                eventData.pointerDrag.GetComponent<RectTransform>()
                    .SetParent(eventData.pointerCurrentRaycast.gameObject.GetComponent<RectTransform>(), false);
                eventData.pointerDrag.GetComponent<Drag>().starterParent =
                    eventData.pointerCurrentRaycast.gameObject.GetComponent<RectTransform>();
                break;
        }
    }
}