using System;
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
        public int i;

        public List<string> Topics = new List<string>();

        public List<string> Periods = new List<string>();

        public List<string> Preparations = new List<string>();
        public AddContent()
        {
            InitializeComponent();

            lblAddTopic.Content = $"Add Topic for Class # {i+1}";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddToTable();
            AddToLists();
            lblAddTopic.Content = $"Add Topic for Class # {i+1}";
        }

        public void AddToLists()
        {
            Topics.Add(txtTopicName.Text);
            Periods.Add(txtNumTopicPeriods.Text);
            Preparations.Add(txtTopicPreparation.Text);

            i = Topics.Count();

            foreach (var item in Topics)
                MessageBox.Show(item + " " + i);

            ClearText();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FinalCount(Topics);
            AllTopics(Topics);
            AllPeriods(Periods);
            AllPreparations(Preparations);

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
        public void ClearText()
        {
            txtTopicName.Clear();
            txtNumTopicPeriods.Clear();
            txtTopicPreparation.Clear();
        }

        public int FinalCount(List<string> Topics)
        {
            return Topics.Count;
        }
        public List<string> AllTopics(List<string> topics)
        {
            return Topics;
        }
        public List<string> AllPeriods(List<string> periods)
        {
            return Periods;
        }
        public List<string> AllPreparations(List<string> preparations)
        {
            return Preparations;
        }
    }
}
