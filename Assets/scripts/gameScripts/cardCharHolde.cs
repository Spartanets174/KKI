using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Скрипт, который отвечает за взаимодействие карточек персонажей в интерфейсе
//С основным battleSystem
public class cardCharHolde : MonoBehaviour
{
    //Ссылка на саму карту
    public Card card;
    //Выбрана ли карта сейчас
    public bool isSelected;
    //Была ли выбрана карта до этого (нужно для расстановки юнитов в начале игры)
    public bool wasChosen;
    public BattleSystem battleSystem;
    //Устанавливает isSelected в true
    public void setSelected()
    {
        //Проверка на то, идёт ли сейчас расстановка юнитов, т.к. логика немного отличается
        if (battleSystem.isUnitPlacement)
        {
            isSelected = true;
            for (int i = 0; i < battleSystem.charCardsUI.Count; i++)
            {
                //Отключение кнопок, чтобы низя был перевыбрать юнита, пока ты не поставил его на нужное место на поле
                battleSystem.charCardsUI[i].GetComponent<Button>().interactable = false;
                battleSystem.charCardsUI[i].GetComponent<Button>().enabled = false;
            }
            this.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
    }
}
