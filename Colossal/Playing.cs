using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Factory_Game.Scenes;
using Factory_Game.Controls;
using System.IO;

namespace Factory_Game.Scenes
{

  
    class Playing : Scene
    {

        public static int _money, _pollution, _publicAnger, _workerAnger;

        private UI _tutorial1, _tutorial2, _tutorial3;
        public static int _tutorialNumber;

        private UI _monthBox;
        public static UI _moneyBar, _pollutionBar, _publicRepBar, _workforceRepBar;
        public static UI _moneyIcon, _pollutionIcon, _publicRepIcon, _workforceRepIcon;

        private Text _monthText;
        private Text _nextMonthText;

        private Vector2 _monBarPos, _polBarPos, _pubBarPos, _worBarPos;
        private Vector2 _monthBoxPos;

        public static Bars _moneyFill, _pollutionFill, _publicFill, _workforceFill;

        private Animations _truckCentipedeTheMovie;
        private Animations _smoke1, _smoke2;
        private Animations _moneyAlert, _pollutionAlert, _publicAlert, _workforceAlert;
        private Animations _nextMonthBox;

        private Texture2D _eventBoxTexture;
        private Texture2D _characterSpriteSheet;
        private Texture2D _barTexture;
        private Texture2D _barIconSpriteSheet;
        private Texture2D _barColours;
        private Texture2D _outcomeBoxTexture;
        private Texture2D _fileTexture;

        private SpriteFont _eventFont, _nameFont;

        private bool _debugMode = false;
        private Text _testEventNumber;

        public static int _month;
        private int _eventNumber = 0;
        public static int _numberGenericEvents = 24;
        public static int _numberMajorEvents = 4;

        private bool[] _genericEventUsed = new bool[_numberGenericEvents + _numberMajorEvents];
        public static bool _eventActive;

        private Random _rng = new Random();

        public enum FactoryState
        {
            Tutorial,
            Coal      
        }

        public static FactoryState _factoryState;

        Events _events;

        //declares string to store raw events data and then the seperated data
        private string[] _rawGenericInfo = new string[_numberGenericEvents + _numberMajorEvents];
        private string[,] _genericEventText = new string[_numberGenericEvents + _numberMajorEvents, 9];
        private int[,] _genericEventsNumbers = new int[_numberGenericEvents + _numberMajorEvents, 17];
        public Playing() : base()
        {
            _factoryState = FactoryState.Tutorial;
            _tutorialNumber = 1;
        }

