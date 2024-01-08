using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRoller : MonoBehaviour
{
    public float forceMultiply;
    private float mass;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            mass = other.GetComponent<Rigidbody>().mass;
            //Åö×²µã
            Vector3 hitPoint = other.ClosestPointOnBounds(transform.position);
            Vector3 realForce = (hitPoint - transform.position).normalized * mass * forceMultiply;
            other.GetComponent<Rigidbody>().AddForceAtPosition(realForce, hitPoint);
        }
    }
}
