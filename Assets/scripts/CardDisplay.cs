using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text name;
    public Text race;
    public Text Class;
    public Image rarity;
    public Text description;
    public Text health;
    public Text speed;
    public Text physAttack;
    public Text magAttack;
    public Text Range;
    public Text physDefence;
    public Text magDefence;
    public Text critChance;
    public Text critNum;
    public Text passiveAbility;
    public Text attackAbility;
    public Text defenceAbility;
    public Text buffAbility;
    public List<Image> image;
    public Text Price;
    void Start()
    {
        name.text = card.name;
        /*race.text = card.race;*/
        /*Class.text = card.Class;*/
        if (card.rarity == "Обычная")
        {
            rarity.color = new Color(125f, 125f, 125f);
        }
        else
        {
            rarity.color = new Color(126f, 0f, 255f);
        }
        /*description.text = card.description;*/
        health.text = Convert.ToString(card.health*100);
        /*speed.text = card.speed.ToString();*/
        physAttack.text = Convert.ToString(card.physAttack*100);
        magAttack.text = Convert.ToString(card.magAttack*100);
        /*Range.text = card.Range.ToString();*/
        physDefence.text = Convert.ToString(card.physDefence*100);
        magDefence.text = Convert.ToString(card.magDefence*100);
      /*  critChance.text = Convert.ToString(card.critChance*100);
        critNum.text = Convert.ToString(card.critNum*100);*/
        /*passiveAbility.text = card.passiveAbility;*/
        attackAbility.text = card.attackAbility;
        defenceAbility.text = card.defenceAbility;
        buffAbility.text = card.buffAbility;
        for (int i = 0; i < image.Count; i++)
        {
            image[i].sprite = card.image;
        }
        Price.text = card.Price.ToString();
    }
}
