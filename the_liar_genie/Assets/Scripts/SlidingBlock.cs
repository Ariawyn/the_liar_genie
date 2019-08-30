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
    public bool isInFinalZone = false;

    private RigidbodyConstraints stayStillConstraint = RigidbodyConstraints.FreezeAll;
    private RigidbodyConstraints allowSlideConstraint = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
        //body.constraints = stayStillConstraint;
    }

    private void FixedUpdate()
    {
        
        this.directionToPush = GetDirectionToPush();

        if (Input.GetButtonDown("Jump") && !isInFinalZone && this.directionToPush != Vector3.zero)
        {
            body.constraints = allowSlideConstraint;
            Debug.Log("Jump pressed! DirectionToPush is " + directionToPush);
            this.body.AddForce(directionToPush.normalized * pushForce, ForceMode.Impulse);
        }

        else if (body.velocity.magnitude <= 0.25f) {
            body.constraints = stayStillConstraint;
            
        }
    }

    public void StopMe() {
        this.body.velocity = directionToPush * 0.25f ;
    }

    private Vector3 GetDirectionToPush() {
        Vector3 directionTo = Vector3.zero;
        

        foreach (Vector3 direction in vectorsToCheck) {
            
            Vector3 startPos = this.transform.position + this.transform.localScale.x/2 * direction - Vector3.up * this.transform.localScale.x/2.2f; // assumes this is a cube.  + (0.5f*this.transform.localScale.x) * direction
            RaycastHit hitInfo;

            //Debug.DrawLine(startPos, startPos + direction * detectPlayerDistance, Color.red);
            //if (Physics.BoxCast(startPos, boxCastSize, direction, out hitInfo, Quaternion.identity, detectPlayerDistance)) {
            if (Physics.Raycast(startPos, direction, out hitInfo, detectPlayerDistance))
            {
                if (hitInfo.collider.gameObject.GetComponent<PlayerDriver>() != null)
                {
                    Debug.Log("I see a player");
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

            Vector3 startPos = this.transform.position + this.transform.localScale.x * direction - Vector3.up * this.transform.localScale.x / 2.2f;// + (0.5f * this.transform.localScale.x) * direction; // assumes this is a cube.
                                                                                                                                            //RaycastHit hitInfo;

            RaycastHit hitInfo;
            //Gizmos.DrawWireCube(startPos + this.transform.localScale.x * -direction, boxCastSize/2);
            //if (Physics.BoxCast(startPos, boxCastSize, direction, out hitInfo, Quaternion.identity, detectPlayerDistance))
            //{
            //   if (hitInfo.collider.gameObject.CompareTag("Player"))
            //        Gizmos.DrawCube(startPos, boxCastSize/2);
            //}
        }
    }
}
