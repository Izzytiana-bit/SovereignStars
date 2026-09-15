using sovereignstars_movement;
using UnityEngine;

namespace sovereignstars_testing
{
    public class TestingCube : MonoBehaviour
    {
        void Update()
        {
            if (VirtualInputManager.instance.MoveForward && VirtualInputManager.instance.MoveBack || 
                VirtualInputManager.instance.MoveRight && VirtualInputManager.instance.MoveLeft)
            {
                return;
            }

            if (VirtualInputManager.instance.MoveBack)
            {
                this.gameObject.transform.Translate(-Vector3.forward * 10f * Time.deltaTime);
            }
            if (VirtualInputManager.instance.MoveForward)
            {
                this.gameObject.transform.Translate(Vector3.forward * 10f * Time.deltaTime);
            }
            if (VirtualInputManager.instance.MoveRight)
            {
                this.gameObject.transform.Translate(Vector3.right * 10f * Time.deltaTime);
            }
            if (VirtualInputManager.instance.MoveLeft)
            {
                this.gameObject.transform.Translate(Vector3.left * 10f * Time.deltaTime);
            }
        }
    }
}


