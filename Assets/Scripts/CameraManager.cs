using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public GameObject targetObject;
    private float targetObjectX;
    Vector3 newCameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetObjectX = targetObject.transform.position.x;
        newCameraPosition = transform.position;
        newCameraPosition.x = targetObjectX;
        transform.position = newCameraPosition;
    }
}
