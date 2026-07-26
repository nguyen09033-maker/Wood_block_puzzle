using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_menu : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Play_Scene");
        Debug.Log("nhan play");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
