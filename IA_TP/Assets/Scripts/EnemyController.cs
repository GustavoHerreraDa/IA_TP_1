using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    
    QuestionNode questionSightToAttack;
    QuestionNode questionSightToChase;
    QuestionNode questionTimeToShoot;
    QuestionNode questionPatrol;

    ActionNode actionIdle;
    ActionNode actionChase;
    ActionNode actionPatrol;
    ActionNode actionAttack;

    INode _init;
    
    FSM<string> finateStateMachine;
    IState<string> idle;
    IState<string> attack;
    IState<string> patrol;
    IState<string> chase;


    Player player;

    private Rigidbody myRigidbody;
    private LineOfSight lineOfSight;

    public Transform firepoint;
    public float dodgeStrenght;
    public float dodgeRadious;
    private WalkToPoints walkToPoints;


    //Shoot timer
    public float fireRate;
    public float shootTimer = 0;
    public bool shouldShoot;

    public Transform fireballOrigin;
    public GameObject fireball;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        walkToPoints = GetComponent<WalkToPoints>();
        lineOfSight = GetComponent<LineOfSight>();
    }
    void Start()
    {
        player = FindObjectOfType<Player>();


        idle = new IA_Idle<string>(this);
        patrol = new IA_Patrol<string>(this, myRigidbody, walkToPoints);
        chase = new IA_Chase<string>(this, myRigidbody, dodgeStrenght, dodgeRadious);
        attack = new IA_Attack<string>(this, firepoint);

        idle.AddTransition("Patrol", patrol);
        patrol.AddTransition("Chase", chase);
        chase.AddTransition("Attack", attack);
        attack.AddTransition("Chase", chase);

        finateStateMachine = new FSM<string>(idle);
        
        DecisionTree();

    }

    private void DecisionTree()
    {
        actionIdle = new ActionNode(GoIdle);
        actionAttack = new ActionNode(GoAttack);
        actionChase = new ActionNode(GoChase);
        actionPatrol = new ActionNode(GoPatrol);

        questionTimeToShoot = new QuestionNode(CheckTimeToFire, actionAttack, actionChase);
        questionSightToAttack = new QuestionNode(IsInSightToAttack,actionAttack, actionChase);
        questionSightToChase = new QuestionNode(IsInSightToChase, actionChase, actionPatrol);
        questionPatrol = new QuestionNode(CheckPatrol, questionSightToChase, actionIdle);

        _init = questionPatrol;

        StartTree();

    }

    // Update is called once per frame
    void Update()
    {
        finateStateMachine.OnUpdate();
    }

    public void StartTree()
    {
        _init.Execute();
    }

    public void Attack()
    {
        GameObject bulletInstance = Instantiate(fireball);
        bulletInstance.transform.forward = fireballOrigin.right;
        bulletInstance.transform.position = fireballOrigin.position;
        this.GetComponent<LifeCounter>().recibirDaño();
    }

    void GoIdle()
    {
        finateStateMachine.Transition("Idle");
    }

    void GoAttack()
    {
        finateStateMachine.Transition("Attack");
    }

    public void GoPatrol()
    {
        finateStateMachine.Transition("Patrol");
    }

    void GoChase()
    {
        finateStateMachine.Transition("Chase");
    }

    bool IsInSightToChase()
    {
        return lineOfSight.IsInSight(player.transform);
    }

    bool IsInSightToAttack()
    {
        Debug.Log("IsInSightToAttack!");

        return lineOfSight.IsInSight(player.transform);
    }

    public bool CheckTimeToFire()
    {
        return shouldShoot;
    }

    public bool CheckPatrol()
    {
        return true;
    }

    void ShootTimer()
    {
        if (fireRate >= shootTimer)
        {
            shootTimer += Time.deltaTime;
            shouldShoot = false;
        }
        else
        {
            shootTimer = 0;
            shouldShoot = true;
        }
    }


}
