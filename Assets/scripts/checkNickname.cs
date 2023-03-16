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
        if (playerData.Name!="")
        {
            SceneManager.LoadScene("menu");
        }
    }

    /*  DB.InsertToPlayers("name",0);
        DB.SelectFromPlayers();

        DB.SelectFromChars();
        DB.IsertIntoChars(playerData);
        
        DB.IsertIntoCardsSupport(playerData);
        DB.SelectFromCardsSupport();
     */
    public void testFunc()
    {
                                  

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
                        playerData.money = 1000;
                        playerData.PlayerId = id;

                        DB.InsertToCardsShopStart(playerData);
                        DB.InsertToCardsSupportShopStart(playerData);


                        List<Card> CardOfPlayer = DB.SelectFromChars();
                        List<cardSupport> CardSupportOfPlayer = DB.SelectFromCardsSupport();
                        List<Card> CardOfShopPlayer = DB.SelectFromCardsShop(playerData);
                        List<cardSupport> CardSupportOfShopPlayer = DB.SelectFromCardsSupportShop(playerData);
/*                        List<Card> OwnedCardOfPlayer = DB.SelectFromChars();
                        List<cardSupport> ownedCardSupportOfPlayer = DB.SelectFromCardsSupport();*/
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
                        playerData.allShopCharCards.Clear();
                        for (int i = 0; i < CardOfShopPlayer.Count; i++)
                        {
                            if (CardOfShopPlayer[i].name == "Бесстрашный \"Страж\"")
                            {
                                CardOfShopPlayer[i].name = "Бесстрашный Страж";
                            }
                            Card card = Resources.Load<Card>($"cards/characters/{CardOfShopPlayer[i].name}");
                            Debug.Log(CardOfShopPlayer[i].name);
                            playerData.allShopCharCards.Add(card);
                            playerData.allShopCharCards[i].name = CardOfShopPlayer[i].name;
                            playerData.allShopCharCards[i].race = CardOfShopPlayer[i].race;
                            playerData.allShopCharCards[i].Class = CardOfShopPlayer[i].Class;
                            playerData.allShopCharCards[i].rarity = CardOfShopPlayer[i].rarity;
                            playerData.allShopCharCards[i].description = CardOfShopPlayer[i].description;
                            playerData.allShopCharCards[i].health = CardOfShopPlayer[i].health;
                            playerData.allShopCharCards[i].speed = CardOfShopPlayer[i].speed;
                            playerData.allShopCharCards[i].physAttack = CardOfShopPlayer[i].physAttack;
                            playerData.allShopCharCards[i].magAttack = CardOfShopPlayer[i].magAttack;
                            playerData.allShopCharCards[i].range = CardOfShopPlayer[i].range;
                            playerData.allShopCharCards[i].physDefence = CardOfShopPlayer[i].physDefence;
                            playerData.allShopCharCards[i].magDefence = CardOfShopPlayer[i].magDefence;
                            playerData.allShopCharCards[i].critNum = CardOfShopPlayer[i].critNum;
                            playerData.allShopCharCards[i].passiveAbility = CardOfShopPlayer[i].passiveAbility;
                            playerData.allShopCharCards[i].attackAbility = CardOfShopPlayer[i].attackAbility;
                            playerData.allShopCharCards[i].defenceAbility = CardOfShopPlayer[i].defenceAbility;
                            playerData.allShopCharCards[i].buffAbility = CardOfShopPlayer[i].buffAbility;
                            playerData.allShopCharCards[i].image = CardOfShopPlayer[i].image;
                            playerData.allShopCharCards[i].Price = CardOfShopPlayer[i].Price;
                            playerData.allShopCharCards[i].id = CardOfShopPlayer[i].id;
                        }
                        playerData.allShopSupportCards.Clear();
                        for (int i = 0; i < CardSupportOfShopPlayer.Count; i++)
                        {
                            cardSupport cardSupport = Resources.Load<cardSupport>($"cards/support/{CardSupportOfShopPlayer[i].name}");
                            playerData.allShopSupportCards.Add(cardSupport);
                            playerData.allShopSupportCards[i].name = CardSupportOfShopPlayer[i].name;
                            playerData.allShopSupportCards[i].race = CardSupportOfShopPlayer[i].race;
                            playerData.allShopSupportCards[i].type = CardSupportOfShopPlayer[i].type;
                            playerData.allShopSupportCards[i].image = CardSupportOfShopPlayer[i].image;
                            playerData.allShopSupportCards[i].ability = CardSupportOfShopPlayer[i].ability;
                            playerData.allShopSupportCards[i].rarity = CardSupportOfShopPlayer[i].rarity;
                            playerData.allShopSupportCards[i].Price = CardSupportOfShopPlayer[i].Price;
                            playerData.allShopSupportCards[i].id = CardSupportOfShopPlayer[i].id;
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
