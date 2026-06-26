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
        private void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                isPlayerInSight = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                isPlayerInSight = false;
            }
        }
    }
}
