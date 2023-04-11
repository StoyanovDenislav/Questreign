using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDevKit : MonoBehaviour
{
    [SerializeField] private GameObject devKit;

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D) && !devKit.activeSelf) devKit.SetActive(true);
        
        else if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D) && devKit.activeSelf) devKit.SetActive(false);

    }
}
