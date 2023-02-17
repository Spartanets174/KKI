using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class removeFromDeck : MonoBehaviour
{
    public addToDeck addToDeck;
    public PlayerManager1 playerManager1;
    public cardSpawner cardSpawner;
    //������ ��� �������� ����� ��������� �� ������
    public void RemoveFRomDeck()
    {
        //������� ���� ���� � ������
        for (int i = 0; i < addToDeck.charCardsWindow.Count; i++)
        {            
            //�������� �� ����� ����� �� �������� ��� ��������
            if (transform.parent.gameObject.name == addToDeck.charCardsWindow[i].name)
            {
                //�������� �� �������� �� �� �� ������ �����
                if (addToDeck.charCardsWindow[i].transform.GetChild(1).GetComponent<Image>().sprite != null)
                {
                    //���������� � ���� ���� ������������ ��������� �����
                    playerManager1.allUserCharCards.Add(playerManager1.deckUserCharCards[i]);
                    //�������� �� ������ ��������� �����
                    playerManager1.deckUserCharCards.Remove(playerManager1.deckUserCharCards[i]);
                    //��������� ���� ������������ ������������ ���������
                    cardSpawner.cardSpawn(playerManager1.allUserCharCards);
                }
            }
            //��������� ������������ ��������� � �����
            addToDeck.charCardsWindow[i].transform.GetChild(1).GetComponent<Image>().sprite = null;
            addToDeck.charCardsWindow[i].transform.GetChild(2).GetComponent<Text>().text = "";
        }
        //�������������� ������������ ��������� � ����� ������������ ����. ���������
        for (int i = 0; i < playerManager1.deckUserCharCards.Count; i++)
        {
            addToDeck.charCardsWindow[i].transform.GetChild(1).GetComponent<Image>().sprite = playerManager1.deckUserCharCards[i].image;
            addToDeck.charCardsWindow[i].transform.GetChild(2).GetComponent<Text>().text = playerManager1.deckUserCharCards[i].name;
        }
    }
    //������ ��� �������� ���� ���������
    public void RemoveFromSupportDeck()
    {
        for (int i = 0; i < addToDeck.supportCardsWindow.Count; i++)
        {
            if (transform.parent.gameObject.name == addToDeck.supportCardsWindow[i].name)
            {
                if (addToDeck.supportCardsWindow[i].transform.GetChild(1).GetComponent<Image>().sprite != null)
                {
                    Debug.Log(i);
                    playerManager1.allUserSupportCards.Add(playerManager1.deckUserSupportCards[i]);
                    playerManager1.deckUserSupportCards.Remove(playerManager1.deckUserSupportCards[i]);
                    cardSpawner.cardSupportSpawn(playerManager1.allUserSupportCards);
                }
            }
            addToDeck.supportCardsWindow[i].transform.GetChild(1).GetComponent<Image>().sprite = null;
            addToDeck.supportCardsWindow[i].transform.GetChild(2).GetComponent<Text>().text = "";
        }
        for (int i = 0; i < playerManager1.deckUserSupportCards.Count; i++)
        {
            addToDeck.supportCardsWindow[i].transform.GetChild(1).GetComponent<Image>().sprite = playerManager1.deckUserSupportCards[i].image;
            addToDeck.supportCardsWindow[i].transform.GetChild(2).GetComponent<Text>().text = playerManager1.deckUserSupportCards[i].name;
        }
    }
}
