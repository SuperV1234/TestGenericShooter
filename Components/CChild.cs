using VeeCollision;
using VeeEntity;

namespace TestGenericShooter.Components
{
    public class CChild : Component
    {
        private readonly CBody _cBody;
        private readonly CBody _parentBody;
        private readonly Entity _parent;

        public CChild(Entity mParent, CBody mCBody)
        {
            _parent = mParent;
            _cBody = mCBody;
            _parentBody = _parent.GetComponentUnSafe<CBody>();

            _parent.OnDestroy += () => Entity.Destroy();
        }

        public override void Update(float mFrameTime) { _cBody.Position = _parentBody.Position; }
    }
}