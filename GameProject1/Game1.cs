using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace GameProject1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private mcSprite _mc;

        private CoinSprite[] _coins;
        private int _coinsLeft;

        private Texture2D _ball;

        private SpriteFont _playerControls;
        private SpriteFont _coinCounter;
        private SpriteFont _win;

        private bool _noCoinsLeft { get; set; } = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //_inputmanager = new InputManager();
            _mc = new mcSprite();
            _coins = new CoinSprite[]
            {
                new CoinSprite(new Vector2(300, 280)),
                new CoinSprite(new Vector2(332, 280))
            };
            _coinsLeft = _coins.Length;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _mc.LoadContent(Content);
            foreach (var coin in _coins) coin.LoadContent(Content);
            _playerControls = Content.Load<SpriteFont>("Controls");
            _coinCounter = Content.Load<SpriteFont>("CoinsLeft");
            _win = Content.Load<SpriteFont>("Congratulations");

            _ball = Content.Load<Texture2D>("ball (1)");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //_inputmanager.Update(gameTime);
            //if (_inputmanager.Exit) Exit();
            _mc.Update(gameTime);
            foreach (var coin in _coins)
            {
                if (!coin.Collected && coin.Bounds.CollidesWith(_mc.Bounds))
                { 
                    coin.Collected = true;
                    _coinsLeft--;
                }

            }
            if (_coinsLeft == 0)
            {
                _noCoinsLeft = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_playerControls, "A and D, or the arrow keys to move. \n Press esc to quit", new Vector2(250, 10), Color.Black);
            foreach (var coin in _coins)
            {
                coin.Draw(gameTime, _spriteBatch);
                //var rect = new Rectangle(
                //    (int)coin.Bounds.Center.X - (int)coin.Bounds.Radius,
                //    (int)coin.Bounds.Center.Y - (int)coin.Bounds.Radius,
                //    (int)(2*coin.Bounds.Radius), (int)(2*coin.Bounds.Radius));
                //_spriteBatch.Draw(_ball, rect, Color.White);
            }
            _spriteBatch.DrawString(_coinCounter, $"Coins Left: {_coinsLeft}", new Vector2(2, 2), Color.Gold);

            var rectG = new Rectangle(
                    (int)_mc.Bounds.Left,
                    (int)_mc.Bounds.Bottom,
                    16, 16);

            var rectT = new Rectangle(
                    (int)_mc.Bounds.Right,
                    (int)_mc.Bounds.Bottom,
                    16, 16);
            var rectE = new Rectangle(
                    (int)_mc.Bounds.Left,
                    (int)_mc.Bounds.Top,
                    16, 16);

            var rectD = new Rectangle(
                    (int)_mc.Bounds.Right,
                    (int)_mc.Bounds.Top,
                    16, 16);

            _spriteBatch.Draw(_ball, rectG, Color.White);
            _spriteBatch.Draw(_ball, rectT, Color.White);
            _spriteBatch.Draw(_ball, rectE, Color.White);
            _spriteBatch.Draw(_ball, rectD, Color.White);
            _mc.Draw(gameTime, _spriteBatch);

            if(_noCoinsLeft)
            {
                _spriteBatch.DrawString(_win, "Congratulations! You win! \n Press esc to exit the game", new Vector2(80, 60), Color.Red);
            }

            _spriteBatch.End();
            base.Draw(gameTime);


        }
    }
}