using BaseRPG.Controller.Input;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Model.Game;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.System;

namespace BaseRPG.Controller
{
    public class Controller
    {
        private PlayerControl playerControl;
        private InputHandler inputHandler;
        private Timer timer;
        public InputHandler InputHandler { get { return inputHandler; } }

        public Game Game { get { return game; } }

        private Game game;
        public Controller(Game game)
        {
            this.game = game;
            playerControl = new PlayerControl(game.Hero,game.PhysicsFactory);
            inputHandler = new InputHandler(
                RawInputProcessedInputMapper.CreateDefault(),
                ProcessedInputActionMapper.CreateDefault(playerControl)
            );
            timer = new Timer();
            timer.Interval = 100;
            timer.Elapsed += (a,b)=>Tick();
            timer.Start();
        }

        public void Tick() {
            inputHandler.OnTick();
            game.CurrentWorld.OnTick();
        }
    }
}
