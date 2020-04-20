using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    EventsManager eventsManager;

    void Awake()
    {
        eventsManager = GameObject.Find("EventsManager").GetComponent<EventsManager>();
        eventsManager.gameEventFailed.AddListener(LoadGameOver);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadIntro()
    {
        SceneManager.LoadScene("Introduction");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }


}

