using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventCheck : MonoBehaviour
{
    Pocket p;
    EventsManager eventsManager;
    public float minDistance = 2.0f;

    void Awake()
    {
        eventsManager = GameObject.Find("EventsManager").GetComponent<EventsManager>();
        p = GetComponent<Pocket>();
    }

    void Start()
    {
        
    } 

    void Update()
    {
        if (p.Empty) return;

        ItemsSingleton.ItemType pocketItem = p.CurrentType;

        var vampires = GameObject.FindGameObjectsWithTag("Vampire");
        foreach (var vampire in vampires)
        {
            float distance = Vector2.Distance(this.transform.position, vampire.transform.position);
            if (distance > minDistance) continue;

            bool check = eventsManager.checkEventCompletable((int)pocketItem, vampire.transform);
            if (check)
            {
                // Empty pocket
                Destroy(p.CurrentObject);
                p.CurrentObject = null;
                p.CurrentType = ItemsSingleton.ItemType.NONE;
                return;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, minDistance);
        
    }
}
