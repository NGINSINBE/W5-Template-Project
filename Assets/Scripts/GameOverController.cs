
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public TMP_Text txtWinner;

    void Start()
    {
        txtWinner.text = PlayerPrefs.GetString(
            "Winner",
            "Game Over"
        );
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }
}
