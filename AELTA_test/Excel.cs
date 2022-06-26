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
                string[] oldOne = new string[PointDataGrid.ColumnCount];
                string[] newOne = new string[PointDataGrid.ColumnCount];
                var wb = workbook.Worksheets.Add("點位");
                //MessageBox.Show(PointDataGrid[6,0].Value.ToString());
                for (int x = 0; x < PointDataGrid.Rows.Count - 1; x++)
                {
                    for (int i = 0; i < PointDataGrid.Columns.Count; i++)
                    {
                        if(oldOne == null)
                        {
                            oldOne[i] = PointDataGrid.Columns[i].ToString();
                        }
                        wb.Cell(x + 1, i + 1).Value = PointDataGrid[i, x].Value.ToString();
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
