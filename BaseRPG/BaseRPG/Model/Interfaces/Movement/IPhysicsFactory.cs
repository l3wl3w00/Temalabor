﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Movement
{
    public interface IPhysicsFactory
    {
        IMovementUnit CreateMovement(params double[] args);
        IPositionUnit CreatePosition(params double[] args);
        IPositionUnit Origin { get; }
    }
}