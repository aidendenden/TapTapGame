using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Jakub : MonoBehaviour
{
     private string rightAnswer ="Jakub";
    [SerializeField] private TMP_InputField inputField;         
    [SerializeField] private Button confirmButton;

    private void Start()
    {
        // Add listener to the button, so it calls ReadAndDetectRightAnswer when clicked
        confirmButton.onClick.AddListener(() => ReadAndDetectRightAnswer(inputField.text));
    }
    public void ReadAndDetectRightAnswer(string s)
    {
        if (s == rightAnswer)
        {
            Debug.Log("�����");
        }else
        {
            Debug.Log("�����");
        }
    }
}
