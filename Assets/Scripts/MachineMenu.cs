using UnityEngine;
using UnityEngine.UI;

public class MachineMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Button leftHandButton;
    private Button rightHandButton;
    public Glove glove;
    public Transform leftHand;
    public Transform rightHand;
    void Start()
    {
        leftHandButton = GameObject.Find("LeftHandButton").GetComponent<Button>();
        rightHandButton = GameObject.Find("RightHandButton").GetComponent<Button>();

        rightHandButton.onClick.AddListener(() =>
        {
            glove.SetHand(rightHand, false);
        });
        leftHandButton.onClick.AddListener(() =>
        {
            glove.SetHand(leftHand, true);
        });
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
