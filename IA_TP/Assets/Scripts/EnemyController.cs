using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    
    QuestionNode _questionSight;
    QuestionNode _questionTimeToShoot;
    QuestionNode _questionRoom;
    ActionNode _actionIdle;
    ActionNode _actionShoot;
    ActionNode _actionMove;

    INode _init;
    
    FSM<string> finateStateMachine;
    IState<string> idle;
    IState<string> attack;
    IState<string> patrol;
    IState<string> chase;


    Player player;

    private Rigidbody myRigidbody;

    public Transform firepoint;
    public float dodgeStrenght;
    public float dodgeRadious;
    private WalkToPoints walkToPoints;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        walkToPoints = GetComponent<WalkToPoints>();
    }
    void Start()
    {
        player = FindObjectOfType<Player>();


        idle = new IA_Idle<string>(this);
        patrol = new IA_Patrol<string>(this, myRigidbody, walkToPoints);
        chase = new IA_Chase<string>(this, myRigidbody, dodgeStrenght, dodgeRadious);
        attack = new IA_Attack<string>(this, firepoint);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTree()
    {
        _init.Execute();
    }
}
