using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Attack : Node
{

    private BattleSystem _battleSystem;
    private BehaviourTree.Tree _EnemyBT;
    public Attack(BattleSystem battleSystem, EnemyBT EnemyBT)
    {
        _battleSystem = battleSystem;
        _EnemyBT = EnemyBT;
    }

    public override NodeState Evaluate()
    {
        GameObject _character = (GameObject)GetData("target");

        _battleSystem.OnAttackButton(_character.GetComponent<character>());        
        state = NodeState.RUNNING;
        ClearData("target");
        _EnemyBT.RestartTree();
        return state;
    }
    
}
