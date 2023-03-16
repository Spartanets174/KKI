using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Support", menuName = "Card support")]
public class cardSupport : ScriptableObject
{
    public new string name;
    public enums.Races race;
    public enums.typeOfSupport type;
    public Sprite image;
    public string ability;
    public enums.Rarity rarity;
    public int Price;
    public int id;
}
