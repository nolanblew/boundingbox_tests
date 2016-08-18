using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TestUWP.Models;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TestUWP.Controls
{
    public sealed partial class PeopleControl : UserControl
    {
        public PeopleControl()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty PersonProperty = DependencyProperty.Register(
            nameof(Person),
            typeof(Person),
            typeof(PeopleControl),
            new PropertyMetadata(default(Person)));

        public Person Person
        {
            get { return (Person)GetValue(PersonProperty); }
            set { SetValue(PersonProperty, value); }
        }
    }
}
