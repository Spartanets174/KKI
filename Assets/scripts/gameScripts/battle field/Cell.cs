using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //Класс для подсветки клетки игрового поля и покрашивание в шахматном порядке
    public Material baseColor, offsetColor, swampColor;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private GameObject hightLight;
    public bool Enabled=true;
    public bool isSwamp;
    private BattleSystem battleSystem;
    //Установление цвета клетки в зависимости от её четности или нечетности
    private void Start()
    {
        battleSystem = GameObject.Find("battleSystem").GetComponent<BattleSystem>();
    }
    public void Init(bool isOffset)
    {
        renderer.material = isOffset?offsetColor:baseColor;
    }
    //При клике на клетку выводятся её координаты и позиция в мире
    private void OnMouseDown()
    {
        //Проверка на то, включена ли клетка
        if (Enabled)
        {
            //Получение и вывод координат клетки
/*            Vector2 pos = new Vector2(transform.position.x, transform.position.z);
            GameObject.Find("field").GetComponent<Field>().GetCellAtPosition(pos);*/
            //Передача данных о текущей клетки в battleSystem            
            if (this.transform.childCount!=1)
            {
                if (this.transform.GetChild(1).GetComponent<character>().isEnemy)
                {
                    battleSystem.cahngeCardWindow(this.transform.GetChild(1).gameObject, true);                  
                }
                else
                {
                    battleSystem.cahngeCardWindow(this.transform.GetChild(1).gameObject, false);                   
                }
                
            }
            if (!battleSystem.isEnemyTurn)
            {
                battleSystem.OnMoveButton(this.gameObject);
            }        
        }        
    }
    private void OnMouseOver()
    {
        if (Enabled)
        {
            if (transform.childCount==1)
            {
                hightLight.SetActive(true);
            }           
        }
       
    }
    private void OnMouseExit()
    {
        if (Enabled)
        {
            hightLight.SetActive(false);
        }        
    }
}
