﻿using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace CourseCalendarCreator
{
    /// <summary>
    /// Interaction logic for AddContent.xaml
    /// </summary>
    public partial class AddContent : Window
    {
        public int i = 0;

        public int ItemsAdded = 1;

        public List<string> Topics = new List<string>();

        public List<string> Periods = new List<string>();

        public List<string> Preparations = new List<string>();
        public AddContent()
        {
            InitializeComponent();

            lblAddTopic.Content = $"Add Topic for Class # {i}";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            i++;
            AddToTable();
            AddToList();
            lblAddTopic.Content = $"Add Topic for Class # {i}";
        }

        public void AddToList()
        {
            Topics.Add(txtTopicName.Text);
            Periods.Add(txtNumTopicPeriods.Text);
            Preparations.Add(txtNumTopicPeriods.Text);

            txtTopicName.Clear();
            txtNumTopicPeriods.Clear();
            txtTopicPreparation.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainPageInput = new MainWindow();
            MainPageInput.SyncLists();

            this.Close();
        }

        public void AddToTable()
        {
            //MainPageInput.CourseTable.Columns.Add(new DataColumn("Date", Type.GetType("System.DateTime")));
            //MainPageInput.CourseTable.Columns.Add(new DataColumn("Topic Name", Type.GetType("System.String")));
            //MainPageInput.CourseTable.Columns.Add(new DataColumn("Periods", Type.GetType("System.String")));
            //MainPageInput.CourseTable.Columns.Add(new DataColumn("Preparation", Type.GetType("System.String"))); 
            //MainPageInput.CourseTable.Rows.Add(txtTopicName.Text, txtNumTopicPeriods.Text, txtTopicPreparation.Text);//pcrTopicStart.Text,
            //MainPageInput.dgPreview.ItemsSource = MainPageInput.CourseTable.DefaultView;
        }
}
}
