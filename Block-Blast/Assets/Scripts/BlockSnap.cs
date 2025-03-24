using UnityEngine;

public class BlockSnap : MonoBehaviour
{
    private Vector3 originalPosition;
    private GameObject snappedCell;
    private BlockDrag blockDrag;

    void Start()
    {
        originalPosition = transform.position;
        blockDrag = GetComponentInParent<BlockDrag>();
    }

    public bool IsSnapped()
    {
        return snappedCell != null;
    }

    public void SnapToCell()
    {
        if (snappedCell != null)
        {
            transform.position = snappedCell.transform.position;
            Debug.Log($"Block piece {name} snapped to {snappedCell.name}");
        }
        else
        {
            Debug.Log($"Block piece {name} failed to snap.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Entered Trigger: {other.gameObject.name}");

        if (other.CompareTag("GridCell"))
        {
            snappedCell = other.gameObject;
            Debug.Log($"Block piece {name} entered {snappedCell.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Exited Trigger: {other.gameObject.name}");

        if (other.CompareTag("GridCell") && snappedCell == other.gameObject)
        {
            snappedCell = null;
            Debug.Log($"Block piece {name} exited {other.gameObject.name}");
        }
    }
}
