using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetString : MonoBehaviour
{
    
    public void resetString()
    {
        PlayerPrefs.DeleteKey("MainString");
        PlayerPrefs.Save();
    }
}