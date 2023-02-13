using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardSpawner : MonoBehaviour
{
    [SerializeField] private List<Card> cardObjects;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform parentToSpawn;
    public List<GameObject> cards;

    private void Start()
    {
        cards.Clear();
        for (int i = 0; i < cardObjects.Count; i++)
        {
            cardPrefab.GetComponent<CardDisplay>().card = cardObjects[i];
            GameObject card = Instantiate(cardPrefab, Vector3.zero,new Quaternion(0,0,0,0),parentToSpawn);
            cards.Add(card);
            card.transform.localPosition = new Vector3(0,0,2);
        }
        
    }
}
