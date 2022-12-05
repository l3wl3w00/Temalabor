using BaseRPG.Model.Attribute;
using BaseRPG.Model.Combat;
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
        private int gold = 0;

        public override AttackabilityService.Group OffensiveGroup => AttackabilityService.Group.Friendly;
        public override AttackabilityService.Group DefensiveGroup => OffensiveGroup;

        public int Level { get => ExperienceManager.Level; }
        public int Gold { get => gold; set { gold = value; GoldChanged?.Invoke(); } } 
        public event Action GoldChanged;
        public ExperienceManager ExperienceManager { get => experienceManager; set => experienceManager = value; }
        public Inventory Inventory => inventory;

        

        private Hero(int maxHp, IMovementManager movementManager, SkillManager skillManager, World world) :
            base(maxHp, movementManager, new EmptyMovementStrategy(), skillManager, world, false)
        {
            ExperienceManager = new();
            inventory = new(12);
        }

        //public void Collect(ICollectible collectible)
        //{
        //    inventory.Collect(collectible);
        //}

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
                return inventory.EquippedWeapon.LightAttackFactory;
            return null;
        }
        private void LightAttack()
        {
            //inventory.EquippedWeapon.OnLightAttack();
        }
        public override void Step(double delta)
        
        {
            base.Step(delta);
            //throw new NotImplementedException();
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
            collectible.OnCollectedByHero(this);
        }

        public override void OnTargetKilled(IAttackable target)
        {
            target.OnKilledByHero(this);
        }

        public override void OnKilledByHero(Hero hero)
        {
            
        }

        public override void OnKilledByEnemy(Enemy.Enemy enemy)
        {
            
        }

        internal void OnEnemyKilled(Enemy.Enemy enemy)
        {
            ExperienceManager.GainExpirence(enemy.XpValue*10);
            Gold += enemy.GoldValue;
            Collect(new SimpleBowFactory().Create(CurrentWorld));
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
