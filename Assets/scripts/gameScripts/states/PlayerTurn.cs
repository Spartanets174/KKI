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
    public override IEnumerator unitStatement()
    {
        //Для чётности/нечетности
        bool isEven = true;
        //Цикл перебора клеток, на ктороые можно установить юнита в начале боя
        for (int i = 0; i < BattleSystem.field.CellsOfFieled.GetLength(0); i++)
        {
            for (int j = 0; j < BattleSystem.field.CellsOfFieled.GetLength(1); j++)
            {
                if (j == BattleSystem.field.CellsOfFieled.GetLength(1) - 1 || j == BattleSystem.field.CellsOfFieled.GetLength(1) - 2)
                {
                    //Расцветка в зависимости от четности/нечетности
                    if (isEven)
                    {
                        BattleSystem.field.CellsOfFieled[i,j].GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
                    }
                    else
                    {
                        BattleSystem.field.CellsOfFieled[i, j].GetComponent<MeshRenderer>().material.color = new Color(0, 0.5f, 0);
                    }
                }
                //Отключение клеток, на которые нельзя ходить
                else
                {
                    BattleSystem.field.CellsOfFieled[i, j].GetComponent<Cell>().Enabled = false;
                }
                isEven = !isEven;
            }           
        }
        yield break;
    }
    public override IEnumerator chooseCharacter(GameObject character)
    {
        if (!BattleSystem.isUnitPlacement)
        {        
            bool isEven = true;
            for (int i = 0; i < BattleSystem.field.CellsOfFieled.GetLength(0); i++)
            {
                for (int j = 0; j < BattleSystem.field.CellsOfFieled.GetLength(1); j++)
                {
                    if (isEven)
                    {
                        BattleSystem.field.CellsOfFieled[i,j].GetComponent<MeshRenderer>().material.color = BattleSystem.field.CellsOfFieled[i, j].baseColor.color;
                    }
                    else
                    {
                        BattleSystem.field.CellsOfFieled[i, j].GetComponent<MeshRenderer>().material.color = BattleSystem.field.CellsOfFieled[i, j].offsetColor.color;
                    }
                    BattleSystem.field.CellsOfFieled[i, j].GetComponent<Cell>().Enabled = true;
                    isEven = !isEven;
                }
            }
            for (int i = 0; i < BattleSystem.charCards.Count; i++)
            {
                BattleSystem.charCards[i].GetComponent<Outline>().enabled = false;
                BattleSystem.charCards[i].GetComponent<character>().isChosen = false;
            }
            character.GetComponent<Outline>().enabled = true;
            character.GetComponent<character>().isChosen = true;
            for (int i = 0; i < BattleSystem.field.CellsOfFieled.GetLength(0); i++)
            {
                for (int j = 0; j < BattleSystem.field.CellsOfFieled.GetLength(1); j++)
                {
                    BattleSystem.field.CellsOfFieled[i, j].GetComponent<Cell>().Enabled = false;
                }
            }
            for (int i = 0; i < BattleSystem.field.CellsOfFieled.GetLength(0); i++)
            {
                for (int j = 0; j < BattleSystem.field.CellsOfFieled.GetLength(1); j++)
                {
                    if (BattleSystem.field.CellsOfFieled[i, j].transform.localPosition== character.transform.parent.localPosition)
                    {
                        isEven = true;
                        //низ
                        for (int k = j+1; k < BattleSystem.field.CellsOfFieled.GetLength(1); k++)
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
                                    if (isEven)
                                    {
                                        BattleSystem.field.CellsOfFieled[i, k].GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
                                    }
                                    else
                                    {
                                        BattleSystem.field.CellsOfFieled[i, k].GetComponent<MeshRenderer>().material.color = new Color(0, 0.5f, 0);
                                    }
                                    isEven = !isEven;
                                }                               
                            }
                            
                        }
                        isEven = true;
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
                                    if (isEven)
                                    {
                                        BattleSystem.field.CellsOfFieled[k, j].GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
                                    }
                                    else
                                    {
                                        BattleSystem.field.CellsOfFieled[k, j].GetComponent<MeshRenderer>().material.color = new Color(0, 0.5f, 0);
                                    }
                                    isEven = !isEven;
                                }                               
                            }
                        }
                        isEven = true;
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
                                    if (isEven)
                                    {
                                        BattleSystem.field.CellsOfFieled[i, k].GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
                                    }
                                    else
                                    {
                                        BattleSystem.field.CellsOfFieled[i, k].GetComponent<MeshRenderer>().material.color = new Color(0, 0.5f, 0);
                                    }
                                    isEven = !isEven;
                                }                                
                            }
                        }
                        isEven = true;
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
                                    if (isEven)
                                    {
                                        BattleSystem.field.CellsOfFieled[k, j].GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
                                    }
                                    else
                                    {
                                        BattleSystem.field.CellsOfFieled[k, j].GetComponent<MeshRenderer>().material.color = new Color(0, 0.5f, 0);
                                    }
                                    isEven = !isEven;
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
            bool isEven = true;
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
                        BattleSystem.charCards[BattleSystem.charCards.Count - 1].GetComponent<character>().index = BattleSystem.charCards.Count - 1;
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
            }
            else
            {
                for (int i = 0; i < BattleSystem.charCards.Count; i++)
                {
                    if (BattleSystem.charCards[i].GetComponent<character>().isChosen)
                    {
                        BattleSystem.charCards[i].transform.SetParent(cell.transform);
                        BattleSystem.charCards[i].transform.localPosition = new Vector3(0, 1, 0);
                        BattleSystem.charCards[i].GetComponent<character>().isChosen = false;
                        BattleSystem.charCards[i].GetComponent<Outline>().enabled = false;
                    }
                }
            }
            for (int i = 0; i < BattleSystem.field.CellsOfFieled.GetLength(0); i++)
            {
                for (int j = 0; j < BattleSystem.field.CellsOfFieled.GetLength(1); j++)
                {
                    BattleSystem.field.CellsOfFieled[i, j].GetComponent<Cell>().Enabled = true;
                    if (isEven)
                    {
                        BattleSystem.field.CellsOfFieled[i, j].GetComponent<MeshRenderer>().material.color = BattleSystem.field.CellsOfFieled[i, j].baseColor.color;
                    }
                    else
                    {
                        BattleSystem.field.CellsOfFieled[i, j].GetComponent<MeshRenderer>().material.color = BattleSystem.field.CellsOfFieled[i, j].offsetColor.color;
                    }
                    isEven = !isEven;
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