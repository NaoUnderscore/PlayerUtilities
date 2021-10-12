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

        public void Awake() => _player = Player.Get(gameObject);

        public void Update()
        {
            if(_player.Role == RoleType.Spectator || _player.Role == RoleType.Tutorial && MainClass.Cfg.AfkChecker.IgnoreTutorials)
                return;
            
            if (_lastPos != _player.Position || _lastRot != _player.Rotation)
            {
                _lastPos = _player.Position;
                _lastRot = _player.Rotation;
                return;
            }

            _afkTime += Time.deltaTime;

            if (_afkTime > MainClass.Cfg.AfkChecker.AfkTime)
            {
                if (MainClass.Cfg.AfkChecker.ReplaceOptions.ShouldReplace)
                {
                    var newPly = Player.Get(Team.RIP).GetRandom();
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
                }
                else
                {
                    _player.Kick("[SRV MOD]" + MainClass.Cfg.AfkChecker.Actions.ActionMsg);
                }

                _afkTime = 0;
                return;
            }

            if (MainClass.Cfg.AfkChecker.Actions.ShowCounter && _afkTime > MainClass.Cfg.AfkChecker.AfkTime - 10)
            {
                _player.Broadcast(1, MainClass.Cfg.AfkChecker.Actions.CounterMsg.Replace("{seconds_left}", $"{MainClass.Cfg.AfkChecker.AfkTime - _afkTime}"), shouldClearPrevious:true);
            }
        }
    }
}