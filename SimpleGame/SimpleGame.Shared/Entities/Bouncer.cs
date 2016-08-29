using System;
using System.Collections.Generic;
using CocosSharp;
using System.Diagnostics;

namespace SimpleGame
{
    public class Bouncer : CCNode
    {
        CCSprite sprite;

        public CCPoint Velocity;

        CCPoint InitialVelocity;

        public Bouncer() : base()
        {

            String spriteName = InitialShape();
            sprite = new CCSprite(spriteName);
            if(sprite == null)
            {
                throw new InvalidOperationException("sprite is null");
            }
            sprite.Scale = 2.0f;
            InitializeVelocity();
            sprite.AnchorPoint = CCPoint.AnchorMiddle;
            AddChild(sprite);
            ScheduleOnce(InitializePosition, 0f);
            Schedule(ApplyVelocity);
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Register for touch events
            var touchListener = new CCEventListenerTouchOneByOne();
            touchListener.IsSwallowTouches = true;
            touchListener.OnTouchBegan = OnTouchBegan;
            AddEventListener(touchListener, this);
        }


        bool OnTouchBegan(CCTouch touch, CCEvent touchEvent)
        {
                if (sprite.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    Velocity = new CCPoint(0,0);
                    return true;
                }
                else
                {
                    return false;
                    //Velocity = InitialVelocity;
                }
        }

        void InitializePosition(float unusedVariable)
        {
            float PositionX = CCRandom.GetRandomFloat(0 + sprite.ContentSize.Width, VisibleBoundsWorldspace.MaxX - sprite.ContentSize.Width);
            float PositionY = CCRandom.GetRandomFloat(0 + sprite.ContentSize.Height, VisibleBoundsWorldspace.MaxY - sprite.ContentSize.Height - 20);
            sprite.Position = new CCPoint(PositionX, PositionY);
        }

        void ApplyVelocity(float unusedVariable)
        {
            // New Position:   
            sprite.Position += Velocity;
            CheckReflection();
        }

        public void StartMoving()
        {
            Velocity = InitialVelocity;
        }

        void CheckReflection()
        {
            var bounds = VisibleBoundsWorldspace;
            float spriteRight = sprite.BoundingBoxTransformedToParent.MaxX;
            float spriteLeft = sprite.BoundingBoxTransformedToParent.MinX;
            float spriteTop = sprite.BoundingBoxTransformedToParent.MaxY;
            float spriteBottom = sprite.BoundingBoxTransformedToParent.MinY;
            // Screen Edges
            float screenRight = bounds.MaxX;
            float screenLeft = bounds.MinX;
            float screenTop = bounds.MaxY - 20;
            float screenBottom = bounds.MinY;
            // Horizontal Reflection:    
            bool shouldReflectXVelocity =
                (spriteRight > screenRight && Velocity.X > 0) ||
                (spriteLeft < screenLeft && Velocity.X < 0);
            if (shouldReflectXVelocity)
            {
                Velocity.X *= -1;
            }
            //Vertical Collision
            bool shouldReflectYVelocity =
                (spriteTop > screenTop && Velocity.Y > 0) ||
                (spriteBottom < screenBottom && Velocity.Y < 0);
            if (shouldReflectYVelocity)
            {
                Velocity.Y *= -1;
            }
            //Debug.WriteLine("Velocity.X: " + Velocity.X + " - Velocity.Y: " + Velocity.Y);

        }
        string InitialShape()
        {
            string[] shapeList = { "circle100", "oval100", "pentagon100", "rectangle100", "square100", "triangle100" };
            int r = CCRandom.Next(0, shapeList.Length);
            return shapeList[r];
        }

        void InitializeVelocity()
        {
            Velocity.X = CCRandom.GetRandomFloat(1, 2) * (CCRandom.Next(0, 2) * 2 - 1);
            Velocity.Y = CCRandom.GetRandomFloat(1, 3) * (CCRandom.Next(0, 2) * 2 - 1);
            InitialVelocity = Velocity;
        }

    }


}

