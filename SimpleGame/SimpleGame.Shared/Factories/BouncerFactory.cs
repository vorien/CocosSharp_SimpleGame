using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using System.Diagnostics;

namespace SimpleGame
{
    class BouncerFactory
    {

        //static Lazy<BouncerFactory> self =
        //    new Lazy<BouncerFactory>(() => new BouncerFactory());

        //// simple singleton implementation
        //public static BouncerFactory Self
        //{
        //    get
        //    {
        //        return self.Value;
        //    }
        //}

        public CCLayer layer { get; set; }

        public event Action<Bouncer> BouncerCreated;

        public BouncerFactory()
        {

        }

        public Bouncer CreateNew()
        {
            Bouncer newBouncer = new Bouncer();

            if (layer == null)
            {
                throw new InvalidOperationException("Need to set Layer before spawning");
            }
            Debug.WriteLine(layer.BoundingBox);
            Debug.WriteLine(layer.BoundingBoxTransformedToWorld);
            Debug.WriteLine(layer.BoundingBoxTransformedToParent);
            //Debug.WriteLine(Layer.Scene.VisibleBoundsScreenspace);
            float PositionX = CCRandom.GetRandomFloat(0 + newBouncer.ContentSize.Width, layer.ContentSize.Width - newBouncer.ContentSize.Width);
            float PositionY = CCRandom.GetRandomFloat(0 + newBouncer.ContentSize.Height, layer.ContentSize.Height - newBouncer.ContentSize.Height);
            newBouncer.Position = new CCPoint(PositionX, PositionY);
            Debug.WriteLine("PositionX: " + PositionX + " - PositionY: " + PositionY + " - newBouncer.Position: " + newBouncer.Position);


            if (BouncerCreated != null)
            {
                BouncerCreated(newBouncer);
            }

            return newBouncer;
        }
    }
}

