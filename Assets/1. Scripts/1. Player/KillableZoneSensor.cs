using UnityEngine;

namespace MyGame
{
    public class KillableZoneSensor : MonoBehaviour
    {
        public bool isKillable { get; private set; }

        public void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.CompareTag("Enemy"))
            {
                isKillable = true;
            }
        }

        public void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.CompareTag("Enemy"))
            {
                isKillable = false;
            }
        }
    }
}
