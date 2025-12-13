using System;
using System.Collections;
using System.Text;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.XR.Interaction.Toolkit;

public class PunchBag : MonoBehaviour
{

    public TMP_Text displayOutput;
    public AnimationCurve scoreFunction;
    public float maxForce = 16.0f;
    private float score;
    private float writtenScore = 0;
    public float Score => score;

    private bool active = true;
    public bool IsActive => active;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        if (!active) return;
        Glove glove = other.GetComponent<Glove>();
        score = Mathf.Floor(scoreFunction.Evaluate(glove.Velocity.magnitude/maxForce)*999);
        Debug.Log(score);
        active = false;
    }

    private string FormatScore(float score)
    {
        StringBuilder output = new StringBuilder();

        int flooredScore = (int) score;
        if (flooredScore < 100) {
            output.Append("0");
        }
        if (flooredScore < 10) {
            output.Append("0");
        }
        output.Append(flooredScore);
        return output.ToString();
    }

    private IEnumerator HandleReset()
    {
        writtenScore = 0;
        score = 0;
        yield return new WaitForSeconds(2);
        active = true;
        displayOutput.text = $"{FormatScore(writtenScore)}";
    } 


    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(writtenScore - score) > 0.1) {
            writtenScore = Mathf.Lerp(writtenScore, score, Time.deltaTime);
            displayOutput.text = $"{FormatScore(writtenScore)}";
        }
        else if (score != 0)
        {
            StartCoroutine(HandleReset());
        }
    }
}
