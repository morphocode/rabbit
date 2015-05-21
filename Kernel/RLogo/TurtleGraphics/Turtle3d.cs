using System;
using System.Collections.Generic;
using System.Text;

using Rhino.Geometry;
//using RMA.Rhino;
using RMA.OpenNURBS;

using Rabbit.Kernel.TurtleGraphics.Commands;

namespace Rabbit.Kernel.TurtleGraphics
{
    public class Turtle3d
    {

        /**
         * Stack used to store the state of the turtle in a given moment that is later restored again
         */ 
        private Stack<TurtleState> _turtleStatesStack;

        private Stack<Branch> branchesStack;

        private IList<Branch> branches;

        private Canvas _canvas;

        /** default step length */
        private double defaultStepLength;
        /** s - used to scale the step length at each generation */
        private double defaultStepLengthScale;

        private double defaultAngleIncrement;

        private double defaultThickness;
        private double defaultThicknessScale;

        /** current state of the Turtle */
        //private TurtleState currentTurtleState;
        private Point3d currentPosition;
        private Plane currentOrientation;
        private double currentStepLength;
        private double currentThickness;
        private Curve currentProfile;

        private Boolean loftSkeleton = false;

        private Curve defaultProfile = null;
        private Plane profilePivot = Plane.Unset;

        /**
         * Constructor
         */
        public Turtle3d(Canvas canvas, Point3d position, Plane orientation, double defaultStep, double defaultStepLengthScale, double defaultAngleIncrement, Curve defaultProfile, Plane profilePivot, double defaultThickness, double defaultThicknessScale, Boolean loftSkeleton)//, Pen pen)
        {
            this._canvas = canvas;

            //step length + step length scale
            this.defaultStepLength = defaultStep;
            this.defaultStepLengthScale = defaultStepLengthScale;
            //angle + angle scale
            this.defaultAngleIncrement = defaultAngleIncrement;
            //thickness + thickness scale
            this.defaultProfile = defaultProfile;
            this.profilePivot = profilePivot;
            this.defaultThickness = defaultThickness;
            this.defaultThicknessScale = defaultThicknessScale;

            this.loftSkeleton = loftSkeleton;

            //init the stack
            _turtleStatesStack = new Stack<TurtleState>();

            branchesStack = new Stack<Branch>();
            Branch rootBranch = new Branch(0);
            branchesStack.Push(rootBranch);

            branches = new List<Branch>();
            branches.Add(rootBranch);

            //init the current state of the turle using the default values
            //TurtleState initialTurtleState = new TurtleState(position, orientation, defaultStepLength);
            currentPosition = position;
            currentOrientation = orientation;
            currentStepLength = defaultStepLength;
            currentThickness = defaultThickness;
            currentProfile = CreateProfile();
            rootBranch.AddProfile(currentProfile);
        }

        public Vector3d GetHeading()
        {
            return GetLocalXAxis();
        }        


        /** Delegates to the current state */
        public double GetCurrentStepLength()
        {
            return currentStepLength;
        }

        /** Modifies the current turtle state */
        public void SetCurrentStepLength(double newStepLength)
        {
            currentStepLength = newStepLength;
        }

        /** Delegates to the current state */
        public double GetStepLengthScale()
        {
            return defaultStepLengthScale;
        }
        
        public double GetDefaultAngleIncrement()
        {
            return defaultAngleIncrement;
        }

        private double GetDefaultStep() {
            return defaultStepLength;
        }

        private Vector3d GetLocalXAxis()
        {
            return currentOrientation.YAxis;
            //return currentOrientation.ZAxis;
        }

        private Vector3d GetLocalYAxis()
        {
            //return currentOrientation.XAxis;
            return currentOrientation.XAxis;
        }

        private Vector3d GetLocalZAxis()
        {
            //return currentOrientation.YAxis;
            return currentOrientation.ZAxis;
        }

        private Branch CurrentBranch()
        {
            return branchesStack.Peek();
        }

        public IList<Branch> GetBranches()
        {
            return branches;
        }

        //TURTLE BEHAVIOR ======================================================
        public void MoveForward()
        {
            Vector3d stepVector = new Vector3d(GetHeading().X * currentStepLength, GetHeading().Y * currentStepLength, GetHeading().Z * currentStepLength);            
            currentPosition.Transform(Transform.Translation(stepVector));
            currentOrientation.Transform(Transform.Translation(stepVector));
            //_canvas.AddVertex(currentPosition);
        }

