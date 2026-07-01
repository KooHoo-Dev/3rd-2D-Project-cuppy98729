using MyGame.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace MyGame
{
    public class PlayerController : MonoBehaviour
    {
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

        // # Vent
        // - Vent와 일정 거리 이상 가까우면 상호작용이 가능
        // - Vent와 상호작용(F)하면 Vent 내부에 은신, 또 누르면 은신 해제
        // - 은신 상태에서 Space 입력할 때마다 현재 위치의 반대쪽으로 이동

        private void UsingVent()
        {

        }
    }
}
