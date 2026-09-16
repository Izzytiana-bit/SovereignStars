using UnityEngine;

public class Aiming : MonoBehaviour
{

    private Vector3 mouse;
    public float rotationSpeed = 3f;

    private Quaternion lookRotation;

    private Vector3 directionTarget;

    public Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            shooting();
        }
        aiming();
    }

    public void aiming()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            mouse = hit.point;
        }

        directionTarget = (mouse - transform.position).normalized;

        lookRotation = Quaternion.LookRotation(directionTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private void shooting()
    {

    }
}
