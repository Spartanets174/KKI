using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.UI;

public class ChooseChar : Node
{
    private BattleSystem _battleSystem;
    private GameObject _character;
    private bool _start;

    public ChooseChar(BattleSystem battleSystem, bool start)
    {
        _battleSystem = battleSystem;
        _start = start;
    }

    public override NodeState Evaluate()
    {
            chooseChar();
            _battleSystem.OnChooseCharacterButton(_character);
            state = NodeState.SUCCESS;
            _start = false;
            return state;
    }
    public void chooseChar()
    {
        int count = 0;
        while (count < 1)
        {
            _character = _battleSystem.EnemyCharObjects[UnityEngine.Random.Range(0, _battleSystem.EnemyCharObjects.Count)];
            if (isCharValid(_character))
            {
                count++;
            }            
        }

    }
    private bool isCharValid(GameObject enemy)
    {
        if (!enemy.GetComponent<character>().wasAttack || enemy.GetComponent<character>().speed > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }
}