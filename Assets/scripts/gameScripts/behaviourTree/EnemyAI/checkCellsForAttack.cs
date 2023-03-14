using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCellsForAttack : Node
{
    private BattleSystem _battleSystem;
    private BehaviourTree.Tree _EnemyBT;
    public checkCellsForAttack(BattleSystem battleSystem, EnemyBT EnemyBT)
    {
        _battleSystem = battleSystem;
        _EnemyBT = EnemyBT;
    }


    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            GameObject character = null;
            List<GameObject> possibleEnemies = new List<GameObject>();
            for (int i = 0; i < _battleSystem.EnemyCharObjects.Count; i++)
            {
                if (_battleSystem.EnemyCharObjects[i].GetComponent<character>().isChosen)
                {
                    character = _battleSystem.EnemyCharObjects[i];
                }
            }
            if (character.GetComponent<character>().wasAttack)
            {
                state = NodeState.FAILURE;
                return state;
            }
            else
            {
                //ѕеребор всех клеток на поле
                for (int i = 0; i < _battleSystem.field.CellsOfFieled.GetLength(0); i++)
                {
                    for (int j = 0; j < _battleSystem.field.CellsOfFieled.GetLength(1); j++)
                    {
                        //≈сли это клетка, на которой стоит персонаж
                        if (_battleSystem.field.CellsOfFieled[i, j].transform.localPosition == character.transform.parent.localPosition)
                        {
                            //ƒальше все 4 цикла отвечают за активацию нужных клеток от выбранного персноажа по каждой из 4 сторон
                            //¬ерх, них, право и лево
                            //низ
                            for (int k = j + 1; k < _battleSystem.field.CellsOfFieled.GetLength(1); k++)
                            {
                                if (_battleSystem.field.CellsOfFieled[i, k].transform.childCount != 1)
                                {
                                    if (!character.GetComponent<character>().wasAttack)
                                    {
                                        if (_battleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).GetComponent<character>() != null && (_battleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).GetComponent<character>().isEnemy == false || _battleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).GetComponent<character>().isStaticEnemy == true) && _battleSystem.isCell(_battleSystem.field.CellsOfFieled[i, k].transform.localPosition.x / 0.27f, character.transform.parent.localPosition.x / 0.27f, character.GetComponent<character>().range))
                                        {
                                            possibleEnemies.Add(_battleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).gameObject);
                                        }
                                    }
                                }
                            }
                            //право
                            for (int k = i + 1; k < _battleSystem.field.CellsOfFieled.GetLength(0); k++)
                            {
                                if (_battleSystem.field.CellsOfFieled[k, j].transform.childCount != 1)
                                {
                                    if (!character.GetComponent<character>().wasAttack)
                                    {
                                        if (_battleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).GetComponent<character>() != null && (_battleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).GetComponent<character>().isEnemy == false || _battleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).GetComponent<character>().isStaticEnemy == true) && _battleSystem.isCell(_battleSystem.field.CellsOfFieled[k, j].transform.localPosition.x / 0.27f, character.transform.parent.localPosition.x / 0.27f, character.GetComponent<character>().range))
                                        {
                                            possibleEnemies.Add(_battleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).gameObject);
                                        }
                                    }
                                }
                            }
                            //верх
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (_battleSystem.field.CellsOfFieled[i, k].transform.childCount != 1)
                                {
                                    if (!character.GetComponent<character>().wasAttack)
                                    {
                                        if (_battleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).GetComponent<character>() != null && (_battleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).GetComponent<character>().isEnemy == false || _battleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).GetComponent<character>().isStaticEnemy == true) && _battleSystem.isCell(_battleSystem.field.CellsOfFieled[i, k].transform.localPosition.x / 0.27f, character.transform.parent.localPosition.x / 0.27f, character.GetComponent<character>().range))
                                        {
                                            possibleEnemies.Add(_battleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).gameObject);
                                        }
                                    }
                                }
                            }
                            //лево
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (_battleSystem.field.CellsOfFieled[k, j].transform.childCount != 1)
                                {
                                    if (!character.GetComponent<character>().wasAttack)
                                    {
                                        if (_battleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).GetComponent<character>() != null && (_battleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).GetComponent<character>().isEnemy == false || _battleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).GetComponent<character>().isStaticEnemy == true) && _battleSystem.isCell(_battleSystem.field.CellsOfFieled[k, j].transform.localPosition.x / 0.27f, character.transform.parent.localPosition.x / 0.27f, character.GetComponent<character>().range))
                                        {
                                            possibleEnemies.Add(_battleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).gameObject);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (possibleEnemies.Count > 0)
                {
                    parent.parent.SetData("target", possibleEnemies[UnityEngine.Random.Range(0, possibleEnemies.Count)]);
                    state = NodeState.RUNNING;
                    return state;
                }
                else
                {
                    state = NodeState.FAILURE;
                    return state;
                }
            }
        }         
        state = NodeState.SUCCESS;
        return state;
    }
}
