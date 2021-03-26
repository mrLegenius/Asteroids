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

    /// <summary>
    /// Открывает панель конца игры и закрывает другие
    /// </summary>
    public void OpenGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        gameScreen.SetActive(false);
        menuScreen.SetActive(false);
    }
    /// <summary>
    /// Открывает панель Меню и закрывает другие
    /// </summary>
    public void OpenMenuScreen()
    {
        gameOverScreen.SetActive(false);
        gameScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

    /// <summary>
    /// Открывает панель игрового процесса и закрывает другие
    /// </summary>
    public void OpenGameScreen()
    {
        gameOverScreen.SetActive(false);
        gameScreen.SetActive(true);
        menuScreen.SetActive(false);
    }

    /// <summary>
    /// Функция вызывающаяся при нажатии на конпку старта игры
    /// </summary>
    public void OnStartButtonClicked()
    {
        GameManager.Instance.StartGame();
    }
    /// <summary>
    /// Обновить текст кол-ва текущих очков
    /// </summary>
    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
    /// <summary>
    /// Обновить текст кол-ва очков рекорда
    /// </summary>
    public void SetHighScore(int score)
    {
        highScoreText.text = score.ToString();
    }
    /// <summary>
    /// Обновить отображение здоровья
    /// </summary>
    /// <param name="ships">Кол-во здоровья, для отображения</param>
    public void SetHealth(int ships)
    {
        for(int i = 0; i < ships; i++)
            shipImages[i].SetActive(true);
        
        for(int i = ships; i < shipImages.Length; i++)
            shipImages[i].SetActive(false);
    }
}
