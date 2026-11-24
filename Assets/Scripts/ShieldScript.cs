using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            EventManagerScript.instance.InvokeShield(GamestateManagerScript.instance.ballColor);
        }
    }
}
