using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPointsPrefab : MonoBehaviour
{
    void Update()
    {
        Destroy(gameObject, 0.01f);
    }
}
