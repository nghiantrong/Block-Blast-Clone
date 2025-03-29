using UnityEngine;
using UnityEngine.UI;

public class BlockSnap : MonoBehaviour
{
    private Vector3 originalPosition;
    private GameObject snappedCell;
    private BlockDrag blockDrag;
    private float snapTolerance = 25f;

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
            Vector3 targetPosition = snappedCell.transform.position;
            Debug.Log($"Distance to cell: {Vector3.Distance(transform.position, snappedCell.transform.position)}");

            float distance = Vector3.Distance(transform.position, targetPosition);
            if (distance <= snapTolerance)
            {
                transform.position = snappedCell.transform.position;
                Debug.Log($"Block piece {name} snapped to {snappedCell.name}");
            }
            else
            {
                Debug.Log($"Block piece {name} is too far to snap.");
            }

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
