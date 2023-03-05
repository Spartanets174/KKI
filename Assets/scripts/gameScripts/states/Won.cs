using System.Collections;

public class Won : State
{
    public Won(BattleSystem battleSystem) : base(battleSystem)
    {
    }
    public override IEnumerator Start()
    {
        /*Логика при победе*/
        BattleSystem.gameLog.text += $"Вы победили" + "\n";
        yield break;
    }
}