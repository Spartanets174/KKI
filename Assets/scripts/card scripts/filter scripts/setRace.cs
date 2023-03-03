using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setRace : MonoBehaviour
{
   /*Класс, который устанавливает значение поля race в классе 
   CardFilter при клике на соответсвующую кнопку фильрации в игре*/
   public cardFilter CardFilter;
   public bool isSelected=false;
    public cardSpawner cardSpawner;
   [SerializeField] private Dropdown dropdown;
    public void SetRace()
    {
        //Если в магазине
        if (cardSpawner.isShop)
        {
            isSelected = !isSelected;
            //Отключение всех др кнопок, отвечающих за выбор расы,
            //чтобы нельзя было выбирать несколько рас для фильтрации
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
            //Если кнопка выбрана, то передаётся значение расы данной кнопки в фунцию
            //cardFiltration класса Card filter
            if (isSelected)
            {
                transform.GetChild(1).GetComponent<Toggle>().isOn = true;
                string rase = transform.GetChild(0).GetComponent<Text>().text;
                if (rase == "Тёмные Эльфы")
                {
                    rase = "ТёмныеЭльфы";
                }
                if (rase == "Магические существа")
                {
                    rase = "МагическиеСущества";
                }
                CardFilter.cardRace = rase;
                CardFilter.cardFiltration();
            }
            //Если кнопка отжата, то передаётся значение пустое значение расы для функции
            //cardFiltration класса Card filter
            else
            {
                transform.GetChild(1).GetComponent<Toggle>().isOn = false;
                CardFilter.cardRace = "";
                CardFilter.cardFiltration();
            }
        }
        //Если в книге карт
        else
        {
            //получение выбранного значения расы из выпадающего списка
            Dropdown tempDropdown = dropdown.GetComponent<Dropdown>();
            if (tempDropdown.options[tempDropdown.value].text=="Все")
            {
                CardFilter.cardRace = "";
            }
            else
            {
                string rase = tempDropdown.options[tempDropdown.value].text;
                if (rase == "Тёмные эльфы")
                {
                    rase = "ТёмныеЭльфы";
                }
                if (rase == "Магические существа")
                {
                    rase = "МагическиеСущества";
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
