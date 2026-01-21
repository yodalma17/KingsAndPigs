using Unity.Cinemachine;
using UnityEngine;

public class CustomCameraOffset : MonoBehaviour
{

    public CinemachineCamera cinemachineCamera;
    public CinemachinePositionComposer PositionComposer;

    private void Start()
    {
        PositionComposer = cinemachineCamera.GetComponent<CinemachinePositionComposer>();
    }

    //OnTriggerEnter2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        PositionComposer.TargetOffset.y = -2.8f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        PositionComposer.TargetOffset.y = 0f;
    }
}
