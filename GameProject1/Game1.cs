using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace GameProject1
{
    /// <summary>
    /// Class for Game Project 1
    /// </summary>
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
        private Song _backgroundMusic;

        /// <summary>
        /// Tells whether there are coins left or not
        /// </summary>
        private bool _noCoinsLeft { get; set; } = false;

        /// <summary>
        /// Game project 1
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
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

        /// <summary>
        /// Loads the content
        /// </summary>
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
            _backgroundMusic = Content.Load<Song>("Monkeys-Spinning-Monkeys(chosic.com)");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_backgroundMusic);

        }

        /// <summary>
        /// Updates the game
        /// </summary>
        /// <param name="gameTime"></param>
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

        /// <summary>
        /// Draws the game
        /// </summary>
        /// <param name="gameTime">The real time elapsed in the game</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_throneRoom, new Vector2(0, 0), null, Color.White);
            _spriteBatch.DrawString(_playerControls, "Get the coins! \nA and D, or the \nleft and right arrow keys to move. \nPress esc to quit.", new Vector2(330, 10), Color.Black);
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