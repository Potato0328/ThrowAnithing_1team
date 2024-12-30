using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "Settings/CameraSettings")]
public class CameraSettingsSO : ScriptableObject
{
    [Range(0.1f, 5f)] public float cameraRotateSpeed = 1.0f;

    // ���� ���� ����
    public void SetCameraSpeed(float speed)
    {
        cameraRotateSpeed = Mathf.Clamp(speed, 0.1f, 5f);
    }

    // ���� �� ��ȯ
    public float GetCameraSpeed()
    {
        return cameraRotateSpeed;
    }
}
