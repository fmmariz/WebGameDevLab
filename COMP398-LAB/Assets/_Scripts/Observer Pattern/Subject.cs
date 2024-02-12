using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject<T> : MonoBehaviour
{
    private List<IObserver<T>> observers = new List<IObserver<T>>();
    public void AddObserver(IObserver<T> observer) => observers.Add(observer);

    public void RemoveObserver(IObserver<T> observer) => observers.Remove(observer);

    public void NotifyObservers(T enums)
    {
        observers.ForEach(x =>
        {
            x.OnNotify(enums);
        });
    }
}