using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dream1Game : MonoBehaviour
{
    public RectTransform box;
    public RectTransform player;
    public List<RectTransform> pointTF;
    public Animator animator;

    private int playerPosition;
    private int boxPosition;
    private bool catchBox = false;

    private void Awake()
    {
        if (pointTF.Count > 0)
        {
            box.anchoredPosition = pointTF[44].anchoredPosition;
            player.anchoredPosition = pointTF[55].anchoredPosition;
            playerPosition = 55;
            boxPosition = 44;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            CatchBox();
        }
        else
        {
            catchBox = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
            animator.SetBool("WalkLeft", true);
        }
        else
        {
            animator.SetBool("WalkLeft", false);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
            animator.SetBool("WalkRight", true);
        }
        else
        {
            animator.SetBool("WalkRight", false);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp();
            animator.SetBool("WalkUp", true);
        }
        else
        {
            animator.SetBool("WalkUp", false);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveDown();
            animator.SetBool("WalkDown", true);
        }
        else
        {
            animator.SetBool("WalkDown", false);
        }
    }

    #region PlayerMovement
    private void MoveLeft()
    {
        if (!CheckRemovable(-1)) return;

        playerPosition -= 1;
        player.anchoredPosition = pointTF[playerPosition].anchoredPosition;

        if (catchBox)
        {
            boxPosition -= 1;
            box.anchoredPosition = pointTF[boxPosition].anchoredPosition;
        }
    }
    private void MoveRight()
    {
        if (!CheckRemovable(1)) return;

        playerPosition += 1;
        player.anchoredPosition = pointTF[playerPosition].anchoredPosition;

        if (catchBox)
        {
            boxPosition += 1;
            box.anchoredPosition = pointTF[boxPosition].anchoredPosition;
        }
    }
    private void MoveUp()
    {
        if (!CheckRemovable(-10)) return;

        playerPosition -= 10;
        player.anchoredPosition = pointTF[playerPosition].anchoredPosition;

        if (catchBox)
        {
            boxPosition -= 10;
            box.anchoredPosition = pointTF[boxPosition].anchoredPosition;
        }
    }
    private void MoveDown()
    {
        if (!CheckRemovable(10)) return;

        playerPosition += 10;
        player.anchoredPosition = pointTF[playerPosition].anchoredPosition;

        if (catchBox)
        {
            boxPosition += 10;
            box.anchoredPosition = pointTF[boxPosition].anchoredPosition;
        }
    }

    private void CatchBox()
    {
        if (Math.Abs(playerPosition - boxPosition) == 1 || Math.Abs(playerPosition - boxPosition) == 10)
        {
            catchBox = true;
        }
    }
    #endregion


    private bool CheckRemovable(int step)
    {
        bool canMove = false;
        if (catchBox)
        {
            if (step == -1)
            {
                if ((boxPosition - playerPosition) == -1 && (boxPosition % 10) != 0)
                {
                    canMove = true;
                }
                else if ((boxPosition - playerPosition) == 1 && (playerPosition % 10) != 0)
                {
                    canMove = true;
                }
                else if (Math.Abs(playerPosition - boxPosition) == 10 && (playerPosition % 10) != 0)
                {
                    canMove = true;
                }
            }
            if (step == 1)
            {
                if ((boxPosition - playerPosition) == -1 && ((playerPosition % 10 % 9) != 0 || playerPosition % 10 == 0))
                {
                    canMove = true;
                }
                else if ((boxPosition - playerPosition) == 1 && ((boxPosition % 10 % 9) != 0 || boxPosition % 10 == 0))
                {
                    canMove = true;
                }
                else if (Math.Abs(playerPosition - boxPosition) == 10 && ((playerPosition % 10 % 9) != 0 || playerPosition % 10 == 0))
                {
                    canMove = true;
                }
            }
            if (step == 10)
            {
                if ((boxPosition - playerPosition) == -10 && playerPosition < 90)
                {
                    canMove = true;
                }
                else if ((boxPosition - playerPosition) == 10 && boxPosition < 90)
                {
                    canMove = true;
                }
                else if (Math.Abs(playerPosition - boxPosition) == 1 && playerPosition < 90)
                {
                    canMove = true;
                }
            }
            if (step == -10)
            {
                if ((boxPosition - playerPosition) == -10 && boxPosition > 10)
                {
                    canMove = true;
                }
                else if ((boxPosition - playerPosition) == 10 && playerPosition > 10)
                {
                    canMove = true;
                }
                else if (Math.Abs(playerPosition - boxPosition) == 1 && playerPosition > 10)
                {
                    canMove = true;
                }
            }
        }
        else
        {
            if (step == -1 && (playerPosition % 10) != 0 && (boxPosition - playerPosition) != -1)
            {
                canMove = true;
            }
            if (step == 1 && ((playerPosition % 10 % 9) != 0 || playerPosition % 10 == 0) && (playerPosition - boxPosition) != -1)
            {
                canMove = true;
            }
            if (step == 10 && playerPosition < 90 && (playerPosition - boxPosition) != -10)
            {
                canMove = true;
            }
            if (step == -10 && playerPosition > 10 && (boxPosition - playerPosition) != -10)
            {
                canMove = true;
            }
        }

        return canMove;
    }
}
