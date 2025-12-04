using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    [Header("Level yang akan dibuka")]
    [SerializeField] private int targetLevelIndex = 2; // isi sesuai Build Index!

    [Header("Visual")]
    [SerializeField] private Sprite unlockedSprite;

    private bool alreadyTriggered = false;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // DIPANGGIL OTOMATIS oleh Object_Movement saat crate masuk
    public void OnCrateEnteredGoal()
    {
        if (alreadyTriggered) return;

        alreadyTriggered = true;
        Debug.Log($"LEVEL SELECTOR: Goal tercapai! Pindah ke scene index {targetLevelIndex}");

        // Ganti visual
        if (sr != null && unlockedSprite != null)
            sr.sprite = unlockedSprite;

        // Simpan unlock permanen
        PlayerPrefs.SetInt("Unlocked_Level_" + targetLevelIndex, 1);
        PlayerPrefs.Save();

        // Pindah level
        SceneManager.LoadScene(targetLevelIndex);
    }
}