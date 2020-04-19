using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireGameEventRef : MonoBehaviour
{
    private GameEvent linkedGameEvent = null;
    public GameEvent LinkedGameEvent 
    { 
        get { return linkedGameEvent; }
        set { linkedGameEvent = value; }
    }
    
    public bool HasGameEvent
    {
        get { return linkedGameEvent != null; }
    }

}
