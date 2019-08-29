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

    private Vector3 directionToPush = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void FixedUpdate()
    {
        this.directionToPush = GetDirectionToPush();

        if (Input.GetButtonDown("Jump"))
        {
            body.constraints = RigidbodyConstraints.FreezeRotation;
            Debug.Log("Jump pressed! DirectionToPush is " + directionToPush);
            this.body.AddForce(directionToPush.normalized * pushForce, ForceMode.Impulse);
        }

        else if (body.velocity.magnitude <= 0.25f) {
            body.constraints = RigidbodyConstraints.FreezeAll;
            
        }
    }

    public void StopMe() {
        this.body.velocity = directionToPush * 0.25f ;
    }

    private Vector3 GetDirectionToPush() {
        Vector3 directionTo = Vector3.zero;
        

        foreach (Vector3 direction in vectorsToCheck) {
            
            Vector3 startPos = this.transform.position; // assumes this is a cube.  + (0.5f*this.transform.localScale.x) * direction
            RaycastHit hitInfo;
            if (Physics.BoxCast(startPos, boxCastSize/2, direction, out hitInfo, Quaternion.identity, detectPlayerDistance)) {
                if (hitInfo.collider.gameObject.GetComponent<PlayerDriver>() != null)
                {
                    //if (hitInfo.collider.gameObject.GetComponent<PlayerDriver>().GetSpeedPercent() >= 0.85f)
                        directionTo = -direction;
                }
            }
        }

        return directionTo;
    }

    private void OnDrawGizmos()
    {
        foreach (Vector3 direction in vectorsToCheck)
        {

            Vector3 startPos = this.transform.position;// + (0.5f * this.transform.localScale.x) * direction; // assumes this is a cube.
            RaycastHit hitInfo;
            Gizmos.DrawWireCube(startPos, boxCastSize/2);
            if (Physics.BoxCast(startPos, boxCastSize/2, direction, out hitInfo, Quaternion.identity, detectPlayerDistance))
            {
                if (hitInfo.collider.gameObject.CompareTag("Player"))
                    Gizmos.DrawCube(startPos, boxCastSize/2);
            }
        }
    }
}
