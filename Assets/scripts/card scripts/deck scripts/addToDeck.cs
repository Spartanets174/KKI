using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//������ ��� ���������� ����� � ������
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
        
        //������� ���� �������� (�������� ���������)
        for (int i = 0; i < cardSpawner.listOfCardObjects.Count; i++)
        {
            //�������� �� ��, ����� ������ �������� ������ ���������������
            if (cardSpawner.listOfCardObjects[i].GetComponent<alWindowOpen>().isOpen)
            {
                //�� �������� �� ���� � ������
                if (playerManager1.deckUserCharCards.Count<5)
                {
                    //�������� �� ���-�� ���� ����������� ���� � ������ (��� ����� � �������� - 2, ��� ��������� - 1)
                    int count = 0;
                    int max = 5;                   
                    for (int j= 0; j < playerManager1.deckUserCharCards.Count; j++)
                    {
                        if (playerManager1.deckUserCharCards[j].Class == cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card.Class)
                        {
                            count++;
                        }
                    }
                    if (cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card.Class==enums.Classes.���|| cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card.Class == enums.Classes.������)
                    {
                        max = 2;
                    }
                    if (cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card.Class == enums.Classes.���������)
                    {
                        max = 1;
                    }
                    Debug.Log($"{count},{max},{cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card.Class}");
                    if (count<max)
                    {
                        //���������� � ������ ��������� �����
                        playerManager1.deckUserCharCards.Add(cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card);
                        //�������� �� ������ ���� ������������ (������ ��� � ������)
                        playerManager1.allUserCharCards.Remove(cardSpawner.listOfCardObjects[i].GetComponent<CardDisplay>().card);
                        //��������� ���� ������������ ,������������ ����. ���������
                        cardSpawner.cardSpawn(playerManager1.allUserCharCards);
                        //�������������� ���� ����� � ������
                        charCardsWindow[playerManager1.deckUserCharCards.Count - 1].transform.GetChild(1).GetComponent<Image>().sprite = playerManager1.deckUserCharCards[playerManager1.deckUserCharCards.Count - 1].image;
                        charCardsWindow[playerManager1.deckUserCharCards.Count - 1].transform.GetChild(2).GetComponent<Text>().text = playerManager1.deckUserCharCards[playerManager1.deckUserCharCards.Count - 1].name;
                        switch (playerManager1.deckUserCharCards[playerManager1.deckUserCharCards.Count - 1].Class)
                        {
                            case enums.Classes.�������:
                                charCardsWindow[playerManager1.deckUserCharCards.Count - 1].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[0];
                                break;
                            case enums.Classes.������:
                                charCardsWindow[playerManager1.deckUserCharCards.Count - 1].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[1];
                                break;
                            case enums.Classes.���:
                                charCardsWindow[playerManager1.deckUserCharCards.Count - 1].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[2];
                                break;
                            case enums.Classes.���������:
                                charCardsWindow[playerManager1.deckUserCharCards.Count - 1].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[3];
                                break;
                        }
                        //������� ���������� ���� ������ �����
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
                            case enums.Classes.������:
                                maxCardText.text = "� ������ ��� �������� �������� (2)";
                                break;
                            case enums.Classes.���:
                                maxCardText.text = "� ������ ��� �������� ����� (2)";
                                break;
                            case enums.Classes.���������:
                                maxCardText.text = "� ������ ��� �������� ��������� (1)";
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
                    //������� ���������� ���� ������ �����
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
        //�������� � ���� ��������� ����, ��� ���� � ������
        for (int i = 0; i < playerManager1.deckUserCharCards.Count; i++)
        {
            charCardsWindow[i].transform.GetChild(1).GetComponent<Image>().sprite = playerManager1.deckUserCharCards[i].image;
            charCardsWindow[i].transform.GetChild(2).GetComponent<Text>().text = playerManager1.deckUserCharCards[i].name;
            switch (playerManager1.deckUserCharCards[i].Class)
            {
                case enums.Classes.�������:
                    charCardsWindow[i].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[0];
                    break;
                case enums.Classes.������:
                    charCardsWindow[i].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[1];
                    break;
                case enums.Classes.���:
                    charCardsWindow[i].transform.GetChild(3).GetComponent<Image>().sprite = classesSprite[2];
                    break;
                case enums.Classes.���������:
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
