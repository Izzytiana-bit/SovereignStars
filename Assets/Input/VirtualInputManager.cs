using UnityEngine;

namespace sovereignstars_movement
{
    public class VirtualInputManager : MonoBehaviour
    {
        public static VirtualInputManager instance = null;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        public bool MoveBack;
        public bool MoveForward;
        public bool MoveLeft;
        public bool MoveRight;
    }
}


