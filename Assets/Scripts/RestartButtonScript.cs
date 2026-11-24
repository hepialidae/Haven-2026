using UnityEngine;

public class RestartButtonScript : MonoBehaviour
{
    public void OnClick()
    {
        GamestateManagerScript.instance.RestartGame();
    }
}
