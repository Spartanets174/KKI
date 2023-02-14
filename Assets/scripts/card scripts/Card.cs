using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//scriptable object для карт со всеми параметрами карты
[CreateAssetMenu(fileName = "New Card",menuName = "Card")]
public class Card: ScriptableObject
{
    public new string name;
    public enums.Races race;
    public enums.Classes Class;
    public enums.Rarity rarity;
    public string description;
    public double health;
    public int speed;
    public double physAttack;
    public double magAttack;
    public int Range;
    public double physDefence;
    public double magDefence;
    public double critChance;
    public double critNum;
    public string passiveAbility;
    public string attackAbility;
    public string defenceAbility;
    public string buffAbility;
    public Sprite image;
    public int Price;
} 
