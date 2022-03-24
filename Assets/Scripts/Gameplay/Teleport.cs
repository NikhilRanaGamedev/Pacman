using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Transform teleportedObject;

    void OnTriggerEnter2D(Collider2D collider)
    {
        teleportedObject = collider.GetComponent<Transform>();
        teleportedObject.transform.position = new Vector3(-teleportedObject.transform.position.x, teleportedObject.transform.position.y, teleportedObject.transform.position.y);
    }
}
