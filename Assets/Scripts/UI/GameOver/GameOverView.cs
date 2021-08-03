using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Views
{
public class GameOverView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;

    [SerializeField]
    private TMP_Text _highScoreText;

    [SerializeField]
    private Button _restartButton;

    public void OnRestartButtonClicked(Action callback)
    {
        _restartButton.onClick.AddListener(callback.Invoke);
    }

    public void Repaint(int score, int highScore)
    {
        _scoreText.SetText($"Your score: {score}");
        _highScoreText.SetText($"High Score: {highScore}");
    }
}
}