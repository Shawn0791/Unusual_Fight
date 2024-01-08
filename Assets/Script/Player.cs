using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isDead;
    public float force;
    public string type;

    private void OnTriggerEnter(Collider other)
    {
        switch (type)
        {
            case "wood":
                SoundService.instance.Play("wood");
                break;
            case "bone":
                SoundService.instance.Play("bone");
                break;
            case "stone":
                SoundService.instance.Play("stone");
                break;
            case "metal":
                SoundService.instance.Play("metal");
                break;
        }
    }
}
