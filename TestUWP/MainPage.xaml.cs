using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
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
            this.SizeChanged += MainPage_SizeChanged;

            _previousTransform = new MatrixTransform();
            _deltaTransform = new CompositeTransform();
            TransformGroup = new TransformGroup();
            TransformGroup.Children.Add(_previousTransform);
            TransformGroup.Children.Add(_deltaTransform);

            DataContext = this;
        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            OverlayGrid(true);
        }

        readonly float _gridSize = 100;

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

        /// <summary>
        /// Rectangle that represents the bounding box
        /// </summary>
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
            // Reset everything
            _previousTransform.Matrix = Matrix.Identity;
            _deltaTransform.Rotation = 0.0;
            _deltaTransform.CenterX = 0;
            _deltaTransform.CenterY = 0;
            _deltaTransform.ScaleX = 1;
            _deltaTransform.ScaleY = 1;
            _deltaTransform.TranslateX = 0;
            _deltaTransform.TranslateY = 0;
        }

        void OverlayGrid(bool includeCoordinates = false)
        {
            int rows = (int)(MainGrid.ActualWidth / _gridSize) + 1;
            int cols = (int)(MainGrid.ActualHeight / _gridSize) + 1;

            LineGrid.Children.Clear();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    CreateGridRect(r, c, includeCoordinates);
                }
            }
        }

        void CreateGridRect(int row, int col, bool includeCoordinates = false)
        {
            Rectangle rct = new Rectangle();
            rct.Stroke = new SolidColorBrush(Colors.LightSkyBlue);
            rct.Width = _gridSize;
            rct.Height = _gridSize;
            rct.HorizontalAlignment = HorizontalAlignment.Left;
            rct.VerticalAlignment = VerticalAlignment.Top;

            rct.Margin = new Thickness(row * _gridSize, col * _gridSize, 0, 0);
            rct.Visibility = Visibility.Visible;
            LineGrid.Children.Add(rct);

            if (includeCoordinates)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = $"({row * _gridSize}, {col * _gridSize})";
                textBlock.Foreground = new SolidColorBrush(Colors.Gray);
                textBlock.FontSize = 8;

                textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                textBlock.VerticalAlignment = VerticalAlignment.Top;
                textBlock.Margin = new Thickness(row * _gridSize + 1, col * _gridSize, 0, 0);
                textBlock.Visibility = Visibility.Visible;

                LineGrid.Children.Add(textBlock);
            }
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
