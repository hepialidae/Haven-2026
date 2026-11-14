using UnityEngine;

public class GamestateManagerScript : MonoBehaviour
{
    public static GamestateManagerScript instance;
    public int greenSpacecraft;
    public int yellowSpacecraft;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GamestateManagerScript found.");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
