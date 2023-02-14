using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class onHover : MonoBehaviour
{
    //Скрипт для появление внешней линии на 3d объекте и
    //чтобы нужный ui элемент следовал за мышкой при,
    //пока мышь находится в зоне этого 3d объекта
    //Применятеся в главном меню при наведении на гору денег и чернильницу
    [SerializeField] private GameObject caption;
    [SerializeField] private Camera cam;
    private Outline outline;
    

    // Update is called once per frame
    private void Start()
    {
        caption.SetActive(false);
        outline = GetComponent<Outline>();
    }
    private void Update()
    {
        caption.transform.localPosition = new Vector3(Input.mousePosition.x - cam.scaledPixelWidth / 2-80, Input.mousePosition.y - cam.scaledPixelHeight / 2+30, 0);
    }
    private void OnMouseEnter()
    {
        caption.SetActive(true);
        outline.enabled = true;
    }
    private void OnMouseExit()
    {
        caption.SetActive(false);
        outline.enabled = false;
    }
}
