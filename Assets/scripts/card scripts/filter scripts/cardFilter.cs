using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class cardFilter : MonoBehaviour
{
    /*�������� ����� ��� ���������� �������� �� ��������� ����������*/
    public string cardRace = "";
    public string cardClass = "";
    public string cardSupportType = "";
    public List<Card> cards;
    public List<cardSupport> supportCards;
    public List<GameObject> raceButtons;
    public List<GameObject> classButtons;
    public List<GameObject> supportButtons;
    public cardSpawner cardSpawner;
    public PlayerManager1 playerManager1;

    /*������ � list "cards" ���� scriptable objects ��� ���������� ��� ���������� */
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

   /* ������� ��� ����������, ������� ��������� 2 string ��������: ���� � ����� �����, 
    * �� ������� � ���������� ���������� list c �������*/
    public void cardFiltration()
    {
        if (cardSpawner.isShop)
        {
            cards = playerManager1.allCharCards;
        }
        else
        {
            cards = playerManager1.allUserCharCards;
        }
       
        //���� ������ �� �������
        if (cardRace == "" && cardClass == "")
        {
            cardSpawner.cardSpawn(cards);
        }
        //���� ������ ������ �����
        if (cardRace == "" && cardClass != "")
        {
            List<Card> onlyClass = cards.Where(x=>x.Class.ToString() == cardClass).ToList();
            cardSpawner.cardSpawn(onlyClass);
        }
        //���� ������� ������ ����
        if (cardRace != "" && cardClass == "")
        {
            List<Card> onlyRace = cards.Where(x => x.race.ToString() == cardRace).ToList();          
            cardSpawner.cardSpawn(onlyRace);
        }
        //���� ������� ��� ���������
        if (cardRace != "" && cardClass != "")
        {
            List<Card> raceAndClass = cards.Where(x => x.race.ToString() == cardRace&& x.Class.ToString() == cardClass).ToList();
            cardSpawner.cardSpawn(raceAndClass);
        }
    }
    //�� �� ����� �������, ��� � ����, �� ��� ���� ������ (���� ������ ����)
    public void cardSupportFiltration()
    {
        if (cardSpawner.isShop)
        {
            supportCards = playerManager1.allSupportCards;
        }
        else
        {           
            supportCards = playerManager1.allUserSupportCards;
        }
        if (cardSupportType == "")
        {   
            cardSpawner.cardSupportSpawn(supportCards);
        }
        if (cardSupportType != "")
        {
            List<cardSupport> type = supportCards.Where(x => x.type.ToString() == cardSupportType).ToList();
            cardSpawner.cardSupportSpawn(type);
        }
    }
}
