using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassExample5and6
{
    public partial class ClassEg5and6 : Form
    {
        public ClassEg5and6()
        {
            InitializeComponent();
        }
        // declare the constants
        const double DiscRate = 0.05;
        const double VATRate = 0.15;
        const double MARGCost = 60;
        const double SPBFCost = 100;
        const double SHPDCost = 95;
        const double FSNSCost = 85;
        const double VGTNCost = 65;
        double dblAccDiscAmt = 0;
        double dblAccVATAmt = 0;
        double dblAccAmtOwing = 0;
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // Declare the variables
            int intQty;
            double dblAmtOwing = 0;
            double dblVATAmt = 0;
            double dblDiscAmt = 0;
            bool blnValidInput = true;

            // Convert the input into numerics
            intQty = Convert.ToInt16(txtQty.Text);

            // Validation of input
            blnValidInput = ValidateInput(blnValidInput);
            if (blnValidInput == true)
            {
                dblAmtOwing = CalcAmtOwing(dblAmtOwing, intQty, dblDiscAmt);

                // Determine discount and subtracting it from the amount owed
                dblDiscAmt = DetermineDiscount(dblDiscAmt, dblAmtOwing);
                dblAmtOwing = dblAmtOwing - dblDiscAmt;

                // Determine the VAT Amount and adding it to the amount owed
                dblVATAmt = dblAmtOwing * VATRate;
                dblAmtOwing = dblAmtOwing + dblVATAmt;

                // Accumulate totals
                AccumulateTotals(dblDiscAmt, dblVATAmt, dblAmtOwing);

                // Display output
                DisplayOutput(dblAmtOwing, dblVATAmt, dblVATAmt);
            }

        }

        private bool ValidateInput(bool blnValidInput)
        {
            // Validate inputs
            if (txtPhoneNo.Text == "")
            {
                blnValidInput = false;
                MessageBox.Show("Please Enter Phone Number.", "Error!");
            }
            if ((txtPizzaCode.Text != "MARG") && (txtPizzaCode.Text != "SPBF") && (txtPizzaCode.Text != "SHPD") && (txtPizzaCode.Text != "FSNS") && (txtPizzaCode.Text != "VGTN") && (txtPizzaCode.Text == ""))
            {
                blnValidInput = false;
                MessageBox.Show("Please Enter a Valid Code.", "Error!");
            }
            if (txtQty.Text == "0")
            {
                blnValidInput = false;
                MessageBox.Show("Please Input a Valid Number of Quantity.", "Error!");
            }
            if (radCard.Checked == false && radCash.Checked == false)
            {
                blnValidInput = false;
                MessageBox.Show("Please Select One Payment Method.", "Error!");
            }

            return blnValidInput;
        }

        private double CalcAmtOwing(double dblAmtOwing, int intQty, double dblDiscAmt)
        {
            // Calculate the amount owed based on the pizza code and quantity
            if (txtPizzaCode.Text == "MARG")
            { dblAmtOwing = intQty * MARGCost; }
            else
                if (txtPizzaCode.Text == "SPBF")
            { dblAmtOwing = intQty * SPBFCost; }
            else
                if (txtPizzaCode.Text == "SHPD")
            { dblAmtOwing = intQty * SHPDCost; }
            else
                if (txtPizzaCode.Text == "FSNS")
            { dblAmtOwing = intQty * FSNSCost; }
            else
                if (txtPizzaCode.Text == "VGTN")
            { dblAmtOwing = intQty * VGTNCost; }

            return dblAmtOwing;
        }

        private double DetermineDiscount(double dblDiscAmt, double dblAmtOwing)
        {
            // Determine discount if payment method is cash
            if (radCash.Checked)
            { dblDiscAmt = dblAmtOwing * DiscRate; }

            return dblDiscAmt;
        }

        private void AccumulateTotals(double dblAmtOwing, double dblDiscAmt, double dblVATAmt)
        {
            // Accumulate the needed totals
            dblAccDiscAmt = dblAccDiscAmt + dblDiscAmt;
            dblAccVATAmt = dblAccVATAmt + dblVATAmt;
            dblAccAmtOwing = dblAccAmtOwing + dblAmtOwing;
        }

        private void DisplayOutput(double dblAmtOwing, double dblVATAmt, double dblDiscAmt)
        {
            // Display all the needed values
            lblAmtOwing.Text = dblAmtOwing.ToString("C2");
            lblVATAmt.Text = dblVATAmt.ToString("C2");
            lblDiscAmt.Text = dblDiscAmt.ToString("C2");
            lblAccDiscAmt.Text = dblAccDiscAmt.ToString("C2");
            lblAccVATAmt.Text = dblAccVATAmt.ToString("C2");
            lblAccAmtOwing.Text = dblAccAmtOwing.ToString("C2");

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Return everything to starting state
            txtPhoneNo.Text = "";
            txtPizzaCode.Text = "";
            txtQty.Text = "";

            lblAmtOwing.Text = "0.00";
            lblDiscAmt.Text = "0.00";
            lblVATAmt.Text = "0.00";

            lblAccAmtOwing.Text = "0.00";
            lblAccVATAmt.Text = "0.00";
            lblAccDiscAmt.Text = "0.00";

            radCard.Checked = false;
            radCash.Checked = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Exit the application
            Application.Exit();
        }
    }
}
