using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dynamixel_sdk;

namespace ControlUI
{
    partial class Form1
    {

        // Control table address
        public const int ADDR_MX_TORQUE_ENABLE = 24;                  // Control table address is different in Dynamixel model
        public const int ADDR_MX_GOAL_POSITION = 30;
        public const int ADDR_MX_PRESENT_POSITION = 36;

        // Protocol version
        public const int PROTOCOL_VERSION = 1;                   // See which protocol version is used in the Dynamixel

        // Default setting
        public const int DXL_ID = 1;                   // Dynamixel ID: 1
        public const int BAUDRATE = 1000000;
        public const string DEVICENAME = "COM4";              // Check which port is being used on your controller
                                                              // ex) Windows: "COM1"   Linux: "/dev/ttyUSB0" Mac: "/dev/tty.usbserial-*"

        public const int TORQUE_ENABLE = 1;                   // Value for enabling the torque
        public const int TORQUE_DISABLE = 0;                   // Value for disabling the torque
        public const int DXL_MINIMUM_POSITION_VALUE = 512;                 // Dynamixel will rotate between this value
        public const int DXL_MAXIMUM_POSITION_VALUE = 0;                // and this value (note that the Dynamixel would not move when the position value is out of movable range. Check e-manual about the range of the Dynamixel you use.)
        public const int DXL_MOVING_STATUS_THRESHOLD = 10;                  // Dynamixel moving status threshold

        public const byte ESC_ASCII_VALUE = 0x1b;

        public const int COMM_SUCCESS = 0;                   // Communication Success result value
        public const int COMM_TX_FAIL = -1001;               // Communication Tx Failed

        int index = 0;
        int dxl_comm_result = COMM_TX_FAIL;                                   // Communication result
        UInt16[] dxl_goal_position = new UInt16[2] { DXL_MINIMUM_POSITION_VALUE, DXL_MAXIMUM_POSITION_VALUE };         // Goal position

        byte dxl_error = 0;                                                   // Dynamixel error
        UInt16 dxl_present_position = 0;                                      // Present position


        public const int Goal_Position_Open = 768;
        public const int Goal_Position_Close = 512;

        private void AX12A_Open_Click(object sender, EventArgs e)
        {
            dynamixel.write2ByteTxRx(port_num, PROTOCOL_VERSION, DXL_ID, ADDR_MX_GOAL_POSITION, Goal_Position_Open);
            VaildPresentPosition(Goal_Position_Open);
        }

        private void AX12A_Close_Click(object sender, EventArgs e)
        {
            dynamixel.write2ByteTxRx(port_num, PROTOCOL_VERSION, DXL_ID, ADDR_MX_GOAL_POSITION, Goal_Position_Close);
            VaildPresentPosition(Goal_Position_Close);
        }
        public void VaildPresentPosition(int Position)
        {
            do
            {
                dxl_present_position = dynamixel.read2ByteTxRx(port_num, PROTOCOL_VERSION, DXL_ID, ADDR_MX_PRESENT_POSITION);
            } while ((Math.Abs(Position - dxl_present_position) > DXL_MOVING_STATUS_THRESHOLD));
        }
    }
}
