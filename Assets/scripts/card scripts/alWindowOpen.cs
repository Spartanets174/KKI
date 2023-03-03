 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alWindowOpen : MonoBehaviour
{
    /*Скрипт нужен для того, чтобы записать нужные данные в
     * модальное окно для карточки, на которую ты кликнул*/
    public bool isOpen = false;
    public CardDisplay cardDisplay;
    public cardSupportDisplay CardSupportDisplay;
    
    void OnMouseUp()
    {
       
        cardSpawner cardSpawner = GameObject.Find("cardSpawner").GetComponent<cardSpawner>();
        for (int i = 0; i < cardSpawner.listOfCardObjects.Count; i++)
        {
            cardSpawner.listOfCardObjects[i].GetComponent<alWindowOpen>().isOpen = false;
        }
        for (int i = 0; i < cardSpawner.listOfCardSupportObjects.Count; i++)
        {
            cardSpawner.listOfCardSupportObjects[i].GetComponent<alWindowOpen>().isOpen = false;
        }
        //Проверка на то, является ли элемент, к которому подключен скрипт магазином
        //Нужно, т.к. у магазина и книги карт разные элементы, которые надо отображать
        if (cardSpawner.isShop)
        {
            isOpen = true;
            if (this.GetComponent<CardDisplay>() != null)
            {
                GameObject.Find("blur modal").GetComponent<setCoordTo0>().setCoord0();
                GameObject.Find("buy card").GetComponent<setCoordTo0>().setCoord0();
                GameObject.Find("buy card").transform.GetChild(1).GetComponent<Image>().sprite = cardDisplay.card.image;
                GameObject.Find("abilitySupport").GetComponent<Text>().text = $"Атакующая способность: {cardDisplay.card.attackAbility}" + "\n" + "\n" +
                    $"Защитная способность: {cardDisplay.card.defenceAbility}" + "\n" + "\n" +
                    $"Усиливающая способность: {cardDisplay.card.buffAbility}" + "\n" + "\n" +
                    $"Пассивная способность: {cardDisplay.card.passiveAbility}";
                GameObject.Find("price text").GetComponent<Text>().text = $"Цена: {cardDisplay.card.Price}$";
                /* отключение всех карточек, чтобы по ним нельзя было клинкуть сквозь модальное окно*/
                for (int i = 0; i < GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardObjects.Count; i++)
                {
                    GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardObjects[i].gameObject.SetActive(false);
                }
            }
            if (this.GetComponent<cardSupportDisplay>() != null)
            {
                
                GameObject.Find("blur modal").GetComponent<setCoordTo0>().setCoord0();
                GameObject.Find("buy card").GetComponent<setCoordTo0>().setCoord0();
                GameObject.Find("buy card").transform.GetChild(1).GetComponent<Image>().sprite = CardSupportDisplay.card.image;
                GameObject.Find("abilitySupport").GetComponent<Text>().text = $"Способность: {CardSupportDisplay.card.ability}";
                GameObject.Find("price text").GetComponent<Text>().text = $"Цена: {CardSupportDisplay.card.Price}$";

                /* отключение всех карточек, чтобы по ним нельзя было клинкуть сквозь модальное окно*/
                for (int i = 0; i < GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardSupportObjects.Count; i++)
                {
                    GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardSupportObjects[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            isOpen = true;
            if (this.GetComponent<CardDisplay>() != null)
            {
                GameObject.Find("character image").GetComponent<Image>().sprite = cardDisplay.card.image;
                GameObject.Find("character stats").GetComponent<Text>().text = $"Здоровье: {cardDisplay.card.health}" + "\n" +
                    $"Физ. атака: {cardDisplay.card.physAttack}" + "\n" +
                    $"Маг. атака: {cardDisplay.card.magAttack}" + "\n" +
                    $"Физ. защита: {cardDisplay.card.physDefence}" + "\n" +
                    $"Маг. защита: {cardDisplay.card.magDefence}" + "\n" +
                    $"Вер. крита: {cardDisplay.card.critChance}";
                GameObject.Find("character descripiton").GetComponent<Text>().text = cardDisplay.card.description;
                GameObject.Find("ability text").GetComponent<Text>().text = $"Атакующая способность: {cardDisplay.card.attackAbility}" + "\n" + "\n" +
                    $"Защитная способность: {cardDisplay.card.defenceAbility}" + "\n" + "\n" +
                    $"Усиливающая способность: {cardDisplay.card.buffAbility}" + "\n" + "\n" +
                    $"Пассивная способность: {cardDisplay.card.passiveAbility}";
                GameObject.Find("max card text").GetComponent<Text>().text = $"";
            }
            if (this.GetComponent<cardSupportDisplay>() != null)
            {
                GameObject.Find("card image").GetComponent<Image>().sprite = CardSupportDisplay.card.image;
                GameObject.Find("card ability").GetComponent<Text>().text = $"Способность: {CardSupportDisplay.card.ability}";
                GameObject.Find("name card").GetComponent<Text>().text = $"{CardSupportDisplay.card.name}";
                if (CardSupportDisplay.rarity.text== "Мифическая")
                {
                    GameObject.Find("rarity support").GetComponent<Image>().color = new Color(126, 0, 255);
                } 
                else
                {

                    GameObject.Find("rarity support").GetComponent<Image>().color = new Color(125, 125, 125);
                }
                GameObject.Find("rarity text").GetComponent<Text>().text = CardSupportDisplay.card.rarity.ToString();
            }
        }
    }
}

