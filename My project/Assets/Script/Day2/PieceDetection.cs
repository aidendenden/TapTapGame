using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDetection : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public string matchString;
    [SerializeField]private bool isMatch;
    public bool IsMatch
    {
        get { return isMatch; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "RotationElement") return;
        if (collision.name==matchString) 
        {
            isMatch = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "RotationElement") return;
        if (collision.name == matchString)
        {
            isMatch = false;
        }
    }
}
