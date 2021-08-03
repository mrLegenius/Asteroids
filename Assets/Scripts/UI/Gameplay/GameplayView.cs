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

    public void Repaint(ShipModel ship)
    {
        _coordsText.SetText($"Coord: ({ship.Coord.x}, {ship.Coord.y})");
        _angleText.SetText($"Angle: {ship.Angle}");
        _speedText.SetText($"Speed: {ship.Movement.Speed}");
        _lasersText.SetText($"Lasers: {ship.LaserFiring.LaserCount}");
        _cooldownText.SetText($"Laser Cooldown: {ship.LaserFiring.LaserCurrentCooldown}");
    }
}
}