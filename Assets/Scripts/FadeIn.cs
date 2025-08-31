using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    public Image panel;
    public TextMeshProUGUI gameOverText;

    public float fadeDuration = 1f; // 페이드 인 시간
    public float finalAlpha = 1f; // 최종 알파 값

    public float startScale = 0.1f; // 시작 스케일
    public float endScale = 1f; // 최종 스케일
    public float scaleDuration = 0.9f; // 스케일 애니메이션 시간
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public IEnumerator CoFade()
    {
        gameOverText.enabled = true;
        float t = 0f;   // 경과 시간
        float s = 0f;   // 스케일 애니메이션 경과 시간

        Color color = panel.color;
        float startAlpha = color.a; // 시작 알파 값
        float endAlpha = finalAlpha; // 최종 알파 값

        var textRect = gameOverText.rectTransform;

        while (t < fadeDuration || s < scaleDuration)
        {
            // 페이드 인 처리
            if (t < fadeDuration)
            {
                t += Time.unscaledDeltaTime; // timeScale 영향 받지 않도록 unscaledDeltaTime 사용
                float alpha = Mathf.Lerp(startAlpha, endAlpha, t / fadeDuration);
                panel.color = new Color(color.r, color.g, color.b, alpha);
            }
            // 스케일 커지기
            if (s < scaleDuration)
            {
                s += Time.unscaledDeltaTime;
                // AnimationCurve를 사용하여 스케일 애니메이션 적용
                float scale = Mathf.Lerp(startScale, endScale, scaleCurve.Evaluate(s / scaleDuration));
                textRect.localScale = new Vector3(scale, scale, 1f);
            }
            yield return null; // 다음 프레임까지 대기
        }

    }
}
