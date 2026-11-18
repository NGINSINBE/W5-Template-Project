
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Image components
    Image imgYou;
    Image imgCom;

    // TextMeshPro components
    TMP_Text txtYou;
    TMP_Text txtCom;
    TMP_Text txtResult;

    // Score
    int cntYou = 0;
    int cntCom = 0;

    // Prevent playing after the game ends
    bool gameFinished = false;

    void Start()
    {
        InitGame();
    }

    void InitGame()
    {
        // Find player and computer images
        GameObject youImage = GameObject.Find("ImgYou");
        GameObject comImage = GameObject.Find("ImgCom");

        // Find score and result texts
        GameObject youText = GameObject.Find("TxtYou");
        GameObject comText = GameObject.Find("TxtCom");
        GameObject resultText = GameObject.Find("TxtResult");

        // Check missing objects
        if (youImage == null || comImage == null ||
            youText == null || comText == null || resultText == null)
        {
            Debug.LogError("UI objects not found! Check their names.");
            return;
        }

        // Get components
        imgYou = youImage.GetComponent<Image>();
        imgCom = comImage.GetComponent<Image>();

        txtYou = youText.GetComponent<TMP_Text>();
        txtCom = comText.GetComponent<TMP_Text>();
        txtResult = resultText.GetComponent<TMP_Text>();

        // Check missing components
        if (imgYou == null || imgCom == null ||
            txtYou == null || txtCom == null || txtResult == null)
        {
            Debug.LogError("Missing Image or TextMeshPro component!");
            return;
        }

        // Initial images
        imgYou.sprite = Resources.Load<Sprite>("img_4");
        imgCom.sprite = Resources.Load<Sprite>("img_4");

        // Reset scores
        cntYou = 0;
        cntCom = 0;
        gameFinished = false;

        txtYou.text = "0";
        txtCom.text = "0";

        txtResult.text = "Select the button below";

        // Reset rotations
        imgYou.rectTransform.rotation = Quaternion.identity;
        imgCom.rectTransform.rotation = Quaternion.identity;

        Debug.Log("Game initialized successfully!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnButtonClick(GameObject buttonObject)
    {
        if (gameFinished) return;

        Debug.Log(buttonObject.name + " clicked");

        // Get button number
        int you = int.Parse(buttonObject.name.Substring(0, 1));

        if (you < 1 || you > 3)
            return;

        CheckResult(you);
    }

    void CheckResult(int yourResult)
    {
        // Computer randomly chooses 1, 2, or 3
        int comResult = Random.Range(1, 4);

        int k = yourResult - comResult;

        if (k == 0)
        {
            txtResult.text = "Draw!";
        }
        else if (k == 1 || k == -2)
        {
            txtResult.text = "You win!";
            cntYou++;
        }
        else
        {
            txtResult.text = "Computer wins!";
            cntCom++;
        }

        SetResult(yourResult, comResult);

        // Check winner
        if (cntYou >= 3)
        {
            EndGame("YOU WIN!");
        }
        else if (cntCom >= 3)
        {
            EndGame("COMPUTER WINS!");
        }
    }

    void SetResult(int you, int com)
    {
        // Load player and computer sprites
        imgYou.sprite = Resources.Load<Sprite>("img_" + you);
        imgCom.sprite = Resources.Load<Sprite>("img_" + com);

        // Keep player image straight
        imgYou.rectTransform.rotation = Quaternion.identity;

        // Random computer image rotation
        float randomAngle = Random.Range(-30f, 30f);
        imgCom.rectTransform.rotation =
            Quaternion.Euler(0f, 0f, randomAngle);

        // Flip computer image
        imgCom.transform.localScale = new Vector3(-1, 1, 1);

        // Update scores
        txtYou.text = cntYou.ToString();
        txtCom.text = cntCom.ToString();
    }

    void EndGame(string winner)
    {
        gameFinished = true;

        // Save winner for GameOverScene
        PlayerPrefs.SetString("Winner", winner);
        PlayerPrefs.Save();

        // Open GameOverScene
        SceneManager.LoadScene("GameOverScene");
    }
}
