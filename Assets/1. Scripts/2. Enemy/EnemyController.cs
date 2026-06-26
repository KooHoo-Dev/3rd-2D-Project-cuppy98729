using System.Collections;
using UnityEngine;

namespace MyGame
{
    public class EnemyController : MonoBehaviour
    {
        [field: SerializeField] public float detectCountDown = 5.0f;
        [field: SerializeField] public Transform Target;

        private const float IDLE_WAIT_TIME = 3.0f;

        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private Transform patrolPoint;

        private IEnumerator nextStateCoroutine;

        private EnemySightSensor EnemySight { get; set; }

        private void Awake()
        {
            EnemySight = GetComponentInChildren<EnemySightSensor>();
        }

        private void OnEnable()
        {
            StartCoroutine(FSMCoroutine());
        }

        private IEnumerator FSMCoroutine()
        {
            nextStateCoroutine = IdleStateCoroutine();

            if (gameObject.activeInHierarchy) yield return StartCoroutine(nextStateCoroutine);
        }

        private IEnumerator IdleStateCoroutine()
        {
            // 탈출 조건 1 : 플레이어가 시야 범위 안에 존재함
            if (EnemySight.isPlayerInSight == true)
            {
                Debug.Log($"{EnemySight.isPlayerInSight}");
                nextStateCoroutine = DoubtStateCoroutine();
            }

            yield break;
        }

        private IEnumerator PatrolStateCoroutine()
        {

            yield return null;
        }

        private IEnumerator DoubtStateCoroutine()
        {
            // # 플레이어가 시야 범위 안에 있을 때
            // - 즉시 자리에 멈춤
            // - 포착 카운트 다운 시작
            //  - 만약 플레이어가 5초간 계속 머물렀다면 => break 게임오버
            //  - 도중에 나갔다면 =>
            //    1. 플레이어가 시야에서 떠나기 전 마지막으로 머물렀던 지점으로 이동
            //    2. 머무르며 3배 속도로 소모됐던 카운트 다운 복구
            //    3. 다시 자리로 돌아가
            //    4. Patrol 시작
            float wastedCountDown = 0.0f;

            for (int i = 0; i <= detectCountDown; i++)
            {
                detectCountDown -= 1.0f;
                yield return new WaitForSeconds(1f);

                Debug.Log($"1초 대기");

                if (detectCountDown == 0.0f)
                {
                    Debug.Log("카운트 다운 종료");
                    break;
                }
                else if ((detectCountDown > 0.0f) && (EnemySight.isPlayerInSight == false))
                {
                    IdleStateCoroutine();
                    wastedCountDown = 5.0f - detectCountDown;
                    break;
                }
            }

            


            yield return null;
        }

        private void Move()
        {

        }
    }
}
