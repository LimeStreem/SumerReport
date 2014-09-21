using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlimDX;

namespace SummerReport.Projection
{
    interface IProjectionController
    {
        Matrix GetCurrentProjectionMatrix();
    }

    abstract class ProjectionControllerBase : IProjectionController
    {
        protected float minZ = 1.0E-3f;

        protected float maxZ = (float) (Math.Sqrt(3)*2);//一辺の長さが2の立方体の対角線の長さ

        protected float fovy=(float) (Math.PI/2f);//視野角45°

        protected float aspectRatio=1f;

        public Matrix GetCurrentProjectionMatrix()
        {
            return Matrix.PerspectiveFovLH(fovy, aspectRatio, minZ, maxZ);
        }
    }

    class BasicProjectionController :ProjectionControllerBase
    {
        private readonly Control _parentControl;

        public BasicProjectionController(Control parentControl)
        {
            _parentControl = parentControl;
            parentControl.Resize += parentControl_Resize;
            UpdateAspectRatio();
        }

        void parentControl_Resize(object sender, EventArgs e)
        {
            UpdateAspectRatio();
        }

        private void UpdateAspectRatio()
        {
            this.aspectRatio = (float)_parentControl.Width/_parentControl.Height;
        }
    }
}
