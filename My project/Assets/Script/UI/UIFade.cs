using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIFade : MonoBehaviour
{
    public Image uiImage;
    public float fadeDuration = 1f; // 控制透明度变化的时间
    public Action callBack;

    void Start()
    {
        uiImage = GetComponent<Image>();
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        // 从0到1
        yield return StartCoroutine(FadeImage(0f, 1f));
        // 从1到0
        yield return StartCoroutine(FadeImage(1f, 0f));

        callBack?.Invoke();
    }

    IEnumerator FadeImage(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = uiImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            uiImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
    }
}