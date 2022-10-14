using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    public Text highscoreText;

    int score = 0;
    int highscore = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        highscore = PlayerPrefs.GetInt("highscore", 0);

        scoreText.text = "Your score is: " + score.ToString();
        highscoreText.text = "Highscore: " + highscore.ToString();

    }

   public void AddScore()
    {
        score += 10;
        scoreText.text = "Your score is: " + score.ToString();

        if (highscore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }


    }
}

