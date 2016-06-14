using Meilko.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meilko
{
    public partial class Meilko : Form
    {
        private bool paint = false, loadedOnce = false;
        private SolidBrush brush = new SolidBrush(Color.Black);
        private Point brushSize = new Point(10, 10);
        private Point stickerPosition = new Point(0, 0);
        private int spaceLeft = 500, state = 0;
        private bool[] added = new bool[3];

        //FONT
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;
        //FONT

        public Meilko()
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

            DoubleBuffered = true;
            InitializeComponent();

            btnRoditel.Font = new Font(fonts.Families[0], 20);
        }

        private void prepareInboxScreen()
        {
            BackgroundImage = Resources.background_empty;
            hideControls();
            pbInbox1.Visible = true;
            pbInbox2.Visible = true;
            pbInbox3.Visible = true;
            pbNovaPoraka.Visible = true;
            pbBack.Visible = true;
            pbHome.Visible = true;
            pbPoraka.Visible = true;
        }

        private void hideControls()
        {
            pbPosta.Visible = false;
            btnRoditel.Visible = false;
        }

        private void pbInbox1_Click(object sender, EventArgs e)
        {
            pbInbox1.BackgroundImage = Resources.text1_selected;
            pbInbox2.BackgroundImage = Resources.text2;
            pbInbox3.BackgroundImage = Resources.text3;
            pbPoraka.Image = Resources.poraka1;
            pbPoraka.Visible = true;
        }

        private void pbInbox2_Click(object sender, EventArgs e)
        {
            pbInbox1.BackgroundImage = Resources.text1;
            pbInbox2.BackgroundImage = Resources.text2_selected;
            pbInbox3.BackgroundImage = Resources.text3;
            pbPoraka.Image = Resources.poraka2;
            pbPoraka.Visible = true;
        }

        private void pbInbox3_Click(object sender, EventArgs e)
        {
            pbInbox1.BackgroundImage = Resources.text1;
            pbInbox2.BackgroundImage = Resources.text2;
            pbInbox3.BackgroundImage = Resources.text3_selected;
            pbPoraka.Image = Resources.poraka3;
            pbPoraka.Visible = true;
        }

        private void pbNovaPoraka_Click(object sender, EventArgs e)
        {
            state++;
            hideInboxControls();
            showNewMessageControls();
            if (!loadedOnce)
            {
                loadDefaultStickers();
                loadedOnce = true;
            }
        }

        private void hideInboxControls()
        {
            pbInbox1.Visible = false;
            pbInbox2.Visible = false;
            pbInbox3.Visible = false;
            pbPoraka.Visible = false;
            pbNovaPoraka.Visible = false;
        }

        private void pnlCrtez_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
        }

        private void pnlCrtez_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {
                Graphics g = pnlCrtez.CreateGraphics();
                g.FillEllipse(brush, e.X, e.Y, brushSize.X, brushSize.Y);
                g.Dispose();
                Invalidate();
            }
        }

        private void pnlCrtez_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
        }

        private void showNewMessageControls()
        {
            pnlCrtez.Visible = true;
            pnlCrtez.BackColor = Color.White;
            pbBe.Visible = true;
            pbBe.Image = Resources.be;
            pbR.Visible = true;
            pbR.Image = Resources.r;
            pbG.Visible = true;
            pbG.Image = Resources.g;
            pbY.Visible = true;
            pbY.Image = Resources.y;
            pbBk.Visible = true;
            pbBk.Image = Resources.bk;

            pbPu.Visible = true;
            pbPu.Image = Resources.pu;
            pbO.Visible = true;
            pbO.Image = Resources.o;
            pbS.Visible = true;
            pbS.Image = Resources.s;
            pbBr.Visible = true;
            pbBr.Image = Resources.br;
            pbPi.Visible = true;
            pbPi.Image = Resources.pi;

            pbE.Visible = true;
            pbE.Image = Resources.e;

            pnlSliki.Visible = true;
            pbMinus.Visible = true;
            pbPlus.Visible = true;
            pbTrash.Visible = true;
            pbContact1.Visible = true;
            pbContact2.Visible = true;
            pbContact3.Visible = true;
            pbIsprati.Visible = true;

        }

        private void displayImage(object sender, EventArgs e)
        {
            Graphics g = pnlCrtez.CreateGraphics();
            PictureBox pb = ((PictureBox)sender);
            if(spaceLeft < pb.Width)
            {
                stickerPosition.Y += 100;
                stickerPosition.X = 0;
                spaceLeft = 500;
            }
            g.DrawImage(pb.Image, stickerPosition);
            stickerPosition.X += pb.Width;
            spaceLeft -= pb.Width;
        }

        private void loadDefaultStickers()
        {
            Image[] images = { Resources._1, Resources._2, Resources._3, Resources._4, Resources._5,
            Resources._6, Resources._7, Resources._8, Resources._9, Resources._10, Resources._11,
            Resources._12, Resources._13, Resources._14, Resources._15, Resources._16, Resources._17,
            Resources._18, Resources._19, Resources._20};
            int x = 0;
            foreach(Image image in images)
            {
                PictureBox pb = new PictureBox();
                pb.Image = image;
                pb.Location = new Point(x, 0);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Width = 80;
                pb.Height = 72;
                pb.Click += new EventHandler(displayImage);
                pnlSliki.Controls.Add(pb);
                x += pb.Width + 30;
            }
        }

        private void pbR_Click(object sender, EventArgs e)
        {
            brush.Color = Color.Red;
        }

        private void pbY_Click(object sender, EventArgs e)
        {
            brush.Color = Color.Yellow;

        }

        private void pbG_Click(object sender, EventArgs e)
        {
            brush.Color = Color.Green;
        }

        private void pbBe_Click(object sender, EventArgs e)
        {
            brush.Color = Color.Blue;
        }

        private void pbBk_Click(object sender, EventArgs e)
        {
            brush.Color = Color.Black;
        }

        private void pbE_Click(object sender, EventArgs e)
        {
            brush.Color = Color.White;
        }

        protected override void WndProc(ref Message m)
        {
            // Suppress the WM_UPDATEUISTATE message
            if (m.Msg == 0x128) return;
            base.WndProc(ref m);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (brushSize.X > 10 && brushSize.Y > 10)
            {
                brushSize.X -= 5;
                brushSize.Y -= 5;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            brushSize.X += 5;
            brushSize.Y += 5;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pnlCrtez.Invalidate();
            stickerPosition = new Point(0, 0);
            spaceLeft = 500;

        }

        private void pbPu_Click(object sender, EventArgs e)
        {
            brush.Color = Color.Purple;
        }

        private void pbO_Click(object sender, EventArgs e)
        {
            brush.Color = Color.Orange;

        }

        private void pbS_Click(object sender, EventArgs e)
        {
            brush.Color = Color.SkyBlue;
        }

        private void pbBr_Click(object sender, EventArgs e)
        {
            brush.Color = Color.Brown;
        }

        private void pbPi_Click(object sender, EventArgs e)
        {

            brush.Color = Color.Pink;
        }

        private void pbContact1_Click(object sender, EventArgs e)
        {
            if (state == 2)
            {
                if (added[0])
                {
                    pbContact1.Image = Resources.text1_not_added;
                    added[0] = false;
                }
                else
                {
                    pbContact1.Image = Resources.text1_added;
                    added[0] = true;
                }
            }

        }

        private void pbContact2_Click(object sender, EventArgs e)
        {
            if (state == 2)
            {
                if (added[1])
                {
                    pbContact2.Image = Resources.text2_not_added;
                    added[1] = false;
                }
                else
                {
                    pbContact2.Image = Resources.text2_added;
                    added[1] = true;
                }
            }

        }

        private void pbContact3_Click(object sender, EventArgs e)
        {
            if(state == 2)
            {
                if (added[2])
                {
                    pbContact3.Image = Resources.text3_not_added;
                    added[2] = false;
                }
                else
                {
                    pbContact3.Image = Resources.text3_added;
                    added[2] = true;
                }
            }

        }

        private void pbIsprati_Click(object sender, EventArgs e)
        {
            state++;
            hideMailCreationDetails();
            sentMailTo();
            pnlCrtez.Visible = true;
            pnlCrtez.BackColor = Color.Transparent;
            pbMail.Visible = true;
        }

        private void sentMailTo()
        {
            if (added[0])
            {
                pbContact1.Visible = true;
            }
            if (added[1])
            {
                pbContact2.Visible = true;
            }
            if (added[2])
            {
                pbContact3.Visible = true;
            }
        }

        private void hideMailCreationDetails()
        {
            pnlCrtez.Visible = false;

            pbBe.Visible = false;
            pbR.Visible = false;
            pbG.Visible = false;
            pbY.Visible = false;
            pbBk.Visible = false;
            pbPu.Visible = false;
            pbO.Visible = false;
            pbS.Visible = false;
            pbBr.Visible = false;
            pbPi.Visible = false;
            pbE.Visible = false;

            pnlSliki.Visible = false;
            pnlCrtez.Visible = false;
            pbMinus.Visible = false;
            pbPlus.Visible = false;
            pbTrash.Visible = false;
            pbContact1.Visible = false;
            pbContact2.Visible = false;
            pbContact3.Visible = false;
            pbIsprati.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ParentControl form = new ParentControl();
            form.Show();
        }

        private void pbHome_Click(object sender, EventArgs e)
        {
            state = 0;
            hideInboxControls();
            hideMailCreationDetails();
            hideControls();
            showState1Controls();
            pbMail.Visible = false;
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            if(state == 1)
            {
                hideInboxControls();
                showState1Controls();
            }
            else if (state == 2)
            {
                showState2Controls();
            }
            else if (state == 3)
            {
                showState3Controls();
            }
            state--;
        }

        private void showState1Controls()
        {
            BackgroundImage = Resources.first_bg;
            pbPosta.Visible = true;
            btnRoditel.Visible = true;
            pbBack.Visible = false;
            pbHome.Visible = false;
        }

        private void showState2Controls()
        {
            hideMailCreationDetails();
            prepareInboxScreen();
            pbPoraka.Visible = true;
        }

        private void pnlCrtez_Paint(object sender, PaintEventArgs e)
        {

        }

        private void showState3Controls()
        {
            pbMail.Visible = false;
            showNewMessageControls();
        }

        private void pbPosta_Click(object sender, EventArgs e)
        {
            prepareInboxScreen();
            hideControls();
            state++;
        }
    }
}
