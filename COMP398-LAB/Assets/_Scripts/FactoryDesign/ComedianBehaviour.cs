using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IJokeTeller
{
    public void TellJoke();
}

public abstract class JokeFactory
{
    public abstract IJokeTeller CreateJoke();
}


public class ComedianBehaviour : MonoBehaviour
{
    public List<JokeFactory> jokeFactories;
    private JokeFactory _jokeFactory;

    public void DeliverFactoryJoke()
    {
        _jokeFactory = jokeFactories[Random.Range(0, jokeFactories.Count)];
        IJokeTeller jokeTeller = _jokeFactory.CreateJoke();
        jokeTeller.TellJoke();
    }

    //[SerializeField] private TextMeshProUGUI _comedianTMP;
    //public List<GameObject> whyDidSomethingJokes;
    //public List<GameObject> barJokes;
    //public List<GameObject> sportsJoke;
    //public JokeType jokeTypeToTell;

    //public void DeliverJoke()
    //{
    //    CreateJokeBasedOnType(jokeTypeToTell);
    //}

    //private void CreateJokeBasedOnType(JokeType jokeType)
    //{
    //    List<GameObject> selected = null;
    //    switch (jokeType)
    //    {
    //        case JokeType.Something:
    //            selected = whyDidSomethingJokes;
    //            break;
    //        case JokeType.Bar:
    //            selected = barJokes;
    //            break;
    //        case JokeType.Sports:
    //            selected = sportsJoke;
    //            break;
    //    }

    //}

}

public enum JokeType
{
    Something,
    Bar,
    Sports
}