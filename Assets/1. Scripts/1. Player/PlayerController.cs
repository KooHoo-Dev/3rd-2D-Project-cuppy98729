using MyGame.Utils;
using UnityEngine;

namespace MyGame
{
    public class PlayerController : MonoBehaviour
    {
        // 1. 좌우(A/D) 이동 구현
        // 2. 



        private SpriteRenderer SpriteRenderer { get; set; }
        private CharacterController2D Controller2D { get; set; }
        private Rigidbody2D Rigidbody2D { get; set; }

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Controller2D = GetComponent<CharacterController2D>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {

        }

        private void ApplyMovement()
        {
            float inputVector = SimpleInput.GetAxisHorizontalRaw();

            //if (inputVector > 0)
        }
    }
}
