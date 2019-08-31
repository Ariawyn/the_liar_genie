using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Pickupable : MonoBehaviour
{
    SphereCollider sphereCollider;

    Inventory currentInventoryUnheld;
    public Inventory currentInventoryHeld;
    

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = this.GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (currentInventoryUnheld != null) {
                currentInventoryUnheld.HoldObject(this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Inventory>() != null) {
            this.currentInventoryUnheld = other.GetComponent<Inventory>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Inventory>() != null && other.GetComponent<Inventory>() == this.currentInventoryUnheld)
        {
            this.currentInventoryUnheld = null;
        }
    }
}
