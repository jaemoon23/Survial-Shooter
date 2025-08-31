using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    public Image panel;
    public TextMeshProUGUI gameOverText;

    public float fadeDuration = 1f;     // ���̵� �� �ð�
    public float finalAlpha = 1f;       // ���� ���� ��

    public float startScale = 0.1f;     // ���� ������
    public float endScale = 1f;         // ���� ������
    public float scaleDuration = 0.9f;  // ������ �ִϸ��̼� �ð�


    public IEnumerator CoFade()
    {
        gameOverText.enabled = true;
        float fadeTime = 0f;         // ��� �ð�
        float scaleTime = 0f;        // ������ �ִϸ��̼� ��� �ð�

        Color color = panel.color;
        float startAlpha = color.a;  // ���� ���� ��
        float endAlpha = finalAlpha; // ���� ���� ��

        var textRect = gameOverText.rectTransform;

        while (fadeTime < fadeDuration || fadeTime < scaleDuration)
        {
            // ���̵��� ó��
            if (fadeTime < fadeDuration)
            {
                fadeTime += Time.unscaledDeltaTime; // timeScale ���� ���� �ʵ��� unscaledDeltaTime ���

                // ���� ���� Lerp(a, b, t) t = 0 -> a, t = 1 -> b
                float alpha = Mathf.Lerp(startAlpha, endAlpha, fadeTime / fadeDuration);
                panel.color = new Color(color.r, color.g, color.b, alpha);
            }
            // ������ Ŀ����
            if (scaleTime < scaleDuration)
            {
                scaleTime += Time.unscaledDeltaTime;
                float scale = Mathf.Lerp(startScale, endScale, scaleTime / scaleDuration);
                textRect.localScale = new Vector3(scale, scale, 1f);
            }
            yield return null;
        }

    }
}
