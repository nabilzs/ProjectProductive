using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Object_Movement[] allObjects;  

    [Header("Win Condition")]
    private int totalGoalsRequired = 0;
    private int goalsAchieved = 0;
    private bool levelComplete = false;

    private void Awake()
    {
        // PASTIKAN HANYA 1 Game_Manager di scene
        if (FindObjectsOfType<Game_Manager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); // Optional: persist ke scene lain
    }

    private void Start()
    {
        if (allObjects.Length == 0)
        {
            allObjects = FindObjectsOfType<Object_Movement>();
        }
        
        totalGoalsRequired = allObjects.Length;
        
        // FIXED: Debug.Log HANYA 1x
        Debug.Log($"🔧 Game Manager Initialized - Total objects: {totalGoalsRequired}", gameObject);
    }

    private void Update()
    {
        if (levelComplete) return; // Stop checking kalau udah win
        
        goalsAchieved = 0;
        foreach (Object_Movement obj in allObjects)
        {
            if (obj != null && obj.IsOnGoal)
            {
                goalsAchieved++;
            }
        }

        if (goalsAchieved == totalGoalsRequired && totalGoalsRequired > 0)
        {
            WinLevel();
        }
    }

    private void WinLevel()
    {
        levelComplete = true;
        Debug.Log("🎉 LEVEL COMPLETE! 🎉", gameObject);
        // Time.timeScale = 0f;
        
        // Restart level (atau Next Level)
        Invoke("RestartLevel", 2f);
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}