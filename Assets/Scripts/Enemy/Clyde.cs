using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clyde : GhostAI
{
    new void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        scatterPos = new Vector3(-12.5f, -16f, 0.0f);
        ghostColor = new Color(255f/255f, 183f/255f, 81f/255f);
        myMonsterPen = new Vector3(2f, 1f, 0f);
        waitTimer = 5f;

        base.Start();
    }

    new void Update()
    {
        CalculateTarget();
        base.Update();
    }

    void CalculateTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        float distanceX = target.x - transform.position.x;
        float distanceY = target.y - transform.position.y;
        float distance = distanceX * distanceX + distanceY * distanceY;

        if(distance < 64f)
          target = scatterPos;
    }

    new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position, 8f);
    }
}
