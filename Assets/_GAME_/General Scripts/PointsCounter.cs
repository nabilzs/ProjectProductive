using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PointsCounter : MonoBehaviour
{
    [Header("UI References - BOLEH KOSONG DI MENU/CREDITS")]
    [SerializeField] private TextMeshProUGUI counterText;      // BISA NULL → aman!
    [SerializeField] private TextMeshProUGUI levelLabelText;    // BISA NULL → aman!

    private int totalObjects = 0;
    private int currentPoints = 0;

    private void Awake()
    {
        // Cuma jalankan kalau scene adalah level gameplay
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MainMenu" || sceneName == "Credits" || sceneName.Contains("Menu"))
        {
            enabled = false;  // Matikan seluruh script → tidak error!
            return;
        }

        // Hitung object hanya kalau perlu
        var objects = FindObjectsOfType<Object_Movement>();
        totalObjects = objects?.Length ?? 0;

        SafeUpdateText();
        SafeUpdateLevelLabel();
    }

    public void AddPoint()
    {
        currentPoints++;
        SafeUpdateText();
    }

    public void RemovePoint()
    {
        if (currentPoints > 0) currentPoints--;
        SafeUpdateText();
    }

    public bool IsLevelComplete() => currentPoints >= totalObjects;

    // UPDATE TEXT YANG 100% AMAN
    private void SafeUpdateText()
    {
        if (counterText != null)
        {
            counterText.text = $"{currentPoints}/{totalObjects}";

            if (currentPoints >= totalObjects && totalObjects > 0)
            {
                counterText.color = Color.green;
                counterText.fontSize = 80;
            }
            else
            {
                counterText.color = Color.white;
                counterText.fontSize = 64;
            }
        }
    }

    // UPDATE LABEL YANG 100% AMAN
    private void SafeUpdateLevelLabel()
    {
        if (levelLabelText != null)
        {
            levelLabelText.text = GetLevelName();
        }
    }

    private string GetLevelName()
    {
        int idx = SceneManager.GetActiveScene().buildIndex;
        return idx switch
        {
            0 => "Main Menu",
            1 => "Level Selecting",
            2 => "Tutorial",
            3 => "Level 1",
            4 => "Level 2",
            5 => "Level 3",
            6 => "Level 4",
            7 => "Level 5",
            8 => "Credits",
            _ => "Unknown"
        };
    }

    // OnValidate & OnEnable juga aman!
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            SafeUpdateText();
            SafeUpdateLevelLabel();
        }
    }

    private void OnEnable()
    {
        SafeUpdateText();
        SafeUpdateLevelLabel();
    }
}