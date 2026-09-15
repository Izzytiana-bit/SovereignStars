using UnityEngine;

namespace sovereignstars_movement
{
    public class KeyboardInput : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                VirtualInputManager.instance.MoveForward = true;
            }
            else
            {
                VirtualInputManager.instance.MoveForward = false;
            }

            if (Input.GetKey(KeyCode.S))
            {
                VirtualInputManager.instance.MoveBack = true;
            }
            else
            {
                VirtualInputManager.instance.MoveBack = false;
            }

            if (Input.GetKey(KeyCode.D))
            {
                VirtualInputManager.instance.MoveRight = true;
            }
            else
            {
                VirtualInputManager.instance.MoveRight = false;
            }

            if (Input.GetKey(KeyCode.A))
            {
                VirtualInputManager.instance.MoveLeft = true;
            }
            else
            {
                VirtualInputManager.instance.MoveLeft = false;
            }
        }
    }
}


