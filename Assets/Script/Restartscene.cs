using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restartscene : MonoBehaviour
{
    public void RestartScene()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Đã sửa thành currentScene
    }
}