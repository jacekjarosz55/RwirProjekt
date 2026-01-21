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
    public AnimationCurve dampeningFunction;
    public float maxForce = 16.0f;
    private float score;
    private float writtenScore = 0;
    public float Score => score;

    private bool active = true;
    public bool IsActive => active;
    public GameObject particlesPrefab;

    private HingeJoint hinge;
    private JointSpring jointSpring;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        jointSpring = new JointSpring();
        hinge.useSpring = true;
        hinge.spring = jointSpring;
        jointSpring.damper = dampeningFunction.Evaluate(0);
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (!active) return;
        if (other.TryGetComponent(out Glove glove)) {
            foreach (var contact in collision.contacts)
            {
                Instantiate(particlesPrefab, contact.point, Quaternion.identity);
            }

            score = Mathf.Floor(scoreFunction.Evaluate(glove.Velocity.magnitude/maxForce)*999);
            Debug.Log($"Velocity: {glove.Velocity.magnitude}, Score: {score}, Rel. Velocity: {collision.relativeVelocity}");
            active = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!active) return;
        Glove glove = other.GetComponent<Glove>();
        score = Mathf.Floor(scoreFunction.Evaluate(glove.Velocity.magnitude/maxForce)*999);
        Debug.Log($"Velocity: {glove.Velocity.magnitude}, Score: {score}");
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
        jointSpring.damper = dampeningFunction.Evaluate(0);
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
            displayOutput.text = $"{FormatScore(writtenScore+1)}";
            jointSpring.damper = dampeningFunction.Evaluate(writtenScore / score);
            hinge.spring = jointSpring;
        }
        else if (score != 0)
        {
            StartCoroutine(HandleReset());
        }
    }
}
