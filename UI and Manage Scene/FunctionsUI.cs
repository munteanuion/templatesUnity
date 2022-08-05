using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionsUI : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _gameplayPanel;

    private void Awake()
    {
        DisablePausePanel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && 
            SceneManager.GetActiveScene().buildIndex != 0
            )
            PauseGame();
    }

    public void PauseGame()
    {
        if (_pausePanel.activeSelf == true)
        {
            DisablePausePanel();
            Time.timeScale = 1f;
            EnableGameplayPanel();
        }
        else if (_pausePanel.activeSelf == false)
        {
            EnablePausePanel();
            DisableGameplayPanel();
            Invoke("TimeScale0Invoke", 0.1f);
        }
    }

    public void EnablePausePanel()
    {
        EnablePanel(_pausePanel);
    }

    public void EnableGameplayPanel()
    {
        EnablePanel(_gameplayPanel);
    }

    public void EnableLosePanel()
    {
        EnablePanel(_losePanel);
        DisableGameplayPanel();
        Firebase.Analytics.FirebaseAnalytics.
            LogEvent("lose_ZTS");
        Invoke("TimeScale0Invoke", 0.1f);
    }

    public void EnableLosePanelInvoke1()
    {
        Invoke("EnableLosePanel", 1);
    }

    public void TimeScale0Invoke()
    {
        Time.timeScale = 0f;
    }

    public void DisablePausePanel()
    {
        DisablePanel(_pausePanel);
    }

    public void DisableGameplayPanel()
    {
        DisablePanel(_gameplayPanel);
    }

    public void DisableLosePanel()
    {
        DisablePanel(_losePanel);
    }

    private void DisablePanel(GameObject panel)
    {
        //DisableCursor();
        panel.SetActive(false);
    }

    private void EnablePanel(GameObject panel)
    {
        //EnableCursor();
        panel.SetActive(true);
    }

    public void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
