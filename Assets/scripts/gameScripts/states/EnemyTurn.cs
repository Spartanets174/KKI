using System.Collections;
using UnityEngine;

public class EnemyTurn : State
{
    private static System.Timers.Timer aTimer;
    public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
    {

    }
    public override IEnumerator Start()
    {
        BattleSystem.gameLog.text += $"Ход противника." + "\n";
        BattleSystem.EndMove.interactable = false;
        BattleSystem.StartCoroutine(ExampleCoroutine());       
        BattleSystem.StopCoroutine(ExampleCoroutine());
        yield break;
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        BattleSystem.SetState(new PlayerTurn(BattleSystem));
    }

    public override IEnumerator chooseCharacter(GameObject character)
    {
        /*Логика при выборе перса*/
        yield break;
    }
    public override IEnumerator Move(GameObject cell)
    {
        /*Логика при движении*/
        yield break;
    }
    public override IEnumerator Attack(character target)
    {
        /*Логика при атаке*/
        yield break;
    }
    public override IEnumerator attackAbility()
    {
        /*Логика при применении способности 1*/
        yield break;
    }
    public override IEnumerator defensiveAbility()
    {
        /*Логика при применении способности 2*/
        yield break;
    }
    public override IEnumerator buffAbility()
    {
        /*Логика при применении способности 3*/
        yield break;
    }
    public override IEnumerator supportCard()
    {
        /*Логика при применении карты помощи*/
        yield break;
    }
    public override IEnumerator useItem()
    {
        /*Логика при применении предмета*/
        yield break;
    }
}