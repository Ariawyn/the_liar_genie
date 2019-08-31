using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlidingBlockTarget : MonoBehaviour
{

    public GameObject objectToSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SlidingBlock>() != null) {
            SlidingBlock sb = other.GetComponent<SlidingBlock>();

            sb.StopMe();
            sb.isInFinalZone = true;

            GameObject go = GameObject.Instantiate(objectToSpawn);
            go.transform.position = this.transform.position;
            go.transform.DOJump(this.transform.position + Vector3.left * 4, 2, 2, 1);
        }
    }
}
