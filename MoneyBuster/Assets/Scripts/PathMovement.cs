using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class PathMovement : MonoBehaviour
{

    [SerializeField] private Dog dog;
    public PathCreator pathCreator;
    public bool canMove;
    public float speed = 5f;
    public float distanceTravelled = 5;

    void Update()
    {
        
        
    }
}
