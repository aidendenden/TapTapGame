using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dream1Game : MonoBehaviour
{
    public GameObject player;
    public Camera _camera;

    public GameObject scene1;
    public GameObject scene2;
    public Transform scene1StartPoint;
    public Transform scene2StartPoint;


    private Vector3 cameraOriginPoint = Vector3.zero;
    private Dream1SceneEnum currentScene;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        SceneChange(Dream1SceneEnum.Scene1);
    }

    private void Update()
    {
        _camera.transform.position = new Vector3(player.transform.position.x, 0, -10);
    }

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

    }

    private void ChangeScene2()
    {
        player.transform.position = scene2StartPoint.position;

    }
}

public enum Dream1SceneEnum
{
    Scene1,
    Scene2
}