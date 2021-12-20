using UnityEngine;

public class CollectableBase : MonoBehaviour, ICollectable
{
    [SerializeField]
    private CollectableData itemData;
    public virtual bool isAvailable { get; set; }
    private Sprite image = null;
    public Sprite setImage
    {
        get
        {
            return image;
        }
    }

    public virtual void Start()
    {
        if (itemData == null)
        {
            Debug.LogError("Collecteble in hierarchy " + name.ToString() + ": ItemData in collectable cannot be null");
        }

        isAvailable = true;
        gameObject.name = itemData.name;
        image = itemData.image;
    }

    public virtual void Collect()
    {
        gameObject.SetActive(false);
    }

    public virtual void Drop()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
            isAvailable = false;

            var hitDropSpot = hit.collider.GetComponent<IDropSpot>();

            if (hitDropSpot != null)
            {
                GameObject selectedDropSpot;
                selectedDropSpot = hit.collider.gameObject;
                gameObject.SetActive(true);
                transform.localScale = selectedDropSpot.transform.localScale;
                transform.rotation = selectedDropSpot.transform.rotation;
                transform.position = selectedDropSpot.transform.position;

                isAvailable = false;
            }
        }
    }
}
