using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    
    private float zoom;
    [Header("Zoom")]
    public float zoomMultiplier = 10f;
    public float minZoom = 2f;
    public float maxZoom = 5f;
    private float velocity;
    public float smoothTime = 0.25f;

    [Header("Drag")]
    public float dragSpeed = 2f;
    private Vector3 dragOrigin;

    private Vector2 worldSize;

    void Start()
    {
        zoom = _camera.orthographicSize;

        // Taille visible totalement dézoomer
        float width = 2f * maxZoom * _camera.aspect;
        float height = 2f * maxZoom;
        worldSize = new Vector2(width, height);
    }

    void Update()
    {
        Zoom();
        Drag();
        ClampCamera();
    }

    private void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, zoom, ref velocity, smoothTime);
    }

    // Pour déplacer la caméra lorsqu'on est zoomé
    private void Drag()
    {
        if (Input.GetMouseButtonDown(1)) // Clic droit
        {
            dragOrigin = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - _camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position += difference;
        }
    }

    // Pour que la caméra ne sorte pas du monde
    private void ClampCamera()
    {
        float camHeight = _camera.orthographicSize;
        float camWidth = camHeight * _camera.aspect;

        float halfWidth = worldSize.x / 2f;
        float halfHeight = worldSize.y / 2f;

        float minX = -halfWidth + camWidth;
        float maxX = halfWidth - camWidth;
        float minY = -halfHeight + camHeight;
        float maxY = halfHeight - camHeight;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

}
