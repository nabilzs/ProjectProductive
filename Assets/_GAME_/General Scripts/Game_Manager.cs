using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PointsCounter pointsCounter;

    [Header("Win Settings")]
    [SerializeField] private bool autoNextLevel = true;        // BARU: aktifkan ini!
    [SerializeField] private float nextLevelDelay = 1f;        // Tunggu 2 detik sebelum pindah
    [SerializeField] private bool showWinPanel = true;         // Nanti bisa muncul UI "Level Complete!"

    private bool levelComplete = false;

    private void Awake()
    {
        // Singleton
        Game_Manager[] managers = FindObjectsOfType<Game_Manager>();
        if (managers.Length > 1) { Destroy(gameObject); return; }
    }

    private void Start()
    {
        if (pointsCounter == null)
            pointsCounter = FindObjectOfType<PointsCounter>();
    }

    private void Update()
    {
        if (levelComplete) return;

        if (pointsCounter != null && pointsCounter.IsLevelComplete())
        {
            WinLevel();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartCurrentLevel();
        }
    }

    private void WinLevel()
    {
        if (levelComplete) return;
        levelComplete = true;

        Debug.Log($"LEVEL {SceneManager.GetActiveScene().buildIndex + 1} COMPLETE!");

        // Optional: Play sound, particle, animasi player menang, dll

        if (autoNextLevel)
        {
            Invoke(nameof(LoadNextLevel), nextLevelDelay);
        }
    }

    // DIPANGGIL OTOMATIS setelah delay
    private void LoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Kalau masih ada level berikutnya
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("SEMUA LEVEL SELESAI! Kembali ke Menu.");
            SceneManager.LoadScene(0); // atau scene Menu utama kamu
        }
    }

    // Fungsi buat tombol UI (nanti kalau mau manual)
    public void LoadNextLevelManual() => LoadNextLevel();
    public void RestartCurrentLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}