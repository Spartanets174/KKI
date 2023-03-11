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
            _character = _battleSystem.EnemyCharObjects[getRandomChar()];
            _battleSystem.OnChooseCharacterButton(_character);
            state = NodeState.SUCCESS;
            _start = false;
            return state;
    }
    public int getRandomChar()
    {
        int randomEnemyIndex = UnityEngine.Random.Range(0, 5);
        return randomEnemyIndex;
    }
}