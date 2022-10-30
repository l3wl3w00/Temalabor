﻿using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.ItemInterfaces;
using BaseRPG.Model.Tickable.Item.Weapon.Sword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.Factories
{
    public class SimpleSwordFactory:IItemFactory
    {
        public Item Create()
        {
            return new Weapon.Weapon(
                new AttackBuilder(new DamagingAttackStrategy(40)),
                new AttackBuilder(new DamagingAttackStrategy(40)));
        }
    }
}
