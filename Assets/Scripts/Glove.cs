using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Glove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector3 currentVelocity = Vector2.zero;
    private Vector3 lastPosition;
    public Vector3 Velocity { get => currentVelocity; }


    public void SetHand(Transform hand, bool left)
    {
        transform.SetParent(hand);
        transform.position = hand.position;
        transform.localScale = new Vector3(1, 1 ,1);
    }


    void Start()
    {
        lastPosition = transform.position;
        
    }



    // Update is called once per frame
    void Update()
    {
        currentVelocity = (lastPosition - transform.position) / Time.deltaTime;
        lastPosition = transform.position;
    }
}
