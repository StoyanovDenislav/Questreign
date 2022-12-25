using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject go;
    public void Activate()
    {
        if (!go.activeInHierarchy) go.SetActive(true);
        else go.SetActive(false);
    }
}
