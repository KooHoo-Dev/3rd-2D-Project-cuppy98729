using MyGame.Utils;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyGame
{
    public class Vent : MonoBehaviour
    {
        // 두 출입구 간 이동 시켜주는 기능
        // 피드백: 가까워지면 상호작용 키 이미지 팝업
        [SerializeField] private Transform Entrance1;
        [SerializeField] private Transform Entrance2;
        [SerializeField] private Transform Target;

        private InteractableRange Range { get; set; }

        public Vector2 vent1Pos, vent2Pos, goalVent;

        private void Awake()
        {
            Range = GetComponentInChildren<InteractableRange>();
        }

        private void Update()
        {
            // Vent와 일정 거리 이상 가까우면 상호작용이 가능
            if (Range.isVentEnable == true)
            {
                if (SimpleInput.GetKeyDown(Key.Space))
                {
                    CheckGoalVent();
                    Target.transform.position = goalVent;
                }
            }
        }

        private void CheckGoalVent()
        {
            if ((Vector2.Distance(transform.position, vent1Pos)) < (Vector2.Distance(transform.position, vent2Pos)))
            {
                goalVent = vent2Pos;
            }

            else if ((Vector2.Distance(transform.position, vent1Pos)) > (Vector2.Distance(transform.position, vent2Pos)))
            {
                goalVent = vent1Pos;
            }
        }
    }
}
