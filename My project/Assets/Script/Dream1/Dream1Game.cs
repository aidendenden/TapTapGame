using System;
using System.Collections;
using System.Collections.Generic;
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
    public Transform scene2StartPoint;

    [Header("过渡")]
    public Image transition;

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
    private int sceneItemIndex;
    #endregion

    private void Awake()
    {
        _camera = Camera.main;

        CanMove = true;
        transition.gameObject.SetActive(false);
    }

    private void Start()
    {
        //SceneChange(Dream1SceneEnum.Scene1);
        SceneChange(Dream1SceneEnum.Scene2);

    }

    private void Update()
    {
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

        if (currentScene == Dream1SceneEnum.Scene2)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                sceneItems[sceneItemIndex].transform.position = originTF[sceneItemIndex].position;
                sceneItems[sceneItemIndex].transform.rotation = originTF[sceneItemIndex].rotation;
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
        player.transform.position = scene2StartPoint.position;
        _camera.transform.position = cameraOriginPoint;
        scene1.SetActive(false);
        scene2.SetActive(true);
        transition.gameObject.SetActive(true);
        tilemap1.SetActive(false);
        tilemap2.SetActive(true);

        player.GetComponent<PlayerMovement2D>().action = TriggerSceneItem;

    }
    #endregion



    #region 画作交互
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
    }
    #endregion
}

public enum Dream1SceneEnum
{
    Scene1,
    Scene2
}