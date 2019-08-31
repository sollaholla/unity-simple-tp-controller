using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    [System.Serializable]
    public class UnarmedMeleeHandlerAnimatorSettings
    {
        [SerializeField] private string m_MeleeAnimParam = "Melee";
        public string meleeAnimParam
        {
            get { return m_MeleeAnimParam; }
        }

        [SerializeField] private string m_MeleeIDParam = "MeleeID";
        public string meleeIDParam
        {
            get { return m_MeleeIDParam; }
        }
    }
}