        /** Moves the turtle one step forward and draws a line along */
        public void MoveForwardAndDraw()
        {

            Point3d OldPosition = new Point3d(currentPosition.X, currentPosition.Y, currentPosition.Z);
            MoveForward();

            //draw a line:
            Line line = new Line(new Point3d(OldPosition.X, OldPosition.Y, OldPosition.Z), new Point3d(currentPosition.X, currentPosition.Y, currentPosition.Z));

            //visual feedback about Turtle's orientation & position
            _canvas.AddPlane(currentOrientation);

            if (loftSkeleton)
            {
                currentProfile = CreateProfile();
                _canvas.AddProfile(currentProfile);
                CurrentBranch().AddProfile(currentProfile);                
            }

            _canvas.AddEdge(line);
        }

        //creates profile curve, oriented according to the current position & orientation of the turtle and scaled according to the current thickness scale
        private Curve CreateProfile()
        {
            Curve profile = defaultProfile.DuplicateCurve();
            //Curve profile = new Circle(currentThickness).ToNurbsCurve();

            //Point3d origin = new Point3d(0,0,0);
            //Curve profile = new Line(origin, new Vector3d(0,1,0)).ToNurbsCurve();
            //Curve 

            OrientProfile(ref profile);                       
            return profile.ToNurbsCurve();
        }

        //orient the profile according to the Turtle's position & orientation
        private void OrientProfile(ref Curve profile)
        {
            Plane profilePlane = Plane.Unset;
            if (profile.IsCircle())
            {
                Circle circle;
                profile.TryGetCircle(out circle);
                profilePlane = circle.Plane;
            }
            else
            {
                //profile.TryGetPlane(out profilePlane);
                profilePlane = profilePivot;
            }


            Transform transform = Transform.ChangeBasis(Plane.WorldXY, profilePlane);
            Plane perpendicularPlane = new Plane(currentOrientation.Origin, currentOrientation.ZAxis, currentOrientation.XAxis);

            Transform transform2 = Transform.ChangeBasis(perpendicularPlane, Plane.WorldXY);
            Transform xform = transform * transform2;
            xform = transform2 * transform;
            profile.Transform(xform);

            //NOTE: translation is made in the Orient Transfor: profile.Transform(Transform.Translation(currentPosition.X, currentPosition.Y, currentPosition.Z));
            //scale the profile according to the current thickness
            profile.Transform(Transform.Scale(currentPosition, currentThickness));
        }

        public Brep[] LoftSkeleton()
        {
            Brep[] branchesBreps = new Brep[branches.Count];
            int counter = 0;
            foreach(Branch branch in branches) {
                Brep branchBrep = LoftProfiles(branch.GetProfiles());
                branchesBreps[counter] = branchBrep;
                counter++;
            }

            return branchesBreps;
            //return JoinBreps(branchesBreps);
        }

        public Brep LoftProfiles(IList<Curve> profiles)
        {
            if (true)//adjust sections
            {
                Point3d pointAtStart = profiles[0].PointAtStart;
                Vector3d tangentAtStart = profiles[0].TangentAtStart;
                int num6 = profiles.Count - 1;
                for (int j = 1; j <= num6; j++)
                {
                    Curve curve2 = profiles[j];
                    if (curve2.IsClosed)
                    {
                        double num5 =0;
                        curve2.ClosestPoint(pointAtStart, out num5);
                        Point3d other = curve2.PointAt(num5);
                        Vector3d vectord2 = curve2.TangentAt(num5);
                        if (tangentAtStart.IsParallelTo(vectord2, 1.5707963267948966) < 0)
                        {
                            curve2.Reverse();
                        }
                        double num3 = curve2.PointAtStart.DistanceTo(other);
                        double length = curve2.GetLength();
                        if (num3 > (0.01 * length))
                        {
                            curve2.ChangeClosedCurveSeam(num5);
                        }
                        pointAtStart = other;
                        tangentAtStart = vectord2;
                    }
                }
            }


            //loft
            Brep[] brepsToJoin = null;
            brepsToJoin = Brep.CreateFromLoft(profiles, Point3d.Unset, Point3d.Unset, LoftType.Straight, false);

            return JoinBreps(brepsToJoin);
        }

