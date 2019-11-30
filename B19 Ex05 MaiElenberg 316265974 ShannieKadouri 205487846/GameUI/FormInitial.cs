using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace GameUI
{
    public class FormInitial : Form
    {

        internal const int k_MaxNumGuess = 10;
        internal const int k_MinNumGuess = 4;
        private readonly List<int> r_ComputerChoice;
        private int m_MaxNumGuess;
        private Button m_NumberChancesButton;
        private Button m_StartGameButton;
        private bool m_FlagStartGame;

        internal FormInitial()
        {
            r_ComputerChoice = computerChoice();
            m_MaxNumGuess = k_MinNumGuess;
            m_FlagStartGame = false;

            this.Text = "Bool Pgia";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Height = 135;
            this.Width = 250;

            m_NumberChancesButton = new Button();
            m_NumberChancesButton.Text = "Number of chances: " + k_MinNumGuess;
            m_NumberChancesButton.Location = new Point(10, 10);
            m_NumberChancesButton.Width = this.ClientSize.Width - 16;
            this.Controls.Add(m_NumberChancesButton);
            m_NumberChancesButton.Click += new EventHandler(numberChancesButton_Click);

            m_StartGameButton = new Button();
            m_StartGameButton.Text = "Start";
            m_StartGameButton.Location = new Point(m_NumberChancesButton.Width - 70,
                this.ClientSize.Height - m_NumberChancesButton.Height - 5);
            this.Controls.Add(m_StartGameButton);
            m_StartGameButton.Click += new EventHandler(startGameButton_Click);

            this.MaximizeBox = false;
            this.ShowDialog();
        }

        private List<int> computerChoice()
        {
            List<int> colors = new List<int>(k_MinNumGuess);
            Random currentChoice = new Random();
            for (int i = 0; i < k_MinNumGuess; i++)
            {
                int colorChoice = currentChoice.Next((int)FormGame.eColorChoice.LightGoldenrodYellow);

                while (colors.Contains(colorChoice))
                {
                    colorChoice = currentChoice.Next((int)FormGame.eColorChoice.LightGoldenrodYellow);
                }
                colors.Add(colorChoice);
            }

            return colors;
        }

        private void numberChancesButton_Click(Object sender, EventArgs e)
        {
            if (m_MaxNumGuess == k_MaxNumGuess)
            {
                m_MaxNumGuess = k_MinNumGuess;
                (sender as Button).Text = "Number of chances: " + k_MinNumGuess;
            }
            else
            {
                m_MaxNumGuess++;
                (sender as Button).Text = "Number of chances: " + m_MaxNumGuess;
            }
        }

        private void startGameButton_Click(Object sender, EventArgs e)
        {
            m_FlagStartGame = true;
            this.Close();
        }

        internal List<int> ColorList
        {
            get
            {
                return r_ComputerChoice;
            }
        }

        internal int MaxNumGuess
        {
            get
            {
                return m_MaxNumGuess;
            }
        }

        internal bool FlagStartGame
        {
            get
            {
                return m_FlagStartGame;
            }
        }
    }
}
