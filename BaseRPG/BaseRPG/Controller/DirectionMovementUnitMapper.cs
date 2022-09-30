using BaseRPG.Model.Interfaces.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller
{
    public class DirectionMovementUnitMapper
    {
        private Dictionary<MoveDirection, IMovementUnit> map;
        private IPhysicsFactory physicsFactory;
        public DirectionMovementUnitMapper(Dictionary<MoveDirection, IMovementUnit> map,IPhysicsFactory physicsFactory)
        {
            this.map = map;
        }
        public static DirectionMovementUnitMapper CreateDefault(IPhysicsFactory physicsFactory) {
            DirectionMovementUnitMapper directionVectorMapper =
                new DirectionMovementUnitMapper(new Dictionary<MoveDirection, IMovementUnit>(),physicsFactory);
            directionVectorMapper.map.Add(MoveDirection.Left, physicsFactory.CreateMovement(-1, 0));
            directionVectorMapper.map.Add(MoveDirection.Right, physicsFactory.CreateMovement(1, 0));
            directionVectorMapper.map.Add(MoveDirection.Forward,  physicsFactory.CreateMovement(0, -1));
            directionVectorMapper.map.Add(MoveDirection.Backward,  physicsFactory.CreateMovement(0, 1));
            return directionVectorMapper;
        }

        public IMovementUnit FromDirection(MoveDirection moveDirection)
        {
            return map[moveDirection];
        }
    }
}
