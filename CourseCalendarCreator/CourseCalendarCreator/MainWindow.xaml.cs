﻿using System;
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

        public List<DateTime> TopicDate = new List<DateTime>();

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
            else if (Topics.Count < 1 )
            {
                MessageBox.Show("You have not entered any topics for the course calendar");
            }
            else if (Topics.Count < 2 )
            {
                MessageBox.Show("You don't have very many topics in your course calendar, consider adding more!");
            }
            else
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
                    CalendarSheet.Cells["A4"].Value = ($"Semester {txtSemester.Text}, Day(s): {txtDaysofWeek.Text}");
                    CalendarSheet.Cells["A5"].Value = ($"Start Date: {dprStart.Text}, End Date: {dprEnd.Text}");

                    //Build Columns
                    CalendarSheet.Cells["A6"].Value = "Date";
                    CalendarSheet.Cells["B6"].Value = "Topic";
                    CalendarSheet.Cells["C6"].Value = "Periods to Cover";
                    CalendarSheet.Cells["D6"].Value = "Preparation";

                    for (int i = 0; i <= ItemsAdded - 1; i++)
                    {
                        CalendarSheet.Cells[$"A{7 + i}"].Value = TopicDate[i];
                        CalendarSheet.Cells[$"B{7 + i}"].Value = Topics[i];
                        CalendarSheet.Cells[$"C{7 + i}"].Value = Convert.ToInt32(Periods[i]);
                        CalendarSheet.Cells[$"D{7 + i}"].Value = Preparations[i];
                    }


                    CalendarSheet.Cells["A:D"].AutoFitColumns();

                    CalendarSheet.Cells["A1:D1"].Merge = true;
                    CalendarSheet.Cells["A2:D2"].Merge = true;
                    CalendarSheet.Cells["A3:D3"].Merge = true;
                    CalendarSheet.Cells["A4:B4"].Merge = true;
                    CalendarSheet.Cells["C4:D4"].Merge = true;
                    CalendarSheet.Cells["A4:B4"].Merge = true;
                    CalendarSheet.Cells["C4:D4"].Merge = true;

                    CalendarSheet.Cells["A1:D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    CalendarSheet.Cells["A2:D2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    CalendarSheet.Cells["A3:D3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    CalendarSheet.Cells["A4:B4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    CalendarSheet.Cells["C4:D4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    CalendarSheet.Cells["A4:B4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    CalendarSheet.Cells["C4:D4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                    Excel.SaveAs(ExcelFile);
                }
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
            TopicDate.Add(Convert.ToDateTime(pcrTopicStart.Text));
            Topics.Add(txtTopicName.Text);
            Periods.Add(txtNumTopicPeriods.Text);
            Preparations.Add(txtTopicPreparation.Text);

            ItemsAdded = Topics.Count();

            foreach (var item in Topics)
                MessageBox.Show($"You have added {item} to begin on the {TopicDate} and proceed for {Periods}" +
                    $" class periods. Preparation shall include {Preparations}.");

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

        private void btnCloseApp_Click(object sender, RoutedEventArgs e)
        {
            //https://stackoverflow.com/questions/12977924/how-to-properly-exit-a-c-sharp-application
            Environment.Exit(0);
        }
    }
}
