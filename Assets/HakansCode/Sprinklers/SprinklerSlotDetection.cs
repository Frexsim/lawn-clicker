using UnityEngine;

public class SprinklerSlotDetection : MonoBehaviour
{
    [SerializeField] GameObject myCamera;
    [SerializeField] GameObject sprinkler;
    BoxCollider2D itemSlotCollider;
    BoxCollider2D sprinklerCollider;
    SprinkleSpread sprinkleSpread;

    private void Start()
    {
        itemSlotCollider = GetComponent<BoxCollider2D>();
        sprinkleSpread = FindFirstObjectByType<SprinkleSpread>();
        sprinklerCollider = sprinkler.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!sprinkler.GetComponent<SprinklerDrag>().isDragging)
        {
            if (itemSlotCollider.bounds.Intersects(sprinklerCollider.bounds))
            {
                sprinkler.transform.SetParent(myCamera.transform);
                sprinkleSpread.isChilded = true;
            }
            else if (sprinkler.transform.parent == myCamera.transform)
            {
                sprinkler.transform.SetParent(null);
                sprinkleSpread.isChilded = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Sprinkler"))
        {
            //other.transform.parent = myCamera.transform;
            //other.transform.SetParent(myCamera.transform.parent);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Sprinkler"))
        {

        }
    }
}
