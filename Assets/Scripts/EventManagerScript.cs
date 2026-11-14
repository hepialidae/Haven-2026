using System;
using UnityEngine;

public class EventManagerScript : MonoBehaviour
{
    public static EventManagerScript instance;

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
}
