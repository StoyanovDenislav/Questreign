using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetNewString : MonoBehaviour
{
    [SerializeField] TMP_InputField m_InputField;
    private CollectMistakes _collectMistakes;

    void Start()
    {
        _collectMistakes = FindObjectOfType<CollectMistakes>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && m_InputField.text.Length > 0)
        {
            SaveSystem.SaveNumberString(m_InputField.text);

            _collectMistakes.GetMostMistakes();

            StartCoroutine(showText());
        }
    }

    IEnumerator showText()
    {
        m_InputField.text = "Successfully set string: " + m_InputField.text;

        yield return new WaitForSeconds(3);

        m_InputField.text = "";
    }
}