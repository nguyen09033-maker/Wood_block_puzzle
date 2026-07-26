using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.NetworkInformation;
public class loadingManager : MonoBehaviour
{
    public static string next_scene;
    public GameObject progressBar;
    void Start()
    {
        StartCoroutine(LoadSceneAsync(next_scene));
    }
    public IEnumerator LoadSceneAsync(string scene_name)
    {
      AsyncOperation operation = SceneManager.LoadSceneAsync(scene_name);
      while (!operation.isDone)
      {
        float progress = Mathf.Clamp01(operation.progress/0.9f);
        progressBar.GetComponent<Image>().fillAmount=progress;
        yield return null;
      }


    }
}
