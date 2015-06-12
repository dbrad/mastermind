// Bradley Elliott and David Brad
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MastermindClient
{
    /// <summary>
    /// Interaction logic for GuessResult.xaml
    /// </summary>
    public partial class Results : UserControl
    {
        // ctor
        public Results()
        {
            InitializeComponent();
        }

        // create results for the given number of pegs in a row
        public Results(int numPeg)
        {
            InitializeComponent();
            int colNum = (int)Math.Ceiling((double)numPeg / 2.0);
            for (int i = 0; i < colNum; ++i)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                colDef.Width = GridLength.Auto;
                gridGuess.ColumnDefinitions.Add(colDef);
            }

            for (int r = 0; r < 2; ++r)
            {
                for (int c = 0; c < colNum; ++c)
                {
                    if (r * c + c >= numPeg)
                        break;

                    ResultPeg peg = new ResultPeg();
                    gridGuess.Children.Add(peg);
                    Grid.SetColumn(peg,c);
                    Grid.SetRow(peg, r);
                }
            }
        }
    }
}
