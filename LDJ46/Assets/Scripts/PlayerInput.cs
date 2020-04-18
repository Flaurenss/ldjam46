using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public KeyCode dropKey;
    public KeyCode pickKey;
    public float minPickDistance = 2.0f;


    private Pocket pocket;
    private GameObject closestPickable;

    void Awake()
    {
        pocket = GetComponent<Pocket>();
    }

    // Update is called once per frame
    void Update()
    {
        closestPickable = GetClosestPickableInRange();
        if (closestPickable != null)
            this.closestPickable.GetComponent<PickableBehaviour>().SetKeyHint(pocket.Empty);
        
        

        if (Input.GetKeyDown(pickKey))
        {
            if (closestPickable != null)
            {
                Debug.Log("Picking up object");
                pocket.CurrentObject = closestPickable;
            }
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
        if (closestPickable != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(closestPickable.transform.position, minPickDistance);
        }
    }

    private GameObject GetClosestPickableInRange()
    {
        var pickables = GameObject.FindGameObjectsWithTag("Pickable");

        if (pickables.Length == 0) return null;

        return closestGameObjectInRange(ref pickables);
    }


    private GameObject closestGameObjectInRange(ref GameObject [] gameObjects)
    {
        GameObject closest = null;
        float closestDist = this.minPickDistance;

        foreach (GameObject o in gameObjects)
        {
            o.GetComponent<PickableBehaviour>().SetKeyHint(false);
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
