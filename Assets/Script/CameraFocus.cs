using Unity.VisualScripting;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    public void Focus(Transform position)
    {
        _camera.transform.position = new Vector3(position.position.x, position.position.y, -10);
        _camera.orthographicSize = 2f;
    }
}
