using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MyGame
{
    /// <summary>
    /// Class that represents a quad tree
    /// </summary>
    class QuadTree
    {
        #region Constants
        // The maximum number of objects in a quad
        // before a subdivision occurs
        private const int MAX_OBJECTS_BEFORE_SUBDIVIDE =240;
        #endregion

        #region Variables
        // The game objects held at this level of the tree
        private List<GameObject> _objects;

        // This quad's rectangle area
        private Rectangle _rect;

        // This quad's divisions
        private QuadTree[] _divisions;
        #endregion

        #region Properties
        /// <summary>
        /// The divisions of this quad
        /// </summary>
        public QuadTree[] Divisions { get { return _divisions; } }

        /// <summary>
        /// This quad's rectangle
        /// </summary>
        public Rectangle Rectangle { get { return _rect; } }

        /// <summary>
        /// The game objects inside this quad
        /// </summary>
        public List<GameObject> GameObjects { get { return _objects; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new Quad Tree
        /// </summary>
        /// <param name="x">This quad's x position</param>
        /// <param name="y">This quad's y position</param>
        /// <param name="width">This quad's width</param>
        /// <param name="height">This quad's height</param>
        public QuadTree(int x, int y, int width, int height)
        {
            // Save the rectangle
            _rect = new Rectangle(x, y, width, height);

            // Create the object list
            _objects = new List<GameObject>();

            // No divisions yet
            _divisions = null;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a game object to the quad.  If the quad has too many
        /// objects in it, and hasn't been divided already, it should
        /// be divided
        /// </summary>
        /// <param name="gameObj">The object to add</param>
        public void AddObject(GameObject gameObj)
        {
            // ACTIVITY: Complete this method

            //checking if the game object even lies in the rectangle
            if (_rect.Contains(gameObj.Rectangle))
            {
                _objects.Add(gameObj);
                //if division isnt null
                if (_divisions != null)
                {

                    //check if any of the division can add the object if yes then add it
                    for (int i = 0; i < 4; i++)
                    {
                        if (_divisions[i]._rect.Contains(gameObj.Rectangle))
                        {
                            _divisions[i].AddObject(gameObj);
                            //_objects.Remove(gameObj);
                            
                        }
                    }



                }




                //if the count of the objects is more than 9 then subdivide the quad
                if (_divisions == null)
                {
                    if (_objects.Count > MAX_OBJECTS_BEFORE_SUBDIVIDE)
                    {
                        Divide();
                    }
                }

            }



        }

        /// <summary>
        /// Divides this quad into 4 smaller quads.  Moves any game objects
        /// that are completely contained within the new smaller quads into
        /// those quads and removes them from this one.
        /// </summary>
        public void Divide()
        {
            // ACTIVITY: Complete this method
            //make divisions if their arent any




            //adding the subdivisions
            if (_divisions == null)
            {
                _divisions = new QuadTree[4];
                _divisions[0] = new QuadTree(_rect.X, _rect.Y, _rect.Width / 2, _rect.Height / 2);
                _divisions[1] = new QuadTree(_rect.X + (_rect.Width / 2), _rect.Y, _rect.Width / 2, _rect.Height / 2);
                _divisions[2] = new QuadTree(_rect.X, _rect.Y + (_rect.Height / 2), _rect.Width / 2, _rect.Height / 2);
                _divisions[3] = new QuadTree(_rect.X + (_rect.Width / 2), _rect.Y + (_rect.Height / 2), _rect.Width / 2, _rect.Height / 2);
            }
            //if the subdivision can fit the objects then add the object to its subdivision
            for (int i = _objects.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (_divisions[j]._rect.Contains(_objects[i].Rectangle))
                    {
                        _divisions[j].AddObject(_objects[i]);
                        //_objects.Remove(_objects[i]);
                        break;

                    }
                }
            }


        }

        /// <summary>
        /// Recursively populates a list with all of the rectangles in this
        /// quad and any subdivision quads.  Use the "AddRange" method of
        /// the list class to add the elements from one list to another.
        /// </summary>
        /// <returns>A list of rectangles</returns>
        public List<Rectangle> GetAllRectangles()
        {
            List<Rectangle> rects = new List<Rectangle>();

            // ACTIVITY: Complete this method
            rects.Add(_rect);

            //add all the quads from the subdivisions to this list

            if (_divisions != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    rects.AddRange(_divisions[i].GetAllRectangles());
                }
            }

            return rects;
        }

        /// <summary>
        /// A possibly recursive method that returns the
        /// smallest quad that contains the specified rectangle
        /// </summary>
        /// <param name="rect">The rectangle to check</param>
        /// <returns>The smallest quad that contains the rectangle</returns>
        public QuadTree GetContainingQuad(Rectangle rect)
        {
            // ACTIVITY: Complete this method

            //if it does not contain the rectangle then return null
            if (!_rect.Contains(rect))
            {
                return null;
            }

            //else check if any of the subdivisions contain it
            else
            {
                if (_divisions != null)
                {

                    for (int i = 0; i < 4; i++)
                    {
                        if (_divisions[i]._rect.Contains(rect))
                        {
                            return _divisions[i].GetContainingQuad(rect);
                        }

                        if (i == 3)
                            return this;

                        
                    }


                }

                //if this is the smallest division that contains it then return this
                else
                {
                    return this;
                }

            }



            // Return null if this quad doesn't completely contain
            // the rectangle that was passed in
            return null;
        }
        #endregion

        //method to cear the quad tree
        public void Clear()
        {
            _objects.Clear();
            if (_divisions != null)
            {
                _divisions = null;
            }
        }

        /// <summary>
        /// Method to retrieve all possible objects that could collide with the current object
        /// </summary>
        /// <param name="rect">Object to check the collision with</param>
        /// <returns></returns>
        public List<GameObject> Retrieve(Rectangle rect)
        {
            QuadTree containingQuad = GetContainingQuad(rect);
            List<GameObject> returnObjects = new List<GameObject>();

            if (containingQuad != null)
            {
                returnObjects.AddRange(containingQuad.GameObjects);
            }

            

            return returnObjects;

        }

   
    }
}
