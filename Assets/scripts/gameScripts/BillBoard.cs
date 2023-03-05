using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    //������ ��� �������� health bar ������������ ������
    private Transform cam;
    // Update is called once per frame
    private void Start()
    {
        cam = GameObject.Find("MainCamera").transform;
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
