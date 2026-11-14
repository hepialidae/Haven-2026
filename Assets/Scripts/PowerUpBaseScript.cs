using Unity.VisualScripting;
using UnityEngine;

public class PowerUpBaseScript : MonoBehaviour
{
    private Transform _transform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transform = GetComponent<Transform>();

        // Vector3 newPos = new Vector3 (UnityEngine.Random.Range(-3.567f, 3.567f), UnityEngine.Random.Range(-4.49f, 4.49f), 0f);
        // _transform.position = newPos;
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
