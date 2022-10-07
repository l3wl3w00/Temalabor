using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.EntityView;
using BaseRPG.View.ItemView;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.UnitControl
{
    public enum MoveDirection
    {
        Forward, Backward, Left, Right
    }


    public class PlayerControl:UnitControlBase
    {

        private DirectionMovementUnitMapper directionVectorMapper;
        private List<IMovementUnit> movements = new List<IMovementUnit>();
        public EquippedItemView EquippedItemView { get; set; }

        public PlayerControl( Unit controlledUnit):base(controlledUnit)
        {
            directionVectorMapper = DirectionMovementUnitMapper.CreateDefault2D();
        }

        public void OnMove(MoveDirection moveDirection)
        {
            IMovementUnit movement = directionVectorMapper.FromDirection(moveDirection);
            movements.Add(movement);

        }
        public void LightAttack()
        {
            EquippedItemView.StartLightAttackAnimation();
            //OnLightAttack?.Invoke();
        }
        public void HeavyAttack()
        {

        }
        public void UseSpell()
        {

        }

        public override IMovementUnit NextMovement(double delta)
        {
            if (movements.Count == 0) return null;
            IMovementUnit movementUnit = IMovementUnit.Unite(movements);
            movements.Clear();
            return movementUnit.Scaled(200*delta);
        }
    }
}
