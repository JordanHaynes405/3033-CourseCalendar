using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseCalendarCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddContent Topic = new AddContent();
            Topic.Show();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            ///http://csharp.net-informations.com/excel/csharp-create-excel.htm
            ///https://www.c-sharpcorner.com/UploadFile/bd6c67/how-to-create-excel-file-using-C-Sharp/
            ///https://www.e-iceblue.com/Tutorials/Spire.XLS/Spire.XLS-Program-Guide/Header-and-Footer/Insert-Header-and-Footer-to-Excel-with-C-VB.NET-in-WPF.html

            DataGrid dgvPreview = new DataGrid();

            System.Data.DataTable BuildTable = new System.Data.DataTable();

            BuildTable.Columns.Add();



            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }

            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = xlApp.Workbooks.Add();

            /*
            Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "ID";
            xlWorkSheet.Cells[1, 2] = "Name";
            xlWorkSheet.Cells[2, 1] = "1";
            xlWorkSheet.Cells[2, 2] = "One";
            xlWorkSheet.Cells[3, 1] = "2";
            xlWorkSheet.Cells[3, 2] = "Two";

            xlWorkBook.SaveAs("your-file-name.xls");*/
        }

        private void btnApplyFrame_Click(object sender, RoutedEventArgs e)
        {
            ///https://www.c-sharpcorner.com/UploadFile/bd6c67/how-to-create-excel-file-using-C-Sharp/
            ///https://www.e-iceblue.com/Tutorials/Spire.XLS/Spire.XLS-Program-Guide/Header-and-Footer/Insert-Header-and-Footer-to-Excel-with-C-VB.NET-in-WPF.html

            DataGrid dgvPreview = new DataGrid();

            System.Data.DataTable BuildTable = new System.Data.DataTable();

            BuildTable.Columns.Add();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //https://social.msdn.microsoft.com/Forums/en-US/ea4cf092-6cd0-46a4-b889-0cb85c6501a8/delete-selected-row-from-datagrid-table-in-wpf?forum=wpf
            //DataRowView row = (DataRowView)gridPosts.SelectedItem;
            //dt.Rows.Remove(row.Row);
        }
    }
}
