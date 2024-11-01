using UnityEditor.SceneManagement;
using UnityEngine;

public class SprinkleSpread : MonoBehaviour
{
    [SerializeField] private float raycastRadius = 3f;
    [SerializeField] private LayerMask grassLayer;
    private float maxGrowthSpeedIn = 2.5f;
    public float growthSpeedOut = 5f;
    public bool isChilded = false;

    public Vector3 originalSprinkleSize;

    private SprinklerDrag sprinklerDrag;

    void Start()
    {
        // Find the SprinklerDrag instance in the scene
        sprinklerDrag = FindFirstObjectByType<SprinklerDrag>();
        originalSprinkleSize = gameObject.transform.localScale;
    }

    [System.Obsolete]
    void Update()
    {
        if (!isChilded && !sprinklerDrag.isDragging)
        {
            GrassTileDetection();
        }

        if (isChilded || sprinklerDrag.isDragging)
        {
            ChildedGrowthSpeed();
        }
    }

    [System.Obsolete]
    void GrassTileDetection()
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
        }
        GrowthOutOfRange(grassColliders);
    }

    [System.Obsolete]
    void GrowthOutOfRange(Collider[] inRangeColliders)
    {
        foreach (GrassTileBase grassTile in FindObjectsOfType<GrassTileBase>())
        {
            if (System.Array.IndexOf(inRangeColliders, grassTile.GetComponent<Collider>()) < 0)
            {
                grassTile.growthSpeedMax = growthSpeedOut;
                grassTile.inSprinkleRange = false;
            }
        }
    }

    // När sprinklern inte används
    void ChildedGrowthSpeed()
    {
        Collider[] grassColliders = Physics.OverlapSphere(transform.position, raycastRadius, grassLayer);

        foreach (Collider grassCollider in grassColliders)
        {
            GrassTileBase grassTile = grassCollider.GetComponent<GrassTileBase>();

            if (grassTile != null)
            {
                grassTile.growthSpeedMax = growthSpeedOut;
                grassTile.inSprinkleRange = false;
            }
        }
    }


    void OnDrawGizmos()
    {
        // Draw a red wire sphere for the overlap sphere's radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raycastRadius);
    }
}
