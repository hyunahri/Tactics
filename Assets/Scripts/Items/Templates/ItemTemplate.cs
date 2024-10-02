using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ItemTemplate", menuName = "Items/ItemTemplate", order = 1)]
    public class ItemTemplate : ScriptableObject
    {
        public virtual object ToInstance()
        {
            throw new System.NotImplementedException();
        }

    }
}