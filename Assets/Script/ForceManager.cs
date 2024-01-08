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
        //����
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }


    void Update()
    {
        //��¼���λ��
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
        //��ʾ���
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
        //�������
        if (Input.GetMouseButton(0))
        {
            //hits = Physics.RaycastAll(ray, Mathf.Infinity, table);
            Physics.Raycast(ray, out RaycastHit hitInfo2, Mathf.Infinity, table);

            if (!start)
            {
                //��¼������
                originPos = hitInfo.point;
                //���λ�ù̶�
                point.transform.position = hitInfo.point;
                start = true;
                //��ʾ��ͷ
                arrows.gameObject.SetActive(true);
            }

            //��ͷ����
            var endPos = originPos - length * distance;
            arrows.SetPosition(0, originPos);
            arrows.SetPosition(1, endPos);
            arrowHead.position = endPos;
            arrowHead.rotation = Quaternion.LookRotation(endPos - originPos, Vector3.up);
            //arrowHead.eulerAngles = new Vector3(0, (endPos - originPos), 0);
            //��ͷ���
            arrows.startWidth = startWidth;// * distance.magnitude;
            arrows.endWidth = endWidth;// * distance.magnitude;

            //���������ľ���
            distance = hitInfo2.point - originPos;
        }
    }

    private void LeftButtonUp()
    {
        //̧�����
        if (Input.GetMouseButtonUp(0))
        {
            //������
            F = force * -distance.normalized * distance.magnitude;

            var size = selectObj.GetComponent<Renderer>().bounds.size;
            rb = selectObj.GetComponent<Rigidbody>();
            rb.AddForceAtPosition(new Vector3(F.x, 0, F.z), new Vector3(originPos.x, originPos.y - size.y * 0.5f, originPos.z));

            start = false;
            distance = Vector3.zero;

            //��ͷ��ʧ
            arrows.gameObject.SetActive(false);

            //�ж�����
            point.SetActive(false);
            WaitingBehaviour.instance.WaitingMove(selectObj);
        }
    }

}
