using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Скрипт на подобии скриптов cardDisplay и cardSupportDisplay
//Позволяет отображать объект card на игровом объекте 
//Также позволяет манипулировать данным игровым объектом
//с помощью методов как Damage/Heal
public class character : MonoBehaviour
{
    public Card card;
    public new string name;
    public enums.Races race;
    public enums.Classes Class;
    public enums.Rarity rarity;
    public double health;
    public int speed;
    public double physAttack;
    public double magAttack;
    public int range;
    public double physDefence;
    public double magDefence;
    public double critChance;
    public double critNum;
    public GameObject Model;
    public int index;
    public bool isChosen=false;
    private KeyCode[] keyCodes = new KeyCode[5] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };
    void Start()
    {
        name = card.name;
        race = card.race;
        Class = card.Class; 
        rarity = card.rarity;
        health = card.health;
        speed = card.speed;
        physAttack = card.physAttack;
        magAttack = card.magAttack;
        range = card.range;
        physDefence = card.physDefence;
        magDefence = card.magDefence;
        critChance = card.critChance;
        critNum = card.critNum;
    }
    private void Update()
    {
        if (Input.GetKey(keyCodes[index]))
        {
            GameObject.Find("battleSystem").GetComponent<BattleSystem>().OnChooseCharacterButton(this.gameObject);
        }
    }
    private void OnMouseDown()
    {
        GameObject.Find("battleSystem").GetComponent<BattleSystem>().OnChooseCharacterButton(this.gameObject);
    }
    public bool Damage(int amount)
    {
        health = System.Math.Max(0, health - amount);
        return health == 0;
    }

    public void Heal(int amount)
    {
        health += amount;
    }
}
