using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuadcopterAvoidanceSimulation
{
    class Quad
    {
        public Quad(double x, double y, Timer t)
        {
            _xPosition = x;
            _yPosition = y;
            thrustAcceleration = 100;
            coeffiecientOfDrag = 1;
            pitchAngle = Equations.toRad(0);
            rollAngle = Equations.toRad(30);
            yawAngle = Equations.toRad(0);
            wPitch = 0;
            wRoll = 0;
            wYaw = 0;
            iHatVector = new Vector(1,0);
            jHatVector = new Vector(0,1);
            velocity = new Vector(0,0);
            acceleration = new Vector(0,0);
            accelerationRelativeToQuad = new Vector(0,0);
            time = t;
            previousUpdateTime = time.micros;
        }

        public double xPosition {get {return _xPosition;} set {_xPosition = value;}}
        public double yPosition { get {return _yPosition; } set {_yPosition = value;}}
        public double velocityX { get { return velocity.X; } set { velocity.X = value;}}
        public double roll { get{return rollAngle;} set{rollAngle = value;} }
        public double pitch { get { return pitchAngle; } set { pitchAngle = value; } }
        public double yaw { get { return yawAngle; } set { yawAngle = value; } }
        public double yawRate { get { return wYaw; } set { wYaw = value; } }


        public void updateState(){
            double dT = (time.micros - previousUpdateTime) / 1000000.0; //dT in seconds

            pitchAngle += wPitch * dT;
            rollAngle += wRoll * dT;
            yawAngle += wYaw * dT;
            quadHeading = Equations.polarToCartesian(yawAngle, 1);

            accelerationRelativeToQuad.X = Equations.yComponentOfVector(rollAngle,thrustAcceleration);
            accelerationRelativeToQuad.Y = Equations.yComponentOfVector(pitchAngle, thrustAcceleration);
            acceleration.Y = Equations.vectorProjection(accelerationRelativeToQuad, Equations.orthagonalVector(quadHeading));
            acceleration.X = Equations.vectorProjection(accelerationRelativeToQuad, quadHeading);

            Vector dragForce = Equations.dragVector(coeffiecientOfDrag, velocity);
            Vector netAcceleration = Vector.Add(acceleration, dragForce);

            _xPosition = Equations.kinematics3(_xPosition, velocity.X, netAcceleration.X, dT);
            _yPosition = Equations.kinematics3(_yPosition, velocity.Y, netAcceleration.Y, dT);
            
            velocity.X = Equations.kinematics1(velocity.X, acceleration.X, dT);
            velocity.Y = Equations.kinematics1(velocity.Y, acceleration.Y, dT);
            
            previousUpdateTime = time.micros;
        }
        

        private double _xPosition, _yPosition;
        private double pitchAngle,rollAngle,yawAngle;
        private double thrustAcceleration;
        private double coeffiecientOfDrag;
        private double wPitch, wRoll,wYaw;
        private Vector velocity;
        private Vector acceleration;
        private Vector accelerationRelativeToQuad;
        private Vector iHatVector,jHatVector;
        private Vector quadHeading;
        private Timer time;
        private Int64 previousUpdateTime;
    }
}
