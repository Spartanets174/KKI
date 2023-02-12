using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : StateMachine
{
    /*���� ��������� ���������� ��� ������, �����, ���������� � �.�.*/

    private void Start()
    {
        SetState(new Begin(this));
    }
    public void OnChooseCharacterButton()
    {
        StartCoroutine(State.chooseCharacter());
    }

    public void OnMoveButton()
    {
        StartCoroutine(State.Move());
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
