using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    public ItemsSingleton.ItemType CurrentType { get; set; }
    public GameObject CurrentObject { get; set; }

    public bool Empty
    {
        get { return CurrentType == ItemsSingleton.ItemType.NONE; }
    }

    public void Drop()
    {
        if (Empty) return;

        this.CurrentType = ItemsSingleton.ItemType.NONE;

        CurrentObject.transform.position = this.transform.position;
        CurrentObject.SetActive(true);
    }

    
}
