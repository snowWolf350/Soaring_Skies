using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector2 _mousedeltaPos;
    float _mouseSensitivity = 0.01f;
    float _yaw;
    float _pitch;

    private void Update()
    {
        _mousedeltaPos = GameInput.Instance.GetMouseDelta();
        _yaw -= _mousedeltaPos.x * _mouseSensitivity;
        _pitch += _mousedeltaPos.y * _mouseSensitivity;

        _pitch = Mathf.Clamp(_pitch,-15,15);
        _yaw = Mathf.Clamp(_yaw,-15,15);
        transform.rotation = Quaternion.Euler(_pitch,_yaw,0) ;
    }
}
