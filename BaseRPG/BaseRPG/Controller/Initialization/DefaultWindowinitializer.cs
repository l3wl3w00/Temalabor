using BaseRPG.Controller.Input;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.Window;
using BaseRPG.Model.Attribute;
using BaseRPG.View.UIElements;
using BaseRPG.View.UIElements.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Initialization
{
    public class DefaultWindowinitializer : IWindowsInitializer
    {
        private BindingHandler bindingHandler;
        private readonly Inventory inventory;

        public DefaultWindowinitializer(BindingHandler bindingHandler, Inventory inventory)
        {
            this.bindingHandler = bindingHandler;
            this.inventory = inventory;
        }

        public WindowControl Initialize(MainWindow window)
        {
            var result = new WindowControl.Builder(window.MainCanvas)
                .SettingsWindowAs(new SettingsWindow(exitCallback: window.Close))
                .Window(InventoryWindow.WindowName, new InventoryWindow(inventory,window.Controller.DrawableProvider))
                .Window(KeyBindingsWindow.WindowName,new KeyBindingsWindow(bindingHandler, 2))
                .Build();
            return result;
        }
    }
}
