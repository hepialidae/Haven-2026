using UnityEngine;
using UnityEngine.Playables;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] private PlayableDirector startGame_director;
    [SerializeField] private GameObject greenWin_canvas;
    [SerializeField] private GameObject yellowWin_canvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        greenWin_canvas.SetActive(false);
        yellowWin_canvas.SetActive(false);
        EventManagerScript.instance.ResetGame += PlayStartGame;
        EventManagerScript.instance.LoseGame.AddListener(LoseGame);
    }

    private void PlayStartGame()
    {
        startGame_director.Play();
    }

    private void LoseGame(string input)
    {
        if (input == "green")
        {
            yellowWin_canvas.SetActive(true);
        }
        if (input == "yellow")
        {
            greenWin_canvas.SetActive(true);
        }
    }
}
