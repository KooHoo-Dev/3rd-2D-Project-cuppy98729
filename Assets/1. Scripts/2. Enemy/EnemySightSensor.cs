using UnityEngine;

namespace MyGame
{
    public class EnemySightSensor : MonoBehaviour
    {
        public bool isPlayerInSight { get; private set; }

        /// <summary>
        /// 충돌 판정으로 Player가 시야 안에 있는지 검사
        /// </summary>
        /// <param name="collider"></param>
        public void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                isPlayerInSight = true;
            }
        }

        public void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                isPlayerInSight = false;
            }
        }
    }
}
