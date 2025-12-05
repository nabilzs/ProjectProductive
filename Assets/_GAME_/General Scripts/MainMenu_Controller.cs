using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Controller : MonoBehaviour
{
    [Header("Scene Tujuan")]
    [SerializeField] private string levelSelectingSceneName = "LevelSelecting";

    public void StartGame()
    {
        SceneManager.LoadScene(levelSelectingSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Keluar dari game... (di Editor cuma muncul log)");

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}