using System.Collections;
using UnityEngine;

public class SecondBallScript : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    [SerializeField] private float speed = 6f;
    private Color _green;
    private Color _yellow;
    public string currentColor = null;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();

        _sr.color = Color.white;

        GamestateManagerScript.instance.SpawnCrafts();

        UnityEngine.ColorUtility.TryParseHtmlString("#B0FB76", out Color color);
        _green = color;
        UnityEngine.ColorUtility.TryParseHtmlString("#FEF07B", out Color color2);
        _yellow = color2;

        StartCoroutine(StartGame());
    }

    void OnEnable()
    {
        EventManagerScript.instance.PlanetHit.AddListener(ResetGameWrapper);
        EventManagerScript.instance.ResetGame += StartGameWrapper;
        EventManagerScript.instance.LoseGame.AddListener(LoseGameWrapper);
    }

    void OnDisable()
    {
        EventManagerScript.instance.PlanetHit.RemoveListener(ResetGameWrapper);
        EventManagerScript.instance.ResetGame -= StartGameWrapper;
        EventManagerScript.instance.LoseGame.RemoveListener(LoseGameWrapper);
    }

    private IEnumerator StartGame()
    {
        speed = 6f;
        Vector3 newPos = new Vector3 (0f, 0f, 0f);
        _transform.position = newPos;
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = 0f;

        yield return new WaitForSeconds(0.5f);

        bool isRight = UnityEngine.Random.value >= 0.5f;
        float xVelocity = -1f;
        if (isRight)
        {
            xVelocity = 1f;
        }
        float yVelocity = UnityEngine.Random.Range(-0.5f, 0.5f);
        _rb.linearVelocity = new Vector2(xVelocity * speed, yVelocity * speed);
    }

    private void StartGameWrapper()
    {
        StartCoroutine(StartGame());
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Green")
        {
            AudioManagerScript.instance.Play(AudioManagerScript.instance.bounce);
            _sr.color = _green;
            GamestateManagerScript.instance.ballColor = "green";

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
                GamestateManagerScript.instance.ballColor = "yellow";

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

    private IEnumerator ResetGame()
    {
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = 0f;

        Sprite sprite = _sr.sprite;

        _sr.sprite = null;
        yield return new WaitForSeconds(0.3f);
        _sr.sprite = sprite;
        yield return new WaitForSeconds(0.3f);
        _sr.sprite = null;
        yield return new WaitForSeconds(0.3f);
        _sr.sprite = sprite;

        EventManagerScript.instance.InvokeResetGame();
    }

    private void ResetGameWrapper(string color)
    {
        StartCoroutine(ResetGame());
    }

    private IEnumerator LoseGame()
    {
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = 0f;

        Sprite sprite = _sr.sprite;

        _sr.sprite = null;
        yield return new WaitForSeconds(0.3f);
        _sr.sprite = sprite;
        yield return new WaitForSeconds(0.3f);
        _sr.sprite = null;
        yield return new WaitForSeconds(0.3f);
        _sr.sprite = sprite;
    }

    private void LoseGameWrapper(string color)
    {
        StartCoroutine(LoseGame());
    }
}
