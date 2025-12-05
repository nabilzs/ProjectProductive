using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject[] uiToHideWhenPaused;

    [Header("Scene Names")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private string levelSelectSceneName = "LevelSelecting";

    private bool isPaused = false;

    private void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Pastikan UI gameplay aktif di awal
        SetUIActive(true);
    }

    private void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == mainMenuSceneName || currentScene == "Credits")
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0f : 1f;
        AudioListener.pause = isPaused;

        pausePanel.SetActive(isPaused);

        // ← BARU: Matikan PointsCounter & LevelLabel saat pause
        SetUIActive(!isPaused);
    }

    // Fungsi baru: aktifkan/nonaktifkan UI gameplay
    private void SetUIActive(bool active)
    {
        foreach (GameObject ui in uiToHideWhenPaused)
        {
            if (ui != null)
                ui.SetActive(active);
        }
    }

    // 1. RESUME
    public void Resume()
    {
        TogglePause();
    }

    // 2. RESTART LEVEL
    public void Restart()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SetUIActive(true); // Pastikan UI muncul lagi
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 3. LEVEL SELECTION
    public void GoToLevelSelection()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(levelSelectSceneName);
    }

    // 4. MAIN MENU
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}