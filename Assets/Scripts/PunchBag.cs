using UnityEngine;

public class PunchBag : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log($"Hit!, {col.relativeVelocity * 1000}");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
