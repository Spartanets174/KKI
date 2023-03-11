using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupportCardHolder : MonoBehaviour
{
    //������ �� ���� �����
    public cardSupport card;
    //������� �� ����� ������
    public bool isSelected;
    public BattleSystem battleSystem;
    //������������� isSelected � true
    public void setSelected()
    {
        isSelected = true;
        for (int i = 0; i < battleSystem.charCardsUI.Count; i++)
        {
            //���������� ������, ����� ���� ��� ����������� �����, ���� �� �� �������� ��� �� ������ ����� �� ����
            battleSystem.charCardsUI[i].GetComponent<Button>().interactable = false;
            battleSystem.charCardsUI[i].GetComponent<Button>().enabled = false;
            battleSystem.charCardsUI[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
        this.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
    }
}