        public override void Initialize()
        {
           //sets starting month to 1
            _month = 1;

            //sets the event as active so it can be selected
            _eventActive = false;
            
            //declaring the positions of bars
            _polBarPos = new Vector2(100, 60);
            _monBarPos = new Vector2(100, 160);
            _worBarPos = new Vector2(724, 60);
            _pubBarPos = new Vector2(724, 160);
            _monthBoxPos = new Vector2(540 - 127, 0);

            //declares bars starting values
            _money = 8;
            _pollution = 4;
            _workerAnger = 4;
            _publicAnger = 4;
            _events = new Events();
            _events.Initialise();

            //reading in the event spreadsheet
            _rawGenericInfo = File.ReadAllLines(@"GenericEvents.csv");

            (_genericEventText, _genericEventsNumbers) = GetEventInfo(1, _numberGenericEvents + _numberMajorEvents, 9, 17, _rawGenericInfo);

            base.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            //Loading Textures and fonts for the UI
            _eventFont = content.Load<SpriteFont>(@"EventFont");
            _nameFont = content.Load<SpriteFont>(@"NameFont");

            _eventBoxTexture = content.Load<Texture2D>(@"EventBox");
            _characterSpriteSheet = content.Load<Texture2D>(@"CharacterSpriteSheet");
            _barTexture = content.Load<Texture2D>(@"Bar");
            _barIconSpriteSheet = content.Load<Texture2D>(@"IconSpriteSheet");
            _barColours = content.Load<Texture2D>(@"BarColours");
            _outcomeBoxTexture = content.Load<Texture2D>(@"OutcomeBox");
            _fileTexture = content.Load<Texture2D>(@"File");

            _monthBox = new UI(content.Load<Texture2D>(@"MonthDisplay"));
            _monthBox.Initialize(_monthBoxPos,
                1f,
                new Rectangle(0, 0, 255, 79),
                0.8f);

            _nextMonthText = new Text(new Vector2(1080 - (213 / 2), 920 - (87 / 2)),
                "Next Month",
                _nameFont,
                100,
                0.2f,
                Color.Black);

            _nextMonthBox = new Animations(content.Load<Texture2D>(@"NextButtonSheet"));
            _nextMonthBox.Inintialize(new Vector2(1080 - 213, 920 - 87),
                new Vector2(1080 - 213, 920 - 87),
                1f,
                new Rectangle(0, 0, 213, 87),
                213,
                213,
                0.9f,
                0f,
                0.3f,
                1);

            _smoke1 = new Animations(content.Load<Texture2D>(@"CoalFactorySmokeA_SpriteSheet"));
            _smoke1.Inintialize(new Vector2(110, 0),
                new Vector2(110, 0),
                1f,
                new Rectangle(0, 0, 450, 382),
                450,
                900,
                0.9f,
                0f,
                0.2f,
                1);

            _smoke2 = new Animations(content.Load<Texture2D>(@"CoalFactorySmokeB_SpriteSheet"));
            _smoke2.Inintialize(new Vector2(20, -22),
                new Vector2(20, -22),
                1f,
                new Rectangle(0, 0, 450, 382),
                450,
                900,
                0.9f,
                0f,
                0.2f,
                2);
            
            _moneyAlert = new Animations(_barIconSpriteSheet);
            _moneyAlert.Inintialize(new Vector2(_monBarPos.X + 256, _monBarPos.Y + 10),
                new Vector2(_monBarPos.X + 256, _monBarPos.Y + 10),
                0.1f,
                new Rectangle(0, 0, 320, 320),
                320,
                320,
                0.4f,
                0f,
                0.1f,
                1);

            _pollutionAlert = new Animations(_barIconSpriteSheet);
            _pollutionAlert.Inintialize(new Vector2(_polBarPos.X + 256, _polBarPos.Y + 10),
                new Vector2(_polBarPos.X + 256, _polBarPos.Y + 10),
                0.1f,
                new Rectangle(0, 0, 320, 320),
                320,
                320,
                0.4f,
                0f,
                0.1f,
                1);

            _workforceAlert = new Animations(_barIconSpriteSheet);
            _workforceAlert.Inintialize(new Vector2(_worBarPos.X - 32, _worBarPos.Y + 10),
                new Vector2(_worBarPos.X - 32, _worBarPos.Y + 10),
                0.1f,
                new Rectangle(0, 0, 320, 320),
                320,
                320,
                0.4f,
                0f,
                0.1f,
                1);

            _publicAlert = new Animations(_barIconSpriteSheet);
            _publicAlert.Inintialize(new Vector2(_pubBarPos.X - 32, _pubBarPos.Y + 10),
                new Vector2(_pubBarPos.X - 32, _pubBarPos.Y + 10),
                0.1f,
                new Rectangle(0, 0, 320, 320),
                320,
                320,
                0.4f,
                0f,
                0.1f,
                1);

            _truckCentipedeTheMovie = new Animations(content.Load<Texture2D>(@"TruckCentipedeTheMovie"));
            _truckCentipedeTheMovie.Inintialize(new Vector2(-85, 920 - 49),
                new Vector2(1165, 920 - 49),
                1f,
                new Rectangle(0, 0, 85, 49),
                85,
                170,
                0.9f,
                18f,
                0f,
                1);

            //sending variables for and textures for drawing all bar sprites
            //all bar positions
            //The scale
            //The source rectangle
            //The and the depth
            _pollutionBar = new UI(_barTexture);
            _pollutionBar.Initialize(_polBarPos,
                1f,
                new Rectangle(0, 0, 256, 79),
                0.2f);

            _moneyBar = new UI(_barTexture);
            _moneyBar.Initialize(_monBarPos,
                1f,
                new Rectangle(0, 0, 256, 79),
                0.2f);

            _workforceRepBar = new UI(_barTexture);
            _workforceRepBar.Initialize(_worBarPos,
                1f,
                new Rectangle(0, 0, 256, 79),
                0.2f);

            _publicRepBar = new UI(_barTexture);
            _publicRepBar.Initialize(_pubBarPos,
                1f,
                new Rectangle(0, 0, 256, 79),
                0.2f);

            _pollutionIcon = new UI(_barIconSpriteSheet);
            _pollutionIcon.Initialize(new Vector2(_polBarPos.X - (50 + 40), _polBarPos.Y + (39 - 40)),
                0.25f,
                new Rectangle(2080, 0, 320, 320),
                0.2f);

            _moneyIcon = new UI(_barIconSpriteSheet);
            _moneyIcon.Initialize(new Vector2(_monBarPos.X - (50 + 28), _monBarPos.Y + (39 - 36)),
                0.15f,
                new Rectangle(2518, 0, 370, 480),
                0.2f);

            _workforceRepIcon = new UI(_barIconSpriteSheet);
            _workforceRepIcon.Initialize(new Vector2(_worBarPos.X + (305 - 33), _worBarPos.Y + (39 - 30)),
                0.15f,
                new Rectangle(2910, 0, 440, 410),
                0.2f);

            _publicRepIcon = new UI(_barIconSpriteSheet);
            _publicRepIcon.Initialize(new Vector2(_pubBarPos.X + (305 - 28), _pubBarPos.Y + (39 - 25)),
                0.3f,
                new Rectangle(640, 0, 190, 170),
                0.2f);

            _tutorial1 = new UI(content.Load<Texture2D>(@"Tutorial1MockUp"));
            _tutorial1.Initialize(new Vector2(540 - (966 / 2), 460 - (335 / 2)),
                1f,
                new Rectangle(0, 0, 962, 335),
                0.2f);

            _tutorial2 = new UI(content.Load<Texture2D>(@"Tutorial2MockUp"));
            _tutorial2.Initialize(new Vector2(540 - (966 / 2), 460 - (335 / 2)),
                1f,
                new Rectangle(0, 0, 962, 335),
                0.2f);

            _tutorial3 = new UI(content.Load<Texture2D>(@"Tutorial3MockUp"));
            _tutorial3.Initialize(new Vector2(540 - (966 / 2), 460 - (335 / 2)),
                1f,
                new Rectangle(0, 0, 962, 335),
                0.2f);

            //Sending the textures to the bars class, and position/colour to
            //it's initialise function for drawing the bars as they increase and decrease
            //This is doe for all 4 Bars
            _moneyFill = new Bars(_barColours);
            _moneyFill.Inintialise(_monBarPos, 1);

            _pollutionFill = new Bars(_barColours);
            _pollutionFill.Inintialise(_polBarPos, 2);

            _workforceFill = new Bars(_barColours);
            _workforceFill.Inintialise(_worBarPos, 3);

            _publicFill = new Bars(_barColours);
            _publicFill.Inintialise(_pubBarPos, 4);

            _monthText = new Text(new Vector2(_monthBoxPos.X + 127, _monthBoxPos.Y + 39),
                "Month:  " + _month,
                _nameFont,
                40,
                0.6f,
                Color.Black);
            
            EventSelector();

            //calls/sends fonts and textures to the Load function of the events class
            _events.Load(_characterSpriteSheet, _eventBoxTexture, _eventFont, _nameFont, _barIconSpriteSheet, _outcomeBoxTexture, _fileTexture);

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Game game)
        {

            if (_month >= 25) Game1._gameState = GameState.GameWon;

            _money = Math.Clamp(_money, 0, 10);
            _pollution = Math.Clamp(_pollution, 0, 10);
            _publicAnger = Math.Clamp(_publicAnger, 0, 10);
            _workerAnger = Math.Clamp(_workerAnger, 0, 10);

            _smoke1.Update(gameTime, game);
            _smoke2.Update(gameTime, game);
            _truckCentipedeTheMovie.Update(gameTime, game);

            _nextMonthBox.Update(gameTime, game);


            _moneyAlert.Update(gameTime, game);
            _pollutionAlert.Update(gameTime, game);
            _workforceAlert.Update(gameTime, game);
            _publicAlert.Update(gameTime, game);
            if (!(_debugMode))
            {
                if (_money == 0 || _pollution == 10 || _publicAnger == 10 || _workerAnger == 10)
                    Game1._gameState = GameState.GameOver;
            }

            //Updates all bars with their values
            _moneyFill.Update(_money);
            _pollutionFill.Update(_pollution);
            _workforceFill.Update(_workerAnger);
            _publicFill.Update(_publicAnger);

            EventSelector();

            //calls the events and controls update function
            _events.Update(gameTime, game);

            base.Update(gameTime, game);
        }

