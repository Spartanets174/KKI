using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setSupportType : MonoBehaviour
{
    /*Класс, который устанавливает значение поля class в классе 
   CardFilter при клике на соответсвующую кнопку фильрации в игре*/
    public cardFilter CardFilter;
    public bool isSelected = false;
    public void SetSupport()
    {
        isSelected = !isSelected;
        //Отключение всех др кнопок, отвечающих за выбор класса карты,
        //чтобы нельзя было выбирать несколько рас для фильтрации
        for (int i = 0; i < CardFilter.supportButtons.Count; i++)
        {
            CardFilter.supportButtons[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;
            if (this.name != CardFilter.supportButtons[i].name)
            {
                CardFilter.supportButtons[i].GetComponent<setSupportType>().isSelected = false;
            }
        }
        //Если кнопка выбрана, то передаётся значение класса карты данной кнопки в фунцию
        //cardFiltration класса Card filter
        if (isSelected)
        {
            transform.GetChild(1).GetComponent<Toggle>().isOn = true;
            CardFilter.cardSupportType = transform.GetChild(0).GetComponent<Text>().text;
            CardFilter.cardSupportFiltration();
        }
        //Если кнопка отжата, то передаётся значение пустое значение класса карты для функции
        //cardFiltration класса Card filter
        else
        {
            transform.GetChild(1).GetComponent<Toggle>().isOn = false;
            CardFilter.cardSupportType = "";
            CardFilter.cardSupportFiltration();
        }
    }
}
