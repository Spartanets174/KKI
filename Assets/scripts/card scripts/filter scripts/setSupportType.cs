using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setSupportType : MonoBehaviour
{
    /*�����, ������� ������������� �������� ���� class � ������ 
   CardFilter ��� ����� �� �������������� ������ ��������� � ����*/
    public cardFilter CardFilter;
    public bool isSelected = false;
    public void SetSupport()
    {
        isSelected = !isSelected;
        //���������� ���� �� ������, ���������� �� ����� ������ �����,
        //����� ������ ���� �������� ��������� ��� ��� ����������
        for (int i = 0; i < CardFilter.supportButtons.Count; i++)
        {
            CardFilter.supportButtons[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;
            if (this.name != CardFilter.supportButtons[i].name)
            {
                CardFilter.supportButtons[i].GetComponent<setSupportType>().isSelected = false;
            }
        }
        //���� ������ �������, �� ��������� �������� ������ ����� ������ ������ � ������
        //cardFiltration ������ Card filter
        if (isSelected)
        {
            transform.GetChild(1).GetComponent<Toggle>().isOn = true;
            CardFilter.cardSupportType = transform.GetChild(0).GetComponent<Text>().text;
            CardFilter.cardSupportFiltration();
        }
        //���� ������ ������, �� ��������� �������� ������ �������� ������ ����� ��� �������
        //cardFiltration ������ Card filter
        else
        {
            transform.GetChild(1).GetComponent<Toggle>().isOn = false;
            CardFilter.cardSupportType = "";
            CardFilter.cardSupportFiltration();
        }
    }
}
