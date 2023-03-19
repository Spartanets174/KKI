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
        int count1 = 0;
        int count = 0;
        while (count1 < 1)
        {
            for (int i = 0; i < _battleSystem.EnemyCharObjects.Count; i++)
            {
                if (_battleSystem.EnemyCharObjects[i].GetComponent<character>().speed == 0)
                {
                    count++;
                    if (count == _battleSystem.EnemyCharObjects.Count)
                    {
                        count1++;
                        _battleSystem.endEnemyMove();
                    }
                }
            }
            _character = _battleSystem.EnemyCharObjects[UnityEngine.Random.Range(0, _battleSystem.EnemyCharObjects.Count)];
            if (isCharValid(_character))
            {
                count1++;
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