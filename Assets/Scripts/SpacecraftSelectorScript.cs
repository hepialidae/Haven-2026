using UnityEngine;

public class SpacecraftSelectorScript : MonoBehaviour
{
    [SerializeField] private string color;
    [SerializeField] private Sprite[] scSprites;
    [SerializeField] private Vector2[] spriteSizes;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    private UnityEngine.UI.Image _image;
    private RectTransform _transform;
    private int _currentIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image = GetComponent<UnityEngine.UI.Image>();
        _transform = GetComponent<RectTransform>();
        _image.sprite = scSprites[0];
        _transform.sizeDelta = spriteSizes[0];
        leftArrow.SetActive(false);
        rightArrow.SetActive(true);
    }

    public void LeftArrowClicked()
    {
        if (_currentIndex != 0)
        {
            _currentIndex -= 1;
            _image.sprite = scSprites[_currentIndex];
            _transform.sizeDelta = spriteSizes[_currentIndex];
            if (_currentIndex == 0)
            {
                leftArrow.SetActive(false);
                rightArrow.SetActive(true);
            }
            else
            {
                leftArrow.SetActive(true);
                rightArrow.SetActive(true);
            }
        }
    }

    public void RightArrowClicked()
    {
        if (_currentIndex != 4)
        {
            _currentIndex += 1;
            _image.sprite = scSprites[_currentIndex];
            _transform.sizeDelta = spriteSizes[_currentIndex];
            if (_currentIndex == 4)
            {
                leftArrow.SetActive(true);
                rightArrow.SetActive(false);
            }
            else
            {
                leftArrow.SetActive(true);
                rightArrow.SetActive(true);
            }
        }
    }

    public void SelectClicked()
    {
        if (color == "green")
        {
            GamestateManagerScript.instance.greenSpacecraft = _currentIndex;
        }
        else
        {
            if (color == "yellow")
            {
                GamestateManagerScript.instance.yellowSpacecraft = _currentIndex;
            }
        }
    }
}
