using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace GameUI
{
    internal class ColorForm : Form
    {
        private const int k_NumberOfColors = 8;
        private readonly Button[] r_Colors;
        private Button m_ParentButton;
        private readonly List<int> r_ChosenColors;
        internal Color m_PrevParentColor;
        private bool m_ClosingFlag;

        public ColorForm(Button i_ParentButton, List<int> i_ChosenColors)
        {
            m_ParentButton = i_ParentButton;
            r_ChosenColors = i_ChosenColors;
            m_PrevParentColor = DefaultBackColor;
            m_ClosingFlag = false;
            this.Text = "Pick A Color:";
            this.Height = 160;
            this.Width = 230;
            this.StartPosition = FormStartPosition.CenterScreen;
            r_Colors = new Button[k_NumberOfColors];

            for (int i = 0; i < r_Colors.Length; i++)
            {
                r_Colors[i] = new Button();
                r_Colors[i].BackColor = Color.FromName(Enum.GetName(typeof(FormGame.eColorChoice), (FormGame.eColorChoice)i));
                if (i / 4 == 0)
                {
                    r_Colors[i].Location = new Point(15 + i * 45, 15);
                }
                else
                {
                    r_Colors[i].Location = new Point(15 + (i - 4) * 45, 65);
                }
                r_Colors[i].Height = 40;
                r_Colors[i].Width = 40;
                if (r_ChosenColors.Contains(i))
                {
                    r_Colors[i].Enabled = false;
                }
                else
                {
                    r_Colors[i].Enabled = true;
                }
                this.Controls.Add(r_Colors[i]);
                r_Colors[i].Click += new EventHandler(r_Colors_Click);
            }
            this.ShowDialog();
        }

        private void r_Colors_Click(Object sender, EventArgs e)
        {
            if (!m_ParentButton.BackColor.Equals(DefaultBackColor))
            {
                m_PrevParentColor = m_ParentButton.BackColor;
            }
            m_ParentButton.BackColor = (sender as Button).BackColor;
            m_ClosingFlag = true;
            this.Close();
        }

        internal bool ClosingFlag
        {
            get
            {
                return m_ClosingFlag;
            }
        }
    }
}
