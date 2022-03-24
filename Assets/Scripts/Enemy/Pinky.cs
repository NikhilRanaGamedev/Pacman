using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinky : GhostAI
{
    Vector3 pacmanDir;

    new void Start()
    {
        target = new Vector3(pacman.position.x + 2f, pacman.position.y, pacman.position.z);
        pacmanDir = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().moveDirection;

        scatterPos = new Vector3(-12.5f, 16f, 0.0f);
        ghostColor = new Color(1f, 0.7176471f, 1f, 1f);
        myMonsterPen = new Vector3(0f, 1f, 0f);
        waitTimer = 2f;

        base.Start();
    }

    new void Update()
    {
        CalculateTarget();
        base.Update();
    }

    void CalculateTarget()
    {
        pacmanDir = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().moveDirection;

        if(pacmanDir == Vector3.left)
        {
            target = new Vector3(pacman.position.x - 2f, pacman.position.y, pacman.position.z);
        } else if(pacmanDir == Vector3.right)
        {
            target = new Vector3(pacman.position.x + 2f, pacman.position.y, pacman.position.z);
        } else if(pacmanDir == Vector3.up)
        {
            target = new Vector3(pacman.position.x - 2f, pacman.position.y + 2f, pacman.position.z);
        } else if(pacmanDir == Vector3.down)
        {
            target = new Vector3(pacman.position.x, pacman.position.y - 2f, pacman.position.z);
        }
    }
}
