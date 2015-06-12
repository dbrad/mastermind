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
    /// Interaction logic for ResultPeg.xaml
    /// </summary>
    public partial class ResultPeg : UserControl
    {
        //ctor
        public ResultPeg()
        {
            InitializeComponent();
            peg.Focusable = false;
        }

        // set the colour to the "perfect" colour
        public void SetPerfect()
        {
            peg.Background = Brushes.Green;
        }

        // set the colour to the "right colour only" colour
        public void SetCorrectColour()
        {
            peg.Background = Brushes.Yellow;
        }

        // set colour to the "blank" colour
        public void SetBlank()
        {
            peg.Background = Brushes.White;
        }
    }
}
