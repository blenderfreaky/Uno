namespace Uno
{
    using BeepLive.Net;
    using ProtoBuf;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NetTcpClient TcpClient;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Round()
        {

        }
    }

    [ProtoContract]
    public class StartPacket
    {
        [ProtoMember(1)] public
    }
}
