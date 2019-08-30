using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingBlockTarget : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SlidingBlock>() != null) {
            SlidingBlock sb = other.GetComponent<SlidingBlock>();

            sb.StopMe();
            sb.isInFinalZone = true;

            Debug.Log("Nice place for a sliding block!");
        }
    }
}