        private Brep JoinBreps(Brep[] brepsToJoin)
        {
            if ((brepsToJoin != null) && (brepsToJoin.Length > 0))
            {
                Brep data = null;
                if (brepsToJoin.Length == 1)
                {
                    data = brepsToJoin[0];
                }
                else
                {
                    data = Brep.JoinBreps(brepsToJoin, 1E-05)[0];
                }
                if (data != null)
                {
                    data.Faces.SplitKinkyFaces();
                }
                return data;
            }
            return null;    
        }
        
        private void Rotate3D(double angle, Vector3d axis)
        {
            currentOrientation.Rotate(OnUtil.On_DEGREES_TO_RADIANS * angle, axis);
        }

        /* Rotates the turtle around the Z Axis */
        public void TurnLeft()
        {
            Rotate3D(defaultAngleIncrement, GetLocalZAxis());
        }

        public void TurnRight()
        {
            Rotate3D(-defaultAngleIncrement, GetLocalZAxis());
        }

        /* Rotates the turtle around the Z Axis at 180 degrees */
        public void TurnAround()
        {
            Rotate3D(180, GetLocalZAxis());
        }

        public void RollLeft()
        {
            Rotate3D(-defaultAngleIncrement, GetLocalXAxis());
        }

        public void RollRight()
        {
            Rotate3D(defaultAngleIncrement, GetLocalXAxis());
        }

        public void PitchUp()
        {
            Rotate3D(-defaultAngleIncrement, GetLocalYAxis());
        }

        public void PitchDown()
        {
            Rotate3D(defaultAngleIncrement, GetLocalYAxis());
        }

        public void ScaleStepLength(double s)
        {
            currentStepLength *= s;
        }

        /** scales the thickness using the default thickness scale */
        public void ScaleThickness(double s)
        {
            currentThickness *= s;
        }

        public void ScaleThickness()
        {
            ScaleThickness(defaultThicknessScale);
        }

        public void PushState()
        {
            //clone the object
            Point3d position = new Point3d(currentPosition.X, currentPosition.Y, currentPosition.Z);
            Plane orientation = new Plane(currentOrientation.Origin, currentOrientation.XAxis, currentOrientation.YAxis);
            TurtleState turtleState = new TurtleState(position, orientation, currentStepLength, currentThickness, currentProfile);
            _turtleStatesStack.Push(turtleState);

            Branch newBranch = new Branch(-1);
            branchesStack.Push(newBranch);
            branches.Add(newBranch);
            newBranch.AddProfile(currentProfile);
            //newBranch.AddState(turtleState);

        }

        public void PopState()
        {
            TurtleState lastPushedState = _turtleStatesStack.Pop();
            currentOrientation = lastPushedState.GetOrientation();
            currentPosition = lastPushedState.GetPosition();
            currentStepLength = lastPushedState.GetStepLength();
            currentThickness = lastPushedState.GetThickness();
            currentProfile = lastPushedState.GetProfile();

            //add the branch to the list of branches
            branchesStack.Pop();
            //branches.Add(branchesStack.Pop());
        }

        public void CopyGeometry(String identifier)
        {
            _canvas.AddVertex(currentPosition);
        }

        //======================================================
        /**
         * Executes the specified TurtleCommand on this Turtle
         */ 
        public void Do(TurtleCommand TurtleCommand) {
            TurtleCommand.Execute(this);
        }

        /**
         * Executes the List of turtle commands on this turtle
         * 
         */ 
        public void Do(IList<TurtleCommand> turtleCommands)
        {
            foreach(TurtleCommand TurtleCommand in turtleCommands)
                TurtleCommand.Execute(this);
        }

        
        public Stack<TurtleState> GetTurtleStates()
        {
            return _turtleStatesStack;
        }

        public Canvas GetCanvas()
        {
            return _canvas;
        }


        public class Branch
        {
            IList<TurtleState> states;
            IList<Curve> profiles;
            int order;

            public Branch(int order)
            {
                this.order = order;
                states = new List<TurtleState>();
                profiles = new List<Curve>();
            }

            private void AddState(TurtleState state)
            {
                states.Add(state);
                //profiles.Add(state.GetProfile());
            }

            public void AddProfile(Curve profile)
            {
                profiles.Add(profile);
            }

            
            public IList<Curve> GetProfiles()
            {
                return profiles;
            }

            public override String ToString()
            {
                return "Branch: " + GetHashCode() + " contains " + profiles.Count + " profile curves: "+profiles.ToString();
            }

        }

    }
}
