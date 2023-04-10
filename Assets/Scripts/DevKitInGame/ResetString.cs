using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResetString : MonoBehaviour
{
    
    public void resetString()
    {
        File.Delete(Application.persistentDataPath + "/string.dat");
    }
}