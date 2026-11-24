using Unity.VisualScripting;
using UnityEngine;

public class PowerUpBaseScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            AudioManagerScript.instance.Play(AudioManagerScript.instance.powerUp);

            EventManagerScript.instance.InvokePowerUpTriggered(gameObject);

            Destroy(gameObject);
        }
    }
}
