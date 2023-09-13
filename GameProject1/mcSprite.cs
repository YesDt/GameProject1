using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SharpDX.Direct3D9;
using Microsoft.Xna.Framework.Input;
using GameProject1.Collisions;

namespace GameProject1
{
    public class mcSprite
    {
        //private Texture2D _texture;


        //private Vector2 _position = new Vector2(200, 280);

        //KeyboardState currentKeyboardState;
        //KeyboardState priorKeyboardState;

        ///// <summary>
        ///// The color to blend in with the ghost
        ///// </summary>
        //public Color Color { get; set; } = Color.White;


        //private BoundingCircle _bounds = new BoundingCircle(new Vector2(200, 280), 32f);


        //private bool _flipped;


        //public BoundingCircle Bounds => _bounds;

        //public void LoadContent(ContentManager content)
        //{
        //    _texture = content.Load<Texture2D>("Sprite_MC_Idle");


        //}

        //public void Update(GameTime gameTime)
        //{
        //    priorKeyboardState = currentKeyboardState;
        //    currentKeyboardState = Keyboard.GetState();


        //    if (currentKeyboardState.IsKeyDown(Keys.A) ||
        //        currentKeyboardState.IsKeyDown(Keys.Left))
        //    {
        //        _position += new Vector2(-100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
        //        _flipped = true;
        //    }
        //    if (currentKeyboardState.IsKeyDown(Keys.D) ||
        //        currentKeyboardState.IsKeyDown(Keys.Right))
        //    {
        //        _position += new Vector2(100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
        //        _flipped = false;
        //    }

        //    _bounds.Center = _position;
        //}

        //public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        //{
        //    SpriteEffects spriteEffects = (_flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        //    spriteBatch.Draw(_texture, _position, null, Color.White, 0f, new Vector2(128, 128), 0.5f, spriteEffects, 0);

        //}


        private Texture2D _texture;


        private Vector2 _position = new Vector2(200, 240);

        KeyboardState currentKeyboardState;
        KeyboardState priorKeyboardState;

        private BoundingRectangle _bounds = new BoundingRectangle(new Vector2(200-32, 240-32), 32, 80);


        private bool _flipped;


        public BoundingRectangle Bounds => _bounds;

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Sprite_MC_Idle");


        }

        public void Update(GameTime gameTime)
        {
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();


            if (currentKeyboardState.IsKeyDown(Keys.A) ||
                currentKeyboardState.IsKeyDown(Keys.Left))
            {
                _position += new Vector2(-100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
                _flipped = true;
            }
            if (currentKeyboardState.IsKeyDown(Keys.D) ||
                currentKeyboardState.IsKeyDown(Keys.Right))
            {
                _position += new Vector2(100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
                _flipped = false;
            }

            _bounds.X = _position.X;
            _bounds.Y = _position.Y;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = (_flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(_texture, _position, null, Color.White, 0f, new Vector2(80, 16), 0.5f, spriteEffects, 0);

        }
    }
}
