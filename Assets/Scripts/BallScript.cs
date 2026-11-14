using System.Collections;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    [SerializeField] private float speed = 10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();

        _sr.color = Color.white;

        GamestateManagerScript.instance.SpawnCrafts();

        StartCoroutine(StartGame());
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
        _rb.linearVelocity = new UnityEngine.Vector2(xVelocity * speed, yVelocity * speed);
    }

    private void UpdateSpeed(float newSpeed)
    {
        speed = newSpeed;

        if (_rb.linearVelocity != Vector2.zero)
        {
            _rb.linearVelocity = _rb.linearVelocity * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Green")
        {
            AudioManagerScript.instance.Play(AudioManagerScript.instance.bounce);
            UnityEngine.ColorUtility.TryParseHtmlString("#B0FB76", out Color color);
            _sr.color = color;
        }
        else {
            if (collision.gameObject.tag == "Yellow")
            {
                AudioManagerScript.instance.Play(AudioManagerScript.instance.bounce);
                UnityEngine.ColorUtility.TryParseHtmlString("#FEF07B", out Color color);
                _sr.color = color;
            }
        }
    }
}
