using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Asteroids.Asteroid
{
public class Asteroid
{
    public AsteroidView View { get; set; }
    public AsteroidModel Model { get; set; }
}

public class AsteroidsController : ITickable, IInitializable, IDisposable
{
    private readonly SignalBus _signalBus;
    
    private readonly List<Asteroid> _asteroids = new List<Asteroid>();
    private readonly Dictionary<AsteroidType, Pool<Asteroid>> _pools =
        new Dictionary<AsteroidType, Pool<Asteroid>>
        {
            { AsteroidType.Big, new Pool<Asteroid>() },
            { AsteroidType.Medium, new Pool<Asteroid>() },
            { AsteroidType.Small, new Pool<Asteroid>() }
        };
    
    private bool _isGameStarted;
    private readonly Transform _asteroidsContainer;
    private readonly AsteroidsSettings _asteroidsSettings;
    
    public AsteroidsController(AsteroidsSettings asteroidsSettings, SignalBus signalBus)
    {
        _signalBus = signalBus;
        _asteroidsSettings = asteroidsSettings;
        _asteroidsContainer = new GameObject("Asteroids").transform;
        
        foreach (var pool in _pools)
        {
            pool.Value
                .SetConstructor(() => ConstructAsteroid(pool.Key))
                .OnPopped(asteroid =>
                {
                    asteroid.View.gameObject.SetActive(true);
                    _asteroids.Add(asteroid);
                })
                .OnPushed(asteroid =>
                {
                    asteroid.View.gameObject.SetActive(false);
                })
                .OnCleared(bullets => bullets.ForEach(x
                    => Object.Destroy(x.View.gameObject)));
        }
    }
    
    public void Initialize()
    {
        _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        _signalBus.Subscribe<ShipDestroyedSignal>(OnShipDestroyed);
    }
    public void Dispose()
    {
        _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        _signalBus.Unsubscribe<ShipDestroyedSignal>(OnShipDestroyed);
    }
    public void Tick()
    {
        if(!_isGameStarted) return;
        
        if (!_asteroids.Any())
        {
            _signalBus.Fire<DestroyedAllAsteroidsSignal>();
            return;
        }
        
        foreach (var asteroid in _asteroids)
        {
            Move(asteroid.Model);
            Repaint(asteroid);
        }
    }
    
    public void CreateAsteroid(AsteroidType type,
        Vector2 position,
        float angle)
    {
        var asteroid = _pools[type].Pop();

        var model = asteroid.Model;
        model.Position = position;
        model.Angle = angle;
        model.Speed = model.GetRandomSpeed();
    }

    private Asteroid ConstructAsteroid(AsteroidType type)
    {
        var prefab = _asteroidsSettings.GetRandomAsteroidPrefab(type);
        var view = Object.Instantiate(prefab,
            Utilities.GetFarPoint(),
            Quaternion.identity,
            _asteroidsContainer);

        float speedModifier = 1f;

        for (int i = 0; i < (int)type; i++)
        {
            speedModifier *= _asteroidsSettings.SpeedModifier;
        }
        
        var model = new AsteroidModel
        {
            Type = type,
            MinSpeed = _asteroidsSettings.MinInitialSpeed * speedModifier,
            MaxSpeed = _asteroidsSettings.MaxInitialSpeed * speedModifier,
            AsteroidSplitCount = _asteroidsSettings.SplitCount,
            AsteroidSpawnSpread = _asteroidsSettings.SplitSpreadAngle
        };
        model.Speed = model.GetRandomSpeed();

        var asteroid = new Asteroid
        {
            View = view,
            Model = model,
        };
            
        asteroid.View.OnCollided(OnAsteroidCollided);
        asteroid.View.OnRayHit(OnAsteroidHitByRay);
        return asteroid;
    }
    private void Move(AsteroidModel model)
    {
        var direction = Utilities.GetDirectionFromAngle(model.Angle * Mathf.Deg2Rad);
        model.Position += model.Speed * Time.deltaTime * direction;
        model.Position =
            Utilities.GetWrapAroundPosition(model.Position);
    }
    private void Repaint(Asteroid asteroid)
    {
        asteroid.View.Repaint(asteroid.Model);
    }
    private void Destroy(Asteroid asteroid)
    {
        _pools[asteroid.Model.Type].Push(asteroid);
        _asteroids.Remove(asteroid);
        
        _signalBus.Fire(
            new AsteroidDestroyedSignal(asteroid.Model.ScoreOnDestroyed));
    }

    private void OnAsteroidDestroyed(AsteroidView view)
    {
        var asteroid = _asteroids.FirstOrDefault(x => x.View == view);
        
        if(asteroid == null)
            return;

        Destroy(asteroid);

        var childType = GetChildAsteroidType(asteroid.Model.Type);
        
        if(!childType.HasValue) return;

        float asteroidSpawnSpread = asteroid.Model.AsteroidSpawnSpread;
        for (int i = 0; i < asteroid.Model.AsteroidSplitCount; i++)
        {
            float spread = Random.Range(-asteroidSpawnSpread, asteroidSpawnSpread) / 2;
            float angle = spread + asteroid.Model.Angle;
            CreateAsteroid(childType.Value, asteroid.Model.Position, angle);
        }
    }

    private void OnAsteroidCollided(Collider2D other, AsteroidView view)
    {
        OnAsteroidDestroyed(view);
    }

    private void OnAsteroidHitByRay(AsteroidView view)
    {
        OnAsteroidDestroyed(view);
    }

    private void OnGameStarted(GameStartedSignal signal)
    {
        _isGameStarted = true;
    }

    private void OnShipDestroyed(ShipDestroyedSignal signal)
    {
        _isGameStarted = false;
        foreach (var asteroid in _asteroids)
        {
            _pools[asteroid.Model.Type].Push(asteroid);
            Object.Destroy(asteroid.View.gameObject);
        }
        _asteroids.Clear();
        
        foreach (var pool in _pools.Values)
        {
            pool.Clear();
        }
    }
    
    private static AsteroidType? GetChildAsteroidType(AsteroidType currentType)
    {
        return currentType switch
        {
            AsteroidType.Big => AsteroidType.Medium,
            AsteroidType.Medium => AsteroidType.Small,
            _ => null
        };
    }
}
}