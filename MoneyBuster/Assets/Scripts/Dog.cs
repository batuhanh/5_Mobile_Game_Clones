using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    //Dog Components.
    [SerializeField] private NavMeshAgent dogNavAgent;
    [SerializeField] private Rigidbody dogRb;
    [SerializeField] private Animator dogAnim;


    //Obstacle related variables.
    private GameObject goToObstacle;
    private float oldDistance = 9999;

    [SerializeField] private List<GameObject> obstacles;


    public bool dogCanMove = true;

    private void Awake()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle").ToList();
        DOTween.Init();
    }

    void Start()
    {
    }


    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (dogCanMove)
        {
            MoveDog();
        }
    }

    public void AttackObstacle()
    {
    }

    public void FeatherEmitter()
    {
    }

    private void MoveDog()
    {
        dogAnim.SetBool("run", true);
        dogNavAgent.SetDestination(GetObstacle().gameObject.transform.position);
    }


    private GameObject GetObstacle()
    {
        foreach (GameObject obs in obstacles)
        {
            float dist = Vector3.Distance(this.gameObject.transform.position, obs.transform.position);
            if (dist < oldDistance)
            {
                goToObstacle = obs;
                oldDistance = dist;
               
                
            }
        }

        return goToObstacle; 
    }
}