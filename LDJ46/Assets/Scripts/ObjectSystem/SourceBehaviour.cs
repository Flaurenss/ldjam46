using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceBehaviour : MonoBehaviour
{

    public GameObject objectPrefab;
    public Vector2 direction;
    public Vector2 offset;
    public float intensity;
    public float maxItems;

    public void Dispense()
    {
        GameObject clone = Instantiate(objectPrefab, this.transform.position, Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().AddForce(direction.normalized + offset * intensity);
    }

}
