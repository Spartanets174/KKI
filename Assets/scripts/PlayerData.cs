using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player data")]
public class PlayerData : ScriptableObject
{
    public List<Card> allCharCards;
    public List<cardSupport> allSupportCards;
    public List<Card> allShopCharCards;
    public List<cardSupport> allShopSupportCards;
    public List<Card> allUserCharCards;
    public List<cardSupport> allUserSupportCards;
    public List<Card> deckUserCharCards;
    public List<cardSupport> deckUserSupportCards;
    public int money;
    public string Name;
    public int PlayerId;
}
