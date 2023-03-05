using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Begin : State
{
    //������ ������
    private int cubeValue = Random.Range(1, 6);
    public Begin(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        /*������ ��� ������*/
        BattleSystem.gameLog.text += $"������� ����������� ������." + "\n";
        BattleSystem.pointsOfActionAnd�ube.text = cubeValue.ToString();
        BattleSystem.CreateEnemy();
        BattleSystem.InstantiateEnemy();        
        yield break;
    }

    public override IEnumerator unitStatement()
    {
        //���� �������� ������, �� ������� ����� ���������� ����� � ������ ���
        for (int i = 0; i < BattleSystem.field.CellsOfFieled.GetLength(0); i++)
        {
            for (int j = 0; j < BattleSystem.field.CellsOfFieled.GetLength(1); j++)
            {
                if (j == BattleSystem.field.CellsOfFieled.GetLength(1) - 1 || j == BattleSystem.field.CellsOfFieled.GetLength(1) - 2)
                {
                    //��������� � ����������� �� ��������/����������
                    BattleSystem.isCellEven((i + j) % 2 == 0, false, BattleSystem.field.CellsOfFieled[i, j]);
                }
                //���������� ������, �� ������� ������ ������
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
        /*������ ��� ��������*/
        //�������� �� ��, ����� �� ��������� ����� �� ������
        if (cell.transform.childCount == 1)
        {
            //�������� ��� �� ������ ����������� ������
            if (BattleSystem.isUnitPlacement)
            {
                //���� �������� ui �������� ��� ������ ������ �������� ��� ������
                for (int i = 0; i < BattleSystem.charCardsUI.Count; i++)
                {
                    //�������� �� ��, ����� �������� �������
                    if (BattleSystem.charCardsUI[i].GetComponent<cardCharHolde>().isSelected)
                    {
                        //��������� �������
                        GameObject prefab = BattleSystem.charPrefab;
                        prefab.GetComponent<Outline>().enabled = false;
                        //������ ������ ������ � �������� � ������
                        prefab.GetComponent<character>().card = BattleSystem.charCardsUI[i].GetComponent<cardCharHolde>().card;
                        //�������� �� ����� ������� � ����������� �� ������ �������
                        prefab = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, cell.transform);
                        prefab.transform.localPosition = new Vector3(0, 1, 0);
                        prefab.GetComponent<MeshRenderer>().sharedMaterial = prefab.GetComponent<materialPicker>().green;
                        BattleSystem.charCards.Add(prefab);
                        BattleSystem.charCards[BattleSystem.charCards.Count - 1].GetComponent<character>().index = i;
                        BattleSystem.charCards[BattleSystem.charCards.Count - 1].GetComponent<character>().isEnemy = false;
                        BattleSystem.charCardsUI[i].transform.GetChild(4).GetComponent<healthBar>().SetMaxHealth((float)BattleSystem.charCards[BattleSystem.charCards.Count - 1].GetComponent<character>().health);
                        BattleSystem.charCards[BattleSystem.charCards.Count - 1].transform.GetChild(0).gameObject.SetActive(false);
                        //����� ����� � ui ��������
                        BattleSystem.charCardsUI[i].GetComponent<cardCharHolde>().isSelected = false;
                        BattleSystem.charCardsUI[i].GetComponent<cardCharHolde>().wasChosen = true;
                    }
                    //���� ����� �� ���� ������ �������, �� ���������� ��������� ���������� ui ����
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
                        //��������� � ���������� ���� ������
                        BattleSystem.field.CellsOfFieled[i, j].GetComponent<Cell>().Enabled = true;
                        BattleSystem.isCellEven((i + j) % 2 == 0, true, BattleSystem.field.CellsOfFieled[i, j]);
                    }
                }
            }
        }
        //��� ����������� ���������
        //����������� ����       
        if (BattleSystem.charCards.Count == 5)
        {
            /*  if (cubeValue%2==0)
          {*/
            BattleSystem.gameLog.text += $"�� ������ ������ {cubeValue}, ��� ���." + "\n";
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
            /*       }
                   else
                   {
                       BattleSystem.gameLog.text += $"�� ������ ������ {cubeValue}, ��� ����������" + "\n";
                       BattleSystem.SetState(new EnemyTurn(BattleSystem));
                   }*/
        }
        yield break;
    }
}
