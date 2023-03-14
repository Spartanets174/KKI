using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Класс, проверяющий ближайшие клетки на статическом противнике для атаки
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
        //Перебок всех клеток, которые может достать статический
        for (int i = 0; i < listOfCellToAttack.Count; i++)
        {
            if (listOfCellToAttack[i].transform.childCount>1)
            {
                if (listOfCellToAttack[i].transform.GetChild(1).gameObject.TryGetComponent(out character character))
                {
                    //Если ход врага, то он может атаковать только вражеских юнитов
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
                            }
                        }
                    }
                    else
                    {
                        //Если ход игрока, то он может атаковать только юнитов игрока
                        if (!character.isEnemy)
                        {
                            bool isDeath = character.Damage(this.GetComponent<character>());
                            
                            if (isDeath)
                            {
                                battleSystem.charCards.Remove(character.gameObject);
                                GameObject.Destroy(character.gameObject);
                                battleSystem.gameLogScrollBar.value = 0;
                            }
                        }

                    }
                }               
            }
        }
    }
}
