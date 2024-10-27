using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Jakub : MonoBehaviour
{
     private string rightAnswer ="Jakub";
    private string rightAnswer1 = "jakub";
    [SerializeField] private InputField inputField;         
    [SerializeField] private Button confirmButton;
    public GameObject workerID;
    public GameObject passWordPanel;
    public GameObject paperPazzel;

    private void Start()
    {
        // Add listener to the button, so it calls ReadAndDetectRightAnswer when clicked
        confirmButton.onClick.AddListener(() => ReadAndDetectRightAnswer(inputField.text));
    }
    public void ReadAndDetectRightAnswer(string s)
    {
        if (s == rightAnswer||s==rightAnswer1)
        {
            GameEventManager.Instance.Triggered("GameOnFinsh",transform);
            workerID.SetActive(true);
            passWordPanel.SetActive(false);
            paperPazzel.SetActive(false);
        }else
        {
            Debug.Log("错误");
        }
    }
}
