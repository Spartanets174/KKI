using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card",menuName = "Card")]
public class Card: ScriptableObject
{
    public new string name;
    public string race;
    public string Class;
    public string rarity;
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
