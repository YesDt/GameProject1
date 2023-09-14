﻿using Microsoft.Xna.Framework.Content;
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
    public enum Action
    {
        Idle = 0,
        Running = 1,
    }

    public class mcSprite
    {
        private Texture2D _texture;



        private Vector2 _position = new Vector2(200, 300);

        KeyboardState currentKeyboardState;
        KeyboardState priorKeyboardState;

        private BoundingRectangle _bounds = new BoundingRectangle(new Vector2(200-32, 300-32), 48, 130);

        private double _animationTimer;

        private short _animationFrame;

        private bool _flipped;


        public Action action;

        public BoundingRectangle Bounds => _bounds;

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Sprite_MC");


        }

        public void Update(GameTime gameTime)
        {
            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();


            if (currentKeyboardState.IsKeyDown(Keys.A) ||
                currentKeyboardState.IsKeyDown(Keys.Left))
            {
                _position += new Vector2(-200 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
                action = Action.Running;
                _flipped = true;
            }
            if (currentKeyboardState.IsKeyDown(Keys.D) ||
                currentKeyboardState.IsKeyDown(Keys.Right))
            {
                _position += new Vector2(200 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
                action = Action.Running;
                _flipped = false;
            }
            if (!(currentKeyboardState.IsKeyDown(Keys.A) ||
                currentKeyboardState.IsKeyDown(Keys.Left)) &&
                !(currentKeyboardState.IsKeyDown(Keys.D) ||
                currentKeyboardState.IsKeyDown(Keys.Right))
                )
            {
                action = Action.Idle;
            }
            _bounds.X = _position.X;
            _bounds.Y = _position.Y;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            SpriteEffects spriteEffects = (_flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //Update animationFrame
            if (_animationTimer > 0.2)
            {
                _animationFrame++;
                if (_animationFrame > 3) _animationFrame = 0;
                _animationTimer -= 0.2;
            }
            var source = new Rectangle(_animationFrame * 250, (int)action * 512, 268, 512);
            spriteBatch.Draw(_texture, _position, source, Color.White, 0f, new Vector2(80, 120), 0.5f, spriteEffects, 0);


        }
    }
}
