using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project2
{
    public class Camera
    {
        public Matrix transform { get; private set; }
        public Viewport view { get; set; }
        public Vector2 center { get; set; }
        public Vector2 Position;
        Vector2 playerPositionInWorldSpace;
        Vector2 boundaries;


        public Camera(Viewport newView)
        {
            view = newView;
            Origin = new Vector2(newView.X / 2, newView.Y / 2);
            Zoom = 1.0f;
        }

        public Vector2 Origin { get; set; }
        public float Zoom { get; set; }
        public float Rotation { get; set; }

        public void setBoundaries(int x, int y)
        {
            boundaries = new Vector2(x, y);

        }

        public Matrix GetViewMatrix() //was Vector2 parallax
        {
            
            return Matrix.CreateTranslation(new Vector3(-Position * 0.5f, 0.0f)) *
                
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                //Matrix.CreateRotationZ(Rotation) *   //Rotation don't think this is necessary
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public Vector2 position
        {
            get { return Position; }
            set
            {
                Position = value;

                //// If there's a limit set and the camera is not transformed clamp position to limits
                //if (Limits != null && Zoom == 1.0f && Rotation == 0.0f)
                //{
                //    Position.X = MathHelper.Clamp(Position.X, Limits.Value.X, Limits.Value.X + Limits.Value.Width - view.Width);
                //    Position.Y = MathHelper.Clamp(Position.Y, Limits.Value.Y, Limits.Value.Y + Limits.Value.Height - view.Height);
                //}
            }
        }

        public void ResetCamera()
        {
            Position = Origin;
        }

        public void LookAt(Vector2 position)
        {
            Position = position - new Vector2(view.X/2, view.Y);
        }

        //public Rectangle? Limits
        //{
        //    get { return _limits; }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            // Assign limit but make sure it's always bigger than the viewport
        //            _limits = new Rectangle
        //            {
        //                X = value.Value.X,
        //                Y = value.Value.Y,
        //                Width = System.Math.Max(view.Width, value.Value.Width),
        //                Height = System.Math.Max(view.Height, value.Value.Height)
        //            };

        //            // Validate camera position with new limit
        //            Position = Position;
        //        }
        //        else
        //        {
        //            _limits = null;
        //        }
        //    }
        //}


        //private Rectangle? _limits;

        public void Update(GameTime gameTime, Player player)
        {

            playerPositionInWorldSpace = player.position;

            if (view.Width < boundaries.X)
            {
                if (playerPositionInWorldSpace.X >= view.Width / 2)
                {
                    if (playerPositionInWorldSpace.X <= boundaries.X - view.Width)
                    {
                        Position = playerPositionInWorldSpace * new Vector2(2, 0);
                        //LookAt(playerPositionInWorldSpace * new Vector2(2,0));
                    }
                }
                else if (playerPositionInWorldSpace.Y <= boundaries.Y)
                {
                    ResetCamera();

                }
            }
        }

        //public void Update(GameTime gameTime, Player player)
        //{
            

        //    playerPositionInWorldSpace = player.position;

        //    if (playerPositionInWorldSpace.X >= boundaries.X/2)
        //    {
        //        Position = playerPositionInWorldSpace* new Vector2(2, 1);
        //    }
            
        //    else if (playerPositionInWorldSpace.Y <= boundaries.Y)
        //    {
        //        ResetCamera();
                
        //    }
        
            
            
        //}
    }
}
         
            //Find the center and create the transform matrix
            //center = new Vector2(Position.X, Position.Y);  
            //transform = Matrix.CreateScale(new Vector3(1,1,0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0) );
        
    

