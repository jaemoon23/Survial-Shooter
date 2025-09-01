using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject menuMapnel;
    public GameObject gameOver;

    public FadeOut fadeOut;

   
    private void Start()
    {
        var findGo = GameObject.FindWithTag(Defines.FadeOut);
        fadeOut = findGo.GetComponent<FadeOut>();
    }
    public void OnEnable()
    {
        UpdateScore(0);
        menuMapnel.SetActive(false);
    }
    
    public void UpdateScore(int socre)
    {
        scoreText.text = $"Score: {socre}";
    }

    public void GameOver(bool active)
    {
        gameOver.SetActive(active);
        StartCoroutine(fadeOut.CoFade());
    }
    public void MenuPanel(bool active)
    {
        menuMapnel.SetActive(active);
    }

    public void OnClickReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnClickExitGame()
    {
        Application.Quit();
    }

    
}
