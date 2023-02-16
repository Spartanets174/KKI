using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCoordTo0 : MonoBehaviour
{
  //Костыль для отображения модального окна с информацией о карте при покупке
  //Тут проблема в точ, что префабы с карточками должны запускать и отключать модальное окно
  //с информацие о карточке, но на префабы нельзя делать ссылки на объекты в юнити,
  //поэтому  был придуман данный костыль и с тупым изменением координат модального окна
   public GameObject Object;
    //Устанавливает координаты блюра и модального окна в 0, чтобы его было видно
    public void setCoord0()
    {
        if (Object.name== "blur modal")
        {
            Object.transform.localPosition = new Vector3(Object.transform.position.x, Object.transform.position.y, 1f);
        }
        else
        {
            Object.transform.localPosition = new Vector3(Object.transform.position.x, Object.transform.position.y, 0);
        }
        
    }
    //Устанавливает координаты блюра и модального окна в 1000, чтобы его не было видно
    public void setCoord1000()
    {
        Object.transform.localPosition = new Vector3(Object.transform.position.x, Object.transform.position.y, 1000);
    }
}
