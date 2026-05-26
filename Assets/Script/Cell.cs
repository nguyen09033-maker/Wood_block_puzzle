using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Sprite normal;  
    [SerializeField] private Sprite hightlight; 
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Normal()
    {
       gameObject.SetActive(true);
       spriteRenderer.sprite = normal;
       spriteRenderer.color = Color.white;
    }
    public void Highlight()
    {
       gameObject.SetActive(true);
       spriteRenderer.sprite= hightlight;
       spriteRenderer.color = Color.white;
    }
    public void Hover()
    {
        gameObject.SetActive(true);
        spriteRenderer.color = new Color32(0xF8, 0xD8, 0x6E, 0xFF);
        spriteRenderer.sprite = normal;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
