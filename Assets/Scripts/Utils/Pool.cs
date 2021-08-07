using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids
{
public class Pool<T> where T : new()
{
    private readonly Queue<T> _pooledObjects = new Queue<T>();
    
    private Func<T> _constructor;
    private Action<T> _pushed;
    private Action<T> _popped;
    private Action<List<T>> _cleared;
    public Pool<T> SetConstructor(Func<T> constructor)
    {
        _constructor = constructor;
        return this;
    }
    public Pool<T> OnPushed(Action<T> callback)
    {
        _pushed = callback;
        return this;
    }
    public Pool<T> OnPopped(Action<T> callback)
    {
        _popped = callback;
        return this;
    }

    public Pool<T> OnCleared(Action<List<T>> callback)
    {
        _cleared = callback;
        return this;
    }

    public void Push(T obj)
    {
        _pooledObjects.Enqueue(obj);
        _pushed?.Invoke(obj);
    }
    public T Pop()
    {
        var obj = GetPooledObject();
        _popped?.Invoke(obj);
        return obj;
    }
    public void Clear()
    {
        _cleared?.Invoke(_pooledObjects.ToList());
        _pooledObjects.Clear();
    }

    private T GetPooledObject()
    {
        return _pooledObjects.Count <= 0
            ? Construct()
            : _pooledObjects.Dequeue();
    }

    private T Construct()
    {
        return _constructor == null ? new T() : _constructor.Invoke();
    }
}
}
