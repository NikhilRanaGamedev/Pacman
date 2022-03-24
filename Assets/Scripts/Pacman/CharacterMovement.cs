using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Animator anim;
    public Grid grid;

    public float speed;
    float defSpeed;
    float frightSpeed;

    public Vector3 moveDirection = Vector3.left;

    public Node playerNode;
    public List<Node> neighbours = new List<Node>();

    bool pause = false;

    void Awake()
    {
        grid = GameObject.Find("GridNode").GetComponent<Grid>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        playerNode = grid.NodeFromWorldPoint(transform.position);
        neighbours = grid.GetNeighbours(playerNode);

        defSpeed = LevelManager.pacmanSpeed;
        frightSpeed = LevelManager.pacmanAfterBigFoodSpeed;

        speed = defSpeed;
    }

    void Update()
    {
        if(!GetComponent<Dead>().dead && !pause)
        {
            MoveInput();
            CheckIfCanMove();
        }
    }

    void MoveInput()
    {
        playerNode = grid.NodeFromWorldPoint(transform.position);

        neighbours = grid.GetNeighbours(playerNode);

        if (Input.GetKey("left") && neighbours[3].walkable)
        {
            if(moveDirection == Vector3.up)
            {
                if(transform.position.y <= playerNode.position.y)
                {
                    moveDirection = Vector3.left;
                }
            }
            else if (moveDirection == Vector3.down)
            {
                if (transform.position.y >= playerNode.position.y)
                {
                    moveDirection = Vector3.left;
                }
            }
            else if(moveDirection == Vector3.right)
            {
                moveDirection = Vector3.left;
            }
        }
        else if (Input.GetKey("right") && neighbours[0].walkable)
        {
            if (moveDirection == Vector3.up)
            {
                if (transform.position.y <= playerNode.position.y)
                {
                    moveDirection = Vector3.right;
                }
            }
            else if (moveDirection == Vector3.down)
            {
                if (transform.position.y >= playerNode.position.y)
                {
                    moveDirection = Vector3.right;
                }
            }
            else if (moveDirection == Vector3.left)
            {
                moveDirection = Vector3.right;
            }
        }
        else if (Input.GetKey("up") && neighbours[1].walkable)
        {
            if (moveDirection == Vector3.left)
            {
                if (transform.position.x >= playerNode.position.x)
                {
                    moveDirection = Vector3.up;
                }
            }
            else if (moveDirection == Vector3.right)
            {
                if (transform.position.x <= playerNode.position.x)
                {
                    moveDirection = Vector3.up;
                }
            }
            else if (moveDirection == Vector3.down)
            {
                moveDirection = Vector3.up;
            }
        }
        else if (Input.GetKey("down") && neighbours[2].walkable)
        {
            if ((neighbours[2].gridX == 13 && neighbours[2].gridY == 18) || (neighbours[2].gridX == 14 && neighbours[2].gridY == 18)) // Check if downward node is Monster Pen Entrance
                return;

            if (moveDirection == Vector3.left)
            {
                if (transform.position.x >= playerNode.position.x)
                {
                    moveDirection = Vector3.down;
                }
            }
            else if (moveDirection == Vector3.right)
            {
                if (transform.position.x <= playerNode.position.x)
                {
                    moveDirection = Vector3.down;
                }
            }
            else if (moveDirection == Vector3.up)
            {
                moveDirection = Vector3.down;
            }
        }
    }

    void CheckIfCanMove()
    {
        if(moveDirection == Vector3.left && neighbours[3].walkable)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Move(neighbours[3].position);
        } else if(moveDirection == Vector3.right && neighbours[0].walkable)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 180f);
            Move(neighbours[0].position);
        } else if(moveDirection == Vector3.up && neighbours[1].walkable)
        {
            transform.eulerAngles = new Vector3(0f, 0f, -90f);
            Move(neighbours[1].position);
        } else if(moveDirection == Vector3.down && neighbours[2].walkable)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 90f);
            Move(neighbours[2].position);
        }

        if (moveDirection == Vector3.left && !neighbours[3].walkable ||
            moveDirection == Vector3.right && !neighbours[0].walkable ||
            moveDirection == Vector3.up && !neighbours[1].walkable ||
            moveDirection == Vector3.down && !neighbours[2].walkable)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerNode.position, speed * Time.deltaTime);
            
            if(transform.position == playerNode.position)
            {
                anim.Play("Idle");
            }
        }
    }

    void Move(Vector3 pos)
    {
        anim.Play("Move");
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Food"))
        {
            StartCoroutine(PauseOnEating(1/60));
        }

        if (collider.CompareTag("BigFood"))
        {
            FindObjectOfType<AudioManager>().Play("eatPowerPellet");
            StartCoroutine(PauseOnEating(3/60));

            if(LevelManager.level < 21)
            {
                StartCoroutine(RevertDefSpeed());
            }
        }
    }

    IEnumerator PauseOnEating(int sec)
    {
        pause = true;
        yield return new WaitForSeconds(sec);
        pause = false;
    }

    IEnumerator RevertDefSpeed()
    {
        yield return new WaitForSeconds(LevelManager.ghostFrightTime);
        speed = defSpeed;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Teleport"))
        {
            neighbours = grid.GetNeighbours(playerNode);
        }
    }
}
