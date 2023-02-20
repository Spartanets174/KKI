using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Скрипт для добавления карты в колоду
public class addToDeck : MonoBehaviour
{
    public cardSpawner cardSpawner;
    public PlayerManager1 playerManager1;
    public List<GameObject> charCardsWindow;
    public List<GameObject> supportCardsWindow;
  
    public void AddToDeck()
    {        
        //Перебор всех объектов (карточек персонажа)
        for (int i = 0; i < cardSpawner.listOfCardObjects.Count; i++)
        {
            //Проверка на то, какая именно карточка сейчас просматривается
            if (cardSpawner.listOfCardObjects[i].GetComponent<alWindowOpen>().isOpen)
            {
                //Не максимум ли карт в колоде
                if (playerManager1.deckUserCharCards.Count<5)
                {
                    //Добавление в колоду выбранной карты
                    playerManager1.deckUserCharCards.Add(cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card);
                    //Удаление из списка карт пользователя (теперь она в колоде)
                    playerManager1.allUserCharCards.Remove(cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card);
                    //Переспавн карт пользователя ,относительно пред. изменения
                    cardSpawner.cardSpawn(playerManager1.allUserCharCards);
                    //Редактирование окна карты в колоде
                    charCardsWindow[playerManager1.deckUserCharCards.Count - 1].transform.GetChild(1).GetComponent<Image>().sprite = playerManager1.deckUserCharCards[playerManager1.deckUserCharCards.Count - 1].image;
                    charCardsWindow[playerManager1.deckUserCharCards.Count - 1].transform.GetChild(2).GetComponent<Text>().text = playerManager1.deckUserCharCards[playerManager1.deckUserCharCards.Count - 1].name;
                    break;
                }
                
            }           
        }
        for (int i = 0; i < cardSpawner.listOfCardSupportObjects.Count; i++)
        {
            if (cardSpawner.listOfCardSupportObjects[i].GetComponent<alWindowOpen>().isOpen)
            {
                if (playerManager1.deckUserSupportCards.Count < 7)
                {
                    playerManager1.deckUserSupportCards.Add(cardSpawner.listOfCardSupportObjects[i].GetComponent<cardSupportDisplay>().card);
                    playerManager1.allUserSupportCards.Remove(cardSpawner.listOfCardSupportObjects[i].GetComponent<cardSupportDisplay>().card);
                    cardSpawner.cardSupportSpawn(playerManager1.allUserSupportCards);
                    supportCardsWindow[playerManager1.deckUserSupportCards.Count - 1].transform.GetChild(1).GetComponent<Image>().sprite = playerManager1.deckUserSupportCards[playerManager1.deckUserSupportCards.Count - 1].image;
                    supportCardsWindow[playerManager1.deckUserSupportCards.Count - 1].transform.GetChild(2).GetComponent<Text>().text = playerManager1.deckUserSupportCards[playerManager1.deckUserSupportCards.Count - 1].name;
                    break;
                }
            }
        }
    }
    private void Start()
    {
        for (int i = 0; i < playerManager1.deckUserCharCards.Count; i++)
        {
            charCardsWindow[i].transform.GetChild(1).GetComponent<Image>().sprite = playerManager1.deckUserCharCards[i].image;
            charCardsWindow[i].transform.GetChild(2).GetComponent<Text>().text = playerManager1.deckUserCharCards[i].name;
        }
        for (int i = 0; i < playerManager1.deckUserSupportCards.Count; i++)
        {
            supportCardsWindow[i].transform.GetChild(1).GetComponent<Image>().sprite = playerManager1.deckUserSupportCards[i].image;
            supportCardsWindow[i].transform.GetChild(2).GetComponent<Text>().text = playerManager1.deckUserSupportCards[i].name;
        }
    }
}
