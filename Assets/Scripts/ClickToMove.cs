using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    public Grid grid;

    private Seeker seeker;
    private Path path;
    private int currentWaypoint = 0;
    private bool isMoving = false;

    //enum PlayerState { Idle, Moving, Attacking };
    //PlayerState status;

    SpriteRenderer spriteRenderer;

    //private int health;

    //private Animator anim;
    //private AudioSource audio;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        seeker = GetComponent<Seeker>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //health = GameManager.instance.playersHealth;
        //anim = GetComponent<Animator>();
        //audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        HandleInput();

    }

    public void HandleInput()
    {
        // If right clicking, play attack animation
        if (Input.GetMouseButtonDown(1))
        {
            //anim.SetTrigger("attack_1");
            //GameManager.instance.hasMoved = true;
        }

        // Check if its the players turn and they're not moving then move
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            //GameManager.instance.hasMoved = true;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);

            Vector3 targetPosition = grid.CellToWorld(gridPosition) 
                + new Vector3(0.5f, 0.5f, 0); // Assuming character's sprite is centered

            // Calculate the path using A* Pathfinding
            seeker.StartPath(transform.position, targetPosition, OnPathCalculated);
        }

        if (isMoving)
        {
            MoveAlongPath();
        }

        //GameManager.instance.hasMoved = false;
    }

    private void OnPathCalculated(Path p)
    {
        if (!p.error)
        {
            path = p;
            // Changed to 1 instead 0 because the first node in the path 
            // usually comes out wrong.
            currentWaypoint = 1;
            isMoving = true;
        }
    }

    private void MoveAlongPath()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            // Play idle animation
            //anim.Play("Player_Idle");


            // Reached the final destination
            isMoving = false;

            return;
        }

        MoveOneTile();
    }

    private void MoveOneTile()
    {
        // Play run animation
        //anim.Play("Player_Run");

        //anim.SetInteger("move", 1);
        // Move towards the next waypoint
        Vector3 targetPosition = path.vectorPath[currentWaypoint];
        //Debug.Log(targetPosition);
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Check if reached the current waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypoint++;
        }

        // Flip the sprite based on the movement direction
        if (direction.x < 0f)
        {
            spriteRenderer.flipX = true; // Flip the sprite horizontally
        }
        else if (direction.x > 0f)
        {
            spriteRenderer.flipX = false; // Reset the sprite flipping
        }

    }
}
