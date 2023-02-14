using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOnCards : MonoBehaviour
{
    //ѕри закрытии модального происходит включение всех карточек персонажей дл€ работы с ними
    public void turnOn()
    {
        for (int i = 0; i < GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardObjects.Count; i++)
        {
            GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardObjects[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardSupportObjects.Count; i++)
        {
            GameObject.Find("cardSpawner").GetComponent<cardSpawner>().listOfCardSupportObjects[i].gameObject.SetActive(true);
        }
    }
}
