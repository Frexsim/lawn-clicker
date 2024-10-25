using UnityEngine;

public class SprinkleSpreader : MonoBehaviour
{
    [SerializeField] float raycastRadius = 3f;
    [SerializeField] LayerMask grassLayer;
    public CircleCollider2D mySpreadCollider;
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
        
    }

    private void OnTriggerStay(Collider grassCollider)
    {
        if (!isChilded && sprinklerDrag.isDragging == false)
        {
            //DetectGrassTiles();
        }

        GrassTileBase grassTile = grassCollider.GetComponent<GrassTileBase>();
        grassTile.inSprinkleRange = true;
        grassTile.growthSpeedMax = maxGrowthSpeedIn;
    }

    private void OnTriggerExit2D(Collider2D grassCollider)
    {
        GrassTileBase grassTile = grassCollider.GetComponent<GrassTileBase>();
        grassTile.inSprinkleRange = false;
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
