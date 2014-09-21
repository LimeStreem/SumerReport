
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace SummerReport.Camera
{
    interface ICameraController
    {
        Matrix GetCurrentCameraMatrix();
    }
}
