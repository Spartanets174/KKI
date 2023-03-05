using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    protected BattleSystem BattleSystem;

    public State(BattleSystem battleSystem)
    {
        BattleSystem = battleSystem;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }
    public virtual IEnumerator unitStatement()
    {
        yield break;
    }
    public virtual IEnumerator chooseCharacter(GameObject character)
    {
        yield break;
    }
    public virtual IEnumerator Move(GameObject cell)
    {
        yield break;
    }
    public virtual IEnumerator Attack(character target)
    {
        yield break;
    }
    public virtual IEnumerator attackAbility()
    {
        yield break;
    }
    public virtual IEnumerator defensiveAbility()
    {
        yield break;
    }
    public virtual IEnumerator buffAbility()
    {
        yield break;
    }
    public virtual IEnumerator supportCard()
    {
        yield break;
    }
    public virtual IEnumerator useItem()
    {
        yield break;
    }
}
