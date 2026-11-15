using UnityEngine;
using System.Collections;

public class IconScript : MonoBehaviour
{
    private SpriteRenderer _sr;
    [SerializeField] private Sprite normal;
    [SerializeField] private Sprite onHit;
    [SerializeField] private string color;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManagerScript.instance.PlanetHit.AddListener(OnHitWrapper);
        EventManagerScript.instance.LoseGame.AddListener(LoseGame);
    }

    private IEnumerator OnHit()
    {
        _sr.sprite = onHit;
        yield return new WaitForSeconds(0.3f);
        _sr.sprite = normal;
        yield return new WaitForSeconds(0.3f);
        _sr.sprite = onHit;
        yield return new WaitForSeconds(0.3f);
        _sr.sprite = normal;
    }

    private void OnHitWrapper(string input)
    {
        if (input == color)
        {
            StartCoroutine(OnHit());
        }
    }

    private void LoseGame(string input)
    {
        if (input == color)
        {
            _sr.sprite = onHit;
        }
    }
}
