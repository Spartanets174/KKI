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
    public override IEnumerator chooseCharacter()
    {
        /*Логика при выборе перса*/
        //Проверка идёт ли сейчас расстановка юнитов
        if (BattleSystem.isUnitPlacement)
        {
            //Для чётности/нечетности
            bool isEven = true;        
            //Цикл перебора клеток, на ктороые можно установить юнита в начале боя
            for (int i = 0; i < BattleSystem.field.CellsOfFieled.Count; i++)
            {
                if (BattleSystem.field.CellsOfFieled[i].transform.localPosition.z / 0.27f == 0 || BattleSystem.field.CellsOfFieled[i].transform.localPosition.z / 0.27f == -1)
                {
                    //Расцветка в зависимости от четности/нечетности
                    if (isEven)
                    {
                        BattleSystem.field.CellsOfFieled[i].GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
                    }
                    else
                    {
                        BattleSystem.field.CellsOfFieled[i].GetComponent<MeshRenderer>().material.color = new Color(0, 0.5f, 0);
                    }
                }
                //Отключение клеток, на которые нельзя ходить
                else
                {
                    BattleSystem.field.CellsOfFieled[i].GetComponent<Cell>().Enabled = false;
                }
                isEven = !isEven;
            }
        }
        else
        {

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
                //Для чётности/нечетности
                bool isEven = true;                
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
                //Изменение цвета клеток
                for (int i = 0; i < BattleSystem.field.CellsOfFieled.Count; i++)
                {
                    BattleSystem.field.CellsOfFieled[i].GetComponent<Cell>().Enabled = true;
                    if (isEven)
                    {
                        BattleSystem.field.CellsOfFieled[i].GetComponent<MeshRenderer>().material.color = BattleSystem.field.CellsOfFieled[i].baseColor.color;
                    }
                    else
                    {
                        BattleSystem.field.CellsOfFieled[i].GetComponent<MeshRenderer>().material.color = BattleSystem.field.CellsOfFieled[i].offsetColor.color;
                    }
                    isEven = !isEven;
                }
            }
            else
            {

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