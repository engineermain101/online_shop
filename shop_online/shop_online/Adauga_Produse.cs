using System.Windows.Forms;

namespace shop_online
{
    public partial class Adauga_Produse : Form
    {
        public Adauga_Produse()
        {
            InitializeComponent();
        }

        private void Adauga_Produse_FormClosed( object sender, FormClosedEventArgs e )
        {
            Application.Exit();
        }

        private void Adauga_Produse_Load( object sender, System.EventArgs e )
        {

        }
    }
}
