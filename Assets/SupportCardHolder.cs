using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupportCardHolder : MonoBehaviour
{
    //Ссылка на саму карту
    public cardSupport card;
    //Выбрана ли карта сейчас
    public bool isSelected;
    public BattleSystem battleSystem;
    //Устанавливает isSelected в true
    public void setSelected()
    {
        isSelected = true;
        for (int i = 0; i < battleSystem.charCardsUI.Count; i++)
        {
            //Отключение кнопок, чтобы низя был перевыбрать юнита, пока ты не поставил его на нужное место на поле
            battleSystem.charCardsUI[i].GetComponent<Button>().interactable = false;
            battleSystem.charCardsUI[i].GetComponent<Button>().enabled = false;
            battleSystem.charCardsUI[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
        this.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
    }
}
