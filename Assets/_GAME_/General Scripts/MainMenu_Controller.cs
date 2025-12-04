using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Controller : MonoBehaviour
{
    [Header("Scene Tujuan")]
    [SerializeField] private string levelSelectingSceneName = "LevelSelecting"; // atau index kalau mau pakai angka

    // Dipanggil oleh tombol START
    public void StartGame()
    {
        // Cara 1: pakai nama scene (rekomendasi – lebih aman kalau urutan Build Settings berubah)
        SceneManager.LoadScene(levelSelectingSceneName);

        // Cara 2: pakai index (kalau LevelSelecting pasti index 0)
        // SceneManager.LoadScene(0);
    }

    // Dipanggil oleh tombol EXIT
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