using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PowerUpSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUps;
    private float timer;
    private float nextPowerUp;

    void Start()
    {
        nextPowerUp = 3 + Random.Range(0.1f, 2f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextPowerUp)
        {
            timer = 0;
            Instantiate(powerUps[Random.Range(0, powerUps.Length)]);
        }
    }
}
