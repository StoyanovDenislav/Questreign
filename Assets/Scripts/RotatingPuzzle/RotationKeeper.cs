using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RotationKeeper : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();
    public List<int> rotations = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        StartButtons();
    }

    public void StartButtons()
    {
        buttons = FindObjectsOfType<Button>().ToList();
        for (int i = 0; i < buttons.Count; i++)
        {
            rotations.Add(0);
        }
    }
}