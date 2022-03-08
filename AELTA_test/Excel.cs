using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ControlUI
{
    partial class Form1
    {
        private void exportExcel_Click(object sender, EventArgs e)
        {
            //Creating DataTable
            using (var workbook = new XLWorkbook())
            {
                var wb = workbook.Worksheets.Add("點位");
                //MessageBox.Show(PointDataGrid[6,0].Value.ToString());
                for (int x = 1; x < PointDataGrid.Rows.Count; x++)
                {
                    for (int i = 1; i < 7; i++)
                    {
                        wb.Cell(x, i).Value = PointDataGrid[i, x - 1].Value.ToString();
                    }
                }
                //Exporting to Excel
                string folderPath = @"E:\碩論\MasterCode\ControlUI\";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                workbook.SaveAs(folderPath + "DataGridViewExport.xlsx");
            }
        }
    }
}
