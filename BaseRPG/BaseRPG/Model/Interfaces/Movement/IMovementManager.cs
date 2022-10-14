using System;

namespace BaseRPG.Model.Interfaces.Movement
{
    public interface IMovementManager
    {
        public event Action Moved;
        IMovementUnit LastMovement { get; }
        IPositionUnit Position { get; }

        void Move(IMovementUnit movement);
        IMovementManager Copy();
    }
}