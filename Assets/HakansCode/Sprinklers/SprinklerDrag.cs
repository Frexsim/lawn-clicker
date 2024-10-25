using UnityEngine;

public class SprinklerDrag : MonoBehaviour
{
    public bool isDragging = false;
    SprinkleSpreader sprinkleSpread;

    private void Start()
    {
        sprinkleSpread = GetComponent<SprinkleSpreader>();
    }
    void Update()
    {
        if(isDragging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
