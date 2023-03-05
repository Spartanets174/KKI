using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerTurn : State
{
    public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        /*Логика при выборе старте*/
        //Кол-во очков действий
        BattleSystem.pointsOfActionAndСube.text = "20";
        yield break;
    }
    //При выборе персонажа
    public override IEnumerator chooseCharacter(GameObject character)
    {
        //Если сейчас не расстановка юнитов
        if (!BattleSystem.isUnitPlacement)
        {        
            //Отключение обводки у всех юнитов и переменной, отвечающей за то, какой персонаж выбран
            for (int i = 0; i < BattleSystem.charCards.Count; i++)
            {
                BattleSystem.charCards[i].GetComponent<Outline>().enabled = false;
                BattleSystem.charCards[i].GetComponent<character>().isChosen = false;
                BattleSystem.charCardsUI[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
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
            BattleSystem.cahngeCardWindow(character,false);            
            //Включение обводки и переменной отвечающей за то, какой персонаж выбран у выбранного персонажа
            character.GetComponent<Outline>().enabled = true;
            character.GetComponent<character>().isChosen = true;
            for (int i = 0; i < BattleSystem.field.CellsOfFieled.GetLength(0); i++)
            {
                for (int j = 0; j < BattleSystem.field.CellsOfFieled.GetLength(1); j++)
                {
                    //Отключение и переракрас всех клеток
                    BattleSystem.field.CellsOfFieled[i, j].GetComponent<Cell>().Enabled = false;
                    BattleSystem.isCellEven((i + j) % 2 == 0,true, BattleSystem.field.CellsOfFieled[i, j]);
                }
            }
            //Перебор всех клеток на поле
            for (int i = 0; i < BattleSystem.field.CellsOfFieled.GetLength(0); i++)
            {
                for (int j = 0; j < BattleSystem.field.CellsOfFieled.GetLength(1); j++)
                {
                    //Если это клетка, на которой стоит персонаж
                    if (BattleSystem.field.CellsOfFieled[i, j].transform.localPosition == character.transform.parent.localPosition)
                    {
                        //Дальше все 4 цикла отвечают за активацию нужных клеток от выбранного персноажа по каждой из 4 сторон
                        //Верх, них, право и лево
                        //низ
                        for (int k = j+1; k < BattleSystem.field.CellsOfFieled.GetLength(1); k++)
                        {
                            //Если на какой-то клетке есть др. объект (юнит или препятсвие), то дальше этой клетки в эту сторону ходить нельзя
                            if (BattleSystem.field.CellsOfFieled[i,k].transform.childCount == 1)
                            {
                                //Если координаты клетки мненьше скорости персонажа
                                if (BattleSystem.isCell(BattleSystem.field.CellsOfFieled[i, k].transform.localPosition.x / 0.27f, character.transform.parent.localPosition.x / 0.27f, character.GetComponent<character>().speed))
                                {
                                    //Включение нужной клетки
                                    BattleSystem.field.CellsOfFieled[i, k].GetComponent<Cell>().Enabled = true;
                                    BattleSystem.charCardsUI[character.GetComponent<character>().index].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                                    //Окрас клетки в зависимости от чётности/нечетности
                                    BattleSystem.isCellEven((i + k) % 2 == 0, false, BattleSystem.field.CellsOfFieled[i, k]);
                                }
                            }
                            else
                            {
                                if (BattleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).GetComponent<character>() != null && BattleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).GetComponent<character>().isEnemy == true && BattleSystem.isCell(BattleSystem.field.CellsOfFieled[i, k].transform.localPosition.x / 0.27f, character.transform.parent.localPosition.x / 0.27f, character.GetComponent<character>().range))
                                {
                                    BattleSystem.field.CellsOfFieled[i, k].GetComponent<MeshRenderer>().material.color = new Color(0.7830189f, 0.152664f, 0.152664f, 1);
                                    BattleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).GetComponent<Outline>().enabled = true;
                                    BattleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).GetComponent<character>().isChosen = true;
                                    BattleSystem.field.CellsOfFieled[i, k].GetComponent<Cell>().Enabled = true;
                                }
                                break;
                                                              
                            }
                            
                        }
                        //право
                        for (int k = i+1; k < BattleSystem.field.CellsOfFieled.GetLength(0); k++)
                        {
                            if (BattleSystem.field.CellsOfFieled[k, j].transform.childCount == 1)
                            {
                                if (BattleSystem.isCell(BattleSystem.field.CellsOfFieled[k, j].transform.localPosition.z / 0.27f, character.transform.parent.localPosition.z / 0.27f, character.GetComponent<character>().speed))
                                {
                                    
                                    BattleSystem.field.CellsOfFieled[k, j].GetComponent<Cell>().Enabled = true;
                                    BattleSystem.charCardsUI[character.GetComponent<character>().index].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                                    BattleSystem.isCellEven((k + j) % 2 == 0, false, BattleSystem.field.CellsOfFieled[k, j]);
                                }
                            }
                            else
                            {
                                if (BattleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).GetComponent<character>() != null && BattleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).GetComponent<character>().isEnemy == true && BattleSystem.isCell(BattleSystem.field.CellsOfFieled[k, j].transform.localPosition.z / 0.27f, character.transform.parent.localPosition.z / 0.27f, character.GetComponent<character>().range))
                                {
                                    BattleSystem.field.CellsOfFieled[k, j].GetComponent<MeshRenderer>().material.color = new Color(0.7830189f, 0.152664f, 0.152664f, 1);
                                    BattleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).GetComponent<Outline>().enabled = true;
                                    BattleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).GetComponent<character>().isChosen = true;
                                    BattleSystem.field.CellsOfFieled[k, j].GetComponent<Cell>().Enabled = true;
                                }
                                break;                                                               
                            }
                        }
                        //верх
                        for (int k = j-1; k >= 0; k--)
                        {
                            if (BattleSystem.field.CellsOfFieled[i,k].transform.childCount == 1)
                            {
                                if (BattleSystem.isCell(BattleSystem.field.CellsOfFieled[i, k].transform.localPosition.x / 0.27f, character.transform.parent.localPosition.x / 0.27f, character.GetComponent<character>().speed))
                                {
                                    BattleSystem.field.CellsOfFieled[i, k].GetComponent<Cell>().Enabled = true;
                                    BattleSystem.charCardsUI[character.GetComponent<character>().index].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                                    BattleSystem.isCellEven((i + k) % 2 == 0, false, BattleSystem.field.CellsOfFieled[i, k]);
                                } 
                            }
                            else
                            {
                                if (BattleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).GetComponent<character>() != null && BattleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).GetComponent<character>().isEnemy == true && BattleSystem.isCell(BattleSystem.field.CellsOfFieled[i, k].transform.localPosition.x / 0.27f, character.transform.parent.localPosition.x / 0.27f, character.GetComponent<character>().range))
                                {
                                    BattleSystem.field.CellsOfFieled[i, k].GetComponent<MeshRenderer>().material.color = new Color(0.7830189f, 0.152664f, 0.152664f, 1);
                                    BattleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).GetComponent<Outline>().enabled = true;
                                    BattleSystem.field.CellsOfFieled[i, k].transform.GetChild(1).GetComponent<character>().isChosen = true;
                                    BattleSystem.field.CellsOfFieled[i, k].GetComponent<Cell>().Enabled = true;
                                }
                                break;
                                                               
                            }
                        }
                        //лево
                        for (int k = i-1; k >= 0; k--)
                        {
                            if (BattleSystem.field.CellsOfFieled[k, j].transform.childCount == 1)
                            {
                                if (BattleSystem.isCell(BattleSystem.field.CellsOfFieled[k, j].transform.localPosition.z / 0.27f, character.transform.parent.localPosition.z / 0.27f, character.GetComponent<character>().speed))
                                {
                                    BattleSystem.field.CellsOfFieled[k, j].GetComponent<Cell>().Enabled = true;
                                    BattleSystem.charCardsUI[character.GetComponent<character>().index].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                                    BattleSystem.isCellEven((k + j) % 2 == 0, false, BattleSystem.field.CellsOfFieled[k, j]);
                                }     
                            }
                            else
                            {
                                if (BattleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).GetComponent<character>() != null && BattleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).GetComponent<character>().isEnemy == true && BattleSystem.isCell(BattleSystem.field.CellsOfFieled[k, j].transform.localPosition.z / 0.27f, character.transform.parent.localPosition.z / 0.27f, character.GetComponent<character>().range))
                                {
                                    BattleSystem.field.CellsOfFieled[k, j].GetComponent<MeshRenderer>().material.color = new Color(0.7830189f, 0.152664f, 0.152664f, 1);
                                    BattleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).GetComponent<Outline>().enabled = true;
                                    BattleSystem.field.CellsOfFieled[k, j].transform.GetChild(1).GetComponent<character>().isChosen = true;
                                    BattleSystem.field.CellsOfFieled[k, j].GetComponent<Cell>().Enabled = true;
                                }
                                break;                           
                            }
                        }
                    }            
                }
            }
        }
        yield break;
    }
    public override IEnumerator Move(GameObject cell)
    {
        /*Логика при движении*/
        //Проверка на то, можно ли поставить юнита на клетку
        if (cell.transform.childCount == 1)
        {           
                //Перебор всех персонажей в колоде
                for (int i = 0; i < BattleSystem.charCards.Count; i++)
                {
                    //ПРоверка на то, какой персонаж выбран
                    if (BattleSystem.charCards[i].GetComponent<character>().isChosen)
                    {
                        //Установление координат в новой клетке
                        BattleSystem.charCards[i].transform.SetParent(cell.transform);
                        BattleSystem.charCards[i].transform.localPosition = new Vector3(0, 1, 0);     
                        //Отключение обводки и выбранности перса
                        BattleSystem.charCards[i].GetComponent<character>().isChosen = false;
                        BattleSystem.charCards[i].GetComponent<Outline>().enabled = false;
                    }
                    BattleSystem.charCardsUI[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                }
        }
        else
        {
            if (cell.transform.GetChild(1).GetComponent<character>().isEnemy==true)
            {
                for (int i = 0; i < BattleSystem.charCards.Count; i++)
                {
                    if (BattleSystem.charCards[i].GetComponent<character>().isChosen)
                    {
                        BattleSystem.OnAttackButton(cell.transform.GetChild(1).GetComponent<character>());
                        BattleSystem.cahngeCardWindow(cell.transform.GetChild(1).gameObject, true);
                        break;
                    }                   
                }

            }
        }
            for (int i = 0; i < BattleSystem.field.CellsOfFieled.GetLength(0); i++)
            {
                for (int j = 0; j < BattleSystem.field.CellsOfFieled.GetLength(1); j++)
                {
                    //Включение и переракрас всех клеток
                    BattleSystem.field.CellsOfFieled[i, j].GetComponent<Cell>().Enabled = true;
                    BattleSystem.isCellEven((i + j) % 2 == 0, true, BattleSystem.field.CellsOfFieled[i, j]);
                }
            }
        //Отключение обводки у врагов
        for (int i = 0; i < BattleSystem.EnemyCharObjects.Count; i++)
        {
            BattleSystem.EnemyCharObjects[i].GetComponent<Outline>().enabled = false;
        }
        for (int i = 0; i < BattleSystem.EnemyStaticCharObjects.Count; i++)
        {
            BattleSystem.EnemyStaticCharObjects[i].GetComponent<Outline>().enabled = false;
        }
        yield break;
    }
    public override IEnumerator Attack(character target)
    {
        for (int i = 0; i < BattleSystem.EnemyCharObjects.Count; i++)
        {
            BattleSystem.EnemyCharObjects[i].GetComponent<Outline>().enabled = false;
        }
        for (int i = 0; i < BattleSystem.EnemyStaticCharObjects.Count; i++)
        {
            BattleSystem.EnemyStaticCharObjects[i].GetComponent<Outline>().enabled = false;
        }
        BattleSystem.cahngeCardWindow(target.gameObject, true);
        for (int i = 0; i < BattleSystem.charCards.Count; i++)
        {
            if (BattleSystem.charCards[i].GetComponent<character>().isChosen)
            {
                character charac = BattleSystem.charCards[i].GetComponent<character>();
                BattleSystem.charCards[i].GetComponent<character>().isChosen = false;
                BattleSystem.charCards[i].GetComponent<Outline>().enabled = false;
                bool isDeath = target.Damage(charac);
                target.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<healthBar>().SetHealth((float)target.health);
                if (isDeath)
                {
                    if (target.name == "Ассасин" || target.name == "Голиаф" || target.name == "Элементаль")
                    {
                        BattleSystem.EnemyStaticCharObjects.Remove(target.gameObject);
                        
                    }
                    else
                    {
                        BattleSystem.EnemyCharObjects.Remove(target.gameObject);
                    }
                    
                    GameObject.Destroy(target.gameObject);
                    Debug.Log("враг убит");
                }
                if (BattleSystem.EnemyCharObjects.Count==0)
                {
                    BattleSystem.SetState(new Won(BattleSystem));
                }
            }
            
        }       
        for (int i = 0; i < BattleSystem.field.CellsOfFieled.GetLength(0); i++)
        {
            for (int j = 0; j < BattleSystem.field.CellsOfFieled.GetLength(1); j++)
            {
                //Включение и переракрас всех клеток
                BattleSystem.field.CellsOfFieled[i, j].GetComponent<Cell>().Enabled = true;
                BattleSystem.isCellEven((i + j) % 2 == 0, true, BattleSystem.field.CellsOfFieled[i, j]);
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