using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameStateManagement;
using Microsoft.Xna.Framework;

namespace GameStateManagement
{
    class LevelCompleteScreen : MenuScreen
    {

        public LevelCompleteScreen()
            : base("Level completed!")
        {
            IsPopup = true;

            MenuEntry nextLevelMenuEntry = new MenuEntry("Next Level");
            MenuEntry restartLevelMenuEntry = new MenuEntry("Restart Level");
            MenuEntry quitGameMenuEntry = new MenuEntry("Quit Game");

            nextLevelMenuEntry.Selected += nextLevelMenuEntrySelected;
            restartLevelMenuEntry.Selected += restartLevelMenuEntrySelected;
            quitGameMenuEntry.Selected += quitGameMenuEntrySelected;

            MenuEntries.Add(nextLevelMenuEntry);
            MenuEntries.Add(restartLevelMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
            
        }


        void nextLevelMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            OptionsMenuScreen.SwitchToNextBoard();
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen());

        }

        void restartLevelMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen());
        }

        void quitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to quit this game?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }


        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
        }


        public override void Draw(GameTime gameTime)
        {
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            base.Draw(gameTime);
        }



    }
}
