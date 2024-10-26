using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMoveToTarget : MonoBehaviour
{
    public Vector3 target;
    public float moveSpeed = 5f;

    public void GOStart()
    {
        StartCoroutine(MoveToTargetCoroutine());
    }

    private IEnumerator MoveToTargetCoroutine()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = target;

        float distance = Vector3.Distance(startPos, targetPos);
        float startTime = Time.time;

        while (transform.position != targetPos)
        {
            float currentTime = Time.time - startTime;
            float journeyFraction = Mathf.SmoothStep(0f, 1f, currentTime * moveSpeed / distance);

            transform.position = Vector3.Lerp(startPos, targetPos, journeyFraction);

            yield return null;
        }
        
        // 移动完成后的操作
        Debug.Log("移动完成");
        this.transform.gameObject.SetActive(false);
    }
}