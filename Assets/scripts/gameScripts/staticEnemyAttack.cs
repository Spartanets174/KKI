using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�����, ����������� ��������� ������ �� ����������� ���������� ��� �����
public class staticEnemyAttack : MonoBehaviour
{
    public List<GameObject> listOfCellToAttack = new List<GameObject>();
    private BattleSystem battleSystem;
    private void Start()
    {
        battleSystem = GameObject.Find("battleSystem").GetComponent<BattleSystem>();
    }
    public void attackInRadius(bool isEnemyTurn)
    {
        //������� ���� ������, ������� ����� ������� �����������
        StartCoroutine(startAction(isEnemyTurn));
        StopCoroutine(startAction(isEnemyTurn));
    }

    public IEnumerator startAction(bool isEnemyTurn)
    {
        new WaitForSeconds(1);
        for (int i = 0; i < listOfCellToAttack.Count; i++)
        {
            if (listOfCellToAttack[i].transform.childCount > 1)
            {
                if (listOfCellToAttack[i].transform.GetChild(1).gameObject.TryGetComponent(out character character))
                {
                    //���� ��� �����, �� �� ����� ��������� ������ ��������� ������
                    if (isEnemyTurn)
                    {
                        if (character.isEnemy)
                        {

                            bool isDeath = character.Damage(this.GetComponent<character>());
                            /*target.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<healthBar>().SetHealth((float)target.health);*/
                            if (isDeath)
                            {
                                battleSystem.EnemyCharObjects.Remove(character.gameObject);
                                GameObject.Destroy(character.gameObject);
                                battleSystem.gameLogScrollBar.value = 0;
                                if (battleSystem.EnemyCharObjects.Count == 0)
                                {
                                    battleSystem.SetState(new Won(battleSystem));
                                }
                            }
                        }
                    }
                    else
                    {
                        //���� ��� ������, �� �� ����� ��������� ������ ������ ������
                        if (!character.isEnemy)
                        {
                            bool isDeath = character.Damage(this.GetComponent<character>());

                            if (isDeath)
                            {
                                battleSystem.charCards.Remove(character.gameObject);
                                GameObject.Destroy(character.gameObject);
                                battleSystem.gameLogScrollBar.value = 0;
                                if (battleSystem.charCards.Count == 0)
                                {
                                    battleSystem.SetState(new Lost(battleSystem));
                                }
                            }
                        }

                    }
                }
            }
        }
        yield break;
    }
}
