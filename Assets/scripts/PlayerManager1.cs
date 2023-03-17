using System.Collections.Generic;
using UnityEngine;

public class PlayerManager1:MonoBehaviour
{
    public PlayerData playerData;
    public DbManager DB;
    public List<Card> allCharCards;
    public List<cardSupport> allSupportCards;
    public List<Card> allUserCharCards;
    public List<cardSupport> allUserSupportCards;
    public List<Card> deckUserCharCards;
    public List<cardSupport> deckUserSupportCards;
    public int money = 100000;
    public bool isGame;
    private void Awake()
    {
        this.allCharCards = playerData.allShopCharCards;
        this.allSupportCards = playerData.allShopSupportCards;
        this.allUserCharCards = playerData.allUserCharCards;
        this.allUserSupportCards = playerData.allUserSupportCards;
        this.deckUserCharCards = playerData.deckUserCharCards;
        this.deckUserSupportCards = playerData.deckUserSupportCards;
        this.money = playerData.money;
        //playerData.allCharCards = this.allCharCards;
        //playerData.allSupportCards = this.allSupportCards;
        //playerData.allUserCharCards = this.allUserCharCards;
        //playerData.allUserSupportCards = this.allUserSupportCards;
        //playerData.deckUserCharCards = this.deckUserCharCards;
        //playerData.deckUserSupportCards = this.deckUserSupportCards;
        //playerData.money = this.money;
    }
    private void FixedUpdate()
    {
        playerData.allShopCharCards = this.allCharCards;
        playerData.allShopSupportCards = this.allSupportCards;
        playerData.allUserCharCards = this.allUserCharCards;
        playerData.allUserSupportCards = this.allUserSupportCards;
        playerData.deckUserCharCards = this.deckUserCharCards;
        playerData.deckUserSupportCards = this.deckUserSupportCards;
        playerData.money = this.money;
    }
    private void OnApplicationQuit()
    {
        DB.UpdatePlayerBalance(playerData);

        DB.RemoveCardsSupportShop(playerData);
        DB.InsertToCardsSupportShop(playerData);

        DB.RemoveCardsShop(playerData);
        DB.InsertToCardsShop(playerData);

        DB.RemoveCardsSupportOwn(playerData);
        DB.InsertToCardsSupportOwn(playerData);

        DB.RemoveCardsOwn(playerData);
        DB.InsertToCardsOwn(playerData);


        DB.closeCon();
        Debug.Log("closed");
    }
}