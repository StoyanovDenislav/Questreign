using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetFPS : MonoBehaviour
{
    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Application.targetFrameRate = 60;
        }
    }
}
