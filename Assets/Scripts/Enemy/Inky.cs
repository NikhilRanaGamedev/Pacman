using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inky : GhostAI
{
    Vector3 targetBlinky;

    new void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        scatterPos = new Vector3(12.5f, -16f, 0.0f);
        ghostColor = Color.cyan;
        myMonsterPen = new Vector3(-2f, 1f, 0f);
        waitTimer = 3f;

        base.Start();
    }

    new void Update()
    {
        CalculateTarget();
        base.Update();
    }

    void CalculateTarget()
    {
        targetBlinky = GameObject.Find("Blinky").GetComponent<Transform>().position;
        Vector3 targetPinky = GameObject.Find("Pinky").GetComponent<Pinky>().target;

        float distanceX = targetPinky.x - targetBlinky.x;
        float distanceY = targetPinky.y - targetBlinky.y;

        target = new Vector3(targetPinky.x + distanceX, targetPinky.y + distanceY, 0f);
    }

    new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(targetBlinky, target);
    }
}