        public override void Draw(SpriteBatch _spriteBatch, Game game)
        {
            //calls and sends spritebatch to the draw functions of all our classes draw functions used
            if (_eventActive) _events.Draw(_spriteBatch);

            if (Events._eventState == Events.EventState.NewEvent)
            {
                _monthText = new Text(new Vector2(_monthBoxPos.X + 127, _monthBoxPos.Y + 39),
                "Month:  " + _month,
                _nameFont,
                40,
                0.6f,
                Color.Black);

                _testEventNumber = new Text(new Vector2(100, 40),
                    "Event Number:\n" + _eventNumber,
                    _nameFont,
                    100,
                    0.9f,
                    Color.Black);
            }
            
            if (Events._eventState == Events.EventState.EventOver)
            {
                _nextMonthText.Draw(_spriteBatch);
                _nextMonthBox.Draw(_spriteBatch);
            }

            if (_factoryState == FactoryState.Tutorial)
            {
                switch (_tutorialNumber)
                {
                    case 1:
                        _tutorial1.Draw(_spriteBatch);
                        break;

                    case 2:
                        _tutorial2.Draw(_spriteBatch);
                        break;

                    case 3:
                        _tutorial3.Draw(_spriteBatch);
                        break;
                }
            }
            _moneyFill.Draw(_spriteBatch);
            _pollutionFill.Draw(_spriteBatch);
            _workforceFill.Draw(_spriteBatch);
            _publicFill.Draw(_spriteBatch);

            _pollutionBar.Draw(_spriteBatch);
            _moneyBar.Draw(_spriteBatch);
            _workforceRepBar.Draw(_spriteBatch);
            _publicRepBar.Draw(_spriteBatch);

            _pollutionIcon.Draw(_spriteBatch);
            _moneyIcon.Draw(_spriteBatch);
            _workforceRepIcon.Draw(_spriteBatch);
            _publicRepIcon.Draw(_spriteBatch);

            _monthBox.Draw(_spriteBatch);
            _monthText.Draw(_spriteBatch);

            _smoke1.Draw(_spriteBatch);
            _smoke2.Draw(_spriteBatch);
            _truckCentipedeTheMovie.Draw(_spriteBatch);

            if(_money <= 2) _moneyAlert.Draw(_spriteBatch);
            if (_pollution >= 8) _pollutionAlert.Draw(_spriteBatch);
            if (_workerAnger >= 8) _workforceAlert.Draw(_spriteBatch);
            if (_publicAnger >= 8) _publicAlert.Draw(_spriteBatch);

            if (_debugMode == true) _testEventNumber.Draw(_spriteBatch);

            base.Draw(_spriteBatch, game);
        }

