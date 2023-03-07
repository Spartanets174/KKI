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
    public float health;
    public int speed;
    public float physAttack;
    public float magAttack;
    public int range;
    public float physDefence;
    public float magDefence;
    public float critChance;
    public float critNum;
    public GameObject Model;
    public int index;
    public bool isChosen=false;
    public bool isEnemy = false;
    public bool wasAttack = false;
    private KeyCode[] keyCodes = new KeyCode[5] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };
    private BattleSystem battleSystem;
    void Awake()
    {
        name = card.name;
        race = card.race;
        Class = card.Class; 
        rarity = card.rarity;
        health = (float)card.health;
        speed = card.speed;
        physAttack = (float)card.physAttack;
        magAttack = (float)card.magAttack;
        range = card.range;
        physDefence = (float)card.physDefence;
        magDefence = (float)card.magDefence;
        critChance = (float)card.critChance;
        critNum = (float)card.critNum;

    }
    private void Start()
    {
        battleSystem = GameObject.Find("battleSystem").GetComponent<BattleSystem>();
    }
    private void Update()
    {
        if (Input.GetKey(keyCodes[index]) && !isEnemy)
        {
            battleSystem.OnChooseCharacterButton(this.gameObject);
        }
    }
    private void OnMouseDown()
    {
        if (!isEnemy)
        {
            battleSystem.OnChooseCharacterButton(this.gameObject);
        }
        else
        {
            if (this.isChosen)
            {
                battleSystem.GetComponent<BattleSystem>().OnAttackButton(this);
            }
            battleSystem.GetComponent<BattleSystem>().cahngeCardWindow(this.gameObject, true);
        }
    }
    public bool Damage(character chosenCharacter)
    {
        float crit = isCrit(chosenCharacter);
        float finalPhysDamage = ((11 + chosenCharacter.physAttack) * chosenCharacter.physAttack * crit * (chosenCharacter.physAttack - physDefence + (float)card.health)) / 256;
        float finalMagDamage = ((11 + chosenCharacter.magAttack) * chosenCharacter.magAttack * crit * (chosenCharacter.magAttack - magDefence + (float)card.health)) / 256;
        float finalDamage = System.Math.Max(finalMagDamage, finalPhysDamage);
        Debug.Log("finalMagDamage: " + finalMagDamage + " finalPhysDamage: " + finalPhysDamage);
        health = System.Math.Max(0, health - finalDamage);
        Debug.Log("health: " + health);
        battleSystem.gameLog.text += $"{chosenCharacter.name} наносит  юниту {name} {Mathf.RoundToInt(finalDamage*100)} урона"+"\n";
        battleSystem.gameLogScrollBar.value = 0f;
        return health == 0;
    }

    public float isCrit(character character)
    {
        float chance =  UnityEngine.Random.Range(0f,1f);
        Debug.Log("chance: " + chance);
        if (chance< character.critChance)
        {
            Debug.Log("critNum: " + character.critNum);
            return character.critNum;
        }
        else
        {
            return 1;
        }
    }

    public void Heal(int amount)
    {
        health += amount;
    }
}
