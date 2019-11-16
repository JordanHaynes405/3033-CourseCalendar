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
        public int i = 0;
        public AddContent()
        {
            InitializeComponent();

            MainWindow MainPageInput = new MainWindow();

            //lblAddTopic.Content = $"Add Topic for Class # {i}";
            i++;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainPageInput = new MainWindow();

            //MainPageInput.CourseTable.Columns.Add(new DataColumn("Date", Type.GetType("System.DateTime")));
            MainPageInput.CourseTable.Columns.Add(new DataColumn("Topic Name", Type.GetType("System.String")));
            MainPageInput.CourseTable.Columns.Add(new DataColumn("Periods", Type.GetType("System.String")));
            MainPageInput.CourseTable.Columns.Add(new DataColumn("Preparation", Type.GetType("System.String"))); 

            MainPageInput.CourseTable.Rows.Add(txtTopicName.Text, txtNumTopicPeriods.Text, txtTopicPreparation.Text);//pcrTopicStart.Text,

            MainPageInput.dgPreview.ItemsSource = MainPageInput.CourseTable.DefaultView;

            AddToList();

            this.Close();
        }

        public void AddToList()
        {
            CourseContent CContent = new CourseContent();
            List<string> Topics = new List<string>();
            CContent.Topics = Topics;
                List<string> Periods = new List<string>();
                Periods = CContent.Periods;

                List<string> Preparations = new List<string>();
                Preparations = CContent.Preparations
            
            //CContent.AddToList(txtTopicName.Text, txtNumTopicPeriods.Text, txtNumTopicPeriods.Text);

            //CContent.Periods.Add(txtNumTopicPeriods.Text);
            //CContent.Preparations.Add(txtNumTopicPeriods.Text);
        }
    }
}
