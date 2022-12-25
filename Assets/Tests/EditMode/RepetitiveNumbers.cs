using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class RepetitiveNumbers
{
    // A Test behaves as an ordinary method
    [Test]
    public void RepetitiveNumbersSimplePasses()
    {
       bool isRepeating = false;

        var gameobject = new GameObject();

        var func = gameobject.AddComponent<Question>();
        
        List<float> buttonValues = new List<float>();

       func.GenerateAnswers();

        List<Button> btn = func.buttons;
        
      
       for (int i = 0; i < btn.Count; i++)
        {
            buttonValues[i] = func.GetComponent<ButtonValue>().value;
        }

        for (int i = 0; i < buttonValues.Count; i++)
        {
            for (int x = 0; x < buttonValues.Count; x++)
            {
                if (buttonValues[i] == buttonValues[x])
                {
                    isRepeating = true;
                }
            }
        }

        Assert.AreEqual(false, isRepeating);
    }
}