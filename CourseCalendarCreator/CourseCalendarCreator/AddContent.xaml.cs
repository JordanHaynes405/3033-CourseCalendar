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
using System.Windows.Shapes;

namespace CourseCalendarCreator
{
    /// <summary>
    /// Interaction logic for AddContent.xaml
    /// </summary>
    public partial class AddContent : Window
    {
        public AddContent()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //txtTopicName;
            //txtNumTopicPeriods;
            //txtTopicPreparation;


            //Worked with Saurabh
            AddContent ValuesToTable = new AddContent();

            System.Data.DataTable CourseTable = new System.Data.DataTable();

            CourseTable.Rows.Add();
            CourseTable.Rows.Add();
            CourseTable.Rows.Add();
            CourseTable.Rows.Add();
            //CourseTable.Columns.Add("Date");
            CourseTable.Columns.Add("Topic Name");
            CourseTable.Columns.Add("Periods");
            CourseTable.Columns.Add("Preparation");

            CourseTable.Rows.Add(ValuesToTable.txtTopicName.Text , ValuesToTable.txtNumTopicPeriods.Text, ValuesToTable.txtTopicPreparation.Text);

            DataGrid dgvPreview = new DataGrid();


            this.Close();
        }
    }
}
