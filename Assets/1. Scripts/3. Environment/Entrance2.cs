using UnityEngine;

namespace MyGame
{
    public class Entrance2 : MonoBehaviour
    {
        public bool isCloserE2 { get; private set; }

        public void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log($"E2 : {isCloserE2}");
                isCloserE2 = true;
            }
        }

        public void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log($"E2 : {isCloserE2}");
                isCloserE2 = false;
            }
        }
    }
}
