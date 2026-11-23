using Unity.VisualScripting;
using UnityEngine;

public class PowerUpBaseScript : MonoBehaviour
{
    private Transform _transform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            AudioManagerScript.instance.Play(AudioManagerScript.instance.powerUp);

            Destroy(gameObject);
        }
    }
}
