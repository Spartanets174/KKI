using System.Collections;

public class Won : State
{
    public Won(BattleSystem battleSystem) : base(battleSystem)
    {
    }
    public override IEnumerator Start()
    {
        /*Логика при победе*/
        yield break;
    }
}