using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamestateManagerScript : MonoBehaviour
{
    public static GamestateManagerScript instance;
    public int greenSpacecraft = 99;
    public int yellowSpacecraft = 99;
    private bool gameLoaded = false;

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

    void FixedUpdate()
    {
        if (greenSpacecraft != 99 && yellowSpacecraft != 99 && !gameLoaded)
        {
            gameLoaded = true;
            StartCoroutine(StartGame());
        }
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Pong");
    }
}
