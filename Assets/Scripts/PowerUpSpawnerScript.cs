using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUps;
    private float timer;
    private float nextPowerUp;
    private List<GameObject> existingPowerUps = new List<GameObject>();

    void Start()
    {
        timer = 0;

        StartCoroutine(WaitForCutscene());
    }

    void OnEnable()
    {
        EventManagerScript.instance.ResetGame += ResetGame;
        EventManagerScript.instance.LoseGame.AddListener(LoseGame);
    }

    void OnDisable()
    {
        EventManagerScript.instance.ResetGame -= ResetGame;
        EventManagerScript.instance.LoseGame.RemoveListener(LoseGame);
    }

    private IEnumerator WaitForCutscene()
    {
        nextPowerUp = 9999f;

        yield return new WaitForSeconds(5f);

        nextPowerUp = 3 + Random.Range(0.1f, 2f);
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextPowerUp)
        {
            timer = 0;
            GameObject powerUp = Instantiate(powerUps[Random.Range(0, powerUps.Length)]);
            existingPowerUps.Add(powerUp);
        }
    }

    private void ResetGame()
    {
        for (int i = 0; i < existingPowerUps.Count; i++)
        {
            Destroy(existingPowerUps[i]);
        }

        StartCoroutine(WaitForCutscene());
    }

    private void LoseGame(string color)
    {
        nextPowerUp = 999999999f;
    }
}
