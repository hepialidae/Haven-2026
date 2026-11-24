using UnityEngine;

public class TwoBallsScript : MonoBehaviour
{
    [SerializeField] GameObject secondBall;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && GamestateManagerScript.instance.powerUpStatus["2 Balls"] != 1)
        {
            StartCoroutine(GamestateManagerScript.instance.DoubleBalls(secondBall));
        }
    }
}
