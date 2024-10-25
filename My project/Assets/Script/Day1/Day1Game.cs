using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Day1Game : MonoBehaviour
{
    public static int count = 3;

    [Header("按钮和亮灯")]
    public Button[] btns1 = new Button[count];
    public Button[] btns2 = new Button[count];
    public Image[] lights1 = new Image[count];
    public Image[] lights2 = new Image[count];

    [Header("颜色")]
    public Color[] colors = new Color[count];

    private int index1 = -1;
    private int index2 = -1;

    [Header("正确配对")]
    public int[] pair1 = new int[2];
    public int[] pair2 = new int[2];
    public int[] pair3 = new int[2];
    //public int[] pair4 = new int[2];

    private Dictionary<int, int> pairs = new Dictionary<int, int>();

    private void OnEnable()
    {
        for (int i = 0; i < count; i++)
        {
            int index = i;
            btns1[i].onClick.AddListener(() => OnBtns1Click(index));
            btns2[i].onClick.AddListener(() => OnBtns2Click(index));
        }
    }
    /// <summary>
    /// 第一排按钮点击事件
    /// </summary>
    /// <param name="i"></param>
    private void OnBtns1Click(int i)
    {
        if (CheckPairs(i, -1))
        {
            index1 = -1;
        }
        else
        {
            OnLight(i, -1);
            index1 = i;
        }
        if (index1 >= 0 && index2 >= 0)
        {
            SetPair();
        }
    }
    /// <summary>
    /// 第二排按钮点击事件
    /// </summary>
    /// <param name="i"></param>
    private void OnBtns2Click(int i)
    {
        if (CheckPairs(-1, i))
        {
            index2 = -1;
        }
        else
        {
            OnLight(-1, i);
            index2 = i;
        }
        if (index1 >= 0 && index2 >= 0)
        {
            SetPair();
        }
    }

    private void SetPair()
    {
        pairs.Add(index1, index2);
        if (pairs.Count == count)
        {
            Compare();
            return;
        }
        index1 = -1;
        index2 = -1;
    }
    /// <summary>
    /// 开启点击的灯，关闭原先的灯
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    private void OnLight(int i = -1, int j = -1)
    {
        if (i >= 0 && i != index1)
        {
            if (index1 >= 0)
            {
                lights1[index1].gameObject.SetActive(false);
            }
            lights1[i].gameObject.SetActive(true);
            lights1[i].color = colors[pairs.Count];
        }
        if (j >= 0 && j != index2)
        {
            if (index2 >= 0)
            {
                lights2[index2].gameObject.SetActive(false);
            }
            lights2[j].gameObject.SetActive(true);
            lights2[j].color = colors[pairs.Count];
        }
    }
    /// <summary>
    /// 比较是否和正确配对一致
    /// </summary>
    private void Compare()
    {
        foreach (var pair in pairs)
        {
            if (pair.Key == pair1[0] - 1 && pair.Value == pair1[1] - 1)
            {
                continue;
            }
            else if (pair.Key == pair2[0] - 1 && pair.Value == pair2[1] - 1)
            {
                continue;
            }
            else if (pair.Key == pair3[0] - 1 && pair.Value == pair3[1] - 1)
            {
                continue;
            }
            //else if (pair.Key == pair4[0] - 1 && pair.Value == pair4[1] - 1)
            //{
            //    continue;
            //}
            else
            {
                ReSet();
                return;
            }
        }
        Finish();
    }
    /// <summary>
    /// 查看是否已经存在的使用过的配对按钮
    /// </summary>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <returns></returns>
    private bool CheckPairs(int index1 = -1, int index2 = -1)
    {
        if (index1 >= 0)
        {
            return pairs.ContainsKey(index1);
        }
        if (index2 >= 0)
        {
            return pairs.ContainsValue(index2);
        }
        return false;
    }
    public void ReSet()
    {
        Debug.Log("失败");
        pairs.Clear();
        index1 = -1;
        index2 = -1;
        for (int i = 0; i < count; i++) 
        {
            lights1[i].gameObject.SetActive(false);
            lights2[i].gameObject.SetActive(false);
        }
    }

    private void Finish()
    {
        Debug.Log("完成");
    }

    private void OnDisable()
    {
        for (int i = 0; i < count; i++)
        {
            btns1[i].onClick.RemoveAllListeners();
            btns2[i].onClick.RemoveAllListeners();
        }
    }

    private void OnValidate()
    {
        //根据设置的设置的配对数量来规范数组的长度
        if (btns1.Length != count || btns2.Length != count || lights1.Length != count || lights2.Length != count)
        {
            System.Array.Resize(ref btns1, count);
            System.Array.Resize(ref btns2, count);
            System.Array.Resize(ref lights1, count);
            System.Array.Resize(ref lights2, count);
        }

        if (pair1.Length != 2 || pair2.Length != 2 || pair3.Length != 2 /*|| pair4.Length != 2*/)
        {
            System.Array.Resize(ref pair1, 2);
            System.Array.Resize(ref pair2, 2);
            System.Array.Resize(ref pair3, 2);
            //System.Array.Resize(ref pair4, 2);
        }
    }
}
