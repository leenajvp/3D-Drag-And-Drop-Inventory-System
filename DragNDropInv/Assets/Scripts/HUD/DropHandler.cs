using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    private Inventory inventory;

    private void Awake()
    {
        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();

            if (!inventory)
            {
                Debug.LogError("Inventory HUD does not exist");
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            ICollectable item = eventData.pointerDrag.gameObject.GetComponent<DragHandler>().Item;

            if (item != null)
            {
                inventory.RemoveItem(item);
                item.Drop();
            }
        }
    }
}
