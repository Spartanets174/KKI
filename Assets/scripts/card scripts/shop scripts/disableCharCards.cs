using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disableCharCards : MonoBehaviour
{
    //Переспавн всех карт и отключение кнопок фильрации для их корректного отображения
    public cardFilter CardFilter;
    public void disable()
    {
        for (int i = 0; i < CardFilter.raceButtons.Count; i++)
        {
            CardFilter.raceButtons[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;
            CardFilter.raceButtons[i].GetComponent<setRace>().isSelected = false;
        }
        for (int i = 0; i < CardFilter.classButtons.Count; i++)
        {
            CardFilter.classButtons[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;
            CardFilter.classButtons[i].GetComponent<setCardClass>().isSelected = false;
        }
        for (int i = 0; i < CardFilter.supportButtons.Count; i++)
        {
            CardFilter.supportButtons[i].transform.GetChild(1).GetComponent<Toggle>().isOn = false;
            CardFilter.supportButtons[i].GetComponent<setSupportType>().isSelected = false;
        }
        CardFilter.cardRace = "";
        CardFilter.cardClass = "";
        CardFilter.cardSupportType = "";
        CardFilter.cardSupportFiltration();
        CardFilter.cardFiltration();
    }
 }
