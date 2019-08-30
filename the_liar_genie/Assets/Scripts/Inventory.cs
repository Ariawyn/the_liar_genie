using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Inventory : MonoBehaviour
{
    public Pickupable heldObject;
    public Transform objectHolder;

    public float minXThrow, maxXThrow, minZThrow, maxZthrow;

    public float groundYPos;

    public float throwPower;

    public int throwNumJumps;

    public float throwDuration;
    // Start is called before the first frame update
    void Start()
    {
        if (objectHolder != null && objectHolder.childCount > 0) {
            heldObject = objectHolder.GetComponentInChildren<Pickupable>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            ThrowObject();
    }

    public void HoldObject(Pickupable toHold) {
        if (heldObject != null)
        {
            ThrowObject();
        }

        heldObject = toHold;
        heldObject.transform.parent = objectHolder;
        heldObject.transform.DOLocalJump(Vector3.zero, throwPower, 1, throwDuration);
        //heldObject.transform.localPosition = Vector3.zero;
        heldObject.currentInventoryHeld = this;
    }

    public void ReleaseObjectToOtherInventory(Inventory other) {
        heldObject.currentInventoryHeld = null;
        other.HoldObject(heldObject);
        this.heldObject = null;
    }

    public void ThrowObject() {
        heldObject.transform.parent = null;

        Vector3 throwEnd = new Vector3(objectHolder.transform.position.x + Random.Range(minXThrow, maxXThrow), groundYPos, objectHolder.transform.position.z+Random.Range(minZThrow, maxZthrow));

        heldObject.transform.DOJump(throwEnd, throwPower, throwNumJumps, throwDuration);
        heldObject.currentInventoryHeld = null;
        this.heldObject = null;
    }
}
