using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{

    private GameObject currentObject;
    public GameObject CurrentObject
    {
        get { return currentObject;  }
        set 
        {
            if (Empty)
            {
                currentObject = value;
                currentObject.SetActive(false);
                //currentObject.transform.parent = this.gameObject.transform;
            }
            else
            {
                Debug.Log("Pocket is full");
            }
        }
    }


    public bool Empty
    {
        get { return currentObject == null; }
    }

    /// <summary>
    /// Drops the current object
    /// </summary>
    public void Drop()
    {
        if (Empty) return;

        this.currentObject.transform.position = this.gameObject.transform.position;
        this.currentObject.SetActive(true);
        this.currentObject = null;
    }

    
}
