using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Projectile : MonoBehaviour
{
    public GameObject bullet;

    public float shootForce, upwardForce;

    public float timeBetweenShooting, spread, timeBetweenShots;
    public bool allowButtonHold;

    bool shooting;

    public Camera camera;
    public Transform attackPoint;

    public bool allowInvoke = true;

    private void Awake()
    {
        
    }

    private void Update()
    {
        myInput();
    }

    private void myInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (shooting)
        {
            Shoot();
        }
    }

    private void Shoot()
    {

    }
}
