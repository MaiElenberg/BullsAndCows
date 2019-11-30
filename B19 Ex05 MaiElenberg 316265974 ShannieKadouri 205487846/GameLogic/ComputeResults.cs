using System.Collections.Generic;

namespace GameLogic
{
    public class ComputeResult
    {
        public static int[] CreateFeedback(List<int> i_ComputerChoice, List<int> i_UserChoice)
        {
            int rightPlace = 0;
            int wrongPlace = 0;

            for (int i = 0; i < i_UserChoice.Count; i++)
            {
                if (i_ComputerChoice.Contains(i_UserChoice[i]))
                {
                    if (i_ComputerChoice[i] == i_UserChoice[i])
                    {
                        rightPlace++;
                    }
                    else
                    {
                        wrongPlace++;
                    }
                }

            }

            int[] rightWrong = new int[2] { rightPlace, wrongPlace };

            return rightWrong;
        }
    }
}

