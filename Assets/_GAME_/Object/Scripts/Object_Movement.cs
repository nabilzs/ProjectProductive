using UnityEngine;

[SelectionBase]
public class Object_Movement : MonoBehaviour
{
    [Header("Goal Detection")]
    [SerializeField] private LayerMask goalLayerMask = 1 << 5; // Sesuaikan layer Goal kamu

    private bool isOnGoal = false;
    private PointsCounter pointsCounter;

    private void Awake()
    {
        pointsCounter = FindObjectOfType<PointsCounter>();

        if (pointsCounter == null)
            Debug.LogError("[Object_Movement] PointsCounter TIDAK ADA di scene! Buat Canvas → UI → Text - TextMeshPro, tambah script PointsCounter.cs", this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isGoal = ((goalLayerMask.value & (1 << other.gameObject.layer)) != 0) || other.CompareTag("Goal");

        if (isGoal && !isOnGoal)
        {
            isOnGoal = true;
            pointsCounter?.AddPoint();        // PASTI DIPANGGIL
            Debug.Log($"MASUK GOAL → {gameObject.name}");
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
        }
    }
}