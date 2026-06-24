using UnityEngine;

namespace MyGame
{
    public class Player : MonoBehaviour
    {
        public enum PlayerState
        {
            Idle,
            Move,
            Crouch,
            Kill,
        }

        public static Player LocalPlayer { get; set; }
    }
}
