using MyGame.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyGame
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject Root;
        [SerializeField] private GameObject CrouchState;

        private SpriteRenderer SpriteRenderer;

        public bool isCrouching { get; private set; } = false;

        private void Awake()
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

            CrouchState.SetActive(false);
        }

        private void Update()
        {
            ApplyCrounching();
        }

        // C 입력 시 Crouch 상태 전환 (또 누르면 상태 해제)
        private void ApplyCrounching()
        {
            if (SimpleInput.GetKeyDown(Key.C))
            {
                isCrouching = !isCrouching;

                // 이 부분은 차후 애니메이션으로 바꿈
                Root.SetActive(!Root.activeSelf);
                CrouchState.SetActive(!CrouchState.activeSelf);
            }
        }
    }
}
