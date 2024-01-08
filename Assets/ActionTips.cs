using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTips : MonoBehaviour
{
    public float speed;

    private bool bigger;
    private float scaleX;
    private float scaleY;

    void Start()
    {
        scaleX = scaleY = 1;
    }

    void Update()
    {
        SizeControl();
    }

    private void SizeControl()
    {
        transform.localScale = new Vector3(scaleX, scaleY, transform.localScale.z);
        if (!bigger)
        {
            scaleX -= speed;
            scaleY -= speed;
        }
        else
        {
            scaleX += speed;
            scaleY += speed;
        }

        if (scaleX <= 1)
            bigger = true;
        else if (scaleX >= 1.2f) 
            bigger = false;
    }
}
