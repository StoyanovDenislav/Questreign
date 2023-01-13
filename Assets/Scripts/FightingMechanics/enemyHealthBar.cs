using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class enemyHealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Health m_Health;
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = m_Health.HP;
    }
}
