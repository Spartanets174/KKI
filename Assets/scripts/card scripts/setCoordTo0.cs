using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCoordTo0 : MonoBehaviour
{
   public GameObject Object;
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
    public void setCoord1000()
    {
        Object.transform.localPosition = new Vector3(Object.transform.position.x, Object.transform.position.y, 1000);
    }
}
