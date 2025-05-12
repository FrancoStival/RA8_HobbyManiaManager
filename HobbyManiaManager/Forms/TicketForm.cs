using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HobbyManiaManager.Models;
using HobbyManiaManager.Utils;

namespace HobbyManiaManager.Forms
{
    public partial class TicketForm : Form
    {
        private readonly RentalService _rentalService;

        Movie _movie;
        Customer _customer;
        DateTime _StartDate;
        DateTime _EndDate;
        public TicketForm(Movie movie, Customer customer,  DateTime Start, DateTime End)
        {
            InitializeComponent();
            _movie = movie;
            _customer = customer;
            _StartDate = Start;
            _EndDate = End;

            ConfigureForm();
            CalculatePrice();
        }
        private void ConfigureForm()
        {
            txtCustomerId.ReadOnly = true;
            txtMovieId.ReadOnly = true;
            txtCustomerName.ReadOnly = true;
            txtMovieTitle.ReadOnly = true;
            txtStartDate.ReadOnly = true;
            txtEndDate.ReadOnly = true;
            txtPrice.ReadOnly = true;
        }

        public void CalculatePrice()
        {
            txtCustomerId.Text = _customer.Id.ToString();
            txtCustomerName.Text = _customer.Name;
            txtMovieId.Text = _movie.Id.ToString();
            txtMovieTitle.Text = _movie.Title;
            txtStartDate.Text = _StartDate.ToString("dd/MM/yyyy HH:mm");
            if (_EndDate != null)
            {
                txtEndDate.Text = _EndDate.ToString("dd/MM/yyyy HH:mm");
                double segundos = DateTimeUtils.GetDifferenceInSeconds(_StartDate, _EndDate);
                string precio = Math.Round(segundos * 0.0001, 2).ToString("F2") + " €";
                txtPrice.Text = precio;
            }
            else
            {
                txtPrice.Text = "N/A";
            }

        }
    }
}
