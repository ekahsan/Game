using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Collections.Generic;

namespace GameTest
{


    class AICar : Car
    {
        Vector2[] path;
        Vector2 destination;
        int pathstage = 0;
        public float rotation;
        Vector2 velocity;

        public void Init(Texture2D t,  Vector2[] p)
        {
            path = p;
            base.Init(t, p[pathstage]);
            pathstage++;
            speed = 9f;
            steer = 2;
        }

        public void Update(GameTime gameTime)
        {
            if (pathstage == path.Length)
                pathstage = 0;
            destination = path[pathstage];
            Console.WriteLine(destination + " " + position);
            Vector2 increment = (destination - position) / (Vector2.Distance(destination,position)/speed);
            position += increment;
            velocity = increment;
            float angle = (float)Math.Atan(velocity.Y / velocity.X) * (float)(180 / Math.PI);
            Console.WriteLine(angle);
            //Console.WriteLine(angle);
            if (angle < 45 && angle >= 0)
                    rotation = (270 * (float)Math.PI)/180;
            //velocity.Y pos = down screen,
            if (angle <= 45 && angle < 90 && (velocity.X > 0))
                rotation = (360 * (float)Math.PI) /180 ;

           
            if (angle < 0 && angle >= -45)
                rotation = (180 * (float)Math.PI) /180 ;

            if (angle < -45 && angle >= -90 && (velocity.Y > 0))
                rotation = (90 * (float)Math.PI) /180 ;



            //position = position + new Vector2(speed * (float)Math.Cos(steer), speed * (float)Math.Sin(steer));
            if (Math.Abs(destination.X-position.X)<10 && Math.Abs(destination.Y - position.Y) < 10)
                pathstage++;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 viewport)
        {
            viewport += new Vector2(640, 360);
            spriteBatch.Draw(texture, position - (viewport), null, null, origin, rotation, null, null, SpriteEffects.FlipHorizontally, 0);
        }
    }

    class Car
    {

        Texture2D cartexture;
        Car car;
        public float speed = 0;
        public Texture2D texture;
        Vector2 center;
        public Vector2 position;
        public Vector2 origin;
        public float steer = 0;

        public void Init(Texture2D t, Vector2 pos)
        {
            position = pos;
            center = pos + new Vector2(40,20);
            texture = t;
            origin = new Vector2(t.Width / 2, t.Height / 2);
        }
        public void GetInputs(KeyboardState input)
        {
            if (input.IsKeyDown(Keys.Down) && speed < 15)
            {
                speed += 3;
            }

            if (input.IsKeyDown(Keys.Up) && speed > -20)
            {
                speed -= 1;
            }

            if (speed > 0)
                speed -= 1;
            else if (speed < 0)
                speed += .5f;
            else
                speed = 0;
           
            {
                if (input.IsKeyDown(Keys.Left))
                {
                    steer -= (.15f);
                }

                if (input.IsKeyDown(Keys.Right))
                {
                    steer += (.15f);
                }
            }
        }
        public void Update(GameTime gameTime)
        {
            GetInputs(Keyboard.GetState());
            position = position + new Vector2(speed * (float)Math.Cos(steer), speed * (float)Math.Sin(steer));
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 viewport)
        {
            spriteBatch.Draw(texture, position - viewport, null, null, origin, steer, null, null, SpriteEffects.FlipHorizontally, 0);
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

    }

}
