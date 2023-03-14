using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //����� ��� ��������� ������ �������� ���� � ������������ � ��������� �������
    public Material baseColor, offsetColor, swampColor;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private GameObject hightLight;
    public bool Enabled=true;
    public bool isSwamp;
    private BattleSystem battleSystem;
    //������������ ����� ������ � ����������� �� � �������� ��� ����������
    private void Start()
    {
        battleSystem = GameObject.Find("battleSystem").GetComponent<BattleSystem>();
    }
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
/*            Vector2 pos = new Vector2(transform.position.x, transform.position.z);
            GameObject.Find("field").GetComponent<Field>().GetCellAtPosition(pos);*/
            //�������� ������ � ������� ������ � battleSystem            
            if (this.transform.childCount!=1)
            {
                if (this.transform.GetChild(1).GetComponent<character>().isEnemy)
                {
                    battleSystem.cahngeCardWindow(this.transform.GetChild(1).gameObject, true);                  
                }
                else
                {
                    battleSystem.cahngeCardWindow(this.transform.GetChild(1).gameObject, false);                   
                }
                
            }
            if (!battleSystem.isEnemyTurn)
            {
                battleSystem.OnMoveButton(this.gameObject);
            }        
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
