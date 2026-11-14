using System.Collections;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    [SerializeField] private float speed = 10f;
    private Color _green;
    private Color _yellow;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();

        _sr.color = Color.white;

        GamestateManagerScript.instance.SpawnCrafts();

        StartCoroutine(StartGame());

        UnityEngine.ColorUtility.TryParseHtmlString("#B0FB76", out Color color);
        _green = color;
        UnityEngine.ColorUtility.TryParseHtmlString("#FEF07B", out Color color2);
        _yellow = color2;

        EventManagerScript.instance.DoubleSpeed += StartDoubleSpeed;
        EventManagerScript.instance.EndDoubleSpeed += EndDoubleSpeed;
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5f);

        bool isRight = UnityEngine.Random.value >= 0.5f;
        float xVelocity = -1f;
        if (isRight)
        {
            xVelocity = 1f;
        }
        float yVelocity = UnityEngine.Random.Range(-1, 1);
        _rb.linearVelocity = new Vector2(xVelocity * speed, yVelocity * speed);
    }

    private void UpdateSpeed(float newSpeed)
    {
        float oldSpeed = speed;
        speed = newSpeed;

        if (_rb.linearVelocity != Vector2.zero)
        {
            _rb.linearVelocity = _rb.linearVelocity / oldSpeed * speed;
        }
    }

    private void StartDoubleSpeed()
    {
        UpdateSpeed(speed * 2);

        if (_sr.color == _green)
        {
            GamestateManagerScript.instance.powerUpStatus["2x Speed"] = 1;
        } 
        else
        {
            if (_sr.color == _yellow)
            {
                GamestateManagerScript.instance.powerUpStatus["2x Speed"] = 2;
            }
        }
    }

    private void EndDoubleSpeed()
    {
        UpdateSpeed(speed * 0.5f);

        GamestateManagerScript.instance.powerUpStatus["2x Speed"] = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Green")
        {
            AudioManagerScript.instance.Play(AudioManagerScript.instance.bounce);
            _sr.color = _green;

            if (GamestateManagerScript.instance.powerUpStatus["2x Speed"] == 1)
            {
                UpdateSpeed(speed * 2f);
            }
            if (GamestateManagerScript.instance.powerUpStatus["2x Speed"] == 2)
            {
                UpdateSpeed(speed * 0.5f);
            }
        }
        else {
            if (collision.gameObject.tag == "Yellow")
            {
                AudioManagerScript.instance.Play(AudioManagerScript.instance.bounce);
                _sr.color = _yellow;

                if (GamestateManagerScript.instance.powerUpStatus["2x Speed"] == 1)
                {
                    UpdateSpeed(speed * 0.5f);
                }
                if (GamestateManagerScript.instance.powerUpStatus["2x Speed"] == 2)
                {
                    UpdateSpeed(speed * 2f);
                }
            }
        }
    }
}
