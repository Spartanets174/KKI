using System.Collections;
using UnityEngine;
using BehaviourTree;
using UnityEngine.UI;
using System;

public class EnemyTurn : State
{
    public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
    {

    }
    public override IEnumerator Start()
    {
        BattleSystem.gameLog.text += $"Ход противника." + "\n";
        BattleSystem.EndMove.interactable = false;
        BattleSystem.pointsOfAction = 20;
        BattleSystem.pointsOfActionAndСube.text = BattleSystem.pointsOfAction.ToString();
        BattleSystem.gameLogScrollBar.value = 0;
        BattleSystem.gameLog.text += $"Враг планирует свой ход..." + "\n";
        //Переустановка переменных для ограничения по скорости и атаке в начальное
        for (int i = 0; i < BattleSystem.EnemyCharObjects.Count; i++)
        {           
            BattleSystem.EnemyCharObjects[i].GetComponent<character>().speed = BattleSystem.EnemyCharObjects[i].GetComponent<character>().card.speed;
            BattleSystem.EnemyCharObjects[i].GetComponent<character>().wasAttack = false;
        }
        for (int i = 0; i < BattleSystem.charCards.Count; i++)
        {
            BattleSystem.charCards[i].GetComponent<Outline>().enabled = false;
        }
        for (int i = 0; i < BattleSystem.EnemyStaticCharObjects.Count; i++)
        {
            BattleSystem.EnemyStaticCharObjects[i].GetComponent<Outline>().enabled = false;
            BattleSystem.EnemyStaticCharObjects[i].GetComponent<staticEnemyAttack>().attackInRadius(true);
        }
        for (int i = 0; i < BattleSystem.field.CellsOfFieled.GetLength(0); i++)
        {
            for (int j = 0; j < BattleSystem.field.CellsOfFieled.GetLength(1); j++)
            {
                //Включение и переракрас всех клеток
                BattleSystem.field.CellsOfFieled[i, j].GetComponent<Cell>().Enabled = true;
                BattleSystem.isCellEven((i + j) % 2 == 0, true, BattleSystem.field.CellsOfFieled[i, j]);
                if ((i == 2 && j == 2) || (i == 4 && j == 3) || (i == 2 && j == 7) || (i == 4 && j == 8))
                {
                    BattleSystem.field.CellsOfFieled[i, j].gameObject.GetComponent<MeshRenderer>().material = BattleSystem.field.CellsOfFieled[i, j].swampColor;
                }
            }
        }

        yield break;
    }
    public override IEnumerator chooseCharacter(GameObject character)
    {
        /*Логика при выборе перса*/
        //Отключение обводки у всех юнитов и переменной, отвечающей за то, какой персонаж выбран у врага
        new WaitForSeconds(1);
        if (character.GetComponent<character>().isEnemy)
        {
            for (int i = 0; i < BattleSystem.EnemyCharObjects.Count; i++)
            {
                BattleSystem.EnemyCharObjects[i].GetComponent<Outline>().enabled = false;
                BattleSystem.EnemyCharObjects[i].GetComponent<character>().isChosen = false;
            }
            //Отключение обводки у врагов
            for (int i = 0; i < BattleSystem.EnemyCharObjects.Count; i++)
            {
                BattleSystem.EnemyCharObjects[i].GetComponent<Outline>().enabled = false;
                BattleSystem.EnemyCharObjects[i].GetComponent<character>().isChosen = false;
            }
            for (int i = 0; i < BattleSystem.EnemyStaticCharObjects.Count; i++)
            {
                BattleSystem.EnemyStaticCharObjects[i].GetComponent<Outline>().enabled = false;
                BattleSystem.EnemyStaticCharObjects[i].GetComponent<character>().isChosen = false;
            }
            BattleSystem.cahngeCardWindow(character, true);
            //Включение обводки и переменной отвечающей за то, какой персонаж выбран у выбранного персонажа
            character.GetComponent<Outline>().enabled = true;
            character.GetComponent<character>().isChosen = true;
        }
        yield return character;
    }
    public override IEnumerator Move(GameObject cell)
    {
        /*Логика при движении*/
        new WaitForSeconds(1);
        if (cell.transform.childCount == 1)
        {
            //Перебор всех персонажей врага в колоде
            for (int i = 0; i < BattleSystem.EnemyCharObjects.Count; i++)
            {
                //ПРоверка на то, какой персонаж выбран
                if (BattleSystem.EnemyCharObjects[i].GetComponent<character>().isChosen)
                {
                    int numOfCells = Convert.ToInt32(BattleSystem.howManyCells(cell.transform.localPosition.z / 0.27f, BattleSystem.EnemyCharObjects[i].transform.parent.localPosition.z / 0.27f, "z", cell.GetComponent<Cell>()));
                    if (numOfCells == 0)
                    {
                        numOfCells = Convert.ToInt32(BattleSystem.howManyCells(cell.transform.localPosition.x / 0.27f, BattleSystem.EnemyCharObjects[i].transform.parent.localPosition.x / 0.27f, "x", cell.GetComponent<Cell>()));
                    }
                    BattleSystem.EnemyCharObjects[i].GetComponent<character>().isChosen = false;
                    BattleSystem.EnemyCharObjects[i].GetComponent<Outline>().enabled = false;
                    if (numOfCells <= BattleSystem.pointsOfAction)
                    {
                        BattleSystem.pointsOfAction -= numOfCells;
                        BattleSystem.pointsOfActionAndСube.text = BattleSystem.pointsOfAction.ToString();
                        BattleSystem.EnemyCharObjects[i].GetComponent<character>().speed -= numOfCells;
                        //Установление координат в новой клетке
                        BattleSystem.EnemyCharObjects[i].transform.SetParent(cell.transform);
                        BattleSystem.EnemyCharObjects[i].transform.localPosition = new Vector3(0, 1, 0);
                        for (int j = 0; j < BattleSystem.EnemyStaticCharObjects.Count; j++)
                        {
                            BattleSystem.EnemyStaticCharObjects[j].GetComponent<staticEnemyAttack>().attackInRadius(true);
                        }
                        //Отключение обводки и выбранности перса
                        if (BattleSystem.pointsOfAction == 0)
                        {
                            BattleSystem.endEnemyMove();
                        }
                    }                 
                }
            }
            //Для нанесения урона статичиескими противниками
            
        }
        yield break;
    }
    public override IEnumerator Attack(character target)
    {
        /*Логика при атаке*/
        new WaitForSeconds(1);
        if (2 <= BattleSystem.pointsOfAction)
        {
            for (int i = 0; i < BattleSystem.charCards.Count; i++)
            {
                BattleSystem.charCards[i].GetComponent<Outline>().enabled = false;
            }
            for (int i = 0; i < BattleSystem.EnemyStaticCharObjects.Count; i++)
            {
                BattleSystem.EnemyStaticCharObjects[i].GetComponent<Outline>().enabled = false;
                BattleSystem.EnemyStaticCharObjects[i].GetComponent<staticEnemyAttack>().attackInRadius(true);
            }          
            for (int i = 0; i < BattleSystem.EnemyCharObjects.Count; i++)
            {
                if (BattleSystem.EnemyCharObjects[i].GetComponent<character>().isChosen)
                {
                    character charac = BattleSystem.EnemyCharObjects[i].GetComponent<character>();
                    BattleSystem.EnemyCharObjects[i].GetComponent<character>().isChosen = false;
                    BattleSystem.EnemyCharObjects[i].GetComponent<Outline>().enabled = false;
                    BattleSystem.EnemyCharObjects[i].GetComponent<character>().wasAttack = true;
                    bool isDeath = target.Damage(charac);
                    /*target.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<healthBar>().SetHealth((float)target.health);*/
                    if (isDeath)
                    {
                        if (target.name == "Ассасин" || target.name == "Голиаф" || target.name == "Элементаль")
                        {
                            BattleSystem.EnemyStaticCharObjects.Remove(target.gameObject);

                        }
                        else
                        {
                            BattleSystem.charCards.Remove(target.gameObject);
                        }
                        GameObject.Destroy(target.gameObject);
                        BattleSystem.gameLog.text += $"Вражеский юнит {target.name} убит" + "\n";
                        BattleSystem.gameLogScrollBar.value = 0;
                    }
                    if (BattleSystem.charCards.Count == 0)
                    {
                        BattleSystem.enemyManager.gameObject.SetActive(false);
                        BattleSystem.enemyManager.StopTree();
                        BattleSystem.SetState(new Lost(BattleSystem));
                    }
                }

            }
            BattleSystem.pointsOfAction -= 2;
            BattleSystem.pointsOfActionAndСube.text = BattleSystem.pointsOfAction.ToString();
            if (BattleSystem.pointsOfAction == 0)
            {
                BattleSystem.endEnemyMove();
            }
        }
        yield break;
    }
    public override IEnumerator attackAbility()
    {
        /*Логика при применении способности 1*/
        yield break;
    }
    public override IEnumerator defensiveAbility()
    {
        /*Логика при применении способности 2*/
        yield break;
    }
    public override IEnumerator buffAbility()
    {
        /*Логика при применении способности 3*/
        yield break;
    }
    public override IEnumerator supportCard()
    {
        /*Логика при применении карты помощи*/
        yield break;
    }
    public override IEnumerator useItem()
    {
        /*Логика при применении предмета*/
        yield break;
    }
}