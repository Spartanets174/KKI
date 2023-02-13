using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOnCards : MonoBehaviour
{
    public void turnOn()
    {
        for (int i = 0; i < GameObject.Find("cardSpawner").GetComponent<cardSpawner>().cards.Count; i++)
        {
            GameObject.Find("cardSpawner").GetComponent<cardSpawner>().cards[i].gameObject.SetActive(true);
        }
    }
}
