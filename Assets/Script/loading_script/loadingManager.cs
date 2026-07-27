using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.NetworkInformation;
using System;
using Unity.VisualScripting;
public class loadingManager : MonoBehaviour
{
    public static string next_scene;
    public Image progressBar;
    [SerializeField] private float fillSpeed=0.5f;
    void Start()
    {
      if(!string.IsNullOrEmpty(next_scene))
      {
        StartCoroutine(LoadSceneAsync(next_scene));
      }
      else
      {
        Debug.Log("thieu next_scene");
      }
    }
    public IEnumerator LoadSceneAsync(string scene_name)
    {
      AsyncOperation operation = SceneManager.LoadSceneAsync(scene_name);
      operation.allowSceneActivation=false;
      if(progressBar !=null)
      {
        progressBar.fillAmount=0f;
      }
      while (!operation.isDone)
      {
        float targetProgress = Mathf.Clamp01(operation.progress/0.9f);
        if (progressBar!=null)
        {
          progressBar.fillAmount=Mathf.MoveTowards(progressBar.fillAmount,targetProgress,fillSpeed*Time.deltaTime);   
        }
        if(progressBar.fillAmount>=1f &&  progressBar != null && operation.progress>=0.9f)
        {
          operation.allowSceneActivation=true;
        }
        yield return null;
      }


    }
}
