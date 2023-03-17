using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buyCard : MonoBehaviour
{
    //������ ��� ������� ����
    alWindowOpen alWindowOpen;
    public cardSpawner cardSpawner;
    public cardFilter CardFilter;
    public PlayerManager1 playerManager1;
    public Text Money;
   public void buyAnyCard()
    {
        
            //���������� ������ ����� ������� (����� �� ������)
            this.GetComponent<Button>().interactable = false;
        //���������� ����������, ��� �������� ������������ ���� �������
        for (int i = 0; i < cardSpawner.listOfCardObjects.Count; i++)
        {
            if (cardSpawner.listOfCardObjects[i].GetComponent<alWindowOpen>().isOpen == true)
            {
                alWindowOpen = cardSpawner.listOfCardObjects[i].GetComponent<alWindowOpen>();
            }
        }
        for (int i = 0; i < cardSpawner.listOfCardSupportObjects.Count; i++)
        {
            if (cardSpawner.listOfCardSupportObjects[i].GetComponent<alWindowOpen>().isOpen == true)
            {
                alWindowOpen = cardSpawner.listOfCardSupportObjects[i].GetComponent<alWindowOpen>();
            }
        }
       
            //�������� ����� ��� ����� (����� ������ ��� ���������) ������ ������������
            if (alWindowOpen.cardDisplay != null)
            {
                if (playerManager1.money >= alWindowOpen.cardDisplay.card.Price)
                {
                    //���������� ���� � ������ ��������� � ������������
                    playerManager1.allUserCharCards.Add(alWindowOpen.cardDisplay.card);
                    //�������� ��������� ����� �� ������ � ��������
                    for (int i = 0; i < playerManager1.allCharCards.Count; i++)
                    {
                        if (playerManager1.allCharCards[i].name == alWindowOpen.cardDisplay.card.name)
                        {
                            playerManager1.allCharCards.Remove(playerManager1.allCharCards[i]);
                        }
                    }
                    playerManager1.money -= alWindowOpen.cardDisplay.card.Price;
                    Money.text = $"���� ������: {playerManager1.money}$";
                    CardFilter.cardFiltration();
                }
                else
                {
                    Money.text = "� ��� ������������ �����";
                }
            }
            if (alWindowOpen.CardSupportDisplay != null)
            {
                if (playerManager1.money >= alWindowOpen.CardSupportDisplay.card.Price)
                {
                    playerManager1.allUserSupportCards.Add(alWindowOpen.CardSupportDisplay.card);
                    for (int i = 0; i < playerManager1.allSupportCards.Count; i++)
                    {
                        if (playerManager1.allSupportCards[i].name == alWindowOpen.CardSupportDisplay.card.name)
                        {
                            playerManager1.allSupportCards.Remove(playerManager1.allSupportCards[i]);
                        }
                    }
                    playerManager1.money -= alWindowOpen.CardSupportDisplay.card.Price;
                    Money.text = $"���� ������: {playerManager1.money}$";
                    CardFilter.cardSupportFiltration();

                }
                else
                {
                Debug.Log("sdf");
                    Money.text = "� ��� ������������ �����";
                }
            }
            //�.�. ���������� ��������� ����, ������� ������������ � ��������, �� ���������� �� ���������, ����� ��� �� ��������� ������ ��������� ����
            for (int i = 0; i < GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardObjects.Count; i++)
            {
                GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardObjects[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardSupportObjects.Count; i++)
            {
                GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardSupportObjects[i].gameObject.SetActive(false);
            }
            
              
    }
}
