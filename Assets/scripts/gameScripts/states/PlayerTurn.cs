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
    //При расстановек персонажа
    public override IEnumerator unitStatement()
    {
        //Цикл перебора клеток, на ктороые можно установить юнита в начале боя
        for (int i = 0; i < BattleSystem.field.CellsOfFieled.GetLength(0); i++)
        {
            for (int j = 0; j < BattleSystem.field.CellsOfFieled.GetLength(1); j++)
            {
                if (j == BattleSystem.field.CellsOfFieled.GetLength(1) - 1 || j == BattleSystem.field.CellsOfFieled.GetLength(1) - 2)
                {
                    //Расцветка в зависимости от четности/нечетности
                    BattleSystem.isCellEven((i+j)%2==0, false, BattleSystem.field.CellsOfFieled[i, j]);
                }
                //Отключение клеток, на которые нельзя ходить
                else
                {
                    BattleSystem.field.CellsOfFieled[i, j].GetComponent<Cell>().Enabled = false;
                }
            }           
        }
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
                            if (BattleSystem.field.CellsOfFieled[i,k].transform.childCount != 1)
                            {
                                break;
                            }
                            else
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
                            
                        }
                        //право
                        for (int k = i+1; k < BattleSystem.field.CellsOfFieled.GetLength(0); k++)
                        {
                            if (BattleSystem.field.CellsOfFieled[k, j].transform.childCount != 1)
                            {
                                break;
                            }
                            else
                            {
                                if (BattleSystem.isCell(BattleSystem.field.CellsOfFieled[k, j].transform.localPosition.z / 0.27f, character.transform.parent.localPosition.z / 0.27f, character.GetComponent<character>().speed))
                                {
                                    BattleSystem.field.CellsOfFieled[k, j].GetComponent<Cell>().Enabled = true;
                                    BattleSystem.charCardsUI[character.GetComponent<character>().index].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                                    BattleSystem.isCellEven((k + j) % 2 == 0,false, BattleSystem.field.CellsOfFieled[k, j]);
                                }                               
                            }
                        }
                        //верх
                        for (int k = j-1; k >= 0; k--)
                        {
                            if (BattleSystem.field.CellsOfFieled[i,k].transform.childCount != 1)
                            {
                                break;
                            }
                            else
                            {
                                if (BattleSystem.isCell(BattleSystem.field.CellsOfFieled[i, k].transform.localPosition.x / 0.27f, character.transform.parent.localPosition.x / 0.27f, character.GetComponent<character>().speed))
                                {
                                    BattleSystem.field.CellsOfFieled[i, k].GetComponent<Cell>().Enabled = true;
                                    BattleSystem.charCardsUI[character.GetComponent<character>().index].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                                    BattleSystem.isCellEven((i + k) % 2 == 0, false, BattleSystem.field.CellsOfFieled[i, k]);
                                }                                
                            }
                        }
                        //лево
                        for (int k = i-1; k >= 0; k--)
                        {
                            if (BattleSystem.field.CellsOfFieled[k, j].transform.childCount != 1)
                            {
                                break;
                            }
                            else
                            {
                                if (BattleSystem.isCell(BattleSystem.field.CellsOfFieled[k, j].transform.localPosition.z / 0.27f, character.transform.parent.localPosition.z / 0.27f, character.GetComponent<character>().speed))
                                {
                                    BattleSystem.field.CellsOfFieled[k, j].GetComponent<Cell>().Enabled = true;
                                    BattleSystem.charCardsUI[character.GetComponent<character>().index].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                                    BattleSystem.isCellEven((k + j) % 2 == 0, false, BattleSystem.field.CellsOfFieled[k, j]);
                                }                                
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
            //Проверка идёт ли сейчас расстановка юнитов
            if (BattleSystem.isUnitPlacement)
            {         
                //Цикл перебора ui карточек для спавна нужной карточки при выборе
                for (int i = 0; i < BattleSystem.charCardsUI.Count; i++)
                {
                    //Проверка на то, какая карточка выбрана
                    if (BattleSystem.charCardsUI[i].GetComponent<cardCharHolde>().isSelected)
                    {  
                        //Перевыбор префаба
                        GameObject prefab = BattleSystem.charPrefab;
                        //Запись нужных данных о карточке в префаб
                        prefab.GetComponent<character>().card = BattleSystem.charCardsUI[i].GetComponent<cardCharHolde>().card;
                        //Создание на сцене префаба и расстановка на нужную позицию
                        prefab = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, cell.transform);
                        prefab.transform.localPosition = new Vector3(0, 1, 0);
                        BattleSystem.charCards.Add(prefab);
                        BattleSystem.charCards[BattleSystem.charCards.Count - 1].GetComponent<character>().index = i;         
                        BattleSystem.charCardsUI[i].transform.GetChild(4).GetComponent<healthBar>().SetMaxHealth((float)BattleSystem.charCards[BattleSystem.charCards.Count - 1].GetComponent<character>().health);
                        //Смена полей у ui карточек
                        BattleSystem.charCardsUI[i].GetComponent<cardCharHolde>().isSelected = false;
                        BattleSystem.charCardsUI[i].GetComponent<cardCharHolde>().wasChosen = true;
                    }
                    //Если карта не была раньше выбрана, то происходит включение оставшихся ui карт
                    if (!BattleSystem.charCardsUI[i].GetComponent<cardCharHolde>().wasChosen)
                    {
                        BattleSystem.charCardsUI[i].GetComponent<Button>().interactable = true;
                        BattleSystem.charCardsUI[i].GetComponent<Button>().enabled = true;
                    }                   
                }                
                if (BattleSystem.charCards.Count == 5)
                {
                    BattleSystem.isUnitPlacement = false;
                }
            }
            else
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
            for (int i = 0; i < BattleSystem.field.CellsOfFieled.GetLength(0); i++)
            {
                for (int j = 0; j < BattleSystem.field.CellsOfFieled.GetLength(1); j++)
                {
                    //Включение и переракрас всех клеток
                    BattleSystem.field.CellsOfFieled[i, j].GetComponent<Cell>().Enabled = true;
                    BattleSystem.isCellEven((i + j) % 2 == 0, true, BattleSystem.field.CellsOfFieled[i, j]);
                }
            }
        }       
        yield break;
    }
    public override IEnumerator Attack()
    {
        /*Логика при атаке*/
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