using UnityEditor.SceneManagement;
using UnityEngine;

public class SprinkleSpread : MonoBehaviour
{

    [SerializeField] float raycastRadius = 3f;
    [SerializeField] LayerMask grassLayer;
    float maxGrowthSpeedIn = 2.5f;
    public float growthSpeedOut = 5f;
    public bool isChilded = false;

    SprinklerDrag sprinklerDrag;

    private void Start()
    {
        sprinklerDrag = FindFirstObjectByType<SprinklerDrag>();
    }

    private void Update()
    {
        if (!isChilded && sprinklerDrag.isDragging == false)
        {
            DetectGrassTiles();
        }
    }

    void DetectGrassTiles()
    {
        Collider[] grassColliders = Physics.OverlapSphere(transform.position, raycastRadius, grassLayer);

        foreach (Collider grassCollider in grassColliders)
        {
            GrassTileBase grassTile = grassCollider.GetComponent<GrassTileBase>();

            if (grassTile != null)
            {
                grassTile.growthSpeedMax = maxGrowthSpeedIn;
                grassTile.inSprinkleRange = true;
            }
            else
            {
                grassTile.inSprinkleRange = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raycastRadius);
    }
}
