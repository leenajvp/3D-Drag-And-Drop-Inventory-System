using UnityEngine;

public class DragItem : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float camZDistance;

    void OnMouseDown()
    {
        camZDistance = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = gameObject.transform.position - MouseWorldPoint();
    }

    private Vector3 MouseWorldPoint()

    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = camZDistance;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = MouseWorldPoint() + mouseOffset;
    }
}

