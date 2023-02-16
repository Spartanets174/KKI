using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnUserCards : MonoBehaviour
{
    //Спавн пользовательских карт при запуске библиотеки карт
    public cardSpawner cardSpawner;
    public PlayerManager1 playerManager1;
    public void cardSpawn()
    {
        cardSpawner.cardSpawn(playerManager1.allUserCharCards);
        cardSpawner.cardSupportSpawn(playerManager1.allUserSupportCards);
    }
}
