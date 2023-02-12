using System.Collections;

public class Lost: State
{ 
    public Lost(BattleSystem battleSystem): base(battleSystem)
    {
    }
    public override IEnumerator Start()
    {
        /*Логика при поражении*/
        yield break;
    }
}