using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class UnarmedMeleeHandlerInputSettings
    {
        [SerializeField] private string m_MeleeButton = "Melee";
        public string meleeButton
        {
            get { return m_MeleeButton; }
        }
    }
}