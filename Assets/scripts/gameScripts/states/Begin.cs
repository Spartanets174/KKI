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
        /*Логика при старте*/
        //Бросок кубика
        int cubeValue = Random.Range(1, 6);
        BattleSystem.pointsOfActionAndСube.text = cubeValue.ToString();
        //Определение хода       
      /*  if (cubeValue%2==0)
        {*/
            BattleSystem.gameLog.text += $"На кубице выпало {cubeValue}, ваш ход." + "\n"+"Начните расстановку юнитов." + "\n";
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
 /*       }
        else
        {
            BattleSystem.gameLog.text += $"На кубице выпало {cubeValue}, ход противника" + "\n";
            BattleSystem.SetState(new EnemyTurn(BattleSystem));
        }*/
        
        yield break;
    }
}
