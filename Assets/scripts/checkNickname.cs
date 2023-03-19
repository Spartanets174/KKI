using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class checkNickname : MonoBehaviour
{
    public PlayerData playerData;
    [SerializeField] private InputField Nick;
    [SerializeField] private Text warningText;
    [SerializeField] DbManager DB;
    private void Awake()
    {
        playerData.Name = saveSystem.LoadPlayer();
        if (playerData.Name!="")
        {
            playerData.money = DB.SelectBalancePlayer(playerData);
            Debug.Log(playerData.Name);
            playerData.PlayerId = DB.SelectIdPlayer(playerData.Name);
            playerData.deckUserCharCards.Clear();
            playerData.deckUserSupportCards.Clear();
            playerData.money = DB.SelectBalancePlayer(playerData);
            List<Card> CardOfPlayer = DB.SelectFromChars();
            List<cardSupport> CardSupportOfPlayer = DB.SelectFromCardsSupport();

            for (int i = 0; i < playerData.allCharCards.Count; i++)
            {
                playerData.allCharCards[i].name = CardOfPlayer[i].name;
                playerData.allCharCards[i].race = CardOfPlayer[i].race;
                playerData.allCharCards[i].Class = CardOfPlayer[i].Class;
                playerData.allCharCards[i].rarity = CardOfPlayer[i].rarity;
                playerData.allCharCards[i].description = CardOfPlayer[i].description;
                playerData.allCharCards[i].health = CardOfPlayer[i].health;
                playerData.allCharCards[i].speed = CardOfPlayer[i].speed;
                playerData.allCharCards[i].physAttack = CardOfPlayer[i].physAttack;
                playerData.allCharCards[i].magAttack = CardOfPlayer[i].magAttack;
                playerData.allCharCards[i].range = CardOfPlayer[i].range;
                playerData.allCharCards[i].physDefence = CardOfPlayer[i].physDefence;
                playerData.allCharCards[i].magDefence = CardOfPlayer[i].magDefence;
                playerData.allCharCards[i].critNum = CardOfPlayer[i].critNum;
                playerData.allCharCards[i].passiveAbility = CardOfPlayer[i].passiveAbility;
                playerData.allCharCards[i].attackAbility = CardOfPlayer[i].attackAbility;
                playerData.allCharCards[i].defenceAbility = CardOfPlayer[i].defenceAbility;
                playerData.allCharCards[i].buffAbility = CardOfPlayer[i].buffAbility;
                playerData.allCharCards[i].image = CardOfPlayer[i].image;
                playerData.allCharCards[i].Price = CardOfPlayer[i].Price;
                playerData.allCharCards[i].id = CardOfPlayer[i].id;
            }
            for (int i = 0; i < playerData.allSupportCards.Count; i++)
            {
                playerData.allSupportCards[i].name = CardSupportOfPlayer[i].name;
                playerData.allSupportCards[i].race = CardSupportOfPlayer[i].race;
                playerData.allSupportCards[i].type = CardSupportOfPlayer[i].type;
                playerData.allSupportCards[i].image = CardSupportOfPlayer[i].image;
                playerData.allSupportCards[i].ability = CardSupportOfPlayer[i].ability;
                playerData.allSupportCards[i].rarity = CardSupportOfPlayer[i].rarity;
                playerData.allSupportCards[i].Price = CardSupportOfPlayer[i].Price;
                playerData.allSupportCards[i].id = CardSupportOfPlayer[i].id;
            }
            List<Card> CardOfShopPlayer = DB.SelectFromCardsShop(playerData);
            List<cardSupport> CardSupportOfShopPlayer = DB.SelectFromCardsSupportShop(playerData);
            List<Card> OwnedCardOfPlayer = DB.SelectFromOwnCards(playerData);
            List<cardSupport> OwnedCardSupportOfPlayer = DB.SelectFromOwnCardsSupport(playerData);


            playerData.allShopCharCards.Clear();
            for (int i = 0; i < CardOfShopPlayer.Count; i++)
            {
                if (CardOfShopPlayer[i].name == "Бесстрашный \"Страж\"")
                {
                    CardOfShopPlayer[i].name = "Бесстрашный Страж";
                }
                Card card = Resources.Load<Card>($"cards/characters/{CardOfShopPlayer[i].name}");
                playerData.allShopCharCards.Add(card);
            }
            playerData.allShopSupportCards.Clear();
            for (int i = 0; i < CardSupportOfShopPlayer.Count; i++)
            {
                cardSupport cardSupport = Resources.Load<cardSupport>($"cards/support/{CardSupportOfShopPlayer[i].name}");
                playerData.allShopSupportCards.Add(cardSupport);
            }

            playerData.allUserCharCards.Clear();
            for (int i = 0; i < OwnedCardOfPlayer.Count; i++)
            {
                if (OwnedCardOfPlayer[i].name == "Бесстрашный \"Страж\"")
                {
                    OwnedCardOfPlayer[i].name = "Бесстрашный Страж";
                }
                Card card = Resources.Load<Card>($"cards/characters/{OwnedCardOfPlayer[i].name}");
                playerData.allUserCharCards.Add(card);
            }
            playerData.allUserSupportCards.Clear();
            for (int i = 0; i < OwnedCardSupportOfPlayer.Count; i++)
            {
                cardSupport cardSupport = Resources.Load<cardSupport>($"cards/support/{OwnedCardSupportOfPlayer[i].name}");
                playerData.allUserSupportCards.Add(cardSupport);
            }

            SceneManager.LoadScene("menu");
        }
    }

/*    */
    public void testFunc()
    {
        DB.RemoveCardsSupportShop(playerData);
        DB.InsertToCardsSupportShop(playerData);

    }


    public void checkNick()
    {
        Nick.text = Nick.text.Trim();
        if (Nick.text == "")
        {
            warningText.text = "Вы ничего не ввели!";
            StartCoroutine(onWarningText());
            StopCoroutine(onWarningText());
        }
        else
        {
            if (Nick.text.Length<=15)
            {
                if (Nick.text.Length >=4)
                {
                    bool hasName=false;
                    List<string> nickList = DB.SelectFromPlayers();
                    for (int i = 0; i < nickList.Count; i++)
                    {
                        if (nickList[i]==Nick.text)
                        {
                            hasName = true;
                        }
                    }
                    if (nickList.Count==0||!hasName)
                    {
                        int id = DB.InsertToPlayers(Nick.text, 1000);                       
                        playerData.Name = Nick.text;
                        saveSystem.savePlayer(playerData.Name);
                        playerData.money = 1000;
                        playerData.PlayerId = id;
                        playerData.deckUserCharCards.Clear();
                        playerData.deckUserSupportCards.Clear();
                        List<Card> CardOfPlayer = DB.SelectFromChars();
                        List<cardSupport> CardSupportOfPlayer = DB.SelectFromCardsSupport();

                        for (int i = 0; i < playerData.allCharCards.Count; i++)
                        {
                            playerData.allCharCards[i].name = CardOfPlayer[i].name;
                            playerData.allCharCards[i].race = CardOfPlayer[i].race;
                            playerData.allCharCards[i].Class = CardOfPlayer[i].Class;
                            playerData.allCharCards[i].rarity = CardOfPlayer[i].rarity;
                            playerData.allCharCards[i].description = CardOfPlayer[i].description;
                            playerData.allCharCards[i].health = CardOfPlayer[i].health;
                            playerData.allCharCards[i].speed = CardOfPlayer[i].speed;
                            playerData.allCharCards[i].physAttack = CardOfPlayer[i].physAttack;
                            playerData.allCharCards[i].magAttack = CardOfPlayer[i].magAttack;
                            playerData.allCharCards[i].range = CardOfPlayer[i].range;
                            playerData.allCharCards[i].physDefence = CardOfPlayer[i].physDefence;
                            playerData.allCharCards[i].magDefence = CardOfPlayer[i].magDefence;
                            playerData.allCharCards[i].critNum = CardOfPlayer[i].critNum;
                            playerData.allCharCards[i].passiveAbility = CardOfPlayer[i].passiveAbility;
                            playerData.allCharCards[i].attackAbility = CardOfPlayer[i].attackAbility;
                            playerData.allCharCards[i].defenceAbility = CardOfPlayer[i].defenceAbility;
                            playerData.allCharCards[i].buffAbility = CardOfPlayer[i].buffAbility;
                            playerData.allCharCards[i].image = CardOfPlayer[i].image;
                            playerData.allCharCards[i].Price = CardOfPlayer[i].Price;
                            playerData.allCharCards[i].id = CardOfPlayer[i].id;
                        }
                        for (int i = 0; i < playerData.allSupportCards.Count; i++)
                        {
                            playerData.allSupportCards[i].name = CardSupportOfPlayer[i].name;
                            playerData.allSupportCards[i].race = CardSupportOfPlayer[i].race;
                            playerData.allSupportCards[i].type = CardSupportOfPlayer[i].type;
                            Debug.Log($"{playerData.allSupportCards[i].image},{CardSupportOfPlayer[i].image}");
                            playerData.allSupportCards[i].image = CardSupportOfPlayer[i].image;                            
                            playerData.allSupportCards[i].ability = CardSupportOfPlayer[i].ability;
                            playerData.allSupportCards[i].rarity = CardSupportOfPlayer[i].rarity;
                            playerData.allSupportCards[i].Price = CardSupportOfPlayer[i].Price;
                            playerData.allSupportCards[i].id = CardSupportOfPlayer[i].id;
                            Debug.Log($"{playerData.allSupportCards[i].image},{CardSupportOfPlayer[i].image}");
                        }

                        DB.InsertToCardsShopStart(playerData);
                        DB.InsertToCardsSupportShopStart(playerData);
                        DB.InsertToOwnCardStart(playerData);
                        DB.InsertToOwnCardsSupportStart(playerData);

                        List<Card> CardOfShopPlayer = DB.SelectFromCardsShop(playerData);
                        List<cardSupport> CardSupportOfShopPlayer = DB.SelectFromCardsSupportShop(playerData);
                        List<Card> OwnedCardOfPlayer = DB.SelectFromOwnCards(playerData);
                        List<cardSupport> OwnedCardSupportOfPlayer = DB.SelectFromOwnCardsSupport(playerData);


                        playerData.allShopCharCards.Clear();
                        for (int i = 0; i < CardOfShopPlayer.Count; i++)
                        {
                            if (CardOfShopPlayer[i].name == "Бесстрашный \"Страж\"")
                            {
                                CardOfShopPlayer[i].name = "Бесстрашный Страж";
                            }
                            Card card = Resources.Load<Card>($"cards/characters/{CardOfShopPlayer[i].name}");
                            playerData.allShopCharCards.Add(card);
                        }
                        playerData.allShopSupportCards.Clear();
                        for (int i = 0; i < CardSupportOfShopPlayer.Count; i++)
                        {
                            cardSupport cardSupport = Resources.Load<cardSupport>($"cards/support/{CardSupportOfShopPlayer[i].name}");
                            playerData.allShopSupportCards.Add(cardSupport);
                        }

                        playerData.allUserCharCards.Clear();
                        for (int i = 0; i < OwnedCardOfPlayer.Count; i++)
                        {
                            if (OwnedCardOfPlayer[i].name == "Бесстрашный \"Страж\"")
                            {
                                OwnedCardOfPlayer[i].name = "Бесстрашный Страж";
                            }
                            Card card = Resources.Load<Card>($"cards/characters/{OwnedCardOfPlayer[i].name}");
                            playerData.allUserCharCards.Add(card);
                        }
                        playerData.allUserSupportCards.Clear();
                        for (int i = 0; i < OwnedCardSupportOfPlayer.Count; i++)
                        {
                            cardSupport cardSupport = Resources.Load<cardSupport>($"cards/support/{OwnedCardSupportOfPlayer[i].name}");
                            playerData.allUserSupportCards.Add(cardSupport);
                        }
                        SceneManager.LoadScene("menu");
                    }
                    else
                    {
                        warningText.text = "Данное имя уже существует!";
                        StartCoroutine(onWarningText());
                        StopCoroutine(onWarningText());
                    }
                }
                else
                {
                    warningText.text = "Имя слишком маленькое (минимум 4 символа)!";
                    StartCoroutine(onWarningText());
                    StopCoroutine(onWarningText());
                }               
            }
            else
            {
                warningText.text = "Имя слишком большое (минимум 15 символов)!";
                StartCoroutine(onWarningText());
                StopCoroutine(onWarningText());
            }
        }
    }
    IEnumerator onWarningText()
    {
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        warningText.gameObject.SetActive(false);
    }
}
