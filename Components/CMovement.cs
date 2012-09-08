using SFMLStart.Utilities;
using SFMLStart.Vectors;
using VeeCollision;
using VeeEntity;

namespace TestGenericShooter.Components
{
    public class CMovement : Component
    {
        private readonly CBody _cBody;

        public CMovement(CBody mCBody) { _cBody = mCBody; }

        public float Angle { get; set; }
        public float Speed { get; set; }
        public float Acceleration { get; set; }

        public void Stop()
        {
            Speed = 0;
            Acceleration = 0;
        }

        public override void Update(float mFrameTime)
        {
            var angleVector = Utils.Math.Angles.ToVectorDegrees(Angle);
            _cBody.Velocity = new SSVector2I((int) (angleVector.X*Speed), (int) (angleVector.Y*Speed));
            Speed += Acceleration;
        }
    }
}