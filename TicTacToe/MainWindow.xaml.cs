using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        private MarkType[] mResults; // Holds the current results of cells in the active game
        private bool mPlayer1Turn;
        private bool mPlayer2Turn;
        private bool mGameEnded;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion

        /// <summary>
        /// Starts a new game and resets grid and contents to default
        /// </summary>
        private void NewGame()
        {
            mResults = new MarkType[9]; // Create a new blank array of free cells
            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // Make sure player 1 starts the game
            mPlayer1Turn = true;

            // iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty; // clears grid
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            mGameEnded = false; // make sure the game hasn't finished

        }
        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender"> The button that was clicked</param>
        /// <param name="e"> The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded) // Start a new game on the click after it finished
            {
                NewGame();
                return;
            }

            var button = (Button)sender; // explict cast
            // find button position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            if (mResults[index] != MarkType.Free) // if the cell isn't free
                return;

            // set the cell value
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought; // inline if statement

            button.Content = mPlayer1Turn ? "X" : "0"; // set button text to the result

            // change noughts to green
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            mPlayer1Turn ^= true;

            // check for winner
            CheckForWinner();

        }

        /// <summary>
        /// Checks if there is a winner
        /// </summary>
        private void CheckForWinner()
        {
            #region row1
            // check for horizontal wins
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // game ends
                mGameEnded = true;

                // highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;

            }
            #endregion
            #region row2
            // check for horizontal wins
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // game ends
                mGameEnded = true;

                // highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;

            }
            #endregion
            #region row3
            // check for horizontal wins
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // game ends
                mGameEnded = true;

                // highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;

            }
            #endregion

            #region column1
            // check for horizontal wins
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // game ends
                mGameEnded = true;

                // highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;

            }
            #endregion
            #region column2
            // check for horizontal wins
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // game ends
                mGameEnded = true;

                // highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;

            }
            #endregion
            #region column3
            // check for horizontal wins
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // game ends
                mGameEnded = true;

                // highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;

            }
            #endregion

            #region diagonal1
            // check for horizontal wins
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // game ends
                mGameEnded = true;

                // highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;

            }
            #endregion
            #region diagonal2
            // check for horizontal wins
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // game ends
                mGameEnded = true;

                // highlight winning cells in green
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;

            }
            #endregion

            // checks if game has not ended then check if board is full
            if (!mGameEnded)
            {
                // check for no winner and full board
                if (!mResults.Any(f => f == MarkType.Free))
                {
                    mGameEnded = true;
                    // turn all cells orange

                    Container.Children.Cast<Button>().ToList().ForEach(Button =>
                    {
                        Button.Background = Brushes.Orange;
                    });
                }
            }
        }
    }
}
