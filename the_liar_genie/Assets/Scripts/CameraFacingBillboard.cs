//cameraFacingBillboard.cs v02
//by Neil Carter (NCarter)
//modified by Juan Castaneda (juanelo)
//
//added in-between GRP object to perform rotations on
//added auto-find main camera
//added un-initialized state, where script will do nothing
using UnityEngine;
using System.Collections;


public class CameraFacingBillboard : MonoBehaviour
{

    public Camera m_Camera;
    public bool amActive = false;
    public bool autoInit = false;
    GameObject myContainer;

    SpriteRenderer sr;

    public float clipDistance = 0.5f;

    void Awake()
    {
        if (autoInit == true)
        {
            m_Camera = Camera.main;
            amActive = true;
            sr = this.GetComponent<SpriteRenderer>();
        }

        myContainer = new GameObject();
        myContainer.name = "GRP_" + transform.gameObject.name;
        myContainer.transform.position = transform.position;
        myContainer.transform.parent = transform.parent;
        transform.parent = myContainer.transform;
    }

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        if (amActive == true)
        {
            if (this.transform.position.z - m_Camera.transform.position.z < clipDistance) sr.enabled = false;
            else sr.enabled = true;
            myContainer.transform.LookAt(myContainer.transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);
        }
    }
}