using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogScriptableObject", menuName = "TapTapGame/对话系统配置文件")]
public class DialogScriptableObject : ScriptableObject
{
    [Header("物品交互配置列表")]
    public List<StuffInfo> itemInteraction;
    [Header("场景交互配置列表")]
    public List<SceneInfo> sceneInteraction;
}

[Serializable]
public class StuffInfo
{
    [Header("物品类型")]
    public StuffType type;
    [Header("物品名称")]
    public string name;
    [Header("物品交互内容"), TextArea(3, 10)]
    public List<string> content;
}

[Serializable]
public class SceneInfo
{
    [Header("场景")]
    public SceneType type;
    [Header("场景交互内容"), TextArea(3, 10)]
    public List<string> content;
}

public enum StuffType
{
    背包,
    带有照片的项链,
    女儿送给父亲的手表,
    带有图案谜题的黑色铁盒,
    石堆的差分,
    老旧的报纸,
    保险丝盒里的便条
}

public enum SceneType
{
    Day1,
    Day2,
    Day3,
    Dream1,
    Dream3,
}
