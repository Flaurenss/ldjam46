using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceBehaviour : MonoBehaviour
{

    public ItemsSingleton.ItemType objectType;
    public GameObject prefab;

    public float rechargingSeconds = 5.0f;
    private float rechargingTimer = 0.0f;
    private bool objectAvailable;
    private bool recharging;
    private Animator animator;

    public bool InteractAvailable
    {
        get { return objectAvailable || (!objectAvailable && !recharging); }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        
    }


    void Start()
    {
        recharging = false;
        rechargingTimer = 0.0f;
    }

    void Update()
    {

        if (recharging)
        {
            rechargingTimer += Time.deltaTime;
            if (rechargingTimer >= rechargingSeconds)
            {
                Debug.Log("Object available");
                objectAvailable = true;
                recharging = false;

              

                rechargingTimer = 0.0f;
            }
        }

       
    
    }

    

    public void Interact(ref Pocket p)
    {
        if (objectAvailable)
        {
            if (!p.Empty) return;

            Dispense(ref p);
            objectAvailable = false;
            recharging = false;
        }
        else if (!recharging)
        {
            if (animator != null)
            {
                animator.SetTrigger("StartCharging");
                animator.speed = 1.0f / rechargingSeconds;
            }
            recharging = true;
        }

    }



    private void Dispense(ref Pocket p)
    {
        if (animator != null)
        {
            animator.SetTrigger("Dispense");
            animator.speed = 1.0f;
        }

        GameObject clone = Instantiate(prefab, p.transform.position, Quaternion.identity);
        p.CurrentType = objectType;
        p.CurrentObject = clone;
        clone.SetActive(false);
    }

}
