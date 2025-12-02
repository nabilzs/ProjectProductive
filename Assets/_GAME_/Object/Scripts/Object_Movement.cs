using UnityEngine;
using UnityEngine.Tilemaps;

[SelectionBase]
public class Object_Movement : MonoBehaviour
{
    [Header("Goal Detection")]
    [SerializeField] private bool isOnGoal = false;
    public bool IsOnGoal => isOnGoal;

    [SerializeField] private LayerMask goalLayerMask = 1 << 5; // Layer "Goal"

    private void OnTriggerEnter2D(Collider2D other)
    {
        // FIXED: Cek spesifik TilemapCollider2D + Layer/Tag
        if (other.GetComponent<TilemapCollider2D>() != null && 
            ((1 << other.gameObject.layer) == goalLayerMask || other.CompareTag("Goal")))
        {
            isOnGoal = true;
            Debug.Log($"✅ {gameObject.name} ON GOAL!", gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<TilemapCollider2D>() != null && 
            ((1 << other.gameObject.layer) == goalLayerMask || other.CompareTag("Goal")))
        {
            isOnGoal = false;
            Debug.Log($"❌ {gameObject.name} OFF GOAL!", gameObject);
        }
    }
}