using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
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

    //Вражиk
    public List<GameObject> EnemyCharCards;
    public List<GameObject> EnemySupportCards;

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



        CreateEnemy();
        InstantiateEnemy();
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
        //Когда все 5 юнитов расставлены, то стадия расстановки (isUnitPlacement) отключается и включаются все ui карточки
    }
    public void onUnitStatementButton()
    {
        StartCoroutine(State.unitStatement());
    }
    public void OnChooseCharacterButton(GameObject character)
    {
        StartCoroutine(State.chooseCharacter(character));
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

    //Функция для проверки клеток на крестообразность
    public bool isCell(float cellCoord, float charCoord, int charFeature)
    {
       
        if (Math.Floor(Math.Abs(charCoord - cellCoord))<= charFeature)
        {
            return true;
        }
        else
        {           
            return false;
        }
    }
    //Функция для включения и выключения нужных клеток
    public void isCellEven(bool even, bool isNormal, Cell cell)
    {
        if (isNormal)
        {
            if (even)
            {
                cell.GetComponent<MeshRenderer>().material.color = this.field.CellsOfFieled[0, 0].baseColor.color;
            }
            else
            {
                cell.GetComponent<MeshRenderer>().material.color = this.field.CellsOfFieled[0, 0].offsetColor.color;
            }
        }
        else
        {
            if (even)
            {
                cell.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
            }
            else
            {
                cell.GetComponent<MeshRenderer>().material.color = new Color(0, 0.5f, 0);
            }
        }
    }

    public void CreateEnemy()
    {
        //for (int i = EnemyCharCards.Count; i < 5; i++)
        //{
        //    bool isInDeck = false;
        //    GameObject prefab = charPrefab;
        //    //Запись нужных данных о карточке в префаб
        //    prefab.GetComponent<character>().card = playerManager.allCharCards[UnityEngine.Random.Range(0, playerManager.allCharCards.Count)];
        //    for (int j = 0; j < EnemyCharCards.Count; j++)
        //    {
        //        if (prefab.GetComponent<character>().card.name == EnemyCharCards[j].GetComponent<character>().card.name)
        //        {
        //            isInDeck = true;
        //            break;
        //        }
        //    }
        //    if (isInDeck) { CreateEnemy(); }
        //    else { InstantiateEnemy(prefab, i); }
        //}
        while (EnemyCharCards.Count <= 5)
        {
            Card EnemyMan = GetRandomCard();
            if (!isCardInDeck(EnemyMan))
            {
                GameObject prefab = charPrefab;
                prefab.GetComponent<character>().card = EnemyMan;
                EnemyCharCards.Add(prefab);
                EnemyCharCards[EnemyCharCards.Count - 1].GetComponent<character>().index = EnemyCharCards.Count - 1;
                EnemyCharCards[EnemyCharCards.Count - 1].GetComponent<character>().isEnemy = true;
            }

        }

    }

    private Card GetRandomCard()
    {
        return playerManager.allCharCards[UnityEngine.Random.Range(0, playerManager.allCharCards.Count)];
    }

    private bool isCardInDeck(Card enemy)
    {
        for (int j = 0; j < EnemyCharCards.Count; j++)
        {
            if (enemy.name == EnemyCharCards[j].GetComponent<character>().card.name)
            {
                return true;
            } 
        }
        return false;
    }

    public void InstantiateEnemy()
    {
        int count = 0;

        while (count < 5)
        {
            GameObject Cell = field.CellsOfFieled[UnityEngine.Random.Range(0, field.CellsOfFieled.GetLength(0)), UnityEngine.Random.Range(0, 2)].gameObject;
            if (!isEnemyOnCell(Cell))
            {
                GameObject prefab = EnemyCharCards[count];
                prefab = GameObject.Instantiate(EnemyCharCards[count], Vector3.zero, Quaternion.identity, Cell.transform);
                prefab.transform.localPosition = new Vector3(0, 1, 0);
                count++;   
            }
        }
    }

    private bool isEnemyOnCell(GameObject cell)
    {
        if (cell.transform.childCount != 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
