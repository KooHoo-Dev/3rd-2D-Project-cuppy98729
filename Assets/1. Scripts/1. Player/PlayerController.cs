using MyGame.Utils;
using UnityEngine;

namespace MyGame
{
    public class PlayerController : MonoBehaviour
    {
        // # 남은 과제
        // 암살 기능 넣어야 함
        // 암살 가능 범위는 플레이어한테 부착
        // 동시에 EnemyController가 참조하게 해서 Crouch 안하고 충돌하면 즉시 포착 및 게임오버되게끔
        // 피드백 및 애니메이션 :
        // - 플레이어 즉시 StopMovement로 조작 막음
        // - Enemy 전방 방향 Player 쪽으로 바꾸고 공격 애니메이션 + 이펙트 처리

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

        public void StopMovement()
        {
            Controller2D.SetMoveInput(0.0f);
        }
    }
}
