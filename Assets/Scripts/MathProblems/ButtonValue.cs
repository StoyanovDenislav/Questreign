using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonValue : MonoBehaviour
{
   public float value; //the value of the button
   public TextMeshProUGUI text;
   public TextMeshProUGUI gltext;
   private Question question;
   public bool hasSelected;
   

   public void Start()
   {
     question = FindObjectOfType<Question>();
   }

   public void SubmitAnswer()
   {
       if (question.answer == value)
       {
           gltext.text = "Correct!";
           
          
       }
       else
       {
           gltext.text = "Wrong!";
           
       }

       question.HasAnswered = true;

       //  Invoke("HasAnswered", 1);


   }
}
