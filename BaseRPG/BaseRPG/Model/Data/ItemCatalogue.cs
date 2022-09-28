using BaseRPG.Model.Interfaces.ItemInterfaces;
using BaseRPG.Model.Tickable.Item.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Data
{
    //TODO ezt majd adatbázisból
    public class ItemCatalogue:Catalogue<IItemFactory>
    {

        protected override void FillFactories(Dictionary<string, IItemFactory> factories)
        {
            factories.Add("SimpleShoe",new SimpleShoeFactory());
            factories.Add("SimpleArmor", new SimpleArmorFactory());
            factories.Add("SimpleSword", new SimpleSwordFactory());
        }
    }
}
