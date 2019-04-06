﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Urban_Broccoli.StateManager;

namespace Urban_Broccoli.GameStates
{
    public interface ITitleIntroState : IGameState
    {

    }
    class TitleIntroState : BaseGameState, ITitleIntroState
    {
        #region Field Region

        private readonly Stack<Texture2D> _backgrounds = new Stack<Texture2D>();
        private Rectangle backgroundDestination;
        private SpriteFont titleFont;
        private SpriteFont messageFont;
        private TimeSpan elapsed;
        private Vector2 messagePosition;
        private Vector2 titlePosition;
        private string message;
        private string title;

        #endregion

        #region Constructor Region

        public TitleIntroState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(ITitleIntroState), this);
        }

        #endregion

        #region Method Region

        public override void Initialize()
        {
            backgroundDestination = Game1.ScreenRectangle;
            elapsed = TimeSpan.Zero;
            title = "URBAN BROCCOLI";
            message = "PRESS SPACE TO CONTINUE";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadBackground();
            messageFont = content.Load<SpriteFont>(@"Fonts\InterfaceFont");
            titleFont = content.Load<SpriteFont>(@"Fonts\TitleFont");

            Vector2 size = messageFont.MeasureString(message);
            messagePosition = new Vector2((Game1.ScreenRectangle.Width - size.X) / 2,Game1.ScreenRectangle.Bottom - 50 - messageFont.LineSpacing);

            size = titleFont.MeasureString(title);
            titlePosition = new Vector2((Game1.ScreenRectangle.Width - size.X) / 2, Game1.ScreenRectangle.Top + 50 + titleFont.LineSpacing);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            PlayerIndex index = PlayerIndex.One;
            elapsed += gameTime.ElapsedGameTime;
            base.Update(gameTime);
            UpdateTitlePosition();
        }


        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            foreach (Texture2D background in _backgrounds)
            {
                GameRef.SpriteBatch.Draw(background, backgroundDestination, new Color(1f, 1f, 0.7f, 1f));

            }
            Color color = new Color(1f, 1f, 1f) * (float)Math.Abs(Math.Sin(elapsed.TotalSeconds * 2));

            GameRef.SpriteBatch.DrawString(messageFont, message, messagePosition, color);
            GameRef.SpriteBatch.DrawString(titleFont, title, titlePosition, Color.White);

            GameRef.SpriteBatch.End();
            base.Draw(gameTime);
        }

        private void UpdateTitlePosition()
        {
            Vector2 size = titleFont.MeasureString(title);
            titlePosition = new Vector2((Game1.ScreenRectangle.Width - size.X) / 2, Game1.ScreenRectangle.Top + 50 + titleFont.LineSpacing + ((float)Math.Abs(Math.Sin(elapsed.TotalSeconds * 2)) * 10));
        }

        private void LoadBackground()
        {

            _backgrounds.Push(content.Load<Texture2D>(@"GameScreens\Title\Mushroom-Layer-1"));
            _backgrounds.Push(content.Load<Texture2D>(@"GameScreens\Title\Mushroom-Layer-2"));
            _backgrounds.Push(content.Load<Texture2D>(@"GameScreens\Title\Mushroom-Layer-3"));
            _backgrounds.Push(content.Load<Texture2D>(@"GameScreens\Title\Mushroom-Layer-4"));
            _backgrounds.Push(content.Load<Texture2D>(@"GameScreens\Title\Cloud-Layer-2"));
            _backgrounds.Push(content.Load<Texture2D>(@"GameScreens\Title\Cloud-Layer-1"));
            _backgrounds.Push(content.Load<Texture2D>(@"GameScreens\Title\Background"));
        }

        #endregion
    }
}
