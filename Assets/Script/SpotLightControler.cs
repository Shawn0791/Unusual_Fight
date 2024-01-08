using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightControler : MonoBehaviour
{
    private float timer;
    private float rotateY;
    private float originY;
    private bool turn;

    public float rotateSpeed;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        originY = rotateY = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(30, rotateY, 0);
        
        if (timer > 0)
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (!turn && rotateY > originY - 30)
                rotateY -= rotateSpeed;
            else if (turn && rotateY < originY + 30)  
                rotateY += rotateSpeed;
        }

        if (rotateY < originY - 30 && !turn)
        {
            timer = waitTime;
            turn = true;
        }
        else if (rotateY > originY + 30 && turn)  
        {
            timer = waitTime;
            turn = false;
        }
    }
}
