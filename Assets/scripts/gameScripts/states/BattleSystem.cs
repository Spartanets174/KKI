using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : StateMachine
{
    /*Сюда вставлять переменные для своего, врага, интерфейса и т.д.*/
    public Text captionOfAction;
    public Text pointsOfActionAndСube;
    public Text gameLog;
    public List<GameObject> charCardsUI;
    public List<GameObject> supportCardsUI;
    public List<GameObject> charCards;
    public List<GameObject> supportCards;
    public PlayerManager1 playerManager;
    public Field field;
    public GameObject charPrefab;
    public bool isStart;
    public bool isUnitPlacement = true;

    private void Start()
    {
        //Установление полей ui карточек в нужные значение из playerManager
        for (int i = 0; i < charCardsUI.Count; i++)
        {
            charCardsUI[i].transform.GetChild(3).GetComponent<Image>().sprite = playerManager.deckUserCharCards[i].image;
            charCardsUI[i].transform.GetChild(4).GetComponent<Slider>().maxValue = (float)playerManager.deckUserCharCards[i].health;
            charCardsUI[i].transform.GetChild(4).GetComponent<Slider>().value = (float)playerManager.deckUserCharCards[i].health;
            charCardsUI[i].GetComponent<cardCharHolde>().card = playerManager.deckUserCharCards[i];
        }
        //Установление полей ui карточек поддержки в нужные значение из playerManager
        for (int i = 0; i < supportCardsUI.Count; i++)
        {
            supportCardsUI[i].transform.GetChild(0).GetComponent<Image>().sprite = playerManager.deckUserSupportCards[i].image;
            supportCardsUI[i].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = playerManager.deckUserSupportCards[i].ability;
            supportCardsUI[i].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = playerManager.deckUserSupportCards[i].name;
            Debug.Log(playerManager.deckUserSupportCards[i].rarity);
            if (playerManager.deckUserSupportCards[i].rarity.ToString() == "Обычная")
            {
                supportCardsUI[i].transform.GetChild(3).GetComponent<Image>().color = new Color(126, 126, 126);
            }
            else
            {
                supportCardsUI[i].transform.GetChild(3).GetComponent<Image>().color = new Color(126, 0, 255);
            }
        }
        SetState(new Begin(this));
    }
    private void Update()
    {
        //Перерисовка зоровья юнита, если оно изменяентся
        for (int i = 0; i < charCardsUI.Count; i++)
        {
            charCardsUI[i].transform.GetChild(4).GetComponent<Slider>().value = (float)playerManager.deckUserCharCards[i].health;
        }
        //Нужно для расстановки юнитов
        //Когда все 5 юнитов расставлены, то стадия расстановки (isUnitPlacement) отключается и вклоючаются все ui карточки
        if (charCards.Count == 5)
        {
            isUnitPlacement = false;
            for (int i = 0; i < charCardsUI.Count; i++)
            {
                charCardsUI[i].GetComponent<Button>().interactable = true;
                charCardsUI[i].GetComponent<Button>().enabled = true;
            }
        }
    }
    public void OnChooseCharacterButton()
    {
        StartCoroutine(State.chooseCharacter());
    }
    public void OnMoveButton(GameObject cell)
    {
        StartCoroutine(State.Move(cell));
    }

    public void OnAttackButton()
    {
        StartCoroutine(State.Attack());
    }

    public void OnAttackAbilityButton()
    {
        StartCoroutine(State.attackAbility());
    }

    public void OnDefensiveAbilityButton()
    {
        StartCoroutine(State.defensiveAbility());
    }

    public void OnBuffAbilityButton()
    {
        StartCoroutine(State.buffAbility());
    }
    public void OnSupportCardButton()
    {
        StartCoroutine(State.supportCard());
    }
    public void OnUseItemButton()
    {
        StartCoroutine(State.useItem());
    }

}
