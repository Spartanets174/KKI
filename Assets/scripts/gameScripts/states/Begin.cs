using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : State
{
    public Begin(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        /*������ ��� ������*/
        //������ ������
        int cubeValue = Random.Range(1, 6);
        BattleSystem.pointsOfActionAnd�ube.text = cubeValue.ToString();
        //����������� ����       
      /*  if (cubeValue%2==0)
        {*/
            BattleSystem.gameLog.text += $"�� ������ ������ {cubeValue}, ��� ���." + "\n"+"������� ����������� ������." + "\n";
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
 /*       }
        else
        {
            BattleSystem.gameLog.text += $"�� ������ ������ {cubeValue}, ��� ����������" + "\n";
            BattleSystem.SetState(new EnemyTurn(BattleSystem));
        }*/
        
        yield break;
    }
}
