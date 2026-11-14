using UnityEngine;

public class SelectButtonScript : MonoBehaviour
{
    [SerializeField] Color selectedColor;
    [SerializeField] string color;
    private UnityEngine.UI.Button _button;
    private UnityEngine.UI.Image _image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button = GetComponent<UnityEngine.UI.Button>();
        _image = GetComponent<UnityEngine.UI.Image>();

        _image.color = Color.white;
    }

    public void OnClick()
    {
        _image.color = selectedColor;
    }
}
