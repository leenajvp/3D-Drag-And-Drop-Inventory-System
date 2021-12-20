using UnityEngine;

public class Helmets : CollectableBase
{
    public override void Drop()
    {
        base.Drop();

        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.gameObject.tag == "HatPlacement") // Find correct drop spot
            {
                Debug.Log("yay");

                GameObject hatDropSpot = hit.collider.gameObject;
                string spotName = gameObject.name.ToString(); 
                Transform dropSpots = hatDropSpot.transform.Find(spotName); // Find correct helmet to turn on
                var dropSpotMesh = dropSpots.GetComponent<SkinnedMeshRenderer>();
                HatDropSpot[] checkHelmets = hatDropSpot.GetComponentsInChildren<HatDropSpot>();

                foreach (var child in checkHelmets)
                {
                    HatDropSpot checkExisting = child.GetComponent<HatDropSpot>();

                    if (checkExisting.isPlaced == false)
                    {
                        checkExisting.playerScript.helmetCollected = true;
                        checkExisting.isPlaced = true;
                        dropSpotMesh.enabled = true;
                        Destroy(gameObject);
                    }

                    else // Drop helmet in world if no drop spot hit
                    {
                        gameObject.SetActive(true);
                        gameObject.transform.position = hit.point;
                        isAvailable = false;
                    }
                }
            }
        }
    }
}
