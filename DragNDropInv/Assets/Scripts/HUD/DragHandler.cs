using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public ICollectable Item { get; set; }
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        ChnageSpriteColor();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;

        Image image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
    }

    private void ChnageSpriteColor()
    {
        Image image = GetComponent<Image>();
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000))
        {
            var hitDropSpot = hit.collider.GetComponent<IDropSpot>();
            var hitHatSpot = hit.collider.GetComponent<HatDropSpot>();

            if (hitDropSpot != null || hit.collider.gameObject.tag == "HatPlacement")
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            }

            else
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
            }
        }
    }
}


