using UnityEngine;

namespace MyGame
{
    public class Vent : MonoBehaviour
    {
        // 두 출입구 간 이동 시켜주는 기능
        // 피드백: 가까워지면 상호작용 키 이미지 팝업
        [field: SerializeField] public Transform Entrance1;
        [field: SerializeField] public Transform Entrance2;

        public bool isCloser { get; private set; }

        public void MoveInTheVent()
        {

        }
    }
}
