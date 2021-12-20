using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    private Inventory inventory;
    private Transform inventoryPanel;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        inventoryPanel = inventory.transform;

        inventory.ItemAdded += Inventory_ItemBeenAdded;
        inventory.ItemRemoved += Inventory_ItemBeenRemoved;
    }

    private void Inventory_ItemBeenAdded(object sender, InventoryEventArgs e)
    {
        foreach (Transform slot in inventoryPanel)
        {
           Transform imageTransform = slot.GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            DragHandler itemDragHandler = imageTransform.GetComponent<DragHandler>();

            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.setImage;
                itemDragHandler.Item = e.Item;

                break;
            }               
        }
    }

    private void Inventory_ItemBeenRemoved(object sender, InventoryEventArgs e)
    {
        foreach (Transform slot in inventoryPanel)
        {
            Transform imageTransform = slot.GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            DragHandler itemDragHandler = imageTransform.GetComponent<DragHandler>();

            if (itemDragHandler.Item.Equals(e.Item))
            {
                image.enabled = false;
                image.sprite = null;
                itemDragHandler.Item = null;
                Destroy(slot.gameObject);

                break;
            }
        }
    }
}
