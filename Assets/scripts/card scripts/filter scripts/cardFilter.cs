using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class cardFilter : MonoBehaviour
{
    /*Основной класс для фильтрации карточек по выбранным параметрам*/
    public string cardRace = "";
    public string cardClass = "";
    public string cardSupportType = "";
    public List<Card> cards;
    public List<cardSupport> supportCards;
    public List<GameObject> raceButtons;
    public List<GameObject> classButtons;
    public List<GameObject> supportButtons;
    public cardSpawner cardSpawner;

    /*Запись в list "cards" всех scriptable objects для применения при фильтрации */
    private void Start()
    {
        for (int i = 0; i < cardSpawner.cardObjects.Count; i++)
        {
            cards.Add(cardSpawner.cardObjects[i]);
        }
        for (int i = 0; i < cardSpawner.cardSupportObjects.Count; i++)
        {
            supportCards.Add(cardSpawner.cardSupportObjects[i]);
        }
    }

   /* Функция для фильтрации, которая принимает 2 string значения: расу и класс карты, 
    * по которым и происходит фильтрация list c картами*/
    public void cardFiltration()
    {
        //Если ничего не выбрано
        if (cardRace==""&&cardClass=="")
        {
            cards.Clear();
            for (int i = 0; i < cardSpawner.cardObjects.Count; i++)
            {
                cards.Add(cardSpawner.cardObjects[i]);
            }
            cardSpawner.cardSpawn(cards);
        }
        //Если выбран только класс
        if (cardRace == "" && cardClass != "")
        {
            List<Card> onlyClass = cards.Where(x=>x.Class.ToString() == cardClass).ToList();
            cardSpawner.cardSpawn(onlyClass);
        }
        //Если выбрана только раса
        if (cardRace != "" && cardClass == "")
        {
            List<Card> onlyRace = cards.Where(x => x.race.ToString() == cardRace).ToList();          
            cardSpawner.cardSpawn(onlyRace);
        }
        //Если выбраны оба параметра
        if (cardRace != "" && cardClass != "")
        {
            List<Card> raceAndClass = cards.Where(x => x.race.ToString() == cardRace&& x.Class.ToString() == cardClass).ToList();
            cardSpawner.cardSpawn(raceAndClass);
        }
    }
    public void cardSupportFiltration()
    {
        if (cardSupportType == "")
        {
            supportCards.Clear();
            for (int i = 0; i < cardSpawner.cardSupportObjects.Count; i++)
            {
                supportCards.Add(cardSpawner.cardSupportObjects[i]);
            }
            cardSpawner.cardSupportSpawn(supportCards);
        }
        if (cardSupportType != "")
        {
            List<cardSupport> type = supportCards.Where(x => x.type.ToString() == cardSupportType).ToList();
            cardSpawner.cardSupportSpawn(type);
        }
    }
}