        public void EventSelector()
        {
            
            if(_eventNumber == 0) _eventNumber = _rng.Next(1, _numberGenericEvents + 1);

            if (!_eventActive)
            {
                if (!_genericEventUsed[_eventNumber - 1])
                {
                    _events = new Events(_genericEventText[_eventNumber - 1, 0],
                         _genericEventText[_eventNumber - 1, 1],
                         new int[] { _genericEventsNumbers[_eventNumber - 1, 1], _genericEventsNumbers[_eventNumber - 1, 14], _genericEventsNumbers[_eventNumber - 1, 15], _genericEventsNumbers[_eventNumber - 1, 16] },
                         new int[] { _genericEventsNumbers[_eventNumber - 1, 2], _genericEventsNumbers[_eventNumber - 1, 3], _genericEventsNumbers[_eventNumber - 1, 4], _genericEventsNumbers[_eventNumber - 1, 5],
                                    _genericEventsNumbers[_eventNumber - 1, 6], _genericEventsNumbers[_eventNumber - 1, 7], _genericEventsNumbers[_eventNumber - 1, 8], _genericEventsNumbers[_eventNumber - 1, 9],
                                    _genericEventsNumbers[_eventNumber - 1, 10], _genericEventsNumbers[_eventNumber - 1, 11], _genericEventsNumbers[_eventNumber - 1, 12], _genericEventsNumbers[_eventNumber - 1, 13] },
                         _genericEventText[_eventNumber - 1, 2],
                         new string[] { _genericEventText[_eventNumber - 1, 3], _genericEventText[_eventNumber - 1, 4], _genericEventText[_eventNumber - 1, 5] },
                         new string[] { _genericEventText[_eventNumber - 1, 6], _genericEventText[_eventNumber - 1, 7], _genericEventText[_eventNumber - 1, 8] });

                    _events.Load(_characterSpriteSheet, _eventBoxTexture, _eventFont, _nameFont, _barIconSpriteSheet, _outcomeBoxTexture, _fileTexture);

                    _genericEventUsed[_eventNumber - 1] = true;
                    _eventActive = true;
                }
                else
                {
                    if (_month == 6 || _month == 12 || _month == 18 || _month == 24)
                    {
                        _eventNumber = _rng.Next(_numberGenericEvents + 1, _numberGenericEvents + _numberMajorEvents + 1);
                    }
                    else
                    {
                        _eventNumber = _rng.Next(1, _numberGenericEvents + 1);
                    }
                    
                }
            }
                
        }

