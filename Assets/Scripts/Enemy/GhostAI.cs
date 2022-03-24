using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour, Ghost
{
    Animator anim;
    Grid grid;

    protected Transform pacman;
    public Vector3 target;
    protected Vector3 scatterPos;
    public Vector3 direction = Vector3.right;
    Vector3 targetDestination;

    protected float speed;
    float defSpeed;
    float frightTime;
    float frightSpeed;
    float frightFlashes;
    float tunnelSpeed;

    int chaseTime;
    int scatterTime;

    public bool eaten = false;
    bool isMoving = false;

    public float timer = 0f;
    int scatterAmount = 4;
    protected float waitTimer;

    public string mode = "spawn";

    protected Color ghostColor;

    Vector3 monsterPenEntrance = new Vector3(-0.5f, 4f, 0f);
    Vector3 monsterPen = new Vector3(0f, 1f, 0f);
    public Vector3 myMonsterPen;

    public string modeToBe;

    void Awake()
    {
        anim = GetComponent<Animator>();
        grid = GameObject.Find("GridNode").GetComponent<Grid>();
        pacman = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    protected void Start()
    {
        defSpeed = LevelManager.ghostSpeed;
        frightTime = LevelManager.ghostFrightTime;
        frightSpeed = LevelManager.ghostFrightSpeed;
        frightFlashes = LevelManager.ghostFlashes * 0.4f;
        tunnelSpeed = LevelManager.ghostTunnelSpeed;

        chaseTime = LevelManager.chase[0];
        scatterTime = LevelManager.scatter[0];

        speed = defSpeed;
    }

    protected void Update()
    {
        switch(mode)
        {
            case "chase":
                ChaseMode();
                break;

            case "scatter":
                ScatterMode();
                break;

            case "scared":
                ScaredMode();
                break;

            case "eaten":
                EatenMode();
                break;

            case "spawn":
                SpawnMode();
                break;

            case "turn":
                Turn(modeToBe);
                break;
        }
    }

    public void ChaseMode()
    {
        Move(target);
        Timer();

        if(timer > chaseTime && scatterAmount != 0)
        {
            scatterAmount--;

            if (scatterAmount == 3)
            {
                chaseTime = LevelManager.chase[1];
                scatterTime = LevelManager.scatter[1];
            }
            else if (scatterAmount == 2)
            {
                chaseTime = LevelManager.chase[2];
                scatterTime = LevelManager.scatter[2];
            }
            if (scatterAmount == 1)
            {
                chaseTime = LevelManager.chase[3];
                scatterTime = LevelManager.scatter[3];
            }

            TurnCalculation("scatter");
        }
    }

    public void ScatterMode()
    {
        Move(scatterPos);
        Timer();

        if(timer > scatterTime)
        {
            TurnCalculation("chase");
        }

    }

    public void ScaredMode()
    {
        Timer();

        List<Vector3> randomDirections = new List<Vector3>
        {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right
        };

        int randomNumber = Random.Range(0, 3);

        Move(randomDirections[randomNumber]);
        speed = frightSpeed;

        if(eaten)
        {
            speed *= 2.5f;
            timer = 0f;
            mode = "eaten";
        }

        if(timer < frightTime)
          anim.Play("Frightened");

        if(timer > frightTime && timer < frightTime + frightFlashes)
          anim.Play("Frightened_Flash");

        if(timer > frightTime + frightFlashes)
        {
            speed = 7.5f;
            timer = 0f;
            mode = "chase";
            anim.Play("Idle");
        }
    }

    string entrance = "moving";
    public void EatenMode()
    {
        if (transform.position != monsterPenEntrance && entrance == "moving")
        {
            anim.Play("Eaten");
            Move(monsterPenEntrance);

            if (transform.position == monsterPenEntrance)
            {
                entrance = "home";
            }
        }
        else if (transform.position != monsterPen && entrance == "home")
        {
            transform.position = Vector3.MoveTowards(transform.position, monsterPen, speed * Time.deltaTime);

            if (transform.position == monsterPen)
            {
                entrance = "homePos";
            }
        }
        else if (transform.position.y == 1f && entrance == "homePos")
        {
            transform.position = Vector3.MoveTowards(transform.position, myMonsterPen, speed * Time.deltaTime);

            if (transform.position == myMonsterPen)
            {
                entrance = "reached";
                anim.Play("Idle");
            }
        }
        else if (transform.position == myMonsterPen)
        {
            Timer();

            if (timer > waitTimer && transform.position.y == 1f)
            {
                speed = 7.5f;
                direction = Vector3.right;
                eaten = false;
                entrance = "moving";
                mode = "spawn";
            }
        }
    }

    public void SpawnMode()
    {
        anim.Play("Idle");

        if(transform.position.y == 1f && transform.position != new Vector3(0f, 1f, 0f))
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, 1f, 0f), speed * Time.deltaTime);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, 4f, 0f), speed * Time.deltaTime);

            if(transform.position == new Vector3(0f, 4f, 0f))
              mode = "scatter";
        }
    }

    void Move(Vector3 targetPos)
    {
        float lastPositionX = transform.position.x;
        float lastPositionY = transform.position.y;

        if(!isMoving)
        {
            targetDestination = CheckMoveDirection(targetPos);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetDestination, speed * Time.deltaTime);

            if(transform.position == targetDestination)
            {
                isMoving = false;
            }
        }

        AnimateMovement(lastPositionX, lastPositionY);
    }

    void Turn(string modeTo)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetDestination, speed * Time.deltaTime);

        if(transform.position == targetDestination)
        {
            timer = 0.0f;
            mode = modeTo;
        }
    }

    public void TurnCalculation(string modeTo)
    {
        CalculateTurn();
        modeToBe = modeTo;
        mode = "turn";
    }

    public void CalculateTurn()
    {
        Node ghost = grid.NodeFromWorldPoint(transform.position);
        List<Node> neighbours = grid.GetNeighbours(ghost);

        if (direction == Vector3.up && neighbours[1].walkable)
        {
            targetDestination = neighbours[1].position;
            direction = Vector3.down;
        }
        else if (direction == Vector3.left && neighbours[3].walkable)
        {
            targetDestination = neighbours[3].position;
            direction = Vector3.right;
        }
        else if (direction == Vector3.down && neighbours[2].walkable)
        {
            targetDestination = neighbours[2].position;
            direction = Vector3.up;
        }
        else if (direction == Vector3.right && neighbours[0].walkable)
        {
            targetDestination = neighbours[0].position;
            direction = Vector3.left;
        }
    }

    Vector3 CheckMoveDirection(Vector3 targetPos)
    {
        Node ghost = grid.NodeFromWorldPoint(transform.position);
        List<Node> neighbours = grid.GetNeighbours(ghost); // right, up, down, left
        List<float> dirCost = new List<float>();

        for(int i = 0; i < neighbours.Count; i++)
        {
            float distanceX = targetPos.x - neighbours[i].position.x;
            float distanceY = targetPos.y - neighbours[i].position.y;
            float distance = distanceX * distanceX + distanceY * distanceY;

            dirCost.Add(distance);
        }

        float[] lowest = new float[4];
        for(int i = 0; i < neighbours.Count; i++)
        {
            if(neighbours[i].walkable)
            {
                if((direction == Vector3.right && i == 0) ||
                   (direction == Vector3.up && i == 1) ||
                   (direction == Vector3.down && i == 2) ||
                   (direction == Vector3.left && i == 3))
                {
                    lowest[i] = 2000f;
                } else
                {
                    lowest[i] = dirCost[i];
                }
            } else
            {
                lowest[i] = 2000f;
            }
        }

        float lowestDirCost = Mathf.Min(lowest);
        int directionIndex = 0;

        for(int i = 0; i < dirCost.Count; i++) // priorty up, left, down, right
        {
            if(lowest[i] == lowestDirCost)
            {
                directionIndex = i;

                if(i == 0)
                {
                    direction = Vector3.left;
                } else if(i == 1)
                {
                    direction = Vector3.down;
                } else if(i == 2)
                {
                    direction = Vector3.up;
                } else if(i == 3)
                {
                    direction = Vector3.right;
                }
            }
        }

        if(directionIndex != 1 && lowest[1] == lowestDirCost)
        {
            directionIndex = 1;
        } else if(directionIndex != 1 && directionIndex != 3 && lowest[3] == lowestDirCost)
        {
            directionIndex = 3;
        } else if(directionIndex != 1 && directionIndex != 3 && directionIndex != 2 && lowest[2] == lowestDirCost)
        {
            directionIndex = 2;
        }

        isMoving = true;
        return neighbours[directionIndex].position;
    }

    void AnimateMovement(float xPos, float yPos)
    {
        float lastPositionX = transform.position.x - xPos;
        float lastPositionY = transform.position.y - yPos;

        if(lastPositionX > 0)
        {
            if(mode == "chase" || mode == "scatter" || mode == "spawn")
              anim.Play("Move_Side");

            transform.localScale = new Vector3(1f, 1f, 1f);
        } else if(lastPositionX < 0)
        {
            if(mode == "chase" || mode == "scatter" || mode == "spawn")
              anim.Play("Move_Side");

            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if(lastPositionY > 0)
        {
            if(mode == "chase" || mode == "scatter" || mode == "spawn")
              anim.Play("Move_Up");
        } else if(lastPositionY < 0)
        {
            if(mode == "chase" || mode == "scatter" || mode == "spawn")
              anim.Play("Move_Down");
        }
    }

    void Timer()
    {
        timer += Time.deltaTime;
        timer %= 60f;
    }

    public void Reset()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        mode = "spawn";
        timer = 0f;
        speed = 7.5f;
        direction = Vector3.right;
        isMoving = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && mode == "scared")
        {
            eaten = true;
        }

        if (collider.CompareTag("Tunnel") && (mode == "chase" || mode == "scatter"))
        {
            speed = tunnelSpeed;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Teleport"))
        {
            isMoving = false;
        }

        if (collider.CompareTag("Tunnel") && (mode == "chase" || mode == "scatter"))
        {
            speed = defSpeed;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = ghostColor;
        Gizmos.DrawCube(target, new Vector2(0.5f, 0.5f));
        // Gizmos.DrawCube(scatterPos, new Vector2(0.5f, 0.5f));
    }
}
