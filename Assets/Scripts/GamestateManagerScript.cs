using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamestateManagerScript : MonoBehaviour
{
    public static GamestateManagerScript instance;
    public int greenSpacecraft = 99;
    public int yellowSpacecraft = 99;
    [SerializeField] private GameObject[] greenSpacecrafts;
    [SerializeField] private GameObject[] yellowSpacecrafts;
    private bool gameLoaded = false;
    public string ballColor = null; 
    public Dictionary<string, int> powerUpStatus = new Dictionary<string, int>(); // 0 - inactive, 1 - green, 2 - yellow

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

    void Start()
    {
        EventManagerScript.instance.ResetGame += ResetPowerups;
        EventManagerScript.instance.DoubleSpeed += StartTimeDoubleSpeed;
        powerUpStatus["2x Speed"] = 0;
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Pong");
    }

    public void SpawnCrafts()
    {
        Instantiate(greenSpacecrafts[greenSpacecraft]);
        Instantiate(yellowSpacecrafts[yellowSpacecraft]);
    }

    private void StartTimeDoubleSpeed()
    {
        StartCoroutine(TimeDoubleSpeed());
    }

    private IEnumerator TimeDoubleSpeed()
    {
        yield return new WaitForSeconds(5f);

        EventManagerScript.instance.InvokeEndDoubleSpeed();
    }

    private void ResetPowerups()
    {
        powerUpStatus["2x Speed"] = 0;
    }

    public void RestartGame()
    {
        greenSpacecraft = 99;
        yellowSpacecraft = 99;
        ballColor = null;
        powerUpStatus["2x Speed"] = 0;
        gameLoaded = false;
        SceneManager.LoadScene("SpaceshipSelect");
    }
}
