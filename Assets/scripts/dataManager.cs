using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataManager : MonoBehaviour
{
    public PlayerManager1 playerManager;
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        if (playerManager.isGame)
        {
            LoadData();
        }
    }
    public void SaveData()
    {
        playerData.allCharCards = playerManager.allCharCards;
        playerData.allSupportCards = playerManager.allSupportCards;
        playerData.allUserCharCards = playerManager.allUserCharCards;
        playerData.allUserSupportCards = playerManager.allUserSupportCards;
        playerData.deckUserCharCards = playerManager.deckUserCharCards;
        playerData.deckUserSupportCards = playerManager.deckUserSupportCards;
        playerData.money = playerManager.money;
    }
    public void LoadData()
    {
        playerManager.allCharCards = playerData.allCharCards ;
        playerManager.allSupportCards = playerData.allSupportCards ;
        playerManager.allUserCharCards = playerData.allUserCharCards;
        playerManager.allUserSupportCards = playerData.allUserSupportCards;
        playerManager.deckUserCharCards = playerData.deckUserCharCards;
        playerManager.deckUserSupportCards = playerData.deckUserSupportCards;
        playerManager.money = playerData.money;
    }
}
