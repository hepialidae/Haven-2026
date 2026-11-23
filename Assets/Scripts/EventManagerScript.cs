using System;
using UnityEngine;
using UnityEngine.Events;

public class EventManagerScript : MonoBehaviour
{
    public static EventManagerScript instance;

    [Serializable]
    public class StringEvent : UnityEvent<string> { }

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EventManagerScript found.");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public event Action DoubleSpeed;
    public void InvokeDoubleSpeed()
    {
        DoubleSpeed.Invoke();
    }

    public event Action EndDoubleSpeed;
    public void InvokeEndDoubleSpeed()
    {
        EndDoubleSpeed.Invoke();
    }

    public StringEvent PlanetHit;
    public void InvokePlanetHit(string color)
    {
        PlanetHit.Invoke(color);
    }

    public event Action ResetGame;
    public void InvokeResetGame()
    {
        ResetGame.Invoke();
    }

    public StringEvent LoseGame;
    public void InvokeLoseGame(string color)
    {
        LoseGame.Invoke(color);
    }

    public StringEvent Shield;
    public void InvokeShield(string color)
    {
        Shield.Invoke(color);
    }
}
