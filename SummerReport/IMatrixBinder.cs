using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlimDX;
using SummerReport.Camera;
using SummerReport.Projection;

namespace SummerReport
{
    interface IMatrixBinder
    {
        Matrix GetWorldViewProjection();
    }

    abstract class MatrixBinderBase:IMatrixBinder
    {
        protected abstract ICameraController CameraController { get; }

        protected abstract IProjectionController ProjectionController { get; }

        public Matrix GetWorldViewProjection()
        {
            return this.ProjectionController.GetCurrentProjectionMatrix()*this.CameraController.GetCurrentCameraMatrix();
        }
    }

    class SummerReportMatrixBinder : MatrixBinderBase
    {
        private readonly Control _control;
        private ICameraController _cameraController;
        private IProjectionController _projectionController;

        public SummerReportMatrixBinder(Control control)
        {
            _control = control;
            this._cameraController = new MouseControlCameraController(control);
            this._projectionController=new BasicProjectionController(control);
        }

        protected override ICameraController CameraController
        {
            get { return _cameraController; }
        }

        protected override IProjectionController ProjectionController
        {
            get { return _projectionController; }
        }
    }
}
