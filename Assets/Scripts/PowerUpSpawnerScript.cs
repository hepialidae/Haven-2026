using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private int maxAttempts = 100;
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
        EventManagerScript.instance.PowerUpTriggered.AddListener(RemovePowerUp);
    }

    void OnDisable()
    {
        EventManagerScript.instance.ResetGame -= ResetGame;
        EventManagerScript.instance.LoseGame.RemoveListener(LoseGame);
        EventManagerScript.instance.PowerUpTriggered.RemoveListener(RemovePowerUp);
    }

    private IEnumerator WaitForCutscene()
    {
        nextPowerUp = 9999f;

        yield return new WaitForSeconds(5f);

        nextPowerUp = 3 + UnityEngine.Random.Range(0.1f, 2f);
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextPowerUp)
        {
            timer = 0;
            Vector3 newPos = new Vector3 (UnityEngine.Random.Range(-3.567f, 3.567f), UnityEngine.Random.Range(-4.49f, 4.49f), 0f);

            if (existingPowerUps.Count != 0)
            {
                bool stop = false;
                int attempts = 0;
                while (stop == false && attempts < maxAttempts)
                {
                    for (int i = 0; i < existingPowerUps.Count; i++)
                    {
                        Transform powerUpTransform = existingPowerUps[i].transform;
                        if (Math.Abs(powerUpTransform.position.x - newPos.x) <= 1f && Math.Abs(powerUpTransform.position.y - newPos.y) <= 1.12f)
                        {
                            newPos = new Vector3 (UnityEngine.Random.Range(-3.567f, 3.567f), UnityEngine.Random.Range(-4.49f, 4.49f), 0f);
                            attempts += 1;
                            break;
                        }

                        if (i == existingPowerUps.Count - 1)
                        {
                            stop = true;
                        }
                    }
                }
            }

            GameObject powerUp = Instantiate(powerUps[UnityEngine.Random.Range(0, powerUps.Length)], newPos, Quaternion.identity);
            existingPowerUps.Add(powerUp);
        }
    }

    private void ResetGame()
    {
        if (existingPowerUps.Count != 0)
        {
            for (int i = 0; i < existingPowerUps.Count; i++)
            {
                Destroy(existingPowerUps[i]);
                existingPowerUps = new List<GameObject>();
            }
        }

        StartCoroutine(WaitForCutscene());
    }

    private void LoseGame(string color)
    {
        nextPowerUp = 999999999f;
    }

    private void RemovePowerUp(GameObject input)
    {
        existingPowerUps.Remove(input);
    }
}
