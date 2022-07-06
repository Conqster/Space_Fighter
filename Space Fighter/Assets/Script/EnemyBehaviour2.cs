using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceFighter;

public class EnemyBehaviour2 : Movement
{
    // enemy head rotates every 30 sec
    // rotates to seek for player 
    // if player in search light range, enemy attacks player 
    // focus on player until player is out of range

    //OnCollision with wall does a ray cast left and right to see a perfect direction to go 
    // goes toward the longest distance 

    // different state 
    // attack
    // patrol
    // seacrch


    [SerializeField] AiState aiState;

    private float Timer;
    [SerializeField] private float searchTimer = 30f;

    [SerializeField] private bool search;

    private Vector3 _tankHeadRotPos;

    [SerializeField, Range(1f,15f)] private float enviroDetec;
    private RaycastHit2D hit, checkLeft, checkRight, checkBack;
    public LayerMask _detectLayers;
    //private Vector2 newDirection;
    [SerializeField] private bool changeDirection;
    //[SerializeField] private ContactFilter2D ignoreLayer;
    List<Vector2> newDirection = new List<Vector2>();
    private Vector2 targetDir;


    protected override void Start()
    {
        base.Start();
       // _tankHeadTransform = GetComponent<Transform>();
        //aiState = AiState.Searching;
        //moveDir.y = 1;

    }


    protected override void Update()
    {
        base.Update();
        SetAiState();
        _tankHeadRotPos = _tankHeadTransform.rotation.eulerAngles;

        //SearchTimer();
        //print(Timer);

        //print(_tankHeadTransform.rotation.eulerAngles);

        //print(_tankHeadRotPos.z);

        Timer += Time.deltaTime;
        if(Timer > searchTimer)
        {
            search = true;

            //if(Timer > (searchTimer * 2))
            //{
            //    Timer = 0;
            //    search = false; 
            //}
        }

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        EnvironmentDetection();
    }

    protected override void GetInputs()
    {
        //MoveForward();
        //MoveBack();
       //RotLeft();
        //RotRight();

        //if (aiState == AiState.Searching)
        //{
        //    rotate.x = -1;
        //    moveDir = Vector2.zero;
        //}
        //if(aiState == AiState.Patroling)
        //{
        //    MoveForward();
        //    rotate.x = 0;
        //}
        //if(aiState == AiState.Turning)
        //{
        //    moveDir.y = 0;
        //    // move below to new method later
        //    // changeDirection()
        //    if (newDirection.x < transform.position.x)
        //        RotLeft();
        //    else if (newDirection.x > transform.position.x)
        //        RotRight();
        //}

        switch(aiState)
        {
            case AiState.Patroling:
                {
                    MoveForward();
                    rotate.x = 0;
                    break;
                }
            case AiState.Searching:
                {
                    rotate.x = -1;
                    moveDir = Vector2.zero;
                    break;
                }
            case AiState.Turning:
                {
                    moveDir.y = 0;
                    // move below to new method later
                    // changeDirection()
                    if (targetDir.x < transform.position.x)
                        RotLeft();
                    if (targetDir.x > transform.position.x)
                        RotRight();
                    //else if (newDirection.x > transform.position.x)
                    //    RotRight();
                    break;
                }
        }

    }


    private void EnvironmentDetection()
    {
        //hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity, _detectLayers);
         hit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity, _detectLayers);

        if(hit.point == targetDir)
        {
            changeDirection = false;
            Debug.Log("hit position: " + hit.point + "targetDir: " + targetDir);
            newDirection.Clear();
        }


        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            //print(hit.collider);
            //print(distance);

            if(distance < (enviroDetec/2))
            {
                CheckForDirection();
            }
        }
    }

    private void CheckForDirection()
    {
        checkRight = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, _detectLayers);
        checkLeft = Physics2D.Raycast(transform.position, -transform.right, Mathf.Infinity, _detectLayers);
        checkBack = Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, _detectLayers);

        //List<Vector2> newDirection = new List<Vector2>();
        //131
        //269


        // might still need modificatio because 
        // checkdirection cardinal points might depend on the orintation of the enemy in the scene
        float distanceLeft = Mathf.Abs(checkLeft.point.x - transform.position.x);
        float distanceRight = Mathf.Abs(checkRight.point.x - transform.position.x);
        float distanceBack = Mathf.Abs(checkBack.point.y - transform.position.y);


        if(distanceLeft > distanceRight)
        {
            if(newDirection.Count < 1)
            {
                newDirection.Add(checkLeft.point);
            }
        }
        else if(distanceLeft < distanceRight)
        {
            if (newDirection.Count < 1)
            {
                newDirection.Add(checkRight.point);
            }
        }
        //print(newDirection[1]);
        //if(distanceLeft > distanceRight)
        //{
        //    newDirection = checkLeft.point;
        //    print("left");
        //}
        //else
        //{
        //    newDirection = checkRight.point;
        //    print("right");
        //}

        //print(newDirection);

        targetDir = newDirection[0];
        //print(targetDir);

        changeDirection = true;
    }

    private void AiMovement()
    {

    }


    private void SetAiState()
    {
        if(search)
        {
            aiState = AiState.Searching;
        }
        
        if (search && (_tankHeadRotPos.z > 350 && _tankHeadRotPos.z < 360))
        {
            //print("This condition is met");
            search = false;
            aiState = AiState.Patroling;
            _tankHeadTransform.rotation = Quaternion.identity;
            Timer = 0;
        }

        if (changeDirection)
        {
            aiState = AiState.Turning;
            //search = false;
        }

    }/// <summary>
    /// potential bug search and turning bool 
    /// if both is true one might over shadow the other 
    /// might need a trigger to turn the other false when the first is active
    /// </summary>

    #region Direction

    private void CalMovement()
    {

    }

    private void MoveForward()
    {
        moveDir.y = 1;
    }

    private void MoveBack()
    {
        moveDir.y = -1;
    }

    private void RotLeft()
    {
        moveDir.x = -1;
    }

    private void RotRight()
    {
        moveDir.x = 1;
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector2(enviroDetec, enviroDetec));

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, hit.point);
        Gizmos.DrawLine(transform.position, checkLeft.point);
        Gizmos.DrawLine(transform.position, checkRight.point);
        Gizmos.DrawLine(transform.position, checkBack.point);

        Gizmos.color= Color.red;
        Gizmos.DrawLine(transform.position, targetDir);
    }


}
