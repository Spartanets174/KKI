using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //Класс для подсветки клетки игрового поля и покрашивание в шахматном порядке
    [SerializeField] private Material baseColor, offsetColor;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private GameObject hightLight;
    
   public void Init(bool isOffset)
    {
        renderer.material = isOffset?offsetColor:baseColor;
    }
    //При клике на клетку выводятся её координаты и позиция в мире
    private void OnMouseDown()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.z);
        Debug.Log("f");
        GameObject.Find("field").GetComponent<Field>().GetCellAtPosition(pos);
    }
    private void OnMouseOver()
    {
        hightLight.SetActive(true);
    }
    private void OnMouseExit()
    {
        hightLight.SetActive(false);
    }
}
