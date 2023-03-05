using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Begin : State
{
    //Бросок кубика
    private int cubeValue = Random.Range(1, 6);
    public Begin(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        /*Логика при старте*/
        BattleSystem.gameLog.text += $"Начните расстановку юнитов." + "\n";
        BattleSystem.pointsOfActionAndСube.text = cubeValue.ToString();
        BattleSystem.CreateEnemy();
        BattleSystem.InstantiateEnemy();        
        yield break;
    }

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
                    BattleSystem.isCellEven((i + j) % 2 == 0, false, BattleSystem.field.CellsOfFieled[i, j]);
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
                        prefab.GetComponent<Outline>().enabled = false;
                        //Запись нужных данных о карточке в префаб
                        prefab.GetComponent<character>().card = BattleSystem.charCardsUI[i].GetComponent<cardCharHolde>().card;
                        //Создание на сцене префаба и расстановка на нужную позицию
                        prefab = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, cell.transform);
                        prefab.transform.localPosition = new Vector3(0, 1, 0);
                        prefab.GetComponent<MeshRenderer>().sharedMaterial = prefab.GetComponent<materialPicker>().green;
                        BattleSystem.charCards.Add(prefab);
                        BattleSystem.charCards[BattleSystem.charCards.Count - 1].GetComponent<character>().index = i;
                        BattleSystem.charCards[BattleSystem.charCards.Count - 1].GetComponent<character>().isEnemy = false;
                        BattleSystem.charCardsUI[i].transform.GetChild(4).GetComponent<healthBar>().SetMaxHealth((float)BattleSystem.charCards[BattleSystem.charCards.Count - 1].GetComponent<character>().health);
                        BattleSystem.charCards[BattleSystem.charCards.Count - 1].transform.GetChild(0).gameObject.SetActive(false);
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
        }
        //При расстановек персонажа
        //Определение хода       
        if (BattleSystem.charCards.Count == 5)
        {
            /*  if (cubeValue%2==0)
          {*/
            BattleSystem.gameLog.text += $"На кубице выпало {cubeValue}, ваш ход." + "\n";
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
            /*       }
                   else
                   {
                       BattleSystem.gameLog.text += $"На кубице выпало {cubeValue}, ход противника" + "\n";
                       BattleSystem.SetState(new EnemyTurn(BattleSystem));
                   }*/
        }
        yield break;
    }
}
