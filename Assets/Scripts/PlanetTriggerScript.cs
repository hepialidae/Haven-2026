using System.Collections;
using UnityEngine;

public class PlanetTriggerScript : MonoBehaviour
{
    [SerializeField] private string color;
    [SerializeField] private Sprite[] planets;
    private SpriteRenderer _sr;
    private int hitsTaken = 0;
    private int maxHits = 3;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();

        hitsTaken = 0;
        _sr.sprite = planets[0];
    }

    void OnEnable()
    {
        EventManagerScript.instance.Shield.AddListener(Shielded);
    }

    void OnDisable()
    {
        EventManagerScript.instance.Shield.RemoveListener(Shielded);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hitsTaken += 1;
            AudioManagerScript.instance.Play(AudioManagerScript.instance.hitPlanet);
            if (hitsTaken == maxHits)
            {
                StartCoroutine(animatePlanet(planets[maxHits], null));
                EventManagerScript.instance.InvokeLoseGame(color);
            } 
            else
            {
                StartCoroutine(animatePlanet(planets[hitsTaken - 1], planets[hitsTaken]));
                EventManagerScript.instance.InvokePlanetHit(color);
            }
        }
    }

    private IEnumerator animatePlanet(Sprite oldSprite, Sprite newSprite)
    {
        _sr.sprite = oldSprite;
        yield return new WaitForSeconds(0.3f);
        _sr.sprite = newSprite;
        yield return new WaitForSeconds(0.3f);
        _sr.sprite = oldSprite;
        yield return new WaitForSeconds(0.3f);
        _sr.sprite = newSprite;
    }

    private void Shielded(string input)
    {
        if (input == color && hitsTaken != 0)
        {
            hitsTaken -= 1;
            StartCoroutine(animatePlanet(planets[hitsTaken + 1], planets[hitsTaken]));
        }
    }
}
