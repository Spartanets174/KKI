using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //����� ��� ��������� ������ �������� ���� � ������������ � ��������� �������
    public Material baseColor, offsetColor;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private GameObject hightLight;
    public bool Enabled=true;
    
    //������������ ����� ������ � ����������� �� � �������� ��� ����������
   public void Init(bool isOffset)
    {
        renderer.material = isOffset?offsetColor:baseColor;
    }
    //��� ����� �� ������ ��������� � ���������� � ������� � ����
    private void OnMouseDown()
    {
        //�������� �� ��, �������� �� ������
        if (Enabled)
        {
            //��������� � ����� ��������� ������
            Vector2 pos = new Vector2(transform.position.x, transform.position.z);
            GameObject.Find("field").GetComponent<Field>().GetCellAtPosition(pos);
            //�������� ������ � ������� ������ � battleSystem
            GameObject.Find("battleSystem").GetComponent<BattleSystem>().OnMoveButton(this.gameObject);
        }
        
    }
    private void OnMouseOver()
    {
        if (Enabled)
        {
            if (transform.childCount==1)
            {
                hightLight.SetActive(true);
            }           
        }
       
    }
    private void OnMouseExit()
    {
        if (Enabled)
        {
            hightLight.SetActive(false);
        }        
    }
}
