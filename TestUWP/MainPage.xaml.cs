using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using TestUWP.Annotations;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public MainPage()
        {
            this.InitializeComponent();

            _previousTransform = new MatrixTransform();
            _deltaTransform = new CompositeTransform();
            TransformGroup = new TransformGroup();
            TransformGroup.Children.Add(_previousTransform);
            TransformGroup.Children.Add(_deltaTransform);

            DataContext = this;
        }

        readonly MatrixTransform _previousTransform;

        readonly CompositeTransform _deltaTransform;

        TransformGroup _transformGroup;

        public TransformGroup TransformGroup
        {
            get { return _transformGroup; }
            set
            {
                if (_transformGroup != value)
                {
                    _transformGroup = value;
                    OnPropertyChanged();
                }
            }
        }

        Rect _boundingRect;

        public Rect BoundingRect
        {
            get { return _boundingRect; }
            set
            {
                if (_boundingRect != value)
                {
                    _boundingRect = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void Viewbox_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            _previousTransform.Matrix = _transformGroup.Value;

            var center = _previousTransform.TransformPoint(e.Position);
            _deltaTransform.CenterX = center.X;
            _deltaTransform.CenterY = center.Y;

            _deltaTransform.Rotation = e.Delta.Rotation;

            _deltaTransform.ScaleX = e.Delta.Scale;
            _deltaTransform.ScaleY = e.Delta.Scale;

            _deltaTransform.TranslateX = e.Delta.Translation.X;
            _deltaTransform.TranslateY = e.Delta.Translation.Y;

            var control = e.Container as FrameworkElement;
            var controlRect = new Rect(0, 0, control.DesiredSize.Width, control.DesiredSize.Height);

            BoundingRect = _transformGroup.TransformBounds(controlRect);
        }

        void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _previousTransform.Matrix = Matrix.Identity;
            _deltaTransform.Rotation = 0.0;
            _deltaTransform.CenterX = 0;
            _deltaTransform.CenterY = 0;
            _deltaTransform.ScaleX = 1;
            _deltaTransform.ScaleY = 1;
            _deltaTransform.TranslateX = 0;
            _deltaTransform.TranslateY = 0;
        }
    }
}
