using UnityEngine.SceneManagement;
using UnityEngine;

public class ManageScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && Debug.isDebugBuild)
            LoadNextLevel();
    }
    public void LoadNextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Firebase.Analytics.FirebaseAnalytics.
                LogEvent("firstOpenLevelZTS");
        }
        else
            LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void LoadScene(int index)
    {
        /*Firebase.Analytics.FirebaseAnalytics.
            LogEvent("open_level_"+ index); */
        SceneManager.LoadScene(index);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        Firebase.Analytics.FirebaseAnalytics.
            LogEvent("playAgainLevel");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
