using System.Linq;
using UnityEngine;

namespace ThirdPersonController.InventorySystem
{
    public class InventoryIdentity : ScriptableObject
    {
        [SerializeField] private string m_ID = System.Guid.NewGuid().ToString();
        public string id
        {
            get { return m_ID; }
        }
        
        public static InventoryIdentity LoadFromResources(string id)
        {
            return Resources.LoadAll<InventoryIdentity>(string.Empty).FirstOrDefault(x => x.id == id);
        }
    }
}