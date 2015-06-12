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
    /// Interaction logic for Peg.xaml
    /// </summary>
    public partial class Peg : UserControl
    {
        // member data
        public bool isClickable = false;
        private List<Brush> colors;

        // ctor
        public Peg()
        {
            InitializeComponent();
            colors = new List<Brush>();
            colors.Add(Brushes.Red);
            colors.Add(Brushes.Orange);
            colors.Add(Brushes.Yellow);
            colors.Add(Brushes.Green);
            colors.Add(Brushes.Blue);
            colors.Add(Brushes.Indigo);
            colors.Add(Brushes.Violet);
            peg.Focusable = false;
            this.Tag = -1;
        }

        // the peg has been clicked, change colour
        private void peg_Click(object sender, RoutedEventArgs e)
        {
            if (isClickable)
            {
                this.Tag = (colors.IndexOf(peg.Background) + 1) % colors.Count;
                peg.Background = colors[(colors.IndexOf(peg.Background) + 1) % colors.Count];
            }
            this.Focusable = true;
            this.Focus();
        }

        // set peg with given colour
        public void setColour(int index)
        {
            if (index == -1)
                peg.Background = Brushes.White;
            else
                peg.Background = colors[index];
            this.Tag = index;
        }
    }
}
