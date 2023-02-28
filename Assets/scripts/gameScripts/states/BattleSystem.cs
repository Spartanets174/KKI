using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : StateMachine
{
    /*���� ��������� ���������� ��� ������, �����, ���������� � �.�.*/
    public Text captionOfAction;
    public Text pointsOfActionAnd�ube;
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
        //������������ ����� ui �������� � ������ �������� �� playerManager
        for (int i = 0; i < charCardsUI.Count; i++)
        {
            charCardsUI[i].transform.GetChild(3).GetComponent<Image>().sprite = playerManager.deckUserCharCards[i].image;
            charCardsUI[i].transform.GetChild(4).GetComponent<Slider>().maxValue = (float)playerManager.deckUserCharCards[i].health;
            charCardsUI[i].transform.GetChild(4).GetComponent<Slider>().value = (float)playerManager.deckUserCharCards[i].health;
            charCardsUI[i].GetComponent<cardCharHolde>().card = playerManager.deckUserCharCards[i];
        }
        //������������ ����� ui �������� ��������� � ������ �������� �� playerManager
        for (int i = 0; i < supportCardsUI.Count; i++)
        {
            supportCardsUI[i].transform.GetChild(0).GetComponent<Image>().sprite = playerManager.deckUserSupportCards[i].image;
            supportCardsUI[i].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = playerManager.deckUserSupportCards[i].ability;
            supportCardsUI[i].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = playerManager.deckUserSupportCards[i].name;
            Debug.Log(playerManager.deckUserSupportCards[i].rarity);
            if (playerManager.deckUserSupportCards[i].rarity.ToString() == "�������")
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
        //����������� ������� �����, ���� ��� �����������
        for (int i = 0; i < charCardsUI.Count; i++)
        {
            charCardsUI[i].transform.GetChild(4).GetComponent<Slider>().value = (float)playerManager.deckUserCharCards[i].health;
        }
        //����� ��� ����������� ������
        //����� ��� 5 ������ �����������, �� ������ ����������� (isUnitPlacement) ����������� � ����������� ��� ui ��������
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

    //������� ��� �������� ������ �� ����������������
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
    //������� ��� ��������� � ���������� ������ ������
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
}
