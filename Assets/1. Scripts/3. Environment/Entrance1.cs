using UnityEngine;

namespace MyGame
{
    public class Entrance1 : MonoBehaviour
    {
        public bool isCloserE1 { get; private set; }

        public void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log($"E1 : {isCloserE1}");
                isCloserE1 = true;
            }
        }

        public void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log($"E1: {isCloserE1}");
                isCloserE1 = false;
            }
        }
    }
}
