using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSingleton
{
    public enum ItemType
    {
        NONE = 0,
        BLOOD = 1,
        PILL = 2
    }

    private static ItemsSingleton instance;

    public static ItemsSingleton Instance { 
        get 
        {
            if (instance == null)
                instance = new ItemsSingleton();

            return instance; 
        }

        private set { instance = value; }
    }

    private ItemsSingleton()
    {

        instance = this;
    }



}
