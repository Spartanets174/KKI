using System.Collections;

public class Won : State
{
    public Won(BattleSystem battleSystem) : base(battleSystem)
    {
    }
    public override IEnumerator Start()
    {
        /*Логика при победе*/
        BattleSystem.Camera.enabled = false;
        for (int i = 0; i < BattleSystem.field.CellsOfFieled.GetLength(0); i++)
        {
            for (int j = 0; j < BattleSystem.field.CellsOfFieled.GetLength(1); j++)
            {
                BattleSystem.field.CellsOfFieled[i, j].Enabled = false;
            }
        }
        for (int i = 0; i < BattleSystem.charCards.Count; i++)
        {
            BattleSystem.charCards[i].GetComponent<Outline>().enabled = false;
            BattleSystem.charCards[i].GetComponent<character>().isChosen = false;
            BattleSystem.charCards[i].GetComponent<character>().enabled = false;
        }
        //Отключение обводки у врагов
        for (int i = 0; i < BattleSystem.EnemyCharObjects.Count; i++)
        {
            BattleSystem.EnemyCharObjects[i].GetComponent<Outline>().enabled = false;
            BattleSystem.EnemyCharObjects[i].GetComponent<character>().isChosen = false;
            BattleSystem.EnemyCharObjects[i].GetComponent<character>().enabled = false;
        }
        for (int i = 0; i < BattleSystem.EnemyStaticCharObjects.Count; i++)
        {
            BattleSystem.EnemyStaticCharObjects[i].GetComponent<Outline>().enabled = false;
            BattleSystem.EnemyStaticCharObjects[i].GetComponent<character>().isChosen = false;
            BattleSystem.EnemyStaticCharObjects[i].GetComponent<character>().enabled = false;
        }
        BattleSystem.endGameInterface.gameObject.SetActive(true);
        BattleSystem.gameInterface.gameObject.SetActive(false);
        BattleSystem.endGameText.text = $"Поздравляем с победой, {BattleSystem.playerManager.Name}! В награду вы получаете 3000 валюты!";
        BattleSystem.playerManager.money += 3000;
        yield break;
    }
}