using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void Play() {
        Debug.Log("Play Game");
        SceneManager.LoadScene("Platformer");
    }

    public void Settings() {
        // similar to playing the game
        Debug.Log("Settings");
    }

    public void Exit() {
        Debug.Log("Exit");
    }
}
