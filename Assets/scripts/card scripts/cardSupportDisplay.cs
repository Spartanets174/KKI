using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardSupportDisplay : MonoBehaviour
{
    //Данный скрипт нужен для отображение полей карты помощи,
    //что хранятся scriptable objects на нужные элементы UI в юнити
    public cardSupport card;

    public Text name;
    public Text race;
    public Text type;
    public Image image;
    public Text ability;
    public Text rarity;
    public Text price;

    void Start()
    {
        name.text = card.name;
/*        race.text = card.race.ToString();
        type.text = card.type.ToString();*/
        image.sprite = card.image;
        ability.text = card.ability;
        rarity.text = card.rarity.ToString();
        rarity.color = new Color(255, 255, 255);
        if (card.rarity.ToString() == "Обычная")
        {
            rarity.transform.GetComponentInParent<Image>().color = new Color(125, 125, 125);
        }
        else
        {
            rarity.transform.GetComponentInParent<Image>().color = new Color(126, 0, 255);
        }
/*        price.text = card.Price.ToString();*/
    }
}
