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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RingCentral;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var rc = new RestClient("", "", "https://platform.ringcentral.com");
            await rc.Authorize("", "", "");
            var request = new PerformanceCallsTimelineRequest { 
                grouping = new TimelinePerformanceCallsGrouping { groupBy = "Users"},
                timeSettings = new PerformanceCallsTimeSettings { timeRange = new PerformanceCallsTimeRange { 
                    timeFrom = "2022-02-21T00:00.000Z"
                } },
                responseOptions = new TimelineResponseDataOptions { 
                    timers = new TimelineTimersResponseOptions { allCallsDuration = true}
                }
            };
            var response = await rc.Analytics().Phone().Performance().V1().Accounts("~").Calls().Timeline().Post(request);
            MessageBox.Show(response.data.Length.ToString());
        }
    }
}
