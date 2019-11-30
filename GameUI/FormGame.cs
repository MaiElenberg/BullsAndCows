using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace GameUI
{
    public class FormGame : Form
    {
        public enum eColorChoice
        {
            MediumPurple,
            LightSkyBlue,
            Pink,
            LightGreen,
            PeachPuff,
            Aquamarine,
            LightCoral,
            LightGoldenrodYellow
        }

        internal const int k_NumberOfChoices = 4;
        private FormInitial formInit;
        private readonly Button[] r_HiddenChoices;
        private int m_CurrentTurn;
        private readonly Button[,] r_UserChoices;
        private readonly Button[,] r_UserResults;
        private readonly Button[] r_Errows;
        private int m_PlayerNumberOfColorPick;
        private readonly List<int> r_ChosenColors;

        public FormGame()
        {
            formInit = new FormInitial();
            if(!formInit.FlagStartGame)
            {
                return;
            }
            m_CurrentTurn = 0;
            r_UserChoices = new Button[formInit.MaxNumGuess, k_NumberOfChoices];
            r_UserResults = new Button[formInit.MaxNumGuess, k_NumberOfChoices];
            r_HiddenChoices = new Button[k_NumberOfChoices];
            r_Errows = new Button[formInit.MaxNumGuess];
            r_ChosenColors = new List<int>();
            m_PlayerNumberOfColorPick = 0;

            if (formInit.FlagStartGame)
            {
                gameFlow();
            }
            this.MaximizeBox = false;
            this.ShowDialog();
        }

        internal void gameFlow()
        {
            this.Text = "Bool Pgia";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Height = (formInit.MaxNumGuess + 1) * 45 + 90;
            this.Width = 310;

            initialBlackButtons();
            initialBord();
            playTurn();
        }

        private void initialBlackButtons()
        {
            for (int i = 0; i < r_HiddenChoices.Length; i++)
            {
                r_HiddenChoices[i] = new Button();
                r_HiddenChoices[i].BackColor = Color.Black;
                r_HiddenChoices[i].Location = new Point(15 + i * 45, 15);
                r_HiddenChoices[i].Height = 40;
                r_HiddenChoices[i].Width = 40;
                r_HiddenChoices[i].Enabled = false;
                this.Controls.Add(r_HiddenChoices[i]);
            }
        }

        private void initialBord()
        {
            for (int i = 0; i < r_UserChoices.GetLength(0); i++)
            {
                for (int j = 0; j < r_UserChoices.GetLength(1); j++)
                {
                    r_UserChoices[i, j] = new Button();
                    r_UserChoices[i, j].Location = new Point(15 + j * 45, 85 + i * 45);
                    r_UserChoices[i, j].Height = 40;
                    r_UserChoices[i, j].Width = 40;
                    if (i == 0)
                    {
                        r_UserChoices[i, j].Enabled = true;
                    }
                    else
                    {
                        r_UserChoices[i, j].Enabled = false;
                    }
                    this.Controls.Add(r_UserChoices[i, j]);
                    r_UserChoices[i, j].Click += new EventHandler(userChoices_Click);
                }
            }

            for (int i = 0; i < r_Errows.Length; i++)
            {
                r_Errows[i] = new Button();
                r_Errows[i].Text = "-->>";
                r_Errows[i].Location = new Point(200, 95 + i * 45);
                r_Errows[i].Height = 20;
                r_Errows[i].Width = 40;
                r_Errows[i].Enabled = false;
                this.Controls.Add(r_Errows[i]);
                r_Errows[i].Click += new EventHandler(errows_Click);
            }

            for (int i = 0; i < r_UserResults.GetLength(0); i++)
            {
                for (int j = 0; j < r_UserResults.GetLength(1); j++)
                {
                    r_UserResults[i, j] = new Button();

                    if (j / 2 == 0)
                    {
                        r_UserResults[i, j].Location = new Point(250 + (j % 2) * 20, 85 + i * 45);
                    }
                    else
                    {
                        r_UserResults[i, j].Location = new Point(250 + (j % 2) * 20, 105 + i * 45);
                    }

                    r_UserResults[i, j].Height = 15;
                    r_UserResults[i, j].Width = 15;
                    r_UserResults[i, j].Enabled = false;
                    this.Controls.Add(r_UserResults[i, j]);
                }
            }
        }

        private void playTurn()
        {
            for (int i = 0; i < k_NumberOfChoices; i++)
            {
                r_ChosenColors.Clear();
                r_UserChoices[m_CurrentTurn, i].Enabled = true;
            }
        }

        private void userChoices_Click(Object sender, EventArgs e)
        {
            if ((sender as Button).BackColor.Equals(Button.DefaultBackColor))
            {
                m_PlayerNumberOfColorPick++;
            }
            ColorForm currentForm = new ColorForm(sender as Button, r_ChosenColors);
            if(!currentForm.ClosingFlag)
            {
                return;
            }
            if (!currentForm.m_PrevParentColor.Equals(DefaultBackColor))
            {
                int color = (int)Enum.Parse(typeof(eColorChoice), currentForm.m_PrevParentColor.Name);
                r_ChosenColors.Remove(color);
            }
            r_ChosenColors.Add((int)Enum.Parse(typeof(eColorChoice), (sender as Button).BackColor.Name));
            if(r_ChosenColors.Count == k_NumberOfChoices)
            {
                r_Errows[m_CurrentTurn].Enabled = true;
            }
        }

        private void errows_Click(Object sender, EventArgs e)
        {
            List<int> buttonColors = new List<int>(k_NumberOfChoices);
            for (int i = 0; i < k_NumberOfChoices; i++)
            {
                int currentColor = (int)Enum.Parse(typeof(eColorChoice), r_UserChoices[m_CurrentTurn, i].BackColor.Name);
                buttonColors.Add(currentColor);
            }
            for(int i = 0; i < k_NumberOfChoices; i++)
            {
                r_UserChoices[m_CurrentTurn, i].Enabled = false;
            }
            (sender as Button).Enabled = false;
            int[] results = GameLogic.ComputeResult.CreateFeedback(formInit.ColorList, buttonColors);
            showResults(results);
        }

        private void showResults(int[] i_Results)
        {
            int bull = i_Results[0];
            int cow = i_Results[1];

            for (int i = 0; i < bull; i++)
            {
                r_UserResults[m_CurrentTurn, i].BackColor = Color.Black;
            }

            for(int i = 0; i < cow; i++)
            {
                r_UserResults[m_CurrentTurn, i + i_Results[0]].BackColor = Color.Yellow;
            }

            if(bull == k_NumberOfChoices || m_CurrentTurn == formInit.MaxNumGuess - 1)
            {
                showComputerChoice();
                if(bull == k_NumberOfChoices)
                {
                    Form winForm = new Form();
                    winForm.Text = "YOU WON";
                    winForm.StartPosition = FormStartPosition.CenterScreen;
                    winForm.Height = 110;
                    winForm.Width = 220;
                    Button winButton = new Button();
                    winButton.Text = "YOU WON !!!";
                    winButton.Enabled = true;
                    winButton.Location = new Point(25, 10);
                    winButton.Height = 50;
                    winButton.Width = 150;
                    winForm.Controls.Add(winButton);
                    winForm.ShowDialog();
                }
                return;
            }
            m_CurrentTurn++;
            playTurn();
        }

        private void showComputerChoice()
        {
            for(int i = 0; i < k_NumberOfChoices; i++)
            {
                r_HiddenChoices[i].BackColor = Color.FromName(Enum.GetName(typeof(FormGame.eColorChoice), (eColorChoice)formInit.ColorList[i]));
            }
        }
    }
}
