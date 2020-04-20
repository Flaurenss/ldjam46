using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VampireGameEventRef : MonoBehaviour
{


    private EventsManager eventsManager;

    public GameObject prefabBloodCanvas;
    public GameObject prefabPillsCanvas;

    public Vector3[] offset = { new Vector3(0, 0), new Vector3(-0.25f, 0) };
    public bool [] flip = { false, true };
    public GameEvent [] linkedGameEvent = { null, null };
    private GameObject [] speechCanvas = { null, null };
    
    public int MaxGameSpeeches { get; private set; }
    void Awake()
    {

        eventsManager = GameObject.Find("EventsManager").GetComponent<EventsManager>();
        MaxGameSpeeches = 2;
        for (int i = 0; i<MaxGameSpeeches; i++)
        {
            linkedGameEvent[i] = null;
            speechCanvas[i] = null;
        }
    }

    public int EventCount
    {
        get
        {
            int c = 0;
            for (int i = 0; i < MaxGameSpeeches; i++)
            {
                if (linkedGameEvent[i] != null)
                    c++;
            }

            return c;
        }
    }

    public bool Full
    {
        get
        {
            for (int i = 0; i < MaxGameSpeeches; i++)
            {
                if (linkedGameEvent[i] == null)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public void AddEvent(GameEvent gameEvent)
    {
        for (int i = 0; i < MaxGameSpeeches; i++)
        {
            if (linkedGameEvent[i] == null)
            {
                linkedGameEvent[i] = gameEvent;
                gameEvent.gameUnityEvent.AddListener(OnEventCompleted);
                return;
            }
        }

        Debug.LogWarning("MaxGameSpeeches: "+this.MaxGameSpeeches+" .Event for this vampire is full");
    }



    void Update()
    {
        Debug.Log("[" + (linkedGameEvent[0] != null) + "," + (linkedGameEvent[1] != null) + "]");
        for (int i = 0; i< MaxGameSpeeches; i++)
        {
            if (linkedGameEvent[i] != null && speechCanvas[i] == null)
            {
                speechCanvas[i] = Instantiate(GetPrefabByType(i), this.transform, false);
                speechCanvas[i].GetComponent<Canvas>().worldCamera = Camera.main;
                speechCanvas[i].transform.localPosition = Vector3.zero + offset[i];

                if (flip[i])
                    speechCanvas[i].transform.localScale =
                        new Vector3(-speechCanvas[i].transform.localScale.x,
                        speechCanvas[i].transform.localScale.y);
            }

            if (linkedGameEvent[i] != null && linkedGameEvent[i].checkExpiration() == 0)
                linkedGameEvent[i] = null;

            if (linkedGameEvent[i] == null && speechCanvas != null)
            {
                Destroy(speechCanvas[i]);
                speechCanvas[i] = null;
            }
        }
    }

    private void OnEventCompleted(GameEvent gameEvent)
    {
        for (int i = 0; i < MaxGameSpeeches; i++)
        {
            if (linkedGameEvent[i] == gameEvent)
            {
                Debug.Log("Se ha completado un evento mio");
                linkedGameEvent[i].Completed = true;
                linkedGameEvent[i] = null;
            }
        }

    }
 
    private GameObject GetPrefabByType(int idx)
    {

        return  this.linkedGameEvent[idx].m_type == (int)ItemsSingleton.ItemType.BLOOD ?
            prefabBloodCanvas : 
                this.linkedGameEvent[idx].m_type == (int)ItemsSingleton.ItemType.PILL ?
            prefabPillsCanvas :
            null;
    }



}
