using BaseRPG.Model.Interfaces.ItemInterfaces;
using BaseRPG.Model.Tickable.Item.Factories;
using System.Collections.Generic;

namespace BaseRPG.Model.Data
{
    //TODO ezt majd adatbázisból
    public class ItemCatalogue:Catalogue<IItemFactory>
    {

        protected override void FillFactories(Dictionary<string, IItemFactory> factories)
        {
            factories.Add("SimpleSword",new SimpleSwordFactory());
            factories.Add("SimpleBow", new SimpleBowFactory());
            //factories.Add("SimpleSword", new SimpleSwordFactory());
        }
    }
}
