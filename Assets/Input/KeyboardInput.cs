using UnityEngine;

namespace sovereignstars_movement
{
    public class KeyboardInput : MonoBehaviour
    {
        void Update()
        {
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


