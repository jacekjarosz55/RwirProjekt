using UnityEngine;

public class TestLeftHand : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Glove glove = GameObject.Find("Glove").GetComponent<Glove>();
        Transform leftController = GameObject.Find("XR Controller Left").transform;
        glove.SetHand(leftController, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
