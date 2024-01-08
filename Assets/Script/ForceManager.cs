using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    public LayerMask player;
    public LayerMask table;
    public GameObject point;
    public LineRenderer arrows;
    public Transform arrowHead;
    public float force;
    public float length;
    public float startWidth;
    public float endWidth;

    private Vector3 originPos;
    private Vector3 distance;
    private Vector3 F;
    private bool start;
    private Rigidbody rb;

    private RaycastHit hitInfo;
    private Ray ray;
    private GameObject selectObj;

    public static ForceManager instance;
    private void Awake()
    {
        //单例
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }


    void Update()
    {
        //记录鼠标位置
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (GameManager.instance.gameMode == GameManager.GameMode.Player)
            ForcePoint();

        if (point.activeSelf == true)
        {
            LeftButtonDown();
            LeftButtonUp();
        }
    }

    private void ForcePoint()
    {
        //显示红点
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, player) &&
            hitInfo.transform.GetComponent<Player>().isDead == false &&
            !start) 
        {
            point.SetActive(true);
            point.transform.position = hitInfo.point;
            selectObj = hitInfo.transform.gameObject;
            force = selectObj.GetComponent<Player>().force;
        }
        else if (!Physics.Raycast(ray, out hitInfo, Mathf.Infinity, player)
            && !start)
        {
            point.SetActive(false);
        }
    }

    private void LeftButtonDown()
    {
        //按下左键
        if (Input.GetMouseButton(0))
        {
            //hits = Physics.RaycastAll(ray, Mathf.Infinity, table);
            Physics.Raycast(ray, out RaycastHit hitInfo2, Mathf.Infinity, table);

            if (!start)
            {
                //记录受力点
                originPos = hitInfo.point;
                //红点位置固定
                point.transform.position = hitInfo.point;
                start = true;
                //显示箭头
                arrows.gameObject.SetActive(true);
            }

            //箭头长度
            var endPos = originPos - length * distance;
            arrows.SetPosition(0, originPos);
            arrows.SetPosition(1, endPos);
            arrowHead.position = endPos;
            arrowHead.rotation = Quaternion.LookRotation(endPos - originPos, Vector3.up);
            //arrowHead.eulerAngles = new Vector3(0, (endPos - originPos), 0);
            //箭头宽度
            arrows.startWidth = startWidth;// * distance.magnitude;
            arrows.endWidth = endWidth;// * distance.magnitude;

            //计算拉开的距离
            distance = hitInfo2.point - originPos;
        }
    }

    private void LeftButtonUp()
    {
        //抬起左键
        if (Input.GetMouseButtonUp(0))
        {
            //计算力
            F = force * -distance.normalized * distance.magnitude;

            var size = selectObj.GetComponent<Renderer>().bounds.size;
            rb = selectObj.GetComponent<Rigidbody>();
            rb.AddForceAtPosition(new Vector3(F.x, 0, F.z), new Vector3(originPos.x, originPos.y - size.y * 0.5f, originPos.z));

            start = false;
            distance = Vector3.zero;

            //箭头消失
            arrows.gameObject.SetActive(false);

            //行动结束
            point.SetActive(false);
            WaitingBehaviour.instance.WaitingMove(selectObj);
        }
    }

}
