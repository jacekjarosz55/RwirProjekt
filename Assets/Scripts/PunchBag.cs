using TMPro;
using UnityEngine;

public class PunchBag : MonoBehaviour
{

    public TMP_Text displayOutput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }



    void OnTriggerEnter(Collider other)
    {
        Glove glove = other.GetComponent<Glove>();
        displayOutput.text = $"Score: {Mathf.Floor(glove.Velocity.magnitude * 500)}";
    }


    // Update is called once per frame
    void Update()
    {

    }
}
