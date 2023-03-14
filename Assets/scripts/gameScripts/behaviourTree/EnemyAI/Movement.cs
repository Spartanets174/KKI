using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;


public class Movement : Node
{
    private BattleSystem _battleSystem;
    private BehaviourTree.Tree _EnemyBT;
    public Movement(BattleSystem battleSystem, BehaviourTree.Tree EnemyBT) {
        _battleSystem = battleSystem;
        _EnemyBT = EnemyBT;
    }
    public override NodeState Evaluate()
    {
        GameObject cell = (GameObject)GetData("target");
        _battleSystem.StartCoroutine(StartAction());
        _battleSystem.StopCoroutine(StartAction());
        state = NodeState.SUCCESS;
        ClearData("target");
        return state;
        IEnumerator StartAction()
        {
            yield return new WaitForSeconds(2);
            _battleSystem.OnMoveButton(cell.gameObject);
            _EnemyBT.RestartTree();
        }
    }
   
}
