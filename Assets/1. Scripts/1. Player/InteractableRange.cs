using UnityEngine;

namespace MyGame
{
    public class InteractableRange : MonoBehaviour
    {
        public bool isVentEnable { get; private set; }

        public void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.CompareTag("Interactable"))
            {
                isVentEnable = true;
            }
        }

        public void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.CompareTag("Interactable"))
            {
                isVentEnable = false;
            }
        }
    }
}
