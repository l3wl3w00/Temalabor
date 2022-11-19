using BaseRPG.Model.Attribute;
using BaseRPG.Model.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.DefensiveItem
{
    public class DefensiveItem : Item
    {
        public DefensiveItem(World currentWorld) : base(currentWorld)
        {
        }

        public override void EquippedBy(Inventory inventory)
        {
            inventory.EquippedArmor = this;
        }
    }
}
