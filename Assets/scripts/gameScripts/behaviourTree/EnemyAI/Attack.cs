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
        
        _battleSystem.StartCoroutine(StartAction());
        _battleSystem.StopCoroutine(StartAction());
        state = NodeState.RUNNING;
        ClearData("target");
        return state;
        IEnumerator StartAction()
        {
            _battleSystem.OnAttackButton(_character.GetComponent<character>());
            yield return new WaitForSeconds(2);  
            _EnemyBT.RestartTree();
        }
    }
    
}
