using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factory_Game.Scenes
{

    //Used to run events 
    public class Events
    {
        // string variable event text
        private string _eventTxt;
        private string _characterName;
        private string _jobTitle;

        // string array/list for event choices
        private string[] _choices = new string[3];
        private string[] _outcomeTxt = new string[3];

        // int array/list for the corresponding numbers changes
        private int[] _barChange = new int[12];
        private int[] _emotion = new int[4];

        private Text _eventText, _nameText, _jobTitleText, _but1Text, _but2Text , _but3Text, _butCloseText;
        private Text _outcome1Text, _outcome2Text, _outcome3Text;

        private UI _fileIcon;
        private UI _eventBox, _outcomeBox;
        private UI _characterTexture;

        private Texture2D _eventBoxTexture;
        private Texture2D _characterSpriteSheet;
        private Texture2D _barIconSpriteSheet;
        private Texture2D _outcomeBoxTexture;

        private Vector2 _characterSource;
        private Vector2 _eventWindowPos;
        private Vector2 _fileIconPos;

        private SpriteFont _eventFont, _nameFont;

        private bool _barsUpdated;
        
        public static EventState _eventState;

        public Events()
        {

        }

        
        public Events(string characterName, string jobTitle, int[] emotion, int[] barChange, string eventTxt, string[] choices, string[] outcomeTxt)
        {
            _choices = choices;
            _barChange = barChange;
            _characterName = characterName;
            _jobTitle = jobTitle;
            _eventTxt = eventTxt;
            _emotion = emotion;
            _outcomeTxt = outcomeTxt;

        }

        public void Initialise()
        {
            _eventState = EventState.NewEvent;
            _barsUpdated = false;
        }

        public void Load(Texture2D characterSpriteSheet, Texture2D eventBoxTexture, SpriteFont eventFont, SpriteFont nameFont, Texture2D barIconSpriteSheet, Texture2D outcomeBoxTexture, Texture2D file)
        {
            _eventBoxTexture = eventBoxTexture;
            _characterSpriteSheet = characterSpriteSheet;
            _outcomeBoxTexture = outcomeBoxTexture;

            _eventFont = eventFont;
            _nameFont = nameFont;

            _barIconSpriteSheet = barIconSpriteSheet;

            _eventWindowPos = new Vector2(40, 260);
            _fileIconPos = new Vector2(120 - (197 / 2), 920 - 71);

            _characterSource = CharacterSourceSelect(_emotion[0]);
                   
            _fileIcon = new UI(file);
            _fileIcon.Initialize(_fileIconPos,
                1f,
                new Rectangle(0, 0, 197, 71),
                0.7f);

            _eventBox = new UI(_eventBoxTexture);                    
            _eventBox.Initialize(_eventWindowPos,                      
                1f,                      
                new Rectangle(0, 0, 1000, 380),   
                0.2f);

            _outcomeBox = new UI(_outcomeBoxTexture);
            _outcomeBox.Initialize(_eventWindowPos,
                1f,
                new Rectangle(0, 0, 1000, 380),
                0.2f);

            _characterTexture = new UI(_characterSpriteSheet);
                                
            _characterTexture.Initialize(new Vector2(_eventWindowPos.X + 20, _eventWindowPos.Y + 20),                    
                1f,                   
                new Rectangle((int)_characterSource.X, (int)_characterSource.Y, 320, 320),                                     
                0.1f);
                   
            _eventText = new Text(new Vector2(_eventWindowPos.X + 360 + (620 / 2),                                   
                _eventWindowPos.Y + 100 + (160 / 2)),                     
                _eventTxt,                    
                _eventFont,
                65,
                0.1f,
                Color.Black);
                   
            _nameText = new Text(new Vector2(_eventWindowPos.X + 360 + (210 / 2),                                   
                _eventWindowPos.Y + 8 + (70 / 2)),                    
                _characterName,                     
                _nameFont,
                40,
                0.1f,
                Color.Black);

            _jobTitleText = new Text(new Vector2(_eventWindowPos.X + 360 + (210 / 2),
                _eventWindowPos.Y + 36 + (70 / 2)),
                _jobTitle,
                _nameFont,
                40,
                0.1f,
                Color.Black);


            _but1Text = new Text(new Vector2(_eventWindowPos.X + 360 + (160 / 2),                           
                _eventWindowPos.Y + 270 + (90 / 2)),                    
                _choices[0],                      
                _eventFont,
                10,
                0.1f,
                Color.Black);
                   
            _but2Text = new Text(new Vector2(_eventWindowPos.X + 590 + (160 / 2),                                                   
                _eventWindowPos.Y + 270 + (90 / 2)),                  
                _choices[1],                
                _eventFont,
                10,
                0.1f,
                Color.Black);
                  
            _but3Text = new Text(new Vector2(_eventWindowPos.X + 820 + (160 / 2),                              
                _eventWindowPos.Y + 270 + (90 / 2)),                      
                _choices[2],                    
                _eventFont,
                10,
                0.1f,
                Color.Black);

            _outcome1Text = new Text(new Vector2(_eventWindowPos.X + 360 + (620 / 2),
                _eventWindowPos.Y + 100 + (160 / 2)),
                _outcomeTxt[0],
                _eventFont,
                65,
                0.1f,
                Color.Black);

            _outcome2Text = new Text(new Vector2(_eventWindowPos.X + 360 + (620 / 2),
                _eventWindowPos.Y + 100 + (160 / 2)),
                _outcomeTxt[1],
                _eventFont,
                65,
                0.1f,
                Color.Black);

            _outcome3Text = new Text(new Vector2(_eventWindowPos.X + 360 + (620 / 2),
               _eventWindowPos.Y + 100 + (160 / 2)),
                _outcomeTxt[2],
                _eventFont,
                65,
                0.1f,
                Color.Black);
            
            _butCloseText = new Text(new Vector2(_eventWindowPos.X + 830 + (150 / 2),
                _eventWindowPos.Y + 300 + (60 / 2)),
                "Close",
                _eventFont,
                10,
                0.1f,
                Color.Black);
        }

        public void Update(GameTime gameTime, Game game)
        {

            switch (_eventState)
            {
                case EventState.NewEvent:
                    
                    //_timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    
                    //if(_timer <= 0)
                    //{
                    //    if (_flash == true)
                    //    {
                    //        _flash = false;
                    //    }
                    //    else 
                    //    { 
                    //        _flash = true; 
                    //    }
                    //    _timer = 0.3f;
                    //} 
                    break;

                case EventState.Resolution1:
                    if (_barsUpdated == false)
                    {
                        Playing._money += _barChange[0];
                        Playing._pollution += _barChange[1];
                        Playing._publicAnger += _barChange[2];
                        Playing._workerAnger += _barChange[3];
                        _barsUpdated = true;
                    }

                    _characterSource = CharacterSourceSelect(_emotion[1]);

                    _characterTexture.Initialize(new Vector2(_eventWindowPos.X + 20, _eventWindowPos.Y + 20),
                        1f,
                        new Rectangle((int)_characterSource.X, (int)_characterSource.Y, 320, 320),
                        0.1f);

                    break;

                case EventState.Resolution2:

                    if (_barsUpdated == false)
                    {
                        Playing._money += _barChange[4];
                        Playing._pollution += _barChange[5];
                        Playing._publicAnger += _barChange[6];
                        Playing._workerAnger += _barChange[7];
                        _barsUpdated = true;
                    }

                    _characterSource = CharacterSourceSelect(_emotion[2]);

                    _characterTexture.Initialize(new Vector2(_eventWindowPos.X + 20, _eventWindowPos.Y + 20),
                        1f,
                        new Rectangle((int)_characterSource.X, (int)_characterSource.Y, 320, 320),
                        0.1f);

                    break;

                case EventState.Resolution3:

                    if (_barsUpdated == false)
                    {
                        Playing._money += _barChange[8];
                        Playing._pollution += _barChange[9];
                        Playing._publicAnger += _barChange[10];
                        Playing._workerAnger += _barChange[11];
                        _barsUpdated = true;
                    }

                    _characterSource = CharacterSourceSelect(_emotion[3]);

                    _characterTexture.Initialize(new Vector2(_eventWindowPos.X + 20, _eventWindowPos.Y + 20),
                        1f,
                        new Rectangle((int)_characterSource.X, (int)_characterSource.Y, 320, 320),
                        0.1f);
                
                    break;

                case EventState.EventOver:

                    

                    break;
            }

            if (_eventState == EventState.EventOver &&
                Playing._eventActive)
            {
                
                _barsUpdated = false;
            }
        }


        public void Draw(SpriteBatch _spriteBatch)
        {
            switch (_eventState)
            {
                case EventState.NewEvent:
                    _fileIcon.Draw(_spriteBatch);
                    break;

                case EventState.Descision:
                    _eventText.Draw(_spriteBatch);
                    _nameText.Draw(_spriteBatch);
                    _but1Text.Draw(_spriteBatch);
                    _but2Text.Draw(_spriteBatch);
                    _but3Text.Draw(_spriteBatch);
                    _eventBox.Draw(_spriteBatch);
                    _characterTexture.Draw(_spriteBatch);
                    _jobTitleText.Draw(_spriteBatch);
                    break;
                
                case EventState.Resolution1:
                    _nameText.Draw(_spriteBatch);
                    _outcomeBox.Draw(_spriteBatch);
                    _outcome1Text.Draw(_spriteBatch);
                    _characterTexture.Draw(_spriteBatch);
                    _butCloseText.Draw(_spriteBatch);
                    _jobTitleText.Draw(_spriteBatch);
                    break;
                
                case EventState.Resolution2:
                    _nameText.Draw(_spriteBatch);
                    _outcomeBox.Draw(_spriteBatch);
                    _outcome2Text.Draw(_spriteBatch);
                    _characterTexture.Draw(_spriteBatch);
                    _butCloseText.Draw(_spriteBatch);
                    _jobTitleText.Draw(_spriteBatch);
                    break;
                
                case EventState.Resolution3:
                    _nameText.Draw(_spriteBatch);
                    _outcomeBox.Draw(_spriteBatch);
                    _outcome3Text.Draw(_spriteBatch);
                    _characterTexture.Draw(_spriteBatch);
                    _butCloseText.Draw(_spriteBatch);
                    _jobTitleText.Draw(_spriteBatch);
                    break;

                case EventState.EventOver:

                    break;
            }
        }

        private Vector2 CharacterSourceSelect(int selector)
        {
            switch (_characterName)
            {
                case "Gualberto":
                    switch (selector)
                    {
                        case 1:
                            return new Vector2(0, 0);
                            

                        case 2:
                            return new Vector2(320, 0);
                            

                        case 3:
                            return new Vector2(640, 0);
                            
                        case 4:
                            return new Vector2(960, 0);
                      
                    }
                    break;

                case "Susan":
                    switch (selector)
                    {
                        case 1:
                            return new Vector2(0, 320);


                        case 2:
                            return new Vector2(320, 320);


                        case 3:
                            return new Vector2(640, 320);

                        case 4:
                            return new Vector2(960, 320);

                    }
                    break;

                case "Gregg":
                    switch (selector)
                    {
                        case 1:
                            return new Vector2(0, 640);

                        case 2:
                            return new Vector2(320, 640);

                        case 3:
                            return new Vector2(640, 640);

                        case 4:
                            return new Vector2(960, 640);

                    }
                    break;

                case "Jeffy":
                    switch (selector)
                    {
                        case 1:
                            return new Vector2(0, 960);


                        case 2:
                            return new Vector2(320, 960);


                        case 3:
                            return new Vector2(640, 960);

                        case 4:
                            return new Vector2(960, 960);

                    }
                    break;

                case "Becca":
                    switch (selector)
                    {
                        case 1:
                            return new Vector2(0, 1280);


                        case 2:
                            return new Vector2(320, 1280);


                        case 3:
                            return new Vector2(640, 1280);

                        case 4:
                            return new Vector2(960, 1280);

                    }
                    break;

            }
            switch (selector)
            {
                case 1:
                    return new Vector2(0, 1600);

                case 2:
                    return new Vector2(320, 1600);

                case 3:
                    return new Vector2(640, 1600);

                case 4:
                    return new Vector2(0, 1920);

                case 5:
                    return new Vector2(320, 1920);

                case 6:
                    return new Vector2(640, 1920);
                
                case 7:
                    return new Vector2(0, 2240);
                
                case 8:
                    return new Vector2(320, 2240);
                
                case 9:
                    return new Vector2(640, 2240);

                case 10:
                    return new Vector2(0, 2560);

                case 11:
                    return new Vector2(320, 2560);

                case 12:
                    return new Vector2(640, 2560);

                case 13:
                    return new Vector2(0, 2880);

                case 14:
                    return new Vector2(320, 2880);

                case 15:
                    return new Vector2(640, 2880);

                case 16:
                    return new Vector2(0, 3200);

                case 17:
                    return new Vector2(320, 3200);

                case 18:
                    return new Vector2(0, 3520);

                case 19:
                    return new Vector2(320, 3520);

                case 20:
                    return new Vector2(640, 3520);

            }
            return new Vector2(640, 3200);

        }

        public enum EventState
        {
            NewEvent,
            Descision,
            Resolution1,
            Resolution2,
            Resolution3,
            EventOver
        }
             
    }
}
