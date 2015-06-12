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
    /// Interaction logic for PegRow.xaml
    /// </summary>
    public partial class PegRow : UserControl
    {
        // member data
        private bool locked_ = true;

        // get and set
        public bool isLocked
        {
            get
            {
                return locked_;
            }

            set
            {
                locked_ = value;
                foreach (Peg p in getPegs())
                {
                    p.isClickable = !value;
                }
            }
        }

        // the pegs in the row
        public List<Peg> getPegs()
        {
            List<Peg> pegs = new List<Peg>();
            foreach(UIElement p in gridPegs.Children)
                {
                    if ( p.GetType().ToString() == "MastermindClient.Results")
                        continue;
                    pegs.Add((Peg)p);
                }
            return pegs;
        }

        // the result pegs in the row
        public List<ResultPeg> getResultPegs()
        {
            List<ResultPeg> listRP = new List<ResultPeg>();
            foreach( ResultPeg rp in ((Results)gridPegs.Children[0]).gridGuess.Children )
            {
                listRP.Add(rp);
            }
            return listRP;
        }

        // ctor
        public PegRow()
        {
            InitializeComponent();
        }

        // create a row with given number of pegs
        public PegRow(int numPeg)
        {
            InitializeComponent();
            ColumnDefinition resCol = new ColumnDefinition();
            resCol.Width = GridLength.Auto;
            gridPegs.ColumnDefinitions.Add(resCol);

            Results result = new Results(numPeg);
            gridPegs.Children.Add(result);
            Grid.SetColumn(result, 0);

            for (int i = 1; i <= numPeg; ++i)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                colDef.Width = GridLength.Auto;
                gridPegs.ColumnDefinitions.Add(colDef);

                Peg peg = new Peg();
                gridPegs.Children.Add(peg);
                Grid.SetColumn(peg, i);
            }
        }
    }
}
