using BaseRPG.Model.Data;
using System;

namespace BaseRPG.Model.Interfaces.Movement
{
    public interface IMovementManager
    {
        void QueueMovement(IMovementUnit movement);
        public event Action Moved;
        IMovementUnit LastMovement { get; }
        IPositionUnit Position { get; }
        void MoveQueued();
        void Move(IMovementUnit movement);
        IMovementManager Copy();
        
    }
}