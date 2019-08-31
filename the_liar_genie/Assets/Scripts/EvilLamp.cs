using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pickupable))]
public class EvilLamp : MonoBehaviour
{

    Pickupable pickupable;
    Inventory heldInventory;
    // Start is called before the first frame update
    void Start()
    {
        pickupable = GetComponent<Pickupable>();
        
    }

    // Update is called once per frame
    void Update()
    {
        heldInventory = pickupable.currentInventoryHeld;

        if (heldInventory.gameObject.CompareTag("Pedestal")) {
            //TODO: Lose the game
        }
    }
}
