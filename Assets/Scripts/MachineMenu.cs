using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class MachineMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Button leftHandButton;
    private Button rightHandButton;
    public Glove glove;
    public Transform leftHand;
    public Transform rightHand;

/*
    void SetRenderersEnabled(Renderer[] renderers, bool enabled)
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = enabled;
        }

    }
*/

    void Start()
    {
        leftHandButton = GameObject.Find("LeftHandButton").GetComponent<Button>();
        rightHandButton = GameObject.Find("RightHandButton").GetComponent<Button>();
        XRRayInteractor leftHandRay = leftHand.GetComponentInChildren<XRRayInteractor>();
        XRRayInteractor rightHandRay = rightHand.GetComponentInChildren<XRRayInteractor>();
        var leftJoyModel = leftHand.Find("UniversalController").gameObject;
        var rightJoyModel = rightHand.Find("UniversalController").gameObject;

        // set defaults
        // TODO: dont do this?
        glove.SetHand(rightHand, false);
        rightHandRay.enabled = false;
        leftHandRay.enabled = true;
        rightJoyModel.SetActive(false);
        leftJoyModel.SetActive(true);

        rightHandButton.onClick.AddListener(() =>
        {
            glove.SetHand(rightHand, false);
            rightHandRay.enabled = false;
            leftHandRay.enabled = true;
            rightJoyModel.SetActive(false);
            leftJoyModel.SetActive(true);

        });
        leftHandButton.onClick.AddListener(() =>
        {
            glove.SetHand(leftHand, true);
            rightHandRay.enabled = true;
            leftHandRay.enabled = false;
            rightJoyModel.SetActive(true);
            leftJoyModel.SetActive(false);
        });


    
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
