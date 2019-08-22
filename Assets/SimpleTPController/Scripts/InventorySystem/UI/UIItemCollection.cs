using UnityEngine;

namespace ThirdPersonController.InventorySystem.UI
{
    /// <summary>
    /// A user interface wrapper for the <see cref="ItemCollection" /> class.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class UIItemCollection : MonoBehaviour 
    {
        [SerializeField] private string m_CollectionName = null;
        [SerializeField] private GameObject m_ItemSlotUIPrefab = null;
        [SerializeField] private bool m_ManuallyDefineSlots = false;
        [SerializeField] private Transform m_SlotContainer = null;

        protected UIItemSlot[] m_ItemSlots;

        /// <summary>
        /// The item collection this UI represents.
        /// </summary>
        public ItemCollection itemCollection { get; private set; }

        /// <summary>
        /// The target item collection name.
        /// </summary>
        public string collectionName => m_CollectionName;

        /// <summary>
        /// Initialize this item collection UI with the given item collection.
        /// </summary>
        /// <param name="itemCollection">The item collection.</param>
        public void Initialize(ItemCollection itemCollection)
        {
            this.itemCollection = itemCollection;
            this.itemCollection.slotChanged += OnSlotChanged;
            InitializeItemSlots(itemCollection);
        }

        protected virtual void InitializeItemSlots(ItemCollection collection)
        {
            m_ItemSlots = new UIItemSlot[collection.items.Count];
            if (m_ManuallyDefineSlots)
            {
                var slotComponents = GetComponentsInChildren<UIItemSlot>();

                for (var i = 0; i < slotComponents.Length; i++)
                {
                    if (i >= m_ItemSlots.Length)
                    {
                        continue;
                    }

                    m_ItemSlots[i] = slotComponents[i];
                    slotComponents[i].Initialize((uint)i, this);
                }
            }
            else
            {
                for (var i = 0; i < m_ItemSlots.Length; i++)
                {
                    m_ItemSlots[i] = Instantiate(m_ItemSlotUIPrefab, m_SlotContainer).GetComponent<UIItemSlot>();
                    m_ItemSlots[i].Initialize((uint)i, this);
                }
            }
        }

        protected virtual void OnSlotChanged(uint slot, ItemDataInstance item)
        {
            m_ItemSlots[slot].OnChanged(item);
        }
    }
}