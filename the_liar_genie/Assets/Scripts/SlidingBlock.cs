using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingBlock : MonoBehaviour
{

    public float detectPlayerDistance = 0.2f;

    public Vector3[] vectorsToCheck = new Vector3[4] { Vector3.forward, Vector3.right, -Vector3.forward, -Vector3.right };

    public float pushForce = 100;

    private Rigidbody body;

    public Vector3 boxCastSize;
    
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        this.body.AddForce(GetDirectionToPush() * pushForce, ForceMode.Impulse);

    }

    private Vector3 GetDirectionToPush() {
        Vector3 directionTo = Vector3.zero;
        

        foreach (Vector3 direction in vectorsToCheck) {
            
            Vector3 startPos = this.transform.position + (0.5f*this.transform.localScale.x) * direction; // assumes this is a cube.
            RaycastHit hitInfo;
            if (Physics.BoxCast(startPos, boxCastSize, direction, out hitInfo, Quaternion.identity, detectPlayerDistance)) {
                if (hitInfo.collider.gameObject.CompareTag("Player"))
                    directionTo = -direction;
            }
        }

        return directionTo;
    }

    private void OnDrawGizmos()
    {
        foreach (Vector3 direction in vectorsToCheck)
        {

            Vector3 startPos = this.transform.position + (0.5f * this.transform.localScale.x) * direction; // assumes this is a cube.
            RaycastHit hitInfo;
            Gizmos.DrawWireCube(startPos, boxCastSize);
            if (Physics.BoxCast(startPos, boxCastSize, direction, out hitInfo, Quaternion.identity, detectPlayerDistance))
            {
                if (hitInfo.collider.gameObject.CompareTag("Player"))
                    Gizmos.DrawCube(startPos, boxCastSize);
            }
        }
    }
}
