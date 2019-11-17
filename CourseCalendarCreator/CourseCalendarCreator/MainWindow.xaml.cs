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
        public System.Data.DataTable CourseTable = new System.Data.DataTable();

        public List<string> Topics = new List<string>();
         
        public List<string> Periods = new List<string>();
        
        public List<string> Preparations = new List<string>();

        public int ItemsAdded;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtCourseName.Text)) // && string.IsNullOrEmpty(txtCourseCode.Text) && string.IsNullOrEmpty(txtProfessor.Text) && string.IsNullOrEmpty(txtSemester.Text)
            {
                MessageBox.Show("Please fill the fields on the page before adding topics");
            }

            else
            {

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
                CalendarSheet.Cells["A4"].Value = ($"Semester {txtSemester.Text}, Day(s): {txtDaysofWeek.Text}");
                CalendarSheet.Cells["A5"].Value = ($"Start Date: {dprStart.Text}, End Date: {dprEnd.Text}");

                //Build Columns
                CalendarSheet.Cells["A6"].Value = "Date";
                CalendarSheet.Cells["B6"].Value = "Topic";
                CalendarSheet.Cells["C6"].Value = "Periods to Cover";
                CalendarSheet.Cells["D6"].Value = "Preparation";

                for (int i = 0; i <= ItemsAdded; i++)
                {
                    CalendarSheet.Cells[$"B{6+i}"].Value = Topics[i];
                    CalendarSheet.Cells[$"C{6+i}"].Value = Periods[i];
                    CalendarSheet.Cells[$"C{6+i}"].Value = Preparations[i];
                }

                Excel.SaveAs(ExcelFile);
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            AddToLists();
            //AddToTable();
            
        }

        private void txtCourseName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void AddToLists()
        {
            Topics.Add(txtTopicName.Text);
            Periods.Add(txtNumTopicPeriods.Text);
            Preparations.Add(txtTopicPreparation.Text);

            int ItemsAdded = Topics.Count();

            foreach (var item in Topics)
                MessageBox.Show(item + " " + ItemsAdded);

            ClearText();
        }
        public void ClearText()
        {
            txtTopicName.Clear();
            txtNumTopicPeriods.Clear();
            txtTopicPreparation.Clear();
        }
        public void AddToTable()
        {
            //CourseTable.Columns.Add(new DataColumn("Date", Type.GetType("System.DateTime")));
            //CourseTable.Columns.Add(new DataColumn("Topic Name", Type.GetType("System.String")));
            //CourseTable.Columns.Add(new DataColumn("Periods", Type.GetType("System.String")));
            //CourseTable.Columns.Add(new DataColumn("Preparation", Type.GetType("System.String"))); 
            //CourseTable.Rows.Add(txtTopicName.Text, txtNumTopicPeriods.Text, txtTopicPreparation.Text);//pcrTopicStart.Text,
            //dgPreview.ItemsSource = MainPageInput.CourseTable.DefaultView;
        }
    }
}
