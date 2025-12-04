using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PointsCounter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private TextMeshProUGUI levelLabelText;

    private int totalObjects = 0;
    private int currentPoints = 0;

    private void Awake()
    {
        if (counterText == null)
            counterText = GetComponent<TextMeshProUGUI>();

        // OTOMATIS HITUNG Object_Movement
        totalObjects = FindObjectsOfType<Object_Movement>().Length;

        if (totalObjects == 0)
            Debug.LogWarning("PointsCounter: Tidak ada Object_Movement di scene ini!", this);

        UpdateLevelLabel();  // ← BARU: Update label level
        UpdateText();
        Debug.Log($"PointsCounter siap! {GetLevelName()} - Total object: {totalObjects}");
    }

    public void AddPoint()
    {
        currentPoints++;
        UpdateText();
        Debug.Log($"AddPoint → {currentPoints}/{totalObjects}");
    }

    public void RemovePoint()
    {
        if (currentPoints > 0)
            currentPoints--;

        UpdateText();
        Debug.Log($"RemovePoint → {currentPoints}/{totalObjects}");
    }

    public bool IsLevelComplete() => currentPoints >= totalObjects;

    private void UpdateText()
    {
        if (counterText != null)
            counterText.text = $"{currentPoints}/{totalObjects}";

        // Visual feedback
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

    // ← BARU: Update Level Label berdasarkan scene index
    private void UpdateLevelLabel()
    {
        if (levelLabelText != null)
        {
            levelLabelText.text = GetLevelName();
        }
    }

    // ← BARU: Logic buat tentuin nama level
    private string GetLevelName()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        switch (sceneIndex)
        {
            case 0: return "Level Selected";
            case 1: return "Level Tutorial";
            case 2: return "Level 1";
            case 3: return "Level 2";
            // Tambah terus sesuai scene kamu
            
            default: return $"Level {sceneIndex}";
        }
    }

    private void OnValidate()
    {
        UpdateText();
        UpdateLevelLabel();
    }

    [Header("Debug Info (Read Only)")]
    [SerializeField, Tooltip("Jumlah object di scene (otomatis)")] 
    private int detectedObjects = 0;

    private void OnEnable()
    {
        detectedObjects = totalObjects;
        UpdateLevelLabel();
    }
}