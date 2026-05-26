using UnityEngine;
using TMPro;
public class score : MonoBehaviour
{
    public static score instance;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI highScore;
    private int Score;
    void Awake()
    {
        if(instance == null)
        {
            instance=this;
        }
    }
    void Start()
    {
        currentScore.text=Score.ToString();
        highScore.text=PlayerPrefs.GetInt("highScore",0).ToString();
        UpdatehighScore();

    }

    // Update is called once per frame
    private void UpdatehighScore()
    {
        if(Score>PlayerPrefs.GetInt("highScore",0))
        {
            PlayerPrefs.SetInt("highScore",Score);
            highScore.text=Score.ToString();
        }
    }
    public void UpdateScore()
    {
        Score++;
        currentScore.text=Score.ToString();
        UpdatehighScore();
    }
}
