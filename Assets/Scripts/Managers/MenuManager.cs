using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
    }

    // Called by the PLAY button OnClick
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Called by the QUIT button OnClick
    public void QuitGame()
    {
        Application.Quit();
    }
}
