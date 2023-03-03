using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setRace : MonoBehaviour
{
   /*�����, ������� ������������� �������� ���� race � ������ 
   CardFilter ��� ����� �� �������������� ������ ��������� � ����*/
   public cardFilter CardFilter;
   public bool isSelected=false;
    public cardSpawner cardSpawner;
   [SerializeField] private Dropdown dropdown;
    public void SetRace()
    {
        //���� � ��������
        if (cardSpawner.isShop)
        {
            isSelected = !isSelected;
            //���������� ���� �� ������, ���������� �� ����� ����,
            //����� ������ ���� �������� ��������� ��� ��� ����������
            for (int i = 0; i < CardFilter.raceButtons.Count; i++)
            {
                CardFilter.raceButtons[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;
                if (this.name != CardFilter.raceButtons[i].name)
                {
                    CardFilter.raceButtons[i].GetComponent<setRace>().isSelected = false;
                }
            }
            for (int i = 0; i < CardFilter.supportButtons.Count; i++)
            {
                CardFilter.supportButtons[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;
                CardFilter.supportButtons[i].GetComponent<setSupportType>().isSelected = false;
            }
            //���� ������ �������, �� ��������� �������� ���� ������ ������ � ������
            //cardFiltration ������ Card filter
            if (isSelected)
            {
                transform.GetChild(1).GetComponent<Toggle>().isOn = true;
                string rase = transform.GetChild(0).GetComponent<Text>().text;
                if (rase == "Ҹ���� �����")
                {
                    rase = "Ҹ���������";
                }
                if (rase == "���������� ��������")
                {
                    rase = "������������������";
                }
                CardFilter.cardRace = rase;
                CardFilter.cardFiltration();
            }
            //���� ������ ������, �� ��������� �������� ������ �������� ���� ��� �������
            //cardFiltration ������ Card filter
            else
            {
                transform.GetChild(1).GetComponent<Toggle>().isOn = false;
                CardFilter.cardRace = "";
                CardFilter.cardFiltration();
            }
        }
        //���� � ����� ����
        else
        {
            //��������� ���������� �������� ���� �� ����������� ������
            Dropdown tempDropdown = dropdown.GetComponent<Dropdown>();
            if (tempDropdown.options[tempDropdown.value].text=="���")
            {
                CardFilter.cardRace = "";
            }
            else
            {
                string rase = tempDropdown.options[tempDropdown.value].text;
                if (rase == "Ҹ���� �����")
                {
                    rase = "Ҹ���������";
                }
                if (rase == "���������� ��������")
                {
                    rase = "������������������";
                }
                CardFilter.cardRace = rase;
            }          
            for (int i = 0; i < CardFilter.supportButtons.Count; i++)
            {
                CardFilter.supportButtons[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;
                CardFilter.supportButtons[i].GetComponent<setSupportType>().isSelected = false;
            }
            CardFilter.cardFiltration();
        }
    }
}
