using System;
using Exiled.API.Features;

namespace PlayerUtilities
{
    public class MainClass : Plugin<MainConfig>
    {
        public override string Author { get; } = "Jesus-QC";
        public override string Name { get; } = "PlayerUtilities";
        public override string Prefix { get; } = "PlayerUtilities";
        public override Version Version { get; } = new Version(1, 0, 2);
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 5);

        public static MainConfig Cfg { get; private set; }
        private EventHandler _eventHandler;
        
        public override void OnEnabled()
        {
            Cfg = Config;

            _eventHandler = new EventHandler();

            Exiled.Events.Handlers.Player.Verified += _eventHandler.OnVerified;
            Exiled.Events.Handlers.Player.Left += _eventHandler.OnLeft;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Verified -= _eventHandler.OnVerified;
            Exiled.Events.Handlers.Player.Left -= _eventHandler.OnLeft;

            Cfg = null;
            
            base.OnDisabled();
        }
    }
}