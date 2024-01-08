using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string type;
    public bool canAttack;
    public GameObject target;
    public bool isDead;
    [Tooltip("AI config")]
    public float turnTime=2;
    public float force;
    public float maxDis;
    public float minDis;

    [Range(0, 45)]
    public float angleVariantPercent;
    [Range(0, 50)]
    public float forceVariantPercent;

    private Rigidbody rb;
    private float distance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Attack()
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0;
        distance = direction.magnitude;
        //限制力度
        if (distance > maxDis)
            distance = maxDis;
        else if (distance < minDis)
            distance = minDis;
        //direction是敌人指向我的去除了y轴干扰的单位向量
        direction = direction.normalized;

        var newAngle = Quaternion.AngleAxis(Random.Range(-angleVariantPercent, angleVariantPercent), Vector3.up);
        var baseAngle = Quaternion.LookRotation(direction, Vector3.up);
        //两个Quaternion相乘，就是把他们对应的角度依次变换，得到的是最终的角度Quaternion
        var resultAngle = newAngle * baseAngle;
        //百分比调整后的力度
        var realForce = force * distance * (Random.Range(-forceVariantPercent, forceVariantPercent) / 100f + 1);
        //resultAngle是一个Quaternion，乘以Vector3.forward，得到这个Quaternion的forward对应的箭头的向量
        var finalDirection = resultAngle * Vector3.forward;

        rb.AddForce(finalDirection * realForce);

        //行动结束
        WaitingBehaviour.instance.WaitingMove(gameObject);
        target = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (type)
        {
            case "car":
                SoundService.instance.Play("car");
                break;
            case "gun":
                SoundService.instance.Play("gun");
                break;
            case "guitar_wood":
                SoundService.instance.Play("guitar_wood");
                break;
            case "guitar":
                SoundService.instance.Play("guitar");
                break;
            case "guitar_metal":
                SoundService.instance.Play("guitar_metal");
                break;
            case "metal":
                SoundService.instance.Play("metal");
                break;
            case "wood":
                SoundService.instance.Play("wood");
                break;
        }
    }
}
