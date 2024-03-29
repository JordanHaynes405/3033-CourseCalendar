﻿// Worked by: Jordan, Karan, Saurabh, and Kwame

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
{   /// <summary>
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
            try 
            {
                Reorder();
            }
            catch
            {
            }

            if (string.IsNullOrEmpty(txtCourseName.Text) && string.IsNullOrEmpty(txtCourseCode.Text) &&
                string.IsNullOrEmpty(txtProfessor.Text) && string.IsNullOrEmpty(txtSemester.Text))
            {
                MessageBox.Show("You must set up the calendar before exporting to Excel!");
            }
            else if (Topics.Count == 0)//
            {
                MessageBox.Show("You have not entered any topics for the course calendar");
            }

            else if (Topics.Count < 5)
            {
                //https://stackoverflow.com/questions/3036829/how-do-i-create-a-message-box-with-yes-no-choices-and-a-dialogresult
                dynamic MBResult = MessageBox.Show("You do not have very many topics entered, " +
                    "are you sure you would like to export to excel?", "Warning!", MessageBoxButton.YesNo);

                if (MBResult == System.Windows.MessageBoxResult.Yes)
                {
                    BuildSpreadSheet();
                }
            }

            else if (txtTopicName.Text != null && txtNumTopicPeriods.Text != null && txtTopicPreparation.Text != null)
            {
                MessageBox.Show("You should click the ADD button to save your last entry before exporting!");
            }

            else
                BuildSpreadSheet();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCourseName.Text) && string.IsNullOrEmpty(txtCourseCode.Text) &&
    string.IsNullOrEmpty(txtProfessor.Text) && string.IsNullOrEmpty(txtSemester.Text))
            {
                MessageBox.Show("You must set up the calendar before exporting to Excel!");
            }

            else if (!int.TryParse(txtNumTopicPeriods.Text, out int j))
                MessageBox.Show("You entered a non-numeric value into the --Topic Periods To Cover-- feild, please correct this!");

            else if (Topics.Count >= 1)
                AddDataGridRows();

            else
                AddDataGridColumns();

            if (int.TryParse(txtNumTopicPeriods.Text, out int k))
            AddToLists();
        }

        public void AddDataGridRows()
        {
            System.Data.DataRow ContentEntry = CourseTable.NewRow();
            ContentEntry["Start"] = pcrTopicStart.Text;
            ContentEntry["Topics"] = txtTopicName.Text;
            ContentEntry["Classes"] = txtNumTopicPeriods.Text;
            ContentEntry["Preparation"] = txtTopicPreparation.Text;

            CourseTable.Rows.Add(ContentEntry);
            dgPreview.ItemsSource = CourseTable.DefaultView;
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

            ClearText();
        }
        public void ClearText()
        {
            txtTopicName.Clear();
            txtNumTopicPeriods.Clear();
            txtTopicPreparation.Clear();
        }
        public void AddDataGridColumns()
        {
            //https://stackoverflow.com/questions/704724/programmatically-add-column-rows-to-wpf-datagrid
            //https://stackoverflow.com/questions/11926534/how-to-change-column-width-in-datagridview
            CourseTable.Columns.Add("Start");
            CourseTable.Columns.Add("Topics");
            CourseTable.Columns.Add("Classes");
            CourseTable.Columns.Add("Preparation");

            System.Data.DataRow ContentEntry = CourseTable.NewRow();
            ContentEntry["Start"] = pcrTopicStart.Text;
            ContentEntry["Topics"] = txtTopicName.Text;
            ContentEntry["Classes"] = txtNumTopicPeriods.Text;
            ContentEntry["Preparation"] = txtTopicPreparation.Text;

            CourseTable.Rows.Add(ContentEntry);
            dgPreview.ItemsSource = CourseTable.DefaultView;

            dgPreview.Columns[0].Width = 65;
            dgPreview.Columns[1].Width = 100;
            dgPreview.Columns[2].Width = 50;
            dgPreview.Columns[3].Width = 235;
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
                CalendarSheet.Cells["A3"].Value = ($"Professor: {txtProfessor.Text}");
                CalendarSheet.Cells["A4"].Value = ($"Semester: {txtSemester.Text}");
                CalendarSheet.Cells["C4"].Value = ($"Day(s): {txtDaysofWeek.Text}");
                CalendarSheet.Cells["A5"].Value = ($"Start Date: {dprStart.Text}");
                CalendarSheet.Cells["C5"].Value = ($"End Date: {dprEnd.Text}");
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

                CalendarSheet.Column(1).Width = 11;
                CalendarSheet.Column(2).Width = 30;
                CalendarSheet.Column(3).Width = 11;
                CalendarSheet.Column(4).Width = 35;

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
        public void Reorder()
        {
            TopicDates = TopicDates.OrderBy(x => x.Date).ToList();

            try
            {
                for (int i = 1; i <= ItemsAdded - 1; i++)
                {
                    Topics = Topics.OrderBy(s => TopicDates.IndexOf(TopicDates[i])).ToList();
                }

                for (int i = 1; i <= ItemsAdded - 1; i++)
                {
                    Periods = Periods.OrderBy(s => TopicDates.IndexOf(TopicDates[i])).ToList();
                }

                for (int i = 1; i <= ItemsAdded - 1; i++)
                {
                    Preparations = Preparations.OrderBy(s => TopicDates.IndexOf(TopicDates[i])).ToList();
                }
            }

            catch
            {
                List<string> TempTopics = new List<string>();
                List<string> TempPeriods = new List<string>();
                List<string> TempPreps = new List<string>();

                for (int i = 0; i <= TopicDates.Count; i++)
                {
                    for (int j = 0; i+1 <= TopicDates.Count; j++)
                    {
                        if (Topics.IndexOf(Topics[j]) > Topics.IndexOf(Topics[i]))
                        {
                            TempTopics.Add(Topics[i]);
                            Topics[i] = Topics[j];
                            Topics[j] = TempTopics[i];
                            Topics = TempTopics;
                        }
                    }
                }
                for (int i = 0; i <= TopicDates.Count; i++)
                {
                    for (int j = 0; i + 1 <= Periods.Count; j++)
                    {
                        if (Periods.IndexOf(Periods[j]) > Periods.IndexOf(Periods[i]))
                        {
                            TempPeriods.Add(Periods[i]);
                            Periods[i] = Periods[j];
                            Periods[j] = TempPeriods[i];
                            Periods = TempPeriods;
                        }
                    }
                }
                for (int i = 0; i <= Preparations.Count; i++)
                {
                    for (int j = 0; i + 1 <= Preparations.Count; j++)
                    {
                        if (Preparations.IndexOf(Preparations[j]) > Preparations.IndexOf(Preparations[i]))
                        {
                            TempPreps.Add(Preparations[i]);
                            Preparations[i] = Preparations[j];
                            Preparations[j] = TempPreps[i];
                            Preparations = TempPreps;
                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Topics.Clear();
            TopicDates.Clear();
            Preparations.Clear();
            Periods.Clear();
            dgPreview.Columns.Clear();
        }

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            Help HelpWindow = new Help();
            HelpWindow.Show(); 

        }
    }
}
