using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CountdownWeckerLibrary
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CountdownWeckerLibrary"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CountdownWeckerLibrary;assembly=CountdownWeckerLibrary"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class Wecker : Control
    {
        static Wecker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Wecker), new FrameworkPropertyMetadata(typeof(Wecker)));
        }

        public static readonly DependencyProperty
    CountdownTimeProperty = DependencyProperty.Register(
        "CountdownTime",
        typeof(TimeSpan),
        typeof(Wecker),
        new FrameworkPropertyMetadata(TimeSpan.Zero));

        public TimeSpan CountdownTime
        {
            get { return (TimeSpan)GetValue(CountdownTimeProperty); }
            set { SetValue(CountdownTimeProperty, value); }
        }

        public static readonly DependencyProperty
            RemainingTimeProperty = DependencyProperty.Register(
                "RemainingTime",
                typeof(TimeSpan),
                typeof(Wecker),
                new FrameworkPropertyMetadata(TimeSpan.Zero));

        public static readonly DependencyProperty
        AlarmSetProperty = DependencyProperty.Register(
                "AlarmSet",
              typeof(bool),
                 typeof(Wecker),
        new FrameworkPropertyMetadata(
           false,
           FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool AlarmSet
        {
            get { return (bool)base.GetValue(AlarmSetProperty); }
            set { base.SetValue(AlarmSetProperty, value); }
        }

        public TimeSpan RemainingTime
        {
            get { return (TimeSpan)GetValue(RemainingTimeProperty); }
            set { SetValue(RemainingTimeProperty, value); }
        }


        public static readonly RoutedEvent AlarmEvent =
           EventManager.RegisterRoutedEvent("Alarm",
             RoutingStrategy.Bubble, typeof(RoutedEventHandler),
             typeof(Wecker));

        public event RoutedEventHandler Alarm
        {
            add { base.AddHandler(AlarmEvent, value); }
            remove { base.RemoveHandler(AlarmEvent, value); }
        }

        protected void FireAlarm()
        {
            base.RaiseEvent(new RoutedEventArgs(AlarmEvent));
        }

        protected void RingAlarm()
        {
            SoundPlayer sp = new SoundPlayer(@"c:\windows\media\tada.wav");
            sp.Play();
            FireAlarm();
        }

        private DateTime _lastTick;

        public void OnDisplayTimerTick(object o, EventArgs args)
        {
           // if (!AlarmSet) return;

            var now = DateTime.Now;
            var elapsed = now - _lastTick;
            _lastTick = now;

            RemainingTime -= elapsed;

            if (RemainingTime <= TimeSpan.Zero)
            {
                RemainingTime = TimeSpan.Zero;
              //  AlarmSet = false;
                RingAlarm();
            }
        }

        void OnShowSetAlarmDlg(object sender, RoutedEventArgs ea)
        {
            var dlg = new DateTimeDlg();
            if (dlg.ShowDialog() == true)
            {
                int minutes = dlg.Minutes;
                int seconds = dlg.Seconds;

                if (minutes > 59) minutes = 59;
                if (seconds > 59) seconds = 59;

                CountdownTime = new TimeSpan(0, minutes, seconds);
                RemainingTime = CountdownTime;

                _lastTick = DateTime.Now;
               // AlarmSet = true;
            }
        }


        System.Windows.Threading.DispatcherTimer displayTimer;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Button bSetAlarmDlg =
                (Button)this.Template.FindName("PART_SETALARMBUTTON", this);

            bSetAlarmDlg.Click += OnShowSetAlarmDlg;

            displayTimer = new System.Windows.Threading.DispatcherTimer();
            displayTimer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            displayTimer.Tick += OnDisplayTimerTick;

            displayTimer.Start();

            // Set up a databinding between the checkbox in the template and the flag...
            CheckBox cbAlarmSet = (CheckBox)this.Template.FindName("PART_CHECKBOXALARMSET", this);
            Binding bindingAlarmSet = new Binding();
            bindingAlarmSet.Source = this;
            bindingAlarmSet.Path = new PropertyPath("AlarmSet");
            cbAlarmSet.SetBinding(CheckBox.IsCheckedProperty, bindingAlarmSet);

            TextBlock tbAlarmSetButtonTextPane = (TextBlock)this.Template.FindName("PART_SETALARMBUTTONTEXTPANE", this);
            Binding binding = new Binding("RemainingTime");
            binding.Source = this;
            binding.StringFormat = @"mm\:ss";
            tbAlarmSetButtonTextPane.SetBinding(TextBlock.TextProperty, binding);
        }
    }

}