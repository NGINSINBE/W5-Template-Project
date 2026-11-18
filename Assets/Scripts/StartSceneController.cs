
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("START BUTTON CLICKED!");
        SceneManager.LoadScene("GameScene");
    }
}
