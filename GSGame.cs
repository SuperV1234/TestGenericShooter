using SFML.Graphics;
using SFML.Window;
using SFMLStart;
using SFMLStart.Data;
using VeeEntitySystem2012;

namespace TestGenericShooter
{
    public class GSGame : Game
    {
        private readonly Manager _manager;
        private readonly PhysicsWorld _physicsWorld;        

        public GSGame()
        {
            _manager = new Manager();
            _physicsWorld = new PhysicsWorld(200, 200, 3000, 50, 50);
            Factory = new Factory(this, _manager, _physicsWorld);

            OnUpdate += _manager.Update;
            OnUpdate += UpdateInputs;
            OnUpdate += mFrameTime => GameWindow.RenderWindow.SetTitle(((int) (60/mFrameTime)).ToString());

            OnDrawBeforeCamera += () => GameWindow.RenderWindow.Clear(new Color(155, 155, 255));

            InitializeInputs();
            NewGame();
        }

        public Factory Factory { get; private set; }
        public int NextX { get; private set; }
        public int NextY { get; private set; }
        public int NextAction { get; private set; }

        private void InitializeInputs()
        {
            Bind("w", 0, () => NextY = -1, null, new KeyCombination(Keyboard.Key.W));
            Bind("s", 0, () => NextY = 1, null, new KeyCombination(Keyboard.Key.S));
            Bind("a", 0, () => NextX = -1, null, new KeyCombination(Keyboard.Key.A));
            Bind("d", 0, () => NextX = 1, null, new KeyCombination(Keyboard.Key.D));

            Bind("fire", 0, () => NextAction = 1, null, new KeyCombination(Mouse.Button.Left));
        }
        private void NewGame()
        {
            _manager.Clear();

            const int sizeX = 20;
            const int sizeY = 15;

            var map = new[]
                      {
                          "11111111111111111111",
                          "10000100000000000001",
                          "10200100000000000001",
                          "10000100000000000001",
                          "10000100000000000001",
                          "10000000000000000001",
                          "10000000111100000001",
                          "10000000003000000001",
                          "10000000111100000001",
                          "10000000000000000001",
                          "10000000000000133331",
                          "10000000000000133331",
                          "10000000000000100401",
                          "10000000000000100001",
                          "11111111111111111111"
                      };


            for (var iY = 0; iY < sizeY; iY++)
                for (var iX = 0; iX < sizeX; iX++)
                {
                    if (map[iY].Substring(iX, 1) == "2")
                        Factory.Player(ToCoords(8) + ToCoords(16) * iX, ToCoords(8) + ToCoords(16) * iY);

                    if (map[iY].Substring(iX, 1) == "1")
                        Factory.Wall(ToCoords(8) + ToCoords(16)*iX, ToCoords(8) + ToCoords(16)*iY, ToCoords(16), ToCoords(16));

                    if (map[iY].Substring(iX, 1) == "3")
                        Factory.BreakableWall(ToCoords(8) + ToCoords(16) * iX, ToCoords(8) + ToCoords(16) * iY, ToCoords(16), ToCoords(16));

                    if (map[iY].Substring(iX, 1) == "0")
                        Factory.Enemy(ToCoords(8) + ToCoords(16)*iX, ToCoords(8) + ToCoords(16)*iY);
                }
        }

        private void UpdateInputs(float mFrameTime) { NextX = NextY = NextAction = 0; }

        private static int ToCoords(int mValue) { return mValue*100*3; }
    }
}