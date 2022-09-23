using System.Data.Entity;
namespace ITMO._2022.CSharp.ADO.labs._9
{
    public partial class CustomerViewer : Form
    {
        SampleContext context;
        byte[] Ph;
        public CustomerViewer()
        {
            InitializeComponent();
        }
    }
}