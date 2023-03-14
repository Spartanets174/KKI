using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkCellsForMove : Node
{
    private BattleSystem _battleSystem;
    private BehaviourTree.Tree _EnemyBT;
    public checkCellsForMove(BattleSystem battleSystem, EnemyBT EnemyBT)
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
            List<GameObject> possibleCells = new List<GameObject>();
            for (int i = 0; i < _battleSystem.EnemyCharObjects.Count; i++)
            {
                if (_battleSystem.EnemyCharObjects[i].GetComponent<character>().isChosen)
                {
                    character = _battleSystem.EnemyCharObjects[i];
                }
            }
            //������� ���� ������ �� ����
            for (int i = 0; i < _battleSystem.field.CellsOfFieled.GetLength(0); i++)
            {
                for (int j = 0; j < _battleSystem.field.CellsOfFieled.GetLength(1); j++)
                {
                    //���� ��� ������, �� ������� ����� ��������
                    if (_battleSystem.field.CellsOfFieled[i, j].transform.localPosition == character.transform.parent.localPosition)
                    {
                        //������ ��� 4 ����� �������� �� ��������� ������ ������ �� ���������� ��������� �� ������ �� 4 ������
                        //����, ���, ����� � ����
                        //���
                        for (int k = j + 1; k < _battleSystem.field.CellsOfFieled.GetLength(1); k++)
                        {
                            //���� �� �����-�� ������ ���� ��. ������ (���� ��� ����������), �� ������ ���� ������ � ��� ������� ������ ������
                            if (_battleSystem.field.CellsOfFieled[i, k].transform.childCount == 1)
                            {
                                //���� ���������� ������ ������� �������� ���������
                                if (_battleSystem.isCell(_battleSystem.field.CellsOfFieled[i, k].transform.localPosition.x / 0.27f, character.transform.parent.localPosition.x / 0.27f, character.GetComponent<character>().speed))
                                {
                                    possibleCells.Add(_battleSystem.field.CellsOfFieled[i, k].gameObject);
                                }
                            }
                            else
                            {
                                break;
                            }

                        }
                        //�����
                        for (int k = i + 1; k < _battleSystem.field.CellsOfFieled.GetLength(0); k++)
                        {
                            if (_battleSystem.field.CellsOfFieled[k, j].transform.childCount == 1)
                            {
                                if (_battleSystem.isCell(_battleSystem.field.CellsOfFieled[k, j].transform.localPosition.z / 0.27f, character.transform.parent.localPosition.z / 0.27f, character.GetComponent<character>().speed))
                                {
                                    possibleCells.Add(_battleSystem.field.CellsOfFieled[k, j].gameObject);
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        //����
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (_battleSystem.field.CellsOfFieled[i, k].transform.childCount == 1)
                            {
                                if (_battleSystem.isCell(_battleSystem.field.CellsOfFieled[i, k].transform.localPosition.x / 0.27f, character.transform.parent.localPosition.x / 0.27f, character.GetComponent<character>().speed))
                                {
                                    possibleCells.Add(_battleSystem.field.CellsOfFieled[i, k].gameObject);
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        //����
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (_battleSystem.field.CellsOfFieled[k, j].transform.childCount == 1)
                            {
                                if (_battleSystem.isCell(_battleSystem.field.CellsOfFieled[k, j].transform.localPosition.z / 0.27f, character.transform.parent.localPosition.z / 0.27f, character.GetComponent<character>().speed))
                                {
                                    possibleCells.Add(_battleSystem.field.CellsOfFieled[k, j].gameObject);
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            if (possibleCells.Count>0)
            {
                parent.SetData("target", possibleCells[UnityEngine.Random.Range(0, possibleCells.Count)]);
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                state = NodeState.FAILURE;
                _battleSystem.StartCoroutine(StartAction());
                _battleSystem.StopCoroutine(StartAction());
                return state;
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
    IEnumerator StartAction()
    {
        Debug.Log("������� ���");
        yield return new WaitForSeconds(1);
        Debug.Log("������� �����");
        _EnemyBT.RestartTree();
    }
}
