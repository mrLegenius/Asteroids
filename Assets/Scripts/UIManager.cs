using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private GameObject[] shipImages;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject menuScreen;

    public void OpenGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        gameScreen.SetActive(false);
        menuScreen.SetActive(false);
    }

    public void OpenMenuScreen()
    {
        gameOverScreen.SetActive(false);
        gameScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

    public void OpenGameScreen()
    {
        gameOverScreen.SetActive(false);
        gameScreen.SetActive(true);
        menuScreen.SetActive(false);
    }

    public void OnStartButtonClicked()
    {
        GameManager.Instance.StartGame();
    }
    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetHighScore(int score)
    {
        highScoreText.text = score.ToString();
    }
    public void SetHealth(int ships)
    {
        for(int i = 0; i < ships; i++)
            shipImages[i].SetActive(true);
        
        for(int i = ships; i < shipImages.Length; i++)
            shipImages[i].SetActive(false);
    }
}
