using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
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

        private Texture2D _throneRoom;
        private Texture2D _gameWon;

        private SpriteFont _playerControls;
        private SpriteFont _coinCounter;
        private SpriteFont _win;

        private SoundEffect _coinPickup;

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
            _mc = new mcSprite();
            _coins = new CoinSprite[]
            {
                new CoinSprite(new Vector2(300, 300)),
                new CoinSprite(new Vector2(700, 300)),
                new CoinSprite(new Vector2(5, 300)),
                new CoinSprite(new Vector2(100, 300)),
                new CoinSprite(new Vector2(392, 300))
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
            _throneRoom = Content.Load<Texture2D>("Throne room");
            _gameWon = Content.Load<Texture2D>("gamewon");
            _coinPickup = Content.Load<SoundEffect>("Pickup_Coin15");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _mc.Update(gameTime);
            foreach (var coin in _coins)
            {
                if (!coin.Collected && coin.Bounds.CollidesWith(_mc.Bounds))
                { 
                    coin.Collected = true;
                    _coinPickup.Play();
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
            _spriteBatch.Draw(_throneRoom, new Vector2(0, 0), null, Color.White);
            _spriteBatch.DrawString(_playerControls, "Get the coins! \n A and D, or the \n left and right arrow keys to move. \n Press esc to quit", new Vector2(200, 30), Color.Black);
            foreach (var coin in _coins)
            {
                coin.Draw(gameTime, _spriteBatch);


            }
            _spriteBatch.DrawString(_coinCounter, $"Coins Left: {_coinsLeft}", new Vector2(2, 2), Color.Gold);

            _mc.Draw(gameTime, _spriteBatch);

            if (_noCoinsLeft)
            {
                _spriteBatch.Draw(_gameWon, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                _spriteBatch.DrawString(_win, "Congratulations! You win! \n Press esc to exit the game", new Vector2(60, 0), Color.Green);
            }

            _spriteBatch.End();
            base.Draw(gameTime);


        }
    }
}