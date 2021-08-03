using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Views
{
public class MenuView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _lastScoreText;

    [SerializeField]
    private TMP_Text _highScoreText;

    [SerializeField]
    private Button _startGameButton;

    public void OnStartGameButtonClicked(Action callback)
    {
        _startGameButton.onClick.AddListener(callback.Invoke);
    }

    public void Repaint(int lastScore, int highScore)
    {
        _lastScoreText.SetText($"Score: {lastScore}");
        _highScoreText.SetText($"High Score: {highScore}");
    }
}
}