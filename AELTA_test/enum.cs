using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlUI
{
    // Enum 是一個別名，用名字代表數字
    public enum ImageRecogntionBais
    {
        X = -60,
        Y = -8
    }
    public enum EGripPostion
    {
        original = 0,
        Coupling = 1,
        raiseBoard = 2,
        Vientiane = 3,
        FixedSeat = 4,
        Coupling_back = 5,
        RotationBoard = 10,
        chair_body = 11,
        chair_legs = 12
    }
    public enum Eshape
    {
        Rectangle = 400,
        Circle = 200,
        Square = 300

    }
}
