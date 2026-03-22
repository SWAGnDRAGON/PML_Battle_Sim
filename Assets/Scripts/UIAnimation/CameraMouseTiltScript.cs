using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraMouseTiltScript : CinemachineExtension
{
    [Header("Tilt Settings")]
    public float tiltStrength = 1.5f;
    public float smoothSpeed = 3f;

    private Vector2 _currentOffset;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (stage != CinemachineCore.Stage.Finalize) return;
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Vector2 normalized = new Vector2(
            (mousePos.x - screenCenter.x) / screenCenter.x,
            (mousePos.y - screenCenter.y) / screenCenter.y
        );

        Vector2 target = normalized * tiltStrength;
        _currentOffset = Vector2.Lerp(_currentOffset, target, deltaTime * smoothSpeed);

        // Apply on top of whatever Cinemachine already calculated
        state.RawOrientation *= Quaternion.Euler(-_currentOffset.y, _currentOffset.x, 0f);
    }
}