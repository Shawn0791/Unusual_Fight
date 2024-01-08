using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Up : MonoBehaviour
{
    public float forceMultiply;
    private float mass;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            mass = other.GetComponent<Rigidbody>().mass;
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * mass * forceMultiply);
        }
    }
}
