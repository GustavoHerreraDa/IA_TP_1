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

    public IA_Chase(EnemyController enemy, Rigidbody rb, float ds = 10, float dr = 20, float s = 7)
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
        //seek = new Seek(_enemy.transform, playerT, dodgeStrenght, dodgeRadius);
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
        //dir = seek.GetDir();
        dir.y = 0;
        dir = dir.normalized;
        enemyRigidbody.velocity = dir * speed;
        enemy.transform.forward = Vector3.Lerp(enemy.transform.forward, dir, 0.2f);
    }
}
