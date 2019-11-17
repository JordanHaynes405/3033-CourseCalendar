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
using OfficeOpenXml.Style;

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

        public List<DateTime> TopicDates = new List<DateTime>();

        public int ItemsAdded;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //https://www.codebyamir.com/blog/create-excel-files-in-c-sharp

            if (string.IsNullOrEmpty(txtCourseName.Text) && string.IsNullOrEmpty(txtCourseCode.Text) &&
                string.IsNullOrEmpty(txtProfessor.Text) && string.IsNullOrEmpty(txtSemester.Text))
            {
                MessageBox.Show("You must set up the calendar before exporting to Excel!");
            }
            else if (Topics.Count == 0 )
            {
                MessageBox.Show("You have not entered any topics for the course calendar");
            }
            else if (Topics.Count < 5 )
            {
                //https://stackoverflow.com/questions/3036829/how-do-i-create-a-message-box-with-yes-no-choices-and-a-dialogresult
                dynamic MBResult = MessageBox.Show("You do not have very many topics entered, " +
                    "are you sure you would like to export to excel?", "Warning!", MessageBoxButton.YesNo);

                if (MBResult == System.Windows.MessageBoxResult.Yes)
                {
                    BuildSpreadSheet();
                }
            }
            else if (txtTopicName.Text != null && txtNumTopicPeriods.Text != null && txtTopicPreparation.Text != null && pcrTopicStart.Text != null)
            {
                MessageBox.Show("You should click the ADD button to save your last entry before exporting!");
            }
            else
            {
                BuildSpreadSheet();
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            AddToLists();
            AddToTable();  
        }

        private void txtCourseName_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        public void AddToLists()
        {
            TopicDates.Add(Convert.ToDateTime(pcrTopicStart.Text));
            Topics.Add(txtTopicName.Text);
            Periods.Add(txtNumTopicPeriods.Text);
            Preparations.Add(txtTopicPreparation.Text);

            ItemsAdded = Topics.Count();

            MessageBox.Show($"You have added {txtTopicName.Text} to begin on the " +
                $"{Convert.ToDateTime(pcrTopicStart.Text).ToLongDateString()} and proceed for {txtNumTopicPeriods.Text}" +
                    $" class periods. Preparation shall include {txtTopicPreparation.Text}.");

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
            DataGridTextColumn DateCol = new DataGridTextColumn();
            DateCol.Header = "Start Dates";
            DateCol.Binding = new Binding(TopicDates);

            DataGridTextColumn TopicCol = new DataGridTextColumn();
            TopicCol.Header = "Topics";
            TopicCol.Binding = new Binding(txtTopicName.Text);

            DataGridTextColumn PeriodCol = new DataGridTextColumn();
            PeriodCol.Header = "Periods";
            PeriodCol.Binding = new Binding(txtNumTopicPeriods.Text);

            DataGridTextColumn PreparationCol = new DataGridTextColumn();
            PreparationCol.Header = "Preparations";
            PreparationCol.Binding = new Binding(txtTopicPreparation.Text);
        }

        private void btnCloseApp_Click(object sender, RoutedEventArgs e)
        {
            //https://stackoverflow.com/questions/12977924/how-to-properly-exit-a-c-sharp-application
            Environment.Exit(0);
        }
        public void BuildSpreadSheet ()
        {
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
                CalendarSheet.Cells["A4"].Value = ($"Semester {txtSemester.Text}");
                CalendarSheet.Cells["C4"].Value = ($"Day(s): {txtDaysofWeek.Text}");
                CalendarSheet.Cells["A5"].Value = ($"Start Date: {dprStart.Text}");
                CalendarSheet.Cells["C5"].Value = ($"End Date: {dprEnd.Text}");

                //Build Columns
                CalendarSheet.Cells["A6"].Value = "Date";
                CalendarSheet.Cells["B6"].Value = "Topic";
                CalendarSheet.Cells["C6"].Value = "Classes";
                CalendarSheet.Cells["D6"].Value = "Preparation";

                int ExcelIterate = ItemsAdded;
                for (int i = 0; i <= ExcelIterate - 1; i++)
                {
                    CalendarSheet.Cells[$"A{7 + i}"].Value = TopicDates[i].ToShortDateString();
                    CalendarSheet.Cells[$"B{7 + i}"].Value = Topics[i];
                    CalendarSheet.Cells[$"C{7 + i}"].Value = Convert.ToInt32(Periods[i]);
                    CalendarSheet.Cells[$"D{7 + i}"].Value = Preparations[i];
                }

                var Heading = CalendarSheet.Cells["A1:D6"];
                Heading.Style.Font.Bold = true;
                Heading.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var Body = CalendarSheet.Cells[$"A6:D{ItemsAdded + 7}"];
                Body.Style.Border.Top.Style = Body.Style.Border.Left.Style =
                    Body.Style.Border.Right.Style = Body.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                var Whole = CalendarSheet.Cells[$"A1:D{ItemsAdded + 7}"];
                Whole.Style.WrapText = true;


                CalendarSheet.Column(1).Width = 15;
                CalendarSheet.Column(2).Width = 26;
                CalendarSheet.Column(3).Width = 12;
                CalendarSheet.Column(4).Width = 34;

                CalendarSheet.Cells["A1:D1"].Merge = true;
                CalendarSheet.Cells["A2:D2"].Merge = true;
                CalendarSheet.Cells["A3:D3"].Merge = true;
                CalendarSheet.Cells["A4:B4"].Merge = true;
                CalendarSheet.Cells["C4:D4"].Merge = true;
                CalendarSheet.Cells["A5:B5"].Merge = true;
                CalendarSheet.Cells["C5:D5"].Merge = true;

                Excel.SaveAs(ExcelFile);
            }
        }
    }
}
