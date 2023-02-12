using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardSpawner : MonoBehaviour
{
    [SerializeField] private List<Card> cardObjects;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform parentToSpawn;

    private void Start()
    {
        for (int i = 0; i < cardObjects.Count; i++)
        {
            cardPrefab.GetComponent<CardDisplay>().card = cardObjects[i];
            GameObject card = Instantiate(cardPrefab, Vector3.zero,new Quaternion(0,0,0,0),parentToSpawn);
            card.transform.localPosition = Vector3.zero;
        }
        
    }
}