        public (string[,], int[,]) GetEventInfo(int startingLine , int numberOfEvents, int numberOfTextCells, int numberOfNumberCells, string[] input)
        {
            
            string[,] outputText = new string[numberOfEvents, numberOfTextCells];
            int[,] outputNumber = new int[numberOfEvents, numberOfNumberCells];

            for (int line = startingLine; line <= numberOfEvents; line++)
            {

                int cellText = 0;
                int cellNumber = 0;

                bool isText = true;
                bool newCell = true;

                string currentNumber = "";

                for (int character = 0; character < input[line].Length; character++)
                {
                    
                    if (newCell == true)
                    {
                        if ((input[line][character] >= 48 && input[line][character] <= 57) 
                            || (input[line][character] == 43 || input[line][character] == 45))
                        {
                            isText = false;
                        }
                        else
                        {
                            isText = true;
                        }
                        newCell = false;
                    }
                    
                    if (isText == false) 
                    {
                        if (input[line][character] == ',')
                        {
                            int.TryParse(currentNumber, out outputNumber[line - 1, cellNumber]);
                            currentNumber = "";

                            cellNumber++;
                            newCell = true;
                        }
                        else
                        {
                            currentNumber += input[line][character];
                        }
                    }
                    else
                    {
                        if (input[line][character] == ',')
                        {
                            cellText++;
                            newCell = true;
                        }
                        else if (input[line][character] == '@')
                        {
                            outputText[line - 1, cellText] += ',';
                        }
                        else if(input[line][character] == '%')
                        {
                            outputText[line - 1, cellText] += '"';
                        }
                        else
                        {
                            outputText[line - 1, cellText] += input[line][character];
                        }
                    }
                }
            }
            
            return (outputText, outputNumber);
        }
    }
}
