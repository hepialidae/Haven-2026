using UnityEngine;

public class DoubleSpeedScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            EventManagerScript.instance.InvokeDoubleSpeed();
        }
    }
}
