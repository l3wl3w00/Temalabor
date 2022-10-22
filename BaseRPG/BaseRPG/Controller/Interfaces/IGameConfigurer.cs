﻿using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Interfaces
{
    public interface IGameConfigurer
    {
        void Configure(Controller controller, ViewManager viewManager);
    }
}