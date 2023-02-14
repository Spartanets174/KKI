using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ��� �������� ������ �������� �� �����
public class cardSpawner : MonoBehaviour
{
    //������ scriptable objects, ������� ���� ��������
    public List<Card> cardObjects;
    public List<cardSupport> cardSupportObjects;
    //������, ������� ��������� � ������ scriptable object
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject cardSupportPrefab;
    //�������� ���� ��������
    [SerializeField] private Transform parentToSpawn;
    [SerializeField] private Transform parentSupportToSpawn;

    public cardFilter cardFilter;
    //list ��� ���������� �������� ������ ������������ ����
    public List<GameObject> listOfCardObjects;
    public List<GameObject> listOfCardSupportObjects;

    //������ ��������� ����� ���� ���� �� �����
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
    //�������, ������� � ������� ������ ���-�� ����
    //� ����������� �� ����������� list � ���-�� ��������� �������
    public void cardSpawn(List<Card> listOfCards)
    {
        //����������� ���� ���� �� �����, ����� ������������ ������ ������
        for (int i = 0; i < listOfCardObjects.Count; i++)
        {
            Destroy(listOfCardObjects[i]);
        }
        listOfCardObjects.Clear();
        //�������� ������ ��������
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
        //����������� ���� ���� �� �����, ����� ������������ ������ ������
        for (int i = 0; i < listOfCardSupportObjects.Count; i++)
        {
            Destroy(listOfCardSupportObjects[i]);
        }
        listOfCardSupportObjects.Clear();
        //�������� ������ ��������
        for (int i = 0; i < listOfCards.Count; i++)
        {
            cardSupportPrefab.GetComponent<cardSupportDisplay>().card = listOfCards[i];
            GameObject card = Instantiate(cardSupportPrefab, Vector3.zero, new Quaternion(0, 0, 0, 0), parentSupportToSpawn);
            listOfCardSupportObjects.Add(card);
            card.transform.localPosition = new Vector3(0, 0, 2);
        }
    }
}
