using System;
using System.Linq;
using Exiled.API.Features;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayerUtilities.Features.Components
{
    public class AfkManager : MonoBehaviour
    {
        private float _afkTime;

        private Player _player;
        private Vector3 _lastPos = Vector3.zero;
        private Vector3 _lastRot = Vector2.zero;

        private float _counterTime;
        
        public void Awake() => _player = Player.Get(gameObject);

        public void Update()
        {
            if(_player.Role == RoleType.Spectator || _player.Role == RoleType.Tutorial && MainClass.Cfg.AfkChecker.IgnoreTutorials)
                return;
            
            if (_lastPos != _player.Position || _lastRot != _player.Rotation)
            {
                _lastPos = _player.Position;
                _lastRot = _player.Rotation;
                _afkTime = 0;
                return;
            }

            _afkTime += Time.deltaTime;

            if (_afkTime > MainClass.Cfg.AfkChecker.AfkTime)
            {
                if (MainClass.Cfg.AfkChecker.ReplaceOptions.ShouldReplace)
                {
                    var newPly = Player.Get(Team.RIP).GetRandom();

                    if(newPly == null)
                        return;
                    
                    _player.ReplaceWith(newPly);

                    if (!string.IsNullOrWhiteSpace(MainClass.Cfg.AfkChecker.ReplaceOptions.ReplaceMessage))
                    {
                        if (MainClass.Cfg.AfkChecker.ReplaceOptions.BroadcastMsg)
                            newPly.Broadcast(MainClass.Cfg.AfkChecker.ReplaceOptions.MessageDuration, MainClass.Cfg.AfkChecker.ReplaceOptions.ReplaceMessage);
                        else
                            newPly.ShowHint(MainClass.Cfg.AfkChecker.ReplaceOptions.ReplaceMessage, MainClass.Cfg.AfkChecker.ReplaceOptions.MessageDuration);
                    }
                }

                if (MainClass.Cfg.AfkChecker.Actions.SetSpectator)
                {
                    _player.SetRole(RoleType.Spectator);
                    if(!string.IsNullOrEmpty(MainClass.Cfg.AfkChecker.Actions.ActionMsg))
                        _player.Broadcast(5, MainClass.Cfg.AfkChecker.Actions.ActionMsg);
                    
                }
                else
                {
                    _player.Kick("[SRV MOD]" + MainClass.Cfg.AfkChecker.Actions.ActionMsg);
                }

                _afkTime = 0;
                return;
            }

            if (!MainClass.Cfg.AfkChecker.Actions.ShowCounter || _afkTime < MainClass.Cfg.AfkChecker.AfkTime - 10) return;
            
            if (_counterTime > 1)
            {
                _player.Broadcast(1, MainClass.Cfg.AfkChecker.Actions.CounterMsg.Replace("{seconds_left}", $"{(int)(MainClass.Cfg.AfkChecker.AfkTime - _afkTime)}"), shouldClearPrevious: true);
                _counterTime = 0;
            }
            else
                _counterTime += Time.deltaTime;
        }
    }
}