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

        private void Awake()
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Controller2D = GetComponentInChildren<CharacterController2D>();
        }

        private void Update()
        {
            ApplyMovement();
        }

        /// <summary>
        /// 키보드 입력을 받아 수평 이동
        /// </summary>
        private void ApplyMovement()
        {
            float inputVector = SimpleInput.GetAxisHorizontalRaw();

            Controller2D.SetMoveInput(inputVector);
        }


    }
}
