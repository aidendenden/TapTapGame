using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassWordPanel : MonoBehaviour
{
    public static bool IsIDGet;
    public GameObject passWordPanel;
    public void showPassWordPanel()
    {
        if (!IsIDGet)
        {
            passWordPanel.SetActive(true);
        }
    }

}
