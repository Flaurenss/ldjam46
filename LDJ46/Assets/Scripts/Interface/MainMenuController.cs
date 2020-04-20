using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    EventsManager eventsManager;

    void Awake()
    {
        var aux = GameObject.Find("EventsManager");
        if (aux == null) return;

        eventsManager = aux.GetComponent<EventsManager>();
        eventsManager.gameEventFailed.AddListener(LoadGameOver);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadIntro()
    {
        SceneManager.LoadScene("IntroTraducida");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
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

