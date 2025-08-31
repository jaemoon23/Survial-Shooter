using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    public Image panel;
    public TextMeshProUGUI gameOverText;

    public float fadeDuration = 1f; // ���̵� �� �ð�
    public float finalAlpha = 1f; // ���� ���� ��

    public float startScale = 0.1f; // ���� ������
    public float endScale = 1f; // ���� ������
    public float scaleDuration = 0.9f; // ������ �ִϸ��̼� �ð�
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public IEnumerator CoFade()
    {
        gameOverText.enabled = true;
        float t = 0f;   // ��� �ð�
        float s = 0f;   // ������ �ִϸ��̼� ��� �ð�

        Color color = panel.color;
        float startAlpha = color.a; // ���� ���� ��
        float endAlpha = finalAlpha; // ���� ���� ��

        var textRect = gameOverText.rectTransform;

        while (t < fadeDuration || s < scaleDuration)
        {
            // ���̵� �� ó��
            if (t < fadeDuration)
            {
                t += Time.unscaledDeltaTime; // timeScale ���� ���� �ʵ��� unscaledDeltaTime ���
                float alpha = Mathf.Lerp(startAlpha, endAlpha, t / fadeDuration);
                panel.color = new Color(color.r, color.g, color.b, alpha);
            }
            // ������ Ŀ����
            if (s < scaleDuration)
            {
                s += Time.unscaledDeltaTime;
                // AnimationCurve�� ����Ͽ� ������ �ִϸ��̼� ����
                float scale = Mathf.Lerp(startScale, endScale, scaleCurve.Evaluate(s / scaleDuration));
                textRect.localScale = new Vector3(scale, scale, 1f);
            }
            yield return null; // ���� �����ӱ��� ���
        }

    }
}
