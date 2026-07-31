using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartsceneButton : MonoBehaviour
{
    private Image targetImage;
    public Sprite button_normal;
    public Sprite button_click;
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
        targetImage.sprite=button_click;
        Block.GameOver=false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}