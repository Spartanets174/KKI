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
    public virtual IEnumerator chooseCharacter()
    {
        yield break;
    }
    public virtual IEnumerator Move()
    {
        yield break;
    }
    public virtual IEnumerator Attack()
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
