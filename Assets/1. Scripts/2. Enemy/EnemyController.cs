using Study.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyGame
{
    public class EnemyController : MonoBehaviour
    {
        [field: SerializeField] public Transform Target;

        private const float IDLE_WAIT_TIME = 3.0f;
        private const float ORIGINAL_COUNTDOWN = 2.0f;

        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float baseUpdateTerm = 0.1f;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform patrolPoint;

        private Vector2 pointA, pointB, goalPoint;
        private Vector2 originalScale;
        private IEnumerator nextStateCoroutine;

        private float stoppingDistance = 0.1f;

        private EnemySightSensor EnemySight { get; set; }

        private void Awake()
        {
            EnemySight = GetComponentInChildren<EnemySightSensor>();

            originalScale = transform.localScale;

            pointA = transform.position;
            pointB = patrolPoint.position;
            goalPoint = pointB;
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
            Debug.Log($"IdleStateCoroutine");
            float waitTime = 0.0f;
            WaitForSeconds term = new WaitForSeconds(baseUpdateTerm);

            while (true)
            {
                // 탈출 조건 1 : 2초가 지나면 Patrol 상태로 전환
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
            Debug.Log($"PatrolStateCoroutine");
            stoppingDistance = 0.1f;

            while (true)
            {
                Vector2 adjustGoalPoint = transform.position;
                adjustGoalPoint.x = goalPoint.x;

                // 탈출 조건: 플레이어가 시야 범위 안에 존재
                if (EnemySight.isPlayerInSight == true)
                {
                    nextStateCoroutine = DoubtStateCoroutine();
                    yield break;
                }

                if (transform.IsInRange(adjustGoalPoint, stoppingDistance))
                {
                    goalPoint = transform.IsSamePosition(pointB, stoppingDistance) ? pointA : pointB;
                    nextStateCoroutine = IdleStateCoroutine();
                    yield break;
                }

                Move(adjustGoalPoint);
                yield return null;
            }
        }

        private IEnumerator DoubtStateCoroutine()
        {
            Debug.Log($"DoubtStateCoroutine");
            stoppingDistance = 0.3f;
            // 플레이어가 시야 범위 안에 있을 때
            float detectCountDown = ORIGINAL_COUNTDOWN;
            
            // 1. 즉시 이동을 멈추고
            moveSpeed = 0;
            Vector2 missingTargetPos = Target.transform.position;

            // 2. 포착 카운트 다운 시작
            for (int i = 0; i < ORIGINAL_COUNTDOWN; i++)
            {
                Debug.Log($"{detectCountDown}");
                yield return new WaitForSeconds(1);
                detectCountDown -= 1.0f;

                // 2.1) 만약 플레이어가 2초간 계속 머물렀다면 => 게임 오버(씬 처음으로 되돌림)
                if (detectCountDown <= 0)
                {
                    Debug.Log("카운트 다운 종료. 게임 오버");
                    yield break;
                }

                // 2.2) 도중에 나갔다면
                else if ((detectCountDown > 0) && (EnemySight.isPlayerInSight == false))
                {
                    Debug.Log("Searching");
                    moveSpeed = 2;

                    // i) 플레이어가 시야에서 떠나기 전 마지막으로 머물렀던 지점으로 이동
                    while (Vector2.Distance(transform.position, missingTargetPos) > stoppingDistance)
                    {
                        Move(missingTargetPos);
                        yield return null;
                    }

                    // ii) 4초 대기
                    for (int j = 0; j < 4; j++)
                    {
                        // * 만약 대기 중에 플레이어가 다시 포착되면 Doubt 상태를 처음부터 다시 시작
                        if (EnemySight.isPlayerInSight == true)
                        {
                            nextStateCoroutine = DoubtStateCoroutine();
                            yield break;
                        }
                        else
                        {
                            Debug.Log($"{4 - j}");
                            yield return new WaitForSeconds(1);
                        }
                    }
                    detectCountDown = ORIGINAL_COUNTDOWN;

                    // iii) 시작 때의 자리로 돌아가
                    Move(startPoint.position);

                    // iiii) Patrol 상태로 전환
                    nextStateCoroutine = PatrolStateCoroutine();
                    yield break;
                }

                yield return null;
            }
        }

        private void Move(Vector2 goalPosition)
        {
            float dir = UpdateDirection(goalPosition);
            transform.Translate(new Vector3(dir, 0, 0) * (moveSpeed * Time.deltaTime));
        }

        /// <summary>
        /// 캐릭터의 이동 방향과 전방 방향 반환
        /// </summary>
        /// <param name="goalPosition"></param>
        /// <returns></returns>
        private float UpdateDirection(Vector2 goalPosition)
        {
            float dirToGo = goalPosition.x - transform.position.x;
            float moveDirection = Mathf.Sign(dirToGo);

            transform.localScale = new Vector2(moveDirection * originalScale.x, originalScale.y);

            return moveDirection;
        }
    }
}
