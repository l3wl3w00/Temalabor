﻿using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Skills;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.EntityView;
using BaseRPG.View.EntityView.Health;
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
        private IPositionProvider mousePositionProvider;
        private readonly Hero controlledUnitAsHero;
        private readonly DrawableProvider drawableProvider;

        public IPositionProvider MousePositionProvider { set => mousePositionProvider = value; }

        public PlayerControl( Hero controlledUnitAsHero, DrawableProvider drawableProvider):base(controlledUnitAsHero)
        {
            directionVectorMapper = DirectionMovementUnitMapper.CreateDefault2D();
            this.controlledUnitAsHero = controlledUnitAsHero;
            this.drawableProvider = drawableProvider;
        }

        public void OnMove(MoveDirection moveDirection)
        {
            IMovementUnit movement = directionVectorMapper.FromDirection(moveDirection);
            movements.Add(movement);

        }
        public void LightAttack()
        {
            if (ControlledUnit == null) return;
            AttackBuilder attackFactory = ControlledUnit.AttackFactory("light");
            
            if (attackFactory != null)
                drawableProvider
                    .GetDrawable<EquippedItemView>(controlledUnitAsHero.Inventory.EquippedWeapon,"equipped")
                    .StartLightAttackAnimation(attackFactory);
        }
        public void HeavyAttack()
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
