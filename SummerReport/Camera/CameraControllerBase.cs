using System.Windows.Forms;
using SlimDX;

namespace SummerReport.Camera
{
    abstract class CameraControllerBase: ICameraController
    {
        protected Vector3 CameraPosition = Vector3.UnitZ + Vector3.UnitY;

        protected Vector3 CameraLookAt=Vector3.Zero;

        protected Vector3 CameraUpVec = Vector3.UnitY;
        
        public Matrix GetCurrentCameraMatrix()
        {
            return Matrix.LookAtLH(CameraPosition, CameraLookAt, CameraUpVec);
        }
    }

    class MouseControlCameraController : CameraControllerBase
    {
        private Control parentControl;

        public MouseControlCameraController(Control control)
        {
            this.parentControl = control;
            parentControl.MouseDown += parentControl_MouseDown;
            parentControl.MouseUp += parentControl_MouseUp;
            parentControl.MouseMove += parentControl_MouseMove;
        }

        private void parentControl_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void parentControl_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        void parentControl_MouseDown(object sender, MouseEventArgs e)
        {
            this.CameraPosition = Vector3.TransformCoordinate(this.CameraPosition, Matrix.RotationY(0.1f));
        }
    }
}