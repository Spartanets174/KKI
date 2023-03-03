using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Скрипт для добавления карты в колоду
public class addToDeck : MonoBehaviour
{
    public cardSpawner cardSpawner;
    public PlayerManager1 playerManager1;
    public Text maxCardText;
    public List<GameObject> charCardsWindow;
    public List<GameObject> supportCardsWindow;
    public List<Sprite> classesSprite;
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
                    //Проверка на кол-во карт определённой расы в колоде (для магов и лучников - 2, для кавалерии - 1)
                    int count = 0;
                    int max = 5;                   
                    for (int j= 0; j < playerManager1.deckUserCharCards.Count; j++)
                    {
                        if (playerManager1.deckUserCharCards[j].Class == cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card.Class)
                        {
                            count++;
                        }
                    }
                    if (cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card.Class==enums.Classes.Маг|| cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card.Class == enums.Classes.Лучник)
                    {
                        max = 2;
                    }
                    if (cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card.Class == enums.Classes.Кавалерия)
                    {
                        max = 1;
                    }
                    Debug.Log($"{count},{max},{cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card.Class}");
                    if (count<max)
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
                        switch (playerManager1.deckUserCharCards[playerManager1.deckUserCharCards.Count - 1].Class)
                        {
                            case enums.Classes.Паладин:
                                charCardsWindow[playerManager1.deckUserCharCards.Count - 1].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[0];
                                break;
                            case enums.Classes.Лучник:
                                charCardsWindow[playerManager1.deckUserCharCards.Count - 1].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[1];
                                break;
                            case enums.Classes.Маг:
                                charCardsWindow[playerManager1.deckUserCharCards.Count - 1].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[2];
                                break;
                            case enums.Classes.Кавалерия:
                                charCardsWindow[playerManager1.deckUserCharCards.Count - 1].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[3];
                                break;
                        }
                        //Очистка интерфейса окна выбора карты
                        GameObject.Find("character image").GetComponent<Image>().sprite = null;
                        GameObject.Find("character stats").GetComponent<Text>().text = $"";
                        GameObject.Find("character descripiton").GetComponent<Text>().text = "";
                        GameObject.Find("ability text").GetComponent<Text>().text = $"";
                        maxCardText.text = "";
                    }
                    else
                    {
                        switch (cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card.Class)
                        {
                            case enums.Classes.Лучник:
                                maxCardText.text = "В колоде уже максимум лучников (2)";
                                break;
                            case enums.Classes.Маг:
                                maxCardText.text = "В колоде уже максимум магов (2)";
                                break;
                            case enums.Classes.Кавалерия:
                                maxCardText.text = "В колоде уже максимум кавалерии (1)";
                                break;
                        }      
                    }
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
                    //Очистка интерфейса окна выбора карты
                    GameObject.Find("card image").GetComponent<Image>().sprite = null;
                    GameObject.Find("card ability").GetComponent<Text>().text = $"";
                    GameObject.Find("name card").GetComponent<Text>().text = $"";
                    GameObject.Find("rarity support").GetComponent<Image>().color = new Color(125, 125, 125);
                    GameObject.Find("rarity text").GetComponent<Text>().text = "";
                    break;
                }
            }
        }
    }
    private void Start()
    {
        //Загрузка в окна интерфейс кард, что есть в колоде
        for (int i = 0; i < playerManager1.deckUserCharCards.Count; i++)
        {
            charCardsWindow[i].transform.GetChild(1).GetComponent<Image>().sprite = playerManager1.deckUserCharCards[i].image;
            charCardsWindow[i].transform.GetChild(2).GetComponent<Text>().text = playerManager1.deckUserCharCards[i].name;
            switch (playerManager1.deckUserCharCards[i].Class)
            {
                case enums.Classes.Паладин:
                    charCardsWindow[i].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[0];
                    break;
                case enums.Classes.Лучник:
                    charCardsWindow[i].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[1];
                    break;
                case enums.Classes.Маг:
                    charCardsWindow[i].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[2];
                    break;
                case enums.Classes.Кавалерия:
                    charCardsWindow[i].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[3];
                    break;
            }
        }
        for (int i = 0; i < playerManager1.deckUserSupportCards.Count; i++)
        {
            supportCardsWindow[i].transform.GetChild(1).GetComponent<Image>().sprite = playerManager1.deckUserSupportCards[i].image;
            supportCardsWindow[i].transform.GetChild(2).GetComponent<Text>().text = playerManager1.deckUserSupportCards[i].name;
        }
    }
}
