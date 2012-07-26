using SFMLStart.Utilities;
using VeeEntitySystem2012;

namespace TestGenericShooter.Components
{
    public class CAI : Component
    {
        private readonly CBody _cBody;
        private readonly CMovement _cMovement;
        private readonly CTargeter _cTargeter;
        private readonly GSGame _game;
        private float _angle;
        private float _replicationDelay;
        private float _rotationSpeed;
        private float _shootDelay;
        private float _speed;

        public CAI(GSGame mGame, CBody mCBody, CMovement mCMovement, CTargeter mCTargeter)
        {
            _game = mGame;
            _cBody = mCBody;
            _cTargeter = mCTargeter;
            _cMovement = mCMovement;

            mCBody.OnCollision += (mEntity, mBody) =>
                                  {
                                      if (!mEntity.HasTag("wall")) return;
                                      _angle += 180;
                                      _speed = 0;
                                      _rotationSpeed = 0;
                                  };
        }

        public override void Update(float mFrameTime)
        {
            Utils.Math.Angles.WrapDegrees(_angle);

            _cTargeter.FindTarget("player");
            var playerPosition = _cTargeter.TargetPosition.Position;
            var targetAngle = _cTargeter.GetDegreesTowards(playerPosition.X, playerPosition.Y);

            if (_speed < 400) _speed += 3*mFrameTime;
            if (_rotationSpeed < 0.2f) _rotationSpeed += 0.002f*mFrameTime;

            _shootDelay += mFrameTime;
            //_replicationDelay += mFrameTime;

            if (_shootDelay > 40)
            {
                _shootDelay = 0;
                _game.Factory.Bullet(_cBody.Position.X, _cBody.Position.Y, targetAngle, 800);
                _game.Factory.Bullet(_cBody.Position.X, _cBody.Position.Y, targetAngle + 15, 800);
                _game.Factory.Bullet(_cBody.Position.X, _cBody.Position.Y, targetAngle - 15, 800);
            }

            if (_replicationDelay > 50)
            {
                _replicationDelay = 0;
                _game.Factory.Enemy(_cBody.Position.X, _cBody.Position.Y);
            }

            _angle = Utils.Math.Angles.RotateTowardsAngleDegrees(_angle, targetAngle, _rotationSpeed);

            _cMovement.MoveTowardsAngle(_angle, (int) _speed);
        }
    }
}