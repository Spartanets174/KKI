using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setCardClass : MonoBehaviour
{
    /*�����, ������� ������������� �������� ���� class � ������ 
   CardFilter ��� ����� �� �������������� ������ ��������� � ����*/
    public cardFilter CardFilter;
    public bool isSelected = false;
    public void SetClass()
    {
        isSelected = !isSelected;
        //���������� ���� �� ������, ���������� �� ����� ������ �����,
        //����� ������ ���� �������� ��������� ��� ��� ����������
        for (int i = 0; i < CardFilter.classButtons.Count; i++)
        {
            CardFilter.classButtons[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;
            if (this.name != CardFilter.classButtons[i].name)
            {
                CardFilter.classButtons[i].GetComponent<setCardClass>().isSelected = false;
            }
        }
        //���� ������ �������, �� ��������� �������� ������ ����� ������ ������ � ������
        //cardFiltration ������ Card filter
        if (isSelected)
        {
            transform.GetChild(1).GetComponent<Toggle>().isOn = true;
            CardFilter.cardClass = transform.GetChild(0).GetComponent<Text>().text;
            CardFilter.cardFiltration();
        }
        //���� ������ ������, �� ��������� �������� ������ �������� ������ ����� ��� �������
        //cardFiltration ������ Card filter
        else
        {
            transform.GetChild(1).GetComponent<Toggle>().isOn = false;
            CardFilter.cardClass = "";
            CardFilter.cardFiltration();
        }
    }
}
