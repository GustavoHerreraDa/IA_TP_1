using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Chase<T> : IState<T>
{
    Dictionary<T, IState<T>> dictionaryState = new Dictionary<T, IState<T>>();
    EnemyController enemy;
    Rigidbody enemyRigidbody;
    Transform playerTransform;
    //Seek seek;
    float dodgeStrenght = 10;
    float dodgeRadius = 20;
    Vector3 dir;
    float speed = 2;
    Seek seek;

    private float RotationSpeed = 2.0f;


    public IA_Chase(EnemyController enemy, Rigidbody rb, float ds = 10, float dr = 20, float s = 4)
    {
        this.enemy = enemy;
        enemyRigidbody = rb;
        dodgeStrenght = ds;
        dodgeRadius = dr;
        speed = s;
    }

    public void Awake()
    {
        playerTransform = Object.FindObjectOfType<Player>().transform;
        seek = new Seek(enemy.transform, playerTransform.transform, 3);
    }

    public void Execute()
    {
        Move();
        enemy.StartTree();
    }

    public void Sleep()
    {

    }

    public void AddTransition(T input, IState<T> state)
    {
        if (!dictionaryState.ContainsKey(input))
            dictionaryState.Add(input, state);
    }
    public void RemoveTransition(T input)
    {
        if (dictionaryState.ContainsKey(input))
            dictionaryState.Remove(input);
    }
    public IState<T> GetState(T input)
    {
        if (dictionaryState.ContainsKey(input))
            return dictionaryState[input];
        return null;
    }
    public void Move()
    {
        //dir = seek.GetDirection();
        //dir.y = 0;
        //dir = dir.normalized;
        ////enemyRigidbody.velocity = dir * speed;
        //enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, dir, 0.2f);

        //enemy.transform.forward = Vector3.Lerp(enemy.transform.forward, dir, 0.2f);
        Walk();
    }

    public void Walk()
    {

        float MovementStep = speed * Time.deltaTime;
        float RotationStep = RotationSpeed * Time.deltaTime;

        Vector3 LookAtWayPoint = playerTransform.position - enemy.transform.position;
        Quaternion RotationToTarget = Quaternion.LookRotation(LookAtWayPoint);

        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, RotationToTarget, RotationStep);

        float distance = Vector3.Distance(enemy.transform.position, playerTransform.position);

        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, playerTransform.position, MovementStep);
    }
}
