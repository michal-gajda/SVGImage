using System;
using System.Windows;
using System.ComponentModel;

namespace SvgTestBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string AppTitle      = "SvgTestBox";
        public const string AppErrorTitle = "SvgTestBox - Error";

        #region Private Fields

        private bool _isShown;

        private SvgPage _svgPage;
        private XamlPage _xamlPage;
        private DebugPage _debugPage;

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded  += OnWindowLoaded;
            this.Closing += OnWindowClosing;
        }

        #endregion

        #region Protected Methods

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            if (_isShown)
            {
                return;
            }
            _isShown = true;

            if (_debugPage != null)
            {
                if (!_debugPage.IsTraceStarted)
                {
                    _debugPage.Startup();
                }
            }

            if (_svgPage != null)
            {
                _svgPage.InitializeDocument();
            }

            tabSvgInput.IsSelected = true;
        }

        #endregion

        #region Private Methods

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            // Retrieve the display pages...
            _svgPage   = frameSvgInput.Content   as SvgPage;
            _xamlPage  = frameXamlOutput.Content as XamlPage;
            _debugPage = frameDebugging.Content  as DebugPage;

            if (_svgPage != null && _xamlPage != null)
            {
                _svgPage.XamlPage = _xamlPage;
            }

            if (_debugPage != null)
            {
                if (!_debugPage.IsTraceStarted)
                {
                    _debugPage.Startup();
                }
            }
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (_debugPage != null)
            {
                _debugPage.Shutdown();
            }
        }

        #endregion
    }
}
