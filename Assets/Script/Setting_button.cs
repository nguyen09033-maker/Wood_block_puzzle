using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Setting_button : MonoBehaviour
{
    private Image targetImage;
    public Sprite button_normal;
    public Sprite button_click;
    public GameObject settingPanel; 
    public void Awake()
    {
        targetImage=GetComponent<Image>();
        if (targetImage != null && button_normal != null)
        {
            targetImage.sprite = button_normal;
        }
       
    }

        public void OnReplayClick()
    {
        float delay = 0.6f;
        targetImage.sprite= button_click;
        settingPanel.SetActive(!settingPanel.activeSelf);
        Invoke("ResetButtonSprite", delay);
    }

    private void ResetButtonSprite()
    {
        targetImage.sprite = button_normal;
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
