using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Camera _camera;
    
    [HideInInspector] public float zoom;
    [Header("Zoom")]
    public bool canZoom = true;
    public float zoomMultiplier = 10f;
    public float minZoom = 2f;
    public float maxZoom = 5f;
    [HideInInspector] public float velocityZoom;
    public float smoothTime = 0.25f;

    [Header("Drag")]
    public bool canDrag = true;
    public float dragSpeed = 2f;
    private Vector3 dragOrigin;

    [HideInInspector] public Vector2 worldSize;

    [Header("Focus")]
    public bool canFocus = false;
    public float zoomFocus = 1f;
    private bool unfocus = false;
    private Vector3 velocityPos;
    private Vector3 targetPos = Vector3.zero;


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
        if (canZoom)
            Zoom();

        if (canDrag)
            Drag();

        ClampCamera();

        if (canFocus)
            Focus();
    }

    private void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, zoom, ref velocityZoom, smoothTime);
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

    public void StartFocus(bool enabled, Transform position)
    {
        canFocus = enabled;
        unfocus = true;
        targetPos = new Vector3(position.position.x, position.position.y, -10);
    }

    private void Focus()
    {
        if (unfocus)
            StartCoroutine(UnFocus());
        else
        {
            _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, zoomFocus, ref velocityZoom, smoothTime);
            _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, targetPos, ref velocityPos, smoothTime);
        }
    }

    IEnumerator UnFocus() 
    {
        _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, maxZoom, ref velocityZoom, smoothTime);
        yield return new WaitForSeconds(1f);
        unfocus = false;
    }
}
