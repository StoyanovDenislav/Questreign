using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomRotationButton : MonoBehaviour
{
    List<float> rotations = new List<float>(){0,90,180,270};
   
    public List<Button> buttons = new List<Button>();
    private float lastUsedRotation;

    void Start()
    {
        RandomRotation();
    }

    public void RandomRotation()
    {
        buttons = GetComponentsInChildren<Button>().ToList();
        
        var st = Random.Range(0, rotations.Count);

        lastUsedRotation = rotations[st];

        foreach (Button btn in buttons)
        {
            var rnd = Random.Range(0, rotations.Count);

            if (lastUsedRotation == rotations[rnd])
            {
                do
                { 
                    rnd = Random.Range(0, rotations.Count);
                    
                } while (lastUsedRotation == rotations[rnd]);
            }

            btn.transform.Rotate(0,0,rotations[rnd]);
            
            lastUsedRotation = rotations[rnd];
        }
    }
}