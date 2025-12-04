using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    [Header("Scene Index")]
    [SerializeField] private int targetLevelIndex;

    private bool alreadyTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyTriggered) return;

        if (other.GetComponent<Object_Movement>() != null)
        {
            alreadyTriggered = true;
            Debug.Log($"MASUK GOAL → pindah ke scene index {targetLevelIndex}");

            PlayerPrefs.SetInt("Unlocked_" + targetLevelIndex, 1);
            PlayerPrefs.Save();

            SceneManager.LoadScene(targetLevelIndex);
        }
    }
}