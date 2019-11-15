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

            string Topic = txtTopicName.Text;
            string Periods = txtNumTopicPeriods.Text;
            string Preparation = txtTopicPreparation.Text;

            //MainPageInput.CourseTable.Columns.Add(new DataColumn("Date", Type.GetType("System.DateTime")));
            MainPageInput.CourseTable.Columns.Add(new DataColumn("Topic Name", Type.GetType("System.String")));
            MainPageInput.CourseTable.Columns.Add(new DataColumn("Periods", Type.GetType("System.String")));
            MainPageInput.CourseTable.Columns.Add(new DataColumn("Preparation", Type.GetType("System.String"))); 

            MainPageInput.CourseTable.Rows.Add(txtTopicName.Text, txtNumTopicPeriods.Text, txtTopicPreparation.Text);//pcrTopicStart.Text,

            MainPageInput.dgPreview.ItemsSource = MainPageInput.CourseTable.DefaultView;

            CourseContent CContent = new CourseContent();
            CContent.Topics.Add(Topic);
            CContent.Periods.Add(Periods);
            CContent.Preparations.Add(Preparation);

            this.Close();
        }
    }
}
