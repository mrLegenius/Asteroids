using Asteroids.Models;
using TMPro;
using UnityEngine;

namespace Asteroids.Views
{
public class GameplayView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _coords;

    [SerializeField]
    private TMP_Text _angle;

    [SerializeField]
    private TMP_Text _speed;

    [SerializeField]
    private TMP_Text _lasers;

    [SerializeField]
    private TMP_Text _cooldown;

    public void Repaint(ShipModel ship)
    {
        _coords.SetText($"Coord: ({ship.Coord.x}, {ship.Coord.y})");
        _angle.SetText($"Angle: {ship.Angle}");
        _speed.SetText($"Speed: {ship.Movement.Speed}");
        _lasers.SetText($"Lasers: {ship.LaserCount}");
        _cooldown.SetText($"Laser Cooldown: {ship.LaserCooldown}");
    }
}
}