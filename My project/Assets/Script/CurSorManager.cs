using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurSorManager : MonoBehaviour 
{
    //检测区域添加碰撞体，调查区域添加脚本InvestigateArea,互动区域添加：InteractArea,石头区域添加StoneArea
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] Texture2D investigateCursor;
    [SerializeField] Texture2D interactCursor;

    public GameObject clickParticle;
    public GameObject clickStoneParticle;
    public bool isInStoneArea;

    public static CurSorManager Instance { get; private set; }
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (isInStoneArea)
            {
                Instantiate(clickStoneParticle, mousePosition, Quaternion.identity);
            }
            else 
            {
                Instantiate(clickParticle, mousePosition, Quaternion.identity);
            }
            
        }
    }
    public void SetInvestigateCursor()
    {
        Cursor.SetCursor(investigateCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetInteractCursor()
    {
        Cursor.SetCursor(interactCursor, Vector2.zero, CursorMode.Auto);
    }

 
    public void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
