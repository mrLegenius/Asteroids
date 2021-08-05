using Asteroids.Models;
using TMPro;
using UnityEngine;

namespace Asteroids.Views
{
public class GameplayView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _coordsText;

    [SerializeField]
    private TMP_Text _angleText;

    [SerializeField]
    private TMP_Text _speedText;

    [SerializeField]
    private TMP_Text _lasersText;

    [SerializeField]
    private TMP_Text _cooldownText;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void Repaint(ShipModel ship)
    {
        _coordsText.SetText("Coord: ({0:0.00}, {1:0.00})", ship.Position.x, ship.Position.y);
        _angleText.SetText("Angle: {0:0.00}", ship.Angle);
        _speedText.SetText("Speed: {0:0.00}", ship.Movement.Speed);
        _lasersText.SetText("Lasers: {0:0.00}", ship.LaserFiring.LaserCount);
        _cooldownText.SetText("Laser Cooldown: {0:0.00}", ship.LaserFiring.LaserCurrentCooldown);
    }
}
}