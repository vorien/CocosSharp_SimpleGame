using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace SimpleGame.Shared
{
    public class GameLayer : CCLayerColor
    {

        CCLabel label;
        List<Bouncer> bouncers;
        Random rnd = new Random();
        //BouncerFactory factory;
        //List<CCSprite> sprites;
        Bouncer newBouncer;
        float elapsedTime = 0;

        public GameLayer() : base(CCColor4B.Black)
        {
            // Register for touch events
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesBegan = OnTouchesBegan;
            AddEventListener(touchListener, this);

            label = new CCLabel("A Simple Game", "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            AddChild(label);

            bouncers = new List<Bouncer>();

            for(int ctr = 0; ctr < 5; ctr++)
            {
                newBouncer = new Bouncer();
                AddChild(newBouncer);
                bouncers.Add(newBouncer);
            }

            Schedule(RunGameLogic);

        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Use the bounds to layout the positioning of our drawable assets
            var bounds = VisibleBoundsWorldspace;

            // position the label on the top left of the screen
            label.PositionX = bounds.MinX + 20;
            label.PositionY = bounds.MaxY;
            label.AnchorPoint = CCPoint.AnchorUpperLeft;
            label.Text = "SimpleGame is running.";

            // Register for touch events
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesBegan = OnTouchesBegan;
            AddEventListener(touchListener, this);
        }

        void OnTouchesBegan(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                for(int ctr = 0; ctr < bouncers.Count; ctr++) { 
                    bouncers[ctr].StartMoving();
                };
                //elapsedTime = 0;
                //if (label.BoundingBoxTransformedToWorld.ContainsPoint(touches[0].Location))
                //{
                //    label.Text = "Adding a Bouncer";
                //} else
                //{
                //    label.Text = "Label Changed based on other touch point";
                //}
            }
        }

        void RunGameLogic(float frameTimeInSeconds)
        {
            elapsedTime += frameTimeInSeconds;
            label.Text = "elapsedTime: " + elapsedTime;
            if (GameOver())
            {
                UnscheduleAll();
                label.Text = "You Win!\nElapsedTime: " + elapsedTime;
                RemoveAllListeners();
            }
        }

        bool GameOver()
        {
            for (int ctr = 0; ctr < bouncers.Count; ctr++)
            {
                //Debug.WriteLine("Velocity");
                //Debug.WriteLine(bouncers[ctr].Velocity);
                if (bouncers[ctr].Velocity != new CCPoint(0, 0))
                {
                    Debug.WriteLine("False");
                    return false;
                }
            };
            Debug.WriteLine("True");
            return true;
        }

        //void AddBouncer()
        //{
        //    Bouncer newBouncer = factory.CreateNew();
        //}

        //void HandleBouncerCreated(Bouncer newBouncer)
        //{
        //    AddChild(newBouncer);
        //    Debug.WriteLine("VBW");
        //    Debug.WriteLine(this);
        //    Debug.WriteLine(VisibleBoundsWorldspace);
        //    bouncers.Add(newBouncer);
        //}


    }
}

