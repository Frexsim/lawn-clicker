using UnityEngine;

public class SprinklerDrag : MonoBehaviour
{
    public bool isDragging = false;

    // Update is called once per frame
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
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
}
