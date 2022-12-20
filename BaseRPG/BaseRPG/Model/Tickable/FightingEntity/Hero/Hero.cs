﻿using BaseRPG.Model.Attribute;
using BaseRPG.Model.Combat;
using BaseRPG.Model.Interaction;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collecting;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Movement;
using BaseRPG.Model.Services;
using BaseRPG.Model.Skills;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.Attacks.Lifetime;
using BaseRPG.Model.Tickable.Item.Factories;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Model.Worlds;
using BaseRPG.Model.Worlds.InteractionPoints;
using BaseRPG.Physics.TwoDimensional.Movement;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.FightingEntity.Hero
{
    public class Hero : Unit, ICollector
    {
        protected override string Type { get { return "Hero"; } }
        private Model.Attribute.Inventory inventory;
        private Model.Attribute.ExperienceManager experienceManager;
        private GoldManager goldManager = new();

        public override AttackabilityService.Group OffensiveGroup => AttackabilityService.Group.Friendly;
        public override AttackabilityService.Group DefensiveGroup => OffensiveGroup;

        public int Level { get => ExperienceManager.Level; }
        public int Gold { get => goldManager.Gold; } 
        public event Action<int> GoldChanged;
        public ExperienceManager ExperienceManager { get => experienceManager; set => experienceManager = value; }
        public Inventory Inventory => inventory;

        public GoldManager GoldManager => goldManager;

        private Hero(int maxHp, IMovementManager movementManager, SkillManager skillManager, World world) :
            base(maxHp, movementManager, new EmptyMovementStrategy(), skillManager, world, false)
        {
            ExperienceManager = new();
            inventory = new(12);
            goldManager.GoldChanged += g => GoldChanged?.Invoke(g);
        }

        internal bool SpendGold(int cost)
        {
            return goldManager.SpendGold(cost);
        }
        public void Buy(Shop shop,int i) {
            shop.Buy(this, i);
        }
        public void OnLevelUpCallback(Action<int> callback) {
            ExperienceManager.LevelUp += callback;
        }
        public void OnXpChagedCallback(Action<double,double> action) {
            ExperienceManager.ExperienceChanged += action;
        }
        public override AttackBuilder AttackFactory(string attackName)
        {
            if (inventory.EquippedWeapon == null) return null;
            if (attackName == "light")
                return inventory.EquippedWeapon.LightAttackBuilder;
            return null;
        }
        private void LightAttack()
        {
            //inventory.EquippedWeapon.OnLightAttack();
        }
        public override void Step(double delta)
        {
            base.Step(delta);
        }

        public void CollectItem(Item.Item collectible)
        {
            inventory.Collect(collectible);

        }
        public void Equip(Item.Item item) {
            inventory.Equip(item);
        }

        public void Collect(ICollectible collectible)
        {
            ICollector collector = this;
            collector.OnCollect(collectible);
        }


        public class HeroBuilder : Builder
        {
            public HeroBuilder(int maxHp, IMovementManager movementManager, World world) : base(maxHp, movementManager, new EmptyMovementStrategy(),world)
            {
            }

            public override Hero Build(int maxHp, IMovementManager movementManager, IMovementStrategy movementStrategy, SkillManager skillManager, World world)
            {
                var hero = new Hero(maxHp,movementManager,skillManager,world);
                hero.ExperienceManager.LevelUp += newLevel=>hero.SkillManager.SkillPoints += 1;
                return hero;
            }
        }

        
    }
}
