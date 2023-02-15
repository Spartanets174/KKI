using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alWindowOpen : MonoBehaviour
{
    /*������ ����� ��� ����, ����� �������� ������ ������ �
     * ��������� ���� ��� ��������, �� ������� �� �������*/
    public bool isOpen = false;
    public CardDisplay cardDisplay;
    public cardSupportDisplay CardSupportDisplay;
    void OnMouseUp()
    {
        isOpen = true;
        if (this.GetComponent<CardDisplay>()!=null)
        {
            GameObject.Find("blur modal").GetComponent<setCoordTo0>().setCoord0();
            GameObject.Find("buy card").GetComponent<setCoordTo0>().setCoord0();
            GameObject.Find("buy card").transform.GetChild(1).GetComponent<Image>().sprite = cardDisplay.card.image;
            GameObject.Find("ability").GetComponent<Text>().text = $"��������� �����������: {cardDisplay.card.attackAbility}" + "\n" + "\n" +
                $"�������� �����������: {cardDisplay.card.defenceAbility}" + "\n" + "\n" +
                $"����������� �����������: {cardDisplay.card.buffAbility}" + "\n" + "\n" +
                $"��������� �����������: {cardDisplay.card.passiveAbility}";
            GameObject.Find("price text").GetComponent<Text>().text = $"����: {cardDisplay.card.Price}$";

            /* ���������� ���� ��������, ����� �� ��� ������ ���� �������� ������ ��������� ����*/
            for (int i = 0; i < GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardObjects.Count; i++)
            {
                GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardObjects[i].gameObject.SetActive(false);
            }
        }
        if (this.GetComponent<cardSupportDisplay>() != null)
        {
            GameObject.Find("blur modal").GetComponent<setCoordTo0>().setCoord0();
            GameObject.Find("buy card").GetComponent<setCoordTo0>().setCoord0();
            GameObject.Find("buy card").transform.GetChild(1).GetComponent<Image>().sprite = CardSupportDisplay.card.image;
            GameObject.Find("ability").GetComponent<Text>().text = $"�����������: {CardSupportDisplay.card.ability}";
            GameObject.Find("price text").GetComponent<Text>().text = $"����: {CardSupportDisplay.card.Price}$";

            /* ���������� ���� ��������, ����� �� ��� ������ ���� �������� ������ ��������� ����*/
            for (int i = 0; i < GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardSupportObjects.Count; i++)
            {
                GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardSupportObjects[i].gameObject.SetActive(false);
            }
        }
    }
}
