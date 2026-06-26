using System.Collections;
using UnityEngine;

namespace MyGame
{
    public class EnemyController : MonoBehaviour
    {
        [field: SerializeField] public Transform Target;

        private const float IDLE_WAIT_TIME = 3.0f;

        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float detectCountDown = 3.0f;
        [SerializeField] private float baseUpdateTerm = 0.1f;
        [SerializeField] private Transform startPoint;
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

            while (gameObject.activeInHierarchy) yield return StartCoroutine(nextStateCoroutine);
        }

        private IEnumerator IdleStateCoroutine()
        {
            float waitTime = 0.0f;
            WaitForSeconds term = new WaitForSeconds(baseUpdateTerm);

            while (true)
            {
                // 탈출 조건 1 : 3초가 지나면 Patrol 상태로 전환
                if (waitTime > IDLE_WAIT_TIME)
                {
                    nextStateCoroutine = PatrolStateCoroutine();
                    yield break;
                }

                // 탈출 조건 2 : 플레이어가 시야 범위 안에 존재
                if (EnemySight.isPlayerInSight == true)
                {
                    nextStateCoroutine = DoubtStateCoroutine();
                    yield break;
                }

                yield return term;
                waitTime += baseUpdateTerm;
            }
        }

        private IEnumerator PatrolStateCoroutine()
        {

            yield return null;
        }

        private IEnumerator DoubtStateCoroutine()
        {
            // 플레이어가 시야 범위 안에 있을 때

            float wastedCountDown = 0.0f;

            // 1. 즉시 이동을 멈추고

            // 2. 포착 카운트 다운 시작
            for (int i = 0; i <= detectCountDown; i++)
            {
                Debug.Log($"{detectCountDown}");
                detectCountDown -= 1;
                yield return new WaitForSeconds(1);

                // 2.1) 만약 플레이어가 5초간 계속 머물렀다면 => 게임오버(*미구현)
                if (detectCountDown == 0.0f)
                {
                    Debug.Log("카운트 다운 종료");
                    break;
                }

                // 2.2) 도중에 나갔다면
                else if ((detectCountDown > 0.0f) && (EnemySight.isPlayerInSight == false))
                {
                    // i) 플레이어가 시야에서 떠나기 전 마지막으로 머물렀던 지점으로 이동


                    // ii) 머무르며 3배 속도로 소모됐던 카운트 다운 복구
                    wastedCountDown = 5.0f - detectCountDown;
                    yield return new WaitForSeconds(wastedCountDown * 3);

                    // iii) 다시 자리로 돌아가

                    break;
                }

                // Patrol 상태로 전환
                nextStateCoroutine = PatrolStateCoroutine();
            }
        }

        private void Move(Vector2 goalPosition)
        {

        }
    }
}
