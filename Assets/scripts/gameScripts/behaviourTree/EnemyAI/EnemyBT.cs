using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tree = BehaviourTree.Tree;

public class EnemyBT : Tree
{
    public BattleSystem battleSystem;
    public bool start = true;
    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
                {
                    new ChooseChar(battleSystem,true),   
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>{
                            new checkCellsForAttack(battleSystem,this.GetComponent<EnemyBT>()),
                            new Attack(battleSystem,this.GetComponent<EnemyBT>()),
                        }),
                        new Sequence(new List<Node>{
                            new checkCellsForMove(battleSystem,this.GetComponent<EnemyBT>()),
                            new Movement(battleSystem,this.GetComponent<EnemyBT>()),
                        }),
                    })
                    
                });       
        return root;
    }
}
