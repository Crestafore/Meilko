using Meilko.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meilko
{
    public partial class ParentControl : Form
    {
        private bool[] isDeleted = new bool[3];
        //FONT
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;
        //FONT

        public ParentControl()
        {
            //FONT
            byte[] fontData = Properties.Resources.aero_matics_bold;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.aero_matics_bold.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.aero_matics_bold.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            myFont = new Font(fonts.Families[0], 16.0F);
            //FONT
            InitializeComponent();
            setFont();
        }

        private void pbDodaj_Click(object sender, EventArgs e)
        {
            hideContacts();
            showContactControls();
        }

        private void setFont()
        {
            btnNajava.Font = new Font(fonts.Families[0], 18);
            btnDodaj.Font = new Font(fonts.Families[0], 14);
            btnZacuvaj.Font = new Font(fonts.Families[0], 14);

            lblContacts.Font = new Font(fonts.Families[0], 16);
            lblGodini.Font = new Font(fonts.Families[0], 16);
            lblIme.Font = new Font(fonts.Families[0], 16);
            lblInfo.Font = new Font(fonts.Families[0], 16);
            lblKontaktGodini.Font = new Font(fonts.Families[0], 16);
            lblKontaktIme.Font = new Font(fonts.Families[0], 16);
            lblKontaktPosta.Font = new Font(fonts.Families[0], 16);
            lblLozinka.Font = new Font(fonts.Families[0], 16);
            lblMail.Font = new Font(fonts.Families[0], 16);
            lblPanel.Font = new Font(fonts.Families[0], 36);
            lblPass.Font = new Font(fonts.Families[0], 16);
            lblPosta.Font = new Font(fonts.Families[0], 16);

            tbInfoGodini.Font = new Font(fonts.Families[0], 16);
            tbInfoIme.Font = new Font(fonts.Families[0], 16);
            tbInfoLozinka.Font = new Font(fonts.Families[0], 16);
            tbInfoPosta.Font = new Font(fonts.Families[0], 16);
            tbKontaktGodini.Font = new Font(fonts.Families[0], 16);
            tbKontaktIme.Font = new Font(fonts.Families[0], 16);
            tbKontaktPosta.Font = new Font(fonts.Families[0], 16);
            tbLozinka.Font = new Font(fonts.Families[0], 20);
            tbPosta.Font = new Font(fonts.Families[0], 20);
        }

        private void hideContacts()
        {
            pbKontakt1.Visible = false;
            pbKontakt2.Visible = false;
            pbKontakt3.Visible = false;

            pbDelete1.Visible = false;
            pbDelete2.Visible = false;
            pbDelete3.Visible = false;

            pbDodaj.Visible = false;
        }

        private void showContacts()
        {
            if (!isDeleted[0])
            {
                pbKontakt1.Visible = true;
                pbDelete1.Visible = true;
            }
            if (!isDeleted[1])
            {
                pbKontakt2.Visible = true;
                pbDelete2.Visible = true;
            }
            if (!isDeleted[2])
            {
                pbDelete3.Visible = true;
                pbKontakt3.Visible = true;
            }
            pbDodaj.Visible = true;
        }

        private void showContactControls()
        {
            lblKontaktGodini.Visible = true;
            lblKontaktIme.Visible = true;
            lblKontaktPosta.Visible = true;

            tbKontaktGodini.Visible = true;
            tbKontaktIme.Visible = true;
            tbKontaktPosta.Visible = true;

            btnDodaj.Visible = true;
        }

        private void hideContactControls()
        {
            lblKontaktGodini.Visible = false;
            lblKontaktIme.Visible = false;
            lblKontaktPosta.Visible = false;

            tbKontaktGodini.Visible = false;
            tbKontaktIme.Visible = false;
            tbKontaktPosta.Visible = false;

            btnDodaj.Visible = false;
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            hideContactControls();
            showContacts();
        }

        private void pbDelete1_Click(object sender, EventArgs e)
        {
            pbDelete1.Visible = false;
            pbKontakt1.Visible = false;
            isDeleted[0] = true;
        }

        private void pbDelete2_Click(object sender, EventArgs e)
        {
            pbDelete2.Visible = false;
            pbKontakt2.Visible = false;
            isDeleted[1] = true;
        }

        private void pbDelete3_Click(object sender, EventArgs e)
        {
            pbDelete3.Visible = false;
            pbKontakt3.Visible = false;
            isDeleted[2] = true;
        }

        private void btnNajava_Click(object sender, EventArgs e)
        {
            hideMainControls();
            showParentPanel();
        }
        
        private void hideMainControls()
        {
            lblPass.Visible = false;
            lblPosta.Visible = false;
            tbLozinka.Visible = false;
            tbPosta.Visible = false;
            btnNajava.Visible = false;
        }

        private void showParentPanel()
        {
            lblPanel.Visible = true;
            lblInfo.Visible = true;
            lblContacts.Visible = true;
            pnlInfo.Visible = true;
            pnlKontakti.Visible = true;
            BackgroundImage = Resources.background_empty;
        }
    }
}
