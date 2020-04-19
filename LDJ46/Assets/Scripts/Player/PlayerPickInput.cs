using System.Collections.Generic;
using UnityEngine;

public class PlayerPickInput : MonoBehaviour
{

    public KeyCode dropKey;
    public KeyCode interactKey;
    public float minPickDistance = 2.0f;

    private Pocket pocket;
    private GameObject closestInteractuable;

    void Awake()
    {
        pocket = GetComponent<Pocket>();
    }

    // Update is called once per frame
    void Update()
    {
        closestInteractuable = GetClosestInteractuable();
        if (closestInteractuable != null)
        {
            var script = this.closestInteractuable.GetComponent<KeyHintBehaviour>();
            if (script == null)
            {
                Debug.LogWarning(closestInteractuable.name + " do not has a KeyHintBehaviour");
            }

             script.SetKeyHint(pocket.Empty);
        }
        
        

        if (Input.GetKeyDown(interactKey) && closestInteractuable != null)
        {
            if (closestInteractuable.CompareTag("Pickable"))
                pocket.CurrentObject = closestInteractuable;
            else if (closestInteractuable.CompareTag("Source") && pocket.Empty)
                closestInteractuable.GetComponent<SourceBehaviour>().Dispense();
                
            return;
        }


        if (Input.GetKeyDown(dropKey))
        {
            if (!pocket.Empty)
            {
                Debug.Log("Droping object");
                pocket.Drop();
            }
            else
            {
                Debug.Log("Pocket is empty");
            }
        }

    }

    void OnDrawGizmos()
    {
        if (closestInteractuable != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(closestInteractuable.transform.position, minPickDistance);
        }
    }

    private GameObject GetClosestInteractuable()
    {
        List<GameObject> items = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pickable"));
        items.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Source")));

        return ClosestInRange(ref items);
    }


    private GameObject ClosestInRange(ref List<GameObject> pickables)
    {
        GameObject closest = null;
        float closestDist = this.minPickDistance;

        foreach (GameObject o in pickables)
        {
            o.GetComponent<KeyHintBehaviour>().SetKeyHint(false);
            float distance = Vector2.Distance(this.transform.position, o.transform.position);
            if ( distance <= closestDist)
            {
                closest = o;
                closestDist = distance;
            }
        }

        return closest;
    }

}
