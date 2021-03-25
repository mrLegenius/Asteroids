using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text scoreText;
    [SerializeField] private TMPro.TMP_Text highScoreText;
    [SerializeField] private GameObject[] shipImages;


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
