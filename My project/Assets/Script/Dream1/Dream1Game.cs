using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Dream1Game : MonoBehaviour
{
    [Header("玩家")]
    public GameObject player;
    public Camera _camera;

    [Header("场景相关")]
    public GameObject tilemap1;
    public GameObject tilemap2;
    public GameObject scene1;
    public GameObject scene2;

    [Header("场景1")]
    public List<GameObject> picture;
    public List<string> words;
    public Transform scene1StartPoint;

    [Header("场景2")]
    public List<GameObject> sceneItems;
    public List<Transform> originTF;
    public List<GameObject> endingTriggers;
    public List<string> dialogueOnScene2Start;
    public List<string> dialogueOnItemInteraction;
    public List<string> dialogue;
    [TextArea(3, 10)]
    public List<string> endingDialogue;
    public Image endingPicture;
    public Transform scene2StartPoint;

    [Header("过渡")]
    public UIFade transition;
    public LevelLoader levelLoader;

    [Header("对话框相关")]
    public GameObject dialog;
    public Text text;
    public Text opitionToSee;
    public Text opitionNextTime;
    public Image frame1;
    public Image frame2;

    public static bool CanMove = true;
    private Vector3 cameraOriginPoint = new Vector3(0, 0, -10);
    private Dream1SceneEnum currentScene;

    #region 场景1
    private string triggerWords;
    private int pictureIndex;
    private readonly int BirthdayIndex = 16;
    #endregion

    #region 场景2
    private int sceneItemIndex = -1;
    private List<int> resetObjects = new List<int>();
    private bool ending = false;
    private GameObject endingTriggerObj;
    #endregion

    private void Awake()
    {
        _camera = Camera.main;

        CanMove = true;
        transition.gameObject.SetActive(false);
    }

    private void Start()
    {
        SceneChange(Dream1SceneEnum.Scene1);
        //SceneChange(Dream1SceneEnum.Scene2);

    }

    private void Update()
    {
        if (ending) return;

        if (currentScene == Dream1SceneEnum.Scene1)//画廊
        {
            _camera.transform.position = new Vector3(player.transform.position.x, 0, -10);

            //按回车交互
            if (Input.GetKeyDown(KeyCode.Return) && pictureIndex != BirthdayIndex)
            {
                if (CanMove)//能移动时可以交互，不能移动时关闭对话框
                {
                    SetDialog(triggerWords);
                }
                else
                {
                    CloseDialog();
                }
            }
            else if(Input.GetKeyDown(KeyCode.Return) && pictureIndex == BirthdayIndex && CanMove)
            {
                Birthday(triggerWords);
            }
            else if (pictureIndex == BirthdayIndex && !CanMove)//交互生日画时选择选项
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    frame1.gameObject.SetActive(true);
                    frame2.gameObject.SetActive(false);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    frame1.gameObject.SetActive(false);
                    frame2.gameObject.SetActive(true);
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (frame1.gameObject.activeSelf)//选项操作
                    {
                        //加载第二场景
                        SceneChange(Dream1SceneEnum.Scene2);
                        CloseDialog();
                    }
                    else
                    {
                        CloseDialog();
                    }
                }
            }
        }

        if (currentScene == Dream1SceneEnum.Scene2)//家庭
        {
            if (!CanMove)//不能移动时按回车关闭对话框
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    CloseDialog();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return) && sceneItemIndex != -1 && CanMove && !resetObjects.Contains(sceneItemIndex))//与需要归位的物体进行交互，保存已交互物品的索引
            {
                //交互物品索引为下列3种时展开对话
                if (sceneItemIndex == 4)
                {
                    StartCoroutine(DialogueOnItemInteraction(4));
                }
                else if (sceneItemIndex == 0)
                {
                    StartCoroutine(DialogueOnItemInteraction(0));
                }
                else if (sceneItemIndex == 2)
                {
                    StartCoroutine(DialogueOnItemInteraction(2));
                }
                //物品归位，保存索引
                sceneItems[sceneItemIndex].transform.position = originTF[sceneItemIndex].position;
                sceneItems[sceneItemIndex].transform.rotation = originTF[sceneItemIndex].rotation;
                resetObjects.Add(sceneItemIndex);
            }
            else if (resetObjects.Count == 7)//物品归位完毕，结束
            {
                //End
                ending = true;
                StartCoroutine(EndingDialogue());
            }
        }
    }

    #region 场景切换
    private void SceneChange(Dream1SceneEnum scene)
    {
        currentScene = scene;

        switch  (scene)
        {
            case Dream1SceneEnum.Scene1:
                ChangeScene1();
                break;
            case Dream1SceneEnum.Scene2:
                ChangeScene2();
                break;
        }
    }

    private void ChangeScene1()
    {
        player.transform.position = scene1StartPoint.position;
        scene1.SetActive(true);
        scene2.SetActive(false);
        tilemap1.SetActive(true);
        tilemap2.SetActive(false);

        player.GetComponent<PlayerMovement2D>().action = TriggerPictures;

    }

    private void ChangeScene2()
    {
        transition.callBack = () =>
        {
            scene2.SetActive(true);
            tilemap2.SetActive(true);
        };

        player.transform.position = scene2StartPoint.position;
        _camera.transform.position = cameraOriginPoint;
        player.GetComponent<PlayerMovement2D>().action = TriggerSceneItem;
        tilemap1.SetActive(false);
        scene1.SetActive(false);

        transition.gameObject.SetActive(true);

        StartCoroutine(StartScene2());
    }

    private void UnloadScene()
    {
        scene1.SetActive(false);
        tilemap1.SetActive(false);
        scene2.SetActive(false);
        tilemap2.SetActive(false);
    }
    #endregion

    #region 场景2对话
    IEnumerator StartScene2()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < dialogueOnScene2Start.Count; i++)
        {
            SetDialog(dialogueOnScene2Start[i]);
            yield return new WaitWhile(() => { return dialog.activeSelf; });
            CanMove = false;
            yield return new WaitForSeconds(0.3f);
        }
        CanMove = true;
    }

    IEnumerator DialogueOnItemInteraction(int index)
    {
        if (sceneItemIndex == 4)
        {
            SetDialog(dialogueOnItemInteraction[0]);
        }
        else if (sceneItemIndex == 0)
        {
            SetDialog(dialogueOnItemInteraction[1]);
        }
        else if (sceneItemIndex == 2)
        {
            SetDialog(dialogueOnItemInteraction[2]);
        }
        yield return null;
    }

    IEnumerator EndingDialogue()
    {
        for (int i = 0; i < dialogue.Count; i++)
        {
            SetDialog(dialogue[i]);
            yield return new WaitUntil(() => { return Input.GetKeyDown(KeyCode.Return); });
            CloseDialog();
            CanMove = false;
            yield return new WaitForSeconds(0.3f);
        }

        CanMove = true;

        while (true)
        {
            yield return null;
            if (!Input.GetKeyDown(KeyCode.Return)) continue;
            
            for (int i = 0; i < endingTriggers.Count; i++)
            {
                if (endingTriggers[i] == endingTriggerObj)
                {
                    //结束
                    Ending();
                    yield break;
                }
            }
        }
    }
    #endregion

    private void Ending()
    {
        Debug.Log("Ending");
        CanMove = false;
        player.SetActive(false);
        UnloadScene();

        StartCoroutine(End());
    }

    IEnumerator End()
    {
        endingPicture.gameObject.SetActive(true);
        float elapsedTime = 0f;
        Color color = endingPicture.color;
        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime / 1);
            endingPicture.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        yield return null;

        for (int i = 0; i < endingDialogue.Count; i++)
        {
            SetDialog(endingDialogue[i]);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) );
            CloseDialog();
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(1);

        levelLoader.LoadNextLevel();
    }

    #region 对话
    private void SetDialog(string content)
    {
        CanMove = false;
        dialog.SetActive(true);
        text.text = content;
        Debug.Log(content);
    }

    private void CloseDialog()
    {
        CanMove = true;
        dialog.SetActive(false);
        text.text = "";

        opitionToSee.gameObject.SetActive(false);
        opitionNextTime.gameObject.SetActive(false);
        frame1.gameObject.SetActive(false);
        frame2.gameObject.SetActive(false);
    }
    #endregion

    #region 场景1画作交互

    private void Birthday(string content)
    {
        SetDialog(content);
        opitionToSee.gameObject.SetActive(true);
        opitionNextTime.gameObject.SetActive(true);
        frame1.gameObject.SetActive(true);
        frame2.gameObject.SetActive(false);
    }


    //回调事件
    public void TriggerPictures(GameObject obj)
    {
        for (int i = 0; i < picture.Count; i++)
        {
            if (obj == picture[i])
            {
                pictureIndex = i;
                triggerWords = words[i];
            }
        }
    }
    #endregion

    #region 场景交互
    public void TriggerSceneItem(GameObject obj)
    {
        for(int i = 0; i < sceneItems.Count; i++)
        {
            if (obj == sceneItems[i])
            {
                sceneItemIndex = i;
            }
        }

        for(int i = 0; i < endingTriggers.Count; i++)
        {
            if (endingTriggers[i] == obj)
            {
                endingTriggerObj = obj;
            }
        }
    }
    #endregion
}

public enum Dream1SceneEnum
{
    Scene1,
    Scene2
}