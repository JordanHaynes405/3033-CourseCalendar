using System;
using System.Collections.Generic;
using System.IO;
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

using OfficeOpenXml;

namespace CourseCalendarCreator
{// Worked by: Jordan, Karan, bob
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
            if (string.IsNullOrEmpty(txtCourseName.Text) && string.IsNullOrEmpty(txtCourseCode.Text)
    && string.IsNullOrEmpty(txtProfessor.Text) && string.IsNullOrEmpty(txtSemester.Text))
            {
                MessageBox.Show("Please fill the fields on the page before adding topics");
            }

            else
            {
                AddContent Topic = new AddContent();
                Topic.Show();
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //https://www.codebyamir.com/blog/create-excel-files-in-c-sharp

            using (ExcelPackage Excel = new ExcelPackage())
            {
                var CalendarSheet = Excel.Workbook.Worksheets.Add("Worksheet1");
                Microsoft.Win32.SaveFileDialog DialogT = new Microsoft.Win32.SaveFileDialog();
                var File = DialogT.ShowDialog();
                string Path = DialogT.FileName;
                FileInfo ExcelFile = new FileInfo(Path + ".xlsx");

                CalendarSheet.Cells["A1"].Value = ($"Course Name: {txtCourseName.Text}");
                CalendarSheet.Cells["A2"].Value = ($"Course Code: {txtCourseCode.Text}");
                CalendarSheet.Cells["A3"].Value = ($"Professor {txtProfessor.Text}");
                CalendarSheet.Cells["A4"].Value = ($"Semester {txtSemester.Text} Days: {txtDaysofWeek.Text}");

                Excel.SaveAs(ExcelFile);
            }
        }

        private void btnApplyFrame_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //https://social.msdn.microsoft.com/Forums/en-US/ea4cf092-6cd0-46a4-b889-0cb85c6501a8/delete-selected-row-from-datagrid-table-in-wpf?forum=wpf
            //DataRowView row = (DataRowView)gridPosts.SelectedItem;
            //dt.Rows.Remove(row.Row);
        }

        private void txtCourseName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
