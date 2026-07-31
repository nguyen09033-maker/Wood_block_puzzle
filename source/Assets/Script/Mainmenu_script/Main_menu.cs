using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_menu : MonoBehaviour
{
    public void LoadGame()
    {
        loadingManager.next_scene="Play_Scene";
        SceneManager.LoadScene("Loading_Scene");
        Debug.Log("nhan play");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
