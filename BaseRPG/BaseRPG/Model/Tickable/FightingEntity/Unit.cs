using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Interfaces.Skill;
using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.Item.Weapon;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.FightingEntity
{
    public abstract class Unit : IGameObject, IAttackable, IAttacking, ISkillCaster
    {

        private Health health;
        private IMovementManager movementManager;
        private IMovementStrategy movementStrategy;
        private Dictionary<string,IAttackFactory> attacks;
        private Stat damage;
        private double speed = 100;

        public Health Health { get { return health; } }
        public int Damage => throw new NotImplementedException();
        public IMovementUnit NextMovement { get => movementStrategy.CalculateNextMovement(MovementManager, speed); }
        public IPositionUnit Position { get { return movementManager.Position; } }
        public IMovementUnit LastMovement
        {
            get
            {
                return movementManager.LastMovement;
            }
        }
        public IMovementManager MovementManager => movementManager;

        public abstract void OnTick();
        //public virtual void Attack(string attackName) {
        //    attacks[attackName.ToLower()]?.CreateAttack(this,movementManager.Position);
        //}


        public int CalculateDamage() {
            return damage.Value;
        }

        public void Move(IMovementUnit movement) {
            movementManager.Move(movement);
        }

        public Unit(int maxHp, IMovementManager movementManager, 
            IMovementStrategy movementStrategy, Dictionary<string, IAttackFactory> attacks ) {
            health = new Health(maxHp);
            this.movementManager = movementManager;
            this.movementStrategy = movementStrategy;
            this.attacks = attacks;
        }

        protected abstract string Type { get; }

        public bool Exists => health.CurrentValue > 0;

        public abstract AttackabilityService.Group OffensiveGroup { get; }
        public abstract AttackabilityService.Group DefensiveGroup { get; }

        public void Separate(Dictionary<string, List<IGameObject>> dict)
        {
            string key = Type;
            if (dict.ContainsKey(key))
            {
                dict[key].Add(this);
                return;
            }
            dict.Add(key, new List<IGameObject> { this });
        }

        public void TakeDamage(double damage)
        {
            health.CurrentValue -= damage;
        }

        public virtual void OnCollision(IGameObject gameObject)
        {
            
        }

        public void Cast(ISkill skill)
        {
            skill.OnCast(this);
        }
    }
}
