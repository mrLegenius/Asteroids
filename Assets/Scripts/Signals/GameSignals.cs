namespace Asteroids
{
public struct GameStartedSignal { }

public struct DestroyedAllAsteroidsSignal { }

public readonly struct AsteroidDestroyedSignal
{
    public AsteroidDestroyedSignal(int score) { Score = score; }
    public int Score { get; }
}

public readonly struct UFODestroyedSignal
{
    public UFODestroyedSignal(int score) { Score = score; }
    public int Score { get; }
}
public struct ShipDestroyedSignal { }
}