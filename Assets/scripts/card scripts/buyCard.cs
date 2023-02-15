using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buyCard : MonoBehaviour
{
    alWindowOpen alWindowOpen;
    public cardSpawner cardSpawner;
    public PlayerManager1 playerManager1;
    public Text Money;
   public void buyAnyCard()
    {
        this.GetComponent<Button>().interactable = false;
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
        if (alWindowOpen.cardDisplay!=null) 
        {           
            playerManager1.allUserCharCards.Add(alWindowOpen.cardDisplay.card);
            for (int i = 0; i < playerManager1.allCharCards.Count; i++)
            {
                if (playerManager1.allCharCards[i].name== alWindowOpen.cardDisplay.card.name)
                {
                    playerManager1.allCharCards.Remove(playerManager1.allCharCards[i]);
                }
            }
            
            playerManager1.money -= alWindowOpen.cardDisplay.card.Price;
            cardSpawner.cardSpawn(playerManager1.allCharCards);
        }
        if (alWindowOpen.CardSupportDisplay != null)
        {          
            playerManager1.allUserSupportCards.Add(alWindowOpen.CardSupportDisplay.card);          
            for (int i = 0; i < playerManager1.allCharCards.Count; i++)
            {
                if (playerManager1.allSupportCards[i].name == alWindowOpen.CardSupportDisplay.card.name)
                {
                    playerManager1.allSupportCards.Remove(playerManager1.allSupportCards[i]);
                }
            }
            playerManager1.money -= alWindowOpen.CardSupportDisplay.card.Price;
            cardSpawner.cardSupportSpawn(playerManager1.allSupportCards);
        }
        Money.text =$"Ваши деньги: {playerManager1.money.ToString()}";
    }

}
