using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//������, ������� �������� �� �������������� �������� ���������� � ����������
//� �������� battleSystem
public class cardCharHolde : MonoBehaviour
{
    //������ �� ���� �����
    public Card card;
    //������� �� ����� ������
    public bool isSelected;
    //���� �� ������� ����� �� ����� (����� ��� ����������� ������ � ������ ����)
    public bool wasChosen;
    public BattleSystem battleSystem;
    public healthBar healthBar;
    //������������� isSelected � true
    public void setSelected()
    {
        //�������� �� ��, ��� �� ������ ����������� ������, �.�. ������ ������� ����������
            isSelected = true;
            for (int i = 0; i < battleSystem.charCardsUI.Count; i++)
            {
                //���������� ������, ����� ���� ��� ����������� �����, ���� �� �� �������� ��� �� ������ ����� �� ����
                battleSystem.charCardsUI[i].GetComponent<Button>().enabled = false;
            }
            this.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
    }
}
