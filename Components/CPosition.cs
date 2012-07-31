using VeeEntitySystem2012;

namespace TestGenericShooter.Components
{
    public class CPosition : Component
    {
        public CPosition(GSVector2 mPosition) { Position = mPosition; }

        public GSVector2 Position { get; set; }
        public int X { get { return Position.X; } }
        public int Y { get { return Position.Y; } }
    }
}