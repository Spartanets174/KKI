using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setActive : MonoBehaviour
{
    [SerializeField] private GameObject canvasInterface;
    [SerializeField] private GameObject blur;
    [SerializeField] private GameObject settingsObj;
    [SerializeField] private GameObject shopObj;
    [SerializeField] private Camera mainCamera;

    private Ray _ray;
    private RaycastHit _hit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = mainCamera.ScreenPointToRay(Input.mousePosition);    
            if (Physics.Raycast(_ray, out _hit, 1000f))
            {
                if (_hit.transform == transform)
                {
                    Debug.Log("da");
                    canvasInterface.SetActive(true);
                    blur.SetActive(true);
                    settingsObj.GetComponent<BoxCollider>().enabled = false;
                    shopObj.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }
}
