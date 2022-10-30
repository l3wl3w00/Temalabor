﻿using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collecting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item
{
    public class Item : ICollectible, IGameObject
    {
        public bool Exists => true;

        public event Action OnCeaseToExist;

        //public void OnCollect(ICollector collector)
        //{
        //    //Do something
        //}

        public void OnCollect<T>(ICollector<T> collector) where T : ICollectible
        {
            //throw new NotImplementedException();
        }

        public void OnCollision(IGameObject gameObject)
        {
        }

        public void OnTick(double delta)
        {
            //throw new NotImplementedException();
        }

        public void Separate(Dictionary<string, List<ISeparable>> dict)
        {
            throw new NotImplementedException();
        }
    }
}
