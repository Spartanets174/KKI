using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class BattleSystem : StateMachine
{
    /*Сюда вставлять переменные для своего, врага, интерфейса и т.д.*/
    //Интерфейс
    public Text pointsOfActionAndСube;
    public Text gameLog;
    //Окно персонажа
    public GameObject healthBar;
    public Text physAttack;
    public Text magAttack;
    public Text physDefence;
    public Text magDefence;

    public Image cardImage;
    public Image Class;
    public Text rase;
    public List<Sprite> classesSprite;
    public List<Text> racesSprite;

    //Для игрока
    public List<GameObject> charCardsUI;
    public List<GameObject> supportCardsUI;
    public List<GameObject> charCards;
    public List<GameObject> supportCards;

    //Игровые
    public PlayerManager1 playerManager;
    public Field field;
    public GameObject charPrefab;
    public bool isStart;
    public bool isUnitPlacement = true;

    //Вражиk
    //Двигающиеся враги
    public List<Card> EnemyCharCards;
    public List<GameObject> EnemyCharObjects;
    public List<GameObject> EnemySupportCards;

    //Статичные враги
    public List<Card> EnemyStaticCharCards;
    public List<GameObject> EnemyStaticCharObjects;

    private void Start()
    {
        //Установление полей ui карточек в нужные значение из playerManager
        for (int i = 0; i < charCardsUI.Count; i++)
        {
            charCardsUI[i].transform.GetChild(3).GetComponent<Image>().sprite = playerManager.deckUserCharCards[i].image;
            charCardsUI[i].transform.GetChild(4).GetComponent<Slider>().maxValue = (float)playerManager.deckUserCharCards[i].health;
            charCardsUI[i].transform.GetChild(4).GetComponent<Slider>().value = (float)playerManager.deckUserCharCards[i].health;
            charCardsUI[i].GetComponent<cardCharHolde>().card = playerManager.deckUserCharCards[i];
        }
        //Установление полей ui карточек поддержки в нужные значение из playerManager
        for (int i = 0; i < supportCardsUI.Count; i++)
        {
            supportCardsUI[i].transform.GetChild(0).GetComponent<Image>().sprite = playerManager.deckUserSupportCards[i].image;
            supportCardsUI[i].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = playerManager.deckUserSupportCards[i].ability;
            supportCardsUI[i].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = playerManager.deckUserSupportCards[i].name;
            Debug.Log(playerManager.deckUserSupportCards[i].rarity);
            if (playerManager.deckUserSupportCards[i].rarity.ToString() == "Обычная")
            {
                supportCardsUI[i].transform.GetChild(3).GetComponent<Image>().color = new Color(126, 126, 126);
            }
            else
            {
                supportCardsUI[i].transform.GetChild(3).GetComponent<Image>().color = new Color(126, 0, 255);
            }
        }
        SetState(new Begin(this));
    }
    private void Update()
    {
        //Перерисовка зоровья юнита, если оно изменяентся
        for (int i = 0; i < charCardsUI.Count; i++)
        {
            charCardsUI[i].transform.GetChild(4).GetComponent<Slider>().value = (float)playerManager.deckUserCharCards[i].health;
        }
        //Нужно для расстановки юнитов
        //Когда все 5 юнитов расставлены, то стадия расстановки (isUnitPlacement) отключается и включаются все ui карточки
    }
    public void onUnitStatementButton()
    {
        StartCoroutine(State.unitStatement());
    }
    public void OnChooseCharacterButton(GameObject character)
    {
        StartCoroutine(State.chooseCharacter(character));
    }
    public void OnMoveButton(GameObject cell)
    {
        StartCoroutine(State.Move(cell));
    }

    public void OnAttackButton(character target)
    {
        StartCoroutine(State.Attack(target));
    }

    public void OnAttackAbilityButton()
    {
        StartCoroutine(State.attackAbility());
    }

    public void OnDefensiveAbilityButton()
    {
        StartCoroutine(State.defensiveAbility());
    }

    public void OnBuffAbilityButton()
    {
        StartCoroutine(State.buffAbility());
    }
    public void OnSupportCardButton()
    {
        StartCoroutine(State.supportCard());
    }
    public void OnUseItemButton()
    {
        StartCoroutine(State.useItem());
    }

    //Функция для проверки клеток на крестообразность
    public bool isCell(float cellCoord, float charCoord, int charFeature)
    {
       
        if (Math.Floor(Mathf.Abs(charCoord - cellCoord))<= charFeature)
        {
            return true;
        }
        else
        {           
            return false;
        }
    }
    //Функция для включения и выключения нужных клеток
    public void isCellEven(bool even, bool isNormal, Cell cell)
    {
        if (isNormal)
        {
            if (even)
            {
                cell.GetComponent<MeshRenderer>().material.color = this.field.CellsOfFieled[0, 0].baseColor.color;
            }
            else
            {
                cell.GetComponent<MeshRenderer>().material.color = this.field.CellsOfFieled[0, 0].offsetColor.color;
            }
        }
        else
        {
            if (even)
            {
                cell.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
            }
            else
            {
                cell.GetComponent<MeshRenderer>().material.color = new Color(0, 0.5f, 0);
            }
        }
    }
    //Создание руки врага
    public void CreateEnemy()
    {
        while (EnemyCharCards.Count < 5)
        {
            Card EnemyMan = playerManager.allCharCards[UnityEngine.Random.Range(0, playerManager.allCharCards.Count)];
            if (!isCardInDeck(EnemyMan))
            {
                EnemyCharCards.Add(EnemyMan);
            }

        }

    }
    //Есть ли уже карта в руке
    private bool isCardInDeck(Card enemy)
    {
        for (int j = 0; j < EnemyCharCards.Count; j++)
        {
            if (enemy.name == EnemyCharCards[j].name)
            {
                return true;
            } 
        }
        return false;
    }

    public void InstantiateEnemy()
    {
        int count = 0;
        //Спавн двигающихся врагов
        while (count < 5)
        {
            GameObject Cell = field.CellsOfFieled[UnityEngine.Random.Range(0, field.CellsOfFieled.GetLength(0)), UnityEngine.Random.Range(0, 2)].gameObject;
            if (!isEnemyOnCell(Cell))
            {     
                GameObject prefab = charPrefab;
                prefab.GetComponent<character>().card = EnemyCharCards[count];
                prefab = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, Cell.transform);
                prefab.transform.localPosition = new Vector3(0, 1, 0);
                prefab.GetComponent<MeshRenderer>().sharedMaterial = prefab.GetComponent<materialPicker>().red;
                EnemyCharObjects.Add(prefab);
                EnemyCharObjects[EnemyCharObjects.Count - 1].GetComponent<character>().index = EnemyCharObjects.Count - 1;
                EnemyCharObjects[EnemyCharObjects.Count - 1].GetComponent<character>().isEnemy = true;
                EnemyCharObjects[EnemyCharObjects.Count - 1].transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<healthBar>().SetMaxHealth((float)EnemyCharObjects[EnemyCharObjects.Count - 1].GetComponent<character>().health);
                EnemyCharObjects[count].GetComponent<character>().index = count;
                count++;   
            }
        }
        //Спавн статических врагов
        for (int i = 0; i < field.CellsOfFieled.GetLength(0); i++)
        {
            for (int j = 0; j < field.CellsOfFieled.GetLength(1); j++)
            {
                //Спавн ассасинов
                if ((j == 4 || j == 6) && (i == 0||i==6))
                {
                    GameObject prefab = charPrefab;
                    prefab.GetComponent<character>().card = EnemyStaticCharCards[0];
                    prefab = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, field.CellsOfFieled[i,j].transform);
                    prefab.transform.localPosition = new Vector3(0, 1, 0);
                    prefab.GetComponent<MeshRenderer>().sharedMaterial = prefab.GetComponent<materialPicker>().assasin;
                    EnemyStaticCharObjects.Add(prefab);
                    EnemyStaticCharObjects[EnemyStaticCharObjects.Count - 1].GetComponent<character>().isEnemy = true;
                    EnemyStaticCharObjects[EnemyStaticCharObjects.Count - 1].transform.GetChild(0).gameObject.SetActive(false);
                }
                //Спавн голиафов
                if ((j == 4 || j == 6) && (i == 2 || i == 4))
                {
                    GameObject prefab = charPrefab;
                    prefab.GetComponent<character>().card = EnemyStaticCharCards[1];
                    prefab = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, field.CellsOfFieled[i, j].transform);
                    prefab.transform.localPosition = new Vector3(0, 1, 0);
                    prefab.GetComponent<MeshRenderer>().sharedMaterial = prefab.GetComponent<materialPicker>().goliaf;
                    EnemyStaticCharObjects.Add(prefab);
                    EnemyStaticCharObjects[EnemyStaticCharObjects.Count - 1].GetComponent<character>().isEnemy = true;
                    EnemyStaticCharObjects[EnemyStaticCharObjects.Count - 1].transform.GetChild(0).gameObject.SetActive(false);
                }
                //Спавн элементалей
                if ((j == 2 || j == 8) && i % 2 != 0)
                {
                    GameObject prefab = charPrefab;
                    prefab.GetComponent<character>().card = EnemyStaticCharCards[2];
                    prefab = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, field.CellsOfFieled[i, j].transform);
                    prefab.transform.localPosition = new Vector3(0, 1, 0);
                    prefab.GetComponent<MeshRenderer>().sharedMaterial = prefab.GetComponent<materialPicker>().elemental;
                    EnemyStaticCharObjects.Add(prefab);
                    EnemyStaticCharObjects[EnemyStaticCharObjects.Count - 1].GetComponent<character>().isEnemy = true;
                    EnemyStaticCharObjects[EnemyStaticCharObjects.Count - 1].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
    //Есть ли уже на клетке объект
    private bool isEnemyOnCell(GameObject cell)
    {
        if (cell.transform.childCount != 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void cahngeCardWindow(GameObject character, bool isEnemy)
    {
        cardImage.transform.parent.gameObject.SetActive(true);
       healthBar.GetComponent<healthBar>().SetMaxHealth((float)character.GetComponent<character>().card.health);
        healthBar.GetComponent<healthBar>().SetHealth((float)character.GetComponent<character>().health);
        physAttack.text = $"Физическая атака {character.GetComponent<character>().physAttack * 100}";
        magAttack.text = $"Магическая атака {character.GetComponent<character>().magAttack * 100}";
        physDefence.text = $"Физическая защита {character.GetComponent<character>().physDefence * 100}";
        magDefence.text = $"Магическая атака {character.GetComponent<character>().magDefence * 100}";
        cardImage.sprite = character.GetComponent<character>().card.image;
        switch (character.GetComponent<character>().Class)
        {
            case enums.Classes.Паладин:
                Class.sprite = classesSprite[0];
                break;
            case enums.Classes.Лучник:
               Class.sprite = classesSprite[1];
                break;
            case enums.Classes.Маг:
                Class.sprite = classesSprite[2];
                break;
            case enums.Classes.Кавалерия:
                Class.sprite = classesSprite[3];
                break;
        }
        switch (character.GetComponent<character>().race)
        {
            case enums.Races.Люди:
                rase.text = "Л";
                break;
            case enums.Races.Гномы:
                rase.text = "Г";
                break;
            case enums.Races.Эльфы:
                rase.text = "Э";
                break;
            case enums.Races.ТёмныеЭльфы:
                rase.text = "Т";
                break;
            case enums.Races.МагическиеСущества:
                rase.text = "М";
                break;
        }
        if (isEnemy)
        {
            cardImage.transform.parent.transform.parent.GetComponent<Image>().color = new Color(0.9921569f, 0.8740318f, 0.8666667f, 1);
        }
        else
        {
            cardImage.transform.parent.transform.parent.GetComponent<Image>().color = new Color(0.8707209f, 0.9921569f, 0.8666667f,1);
        }
        for (int i = 0; i < EnemyCharObjects.Count; i++)
        {
            EnemyCharObjects[i].GetComponent<Outline>().enabled =false;
        }
        for (int i = 0; i < EnemyStaticCharObjects.Count; i++)
        {
            EnemyStaticCharObjects[i].GetComponent<Outline>().enabled = false;
        }
        character.GetComponent<Outline>().enabled = true;
    }
}
