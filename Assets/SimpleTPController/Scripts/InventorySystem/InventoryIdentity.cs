using System.Linq;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    public class InventoryIdentity : ScriptableObject
    {
        [Header("Identification")]
        [SerializeField] private string m_ID = System.Guid.NewGuid().ToString();
        public string id
        {
            get { return m_ID; }
        }

        [SerializeField] private string m_DisplayName = null;
        public string displayName
        {
            get { return m_DisplayName; }
        }
        
        public static InventoryIdentity LoadFromResources(string id)
        {
            return Resources.LoadAll<InventoryIdentity>(string.Empty).FirstOrDefault(x => x.id == id);
        }
    }
}