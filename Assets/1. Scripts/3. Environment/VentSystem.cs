using MyGame.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyGame
{
    public class VentSystem : MonoBehaviour
    {
        // 두 출입구 간 이동 시켜주는 기능
        // 피드백: 가까워지면 상호작용 키 이미지 팝업
        [SerializeField] private Transform Vent1;
        [SerializeField] private Transform Vent2;
        [SerializeField] private Transform Target;

        private Entrance1 Entrance1;
        private Entrance2 Entrance2;

        private void Awake()
        {
            Entrance1 = GetComponentInChildren<Entrance1>();
            Entrance2 = GetComponentInChildren<Entrance2>();
        }

        private void Update()
        {
            // Player가 Vent와 접촉하면 Space키로 상호작용 가능
            // Space 키 입력 시 반대편 Vent로 이동
            if (Entrance1.isCloserE1 == true)
            {
                if (SimpleInput.GetKeyDown(Key.Space))
                {
                    Target.transform.position = Vent2.transform.position;
                }
            }

            if (Entrance2.isCloserE2 == true)
            {
                if (SimpleInput.GetKeyDown(Key.Space))
                {
                    Target.transform.position = Vent1.transform.position;
                }
            }
        }
    }
}
