using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс для создания нужных карточек на сцене
public class cardSpawner : MonoBehaviour
{
    //Список scriptable objects, которые надо спавнить
    public List<Card> cardObjects;
    public List<cardSupport> cardSupportObjects;
    //Префаб, который спавнится с нужным scriptable object
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject cardSupportPrefab;
    //Родитель куда спавнить
    [SerializeField] private Transform parentToSpawn;
    [SerializeField] private Transform parentSupportToSpawn;

    public cardFilter cardFilter;
    //list для перезаписи текущего списка заспавненных карт
    public List<GameObject> listOfCardObjects;
    public List<GameObject> listOfCardSupportObjects;

    //Просто начальный спавн всех карт на сцене
    private void Start()
    {
        cardFilter.cards.Clear();
        for (int i = 0; i < cardObjects.Count; i++)
        {
            cardPrefab.GetComponent<CardDisplay>().card = cardObjects[i];
            GameObject card = Instantiate(cardPrefab, Vector3.zero, new Quaternion(0, 0, 0, 0), parentToSpawn);
            listOfCardObjects.Add(card);
            card.transform.localPosition = new Vector3(0, 0, 2);
        }
    }
    //Функция, которая и спавнит нужное кол-во карт
    //в зависимости от переданного list в кач-ве аргумента функции
    public void cardSpawn(List<Card> listOfCards)
    {
        //Уничтожение всех карт на сцене, чтобы отображались только нужные
        for (int i = 0; i < listOfCardObjects.Count; i++)
        {
            Destroy(listOfCardObjects[i]);
        }
        listOfCardObjects.Clear();
        //Создание нужных карточек
        for (int i = 0; i < listOfCards.Count; i++)
        {                   
            cardPrefab.GetComponent<CardDisplay>().card = listOfCards[i];
            GameObject card = Instantiate(cardPrefab, Vector3.zero, new Quaternion(0, 0, 0, 0), parentToSpawn);
            listOfCardObjects.Add(card);
            card.transform.localPosition = new Vector3(0, 0, 2);
        }
    }
    public void cardSupportSpawn(List<cardSupport> listOfCards)
    {
        //Уничтожение всех карт на сцене, чтобы отображались только нужные
        for (int i = 0; i < listOfCardSupportObjects.Count; i++)
        {
            Destroy(listOfCardSupportObjects[i]);
        }
        listOfCardSupportObjects.Clear();
        //Создание нужных карточек
        for (int i = 0; i < listOfCards.Count; i++)
        {
            cardSupportPrefab.GetComponent<cardSupportDisplay>().card = listOfCards[i];
            GameObject card = Instantiate(cardSupportPrefab, Vector3.zero, new Quaternion(0, 0, 0, 0), parentSupportToSpawn);
            listOfCardSupportObjects.Add(card);
            card.transform.localPosition = new Vector3(0, 0, 2);
        }
    }
}
