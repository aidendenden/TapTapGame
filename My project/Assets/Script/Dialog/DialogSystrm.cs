using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystrm : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;

    public GameObject selectButton;

    [Header("文本文件")]
    public DialogScriptableObject dialogData;  // 使用 DialogScriptableObject
    public int index;
    public float textSpeed;

    [Header("头像")]
    //public Sprite face01, face02;

    bool textFinished;
    bool cancelTyping;
    bool oneFinish;

    public static bool isTalk;

    public List<string> textList = new List<string>();

    void Awake()
    {
        // 这里不需要再从 Resources 加载，直接使用外部传入的 dialogData
        GetTextFromDialogData(dialogData);
    }

    private void OnEnable()
    {
        textFinished = true;
        StartCoroutine("SetTextUI");
    }

    void Update()
    {
        if (Input.anyKeyDown && index == textList.Count && !oneFinish)
        {
            isTalk = false;
            oneFinish = true;
            index = 0;
            return;
        }
        if (Input.anyKeyDown && !oneFinish)
        {
            if (textFinished && !cancelTyping)
            {
                StartCoroutine("SetTextUI");
            }
            else if (!textFinished && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }

    // 从 DialogScriptableObject 中获取对话数据
    public void GetTextFromDialogData(DialogScriptableObject dialogData)
    {
        textList.Clear();
        index = 0;

        // 假设我们从 itemInteraction 列表中读取内容
        foreach (var item in dialogData.itemInteraction)
        {
            foreach (var line in item.content)
            {
                textList.Add(line);  // 将每个物品的交互内容加入 textList
            }
        }

        // 你也可以根据场景或其他条件选择从 sceneInteraction 中读取数据
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";

        if (index >= textList.Count)
        {
            yield break;
        }

        string fullText = textList[index];
        int charIndex = 0;
        while (!cancelTyping && charIndex < fullText.Length)
        {
            if (fullText[charIndex] == '<')
            {
                int endIndex = fullText.IndexOf('>', charIndex);
                if (endIndex == -1)
                {
                    endIndex = fullText.Length;
                }
                else
                {
                    endIndex += 1;
                }
                textLabel.text += fullText.Substring(charIndex, endIndex - charIndex);
                charIndex = endIndex;
            }
            else
            {
                textLabel.text += fullText[charIndex];
                charIndex++;
                yield return new WaitForSeconds(textSpeed);
            }
        }

        textLabel.text = fullText;
        cancelTyping = false;
        textFinished = true;
        index++;
    }
}
