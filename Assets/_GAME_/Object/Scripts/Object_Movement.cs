using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class Object_Movement : MonoBehaviour
{
    [Header("Goal Detection")]
    [SerializeField] private LayerMask goalLayerMask = 1 << 5; // Layer "Goal" atau "SelectorGoal"

    // BARU: Event yang dipanggil saat masuk/keluar goal
    public UnityEvent OnEnteredGoal = new UnityEvent();
    public UnityEvent OnExitedGoal  = new UnityEvent();

    private bool isOnGoal = false;
    private PointsCounter pointsCounter;

    private void Awake()
    {
        pointsCounter = FindObjectOfType<PointsCounter>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isGoal = ((goalLayerMask.value & (1 << other.gameObject.layer)) != 0) || other.CompareTag("Goal");

        if (isGoal && !isOnGoal)
        {
            isOnGoal = true;
            pointsCounter?.AddPoint();
            Debug.Log($"MASUK GOAL → {gameObject.name}");

            // INI YANG BARU: kasih tahu semua yang mendengarkan (termasuk GoalTrigger)
            OnEnteredGoal?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        bool isGoal = ((goalLayerMask.value & (1 << other.gameObject.layer)) != 0) || other.CompareTag("Goal");

        if (isGoal && isOnGoal)
        {
            isOnGoal = false;
            pointsCounter?.RemovePoint();
            Debug.Log($"KELUAR GOAL → {gameObject.name}");

            OnExitedGoal?.Invoke();
        }
    }

    // Bonus: biar bisa lihat di inspector
    private void Reset()
    {
        goalLayerMask = 1 << LayerMask.NameToLayer("Goal");
    }
}