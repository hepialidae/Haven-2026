using UnityEngine;

public class DoubleSpeedScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && GamestateManagerScript.instance.powerUpStatus["2x Speed"] == 0)
        {
            EventManagerScript.instance.InvokeDoubleSpeed();
        }
    }
}
