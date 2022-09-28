using BaseRPG.Model.Data;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.WorldInterfaces;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Worlds
{
    public class EmptyWorldInitializationStrategy : IWorldInitializationStrategy
    {
        public void Initialize(GameObjectContainer gameObjects)
        {
            gameObjects.Hero = new Hero(100,new Vector2D(0,0));
        }
    }
}
