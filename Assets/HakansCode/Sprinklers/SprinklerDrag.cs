using UnityEngine;

public class SprinklerDrag : MonoBehaviour
{
    public bool isDragging = false;
    SprinkleSpreader sprinkleSpread;
    Transform originalParent;

    private void Start()
    {
        sprinkleSpread = GetComponent<SprinkleSpreader>();
        originalParent = transform.parent;
    }
    void Update()
    {
        if(isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;
            transform.position = mousePos;
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        sprinkleSpread.mySpreadCollider.enabled = false;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        sprinkleSpread.mySpreadCollider.enabled = true;
    }
}
