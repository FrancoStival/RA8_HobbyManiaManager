﻿using System;
using System.Drawing;
using System.Globalization;
using System.Reflection.Emit;
using System.Windows.Forms;
using HobbyManiaManager.Forms;
using HobbyManiaManager.Models;

namespace HobbyManiaManager
{
    public partial class MovieUserControl : UserControl
    {
        private CultureInfo cultureInfo;
        private RentalService service;
        private Movie Movie;
        public Action _refreshAction;


        public MovieUserControl()
        {
            InitializeComponent();
            this.cultureInfo = new CultureInfo("es-ES");
            this.service = new RentalService();
        }

        public void Load(Movie movie)
        {
            this.labelgenre.Text = "Genres: " + movie.GenresAsSting;
            this.Movie = movie;
            this.labelId.Text = $"ID: {Movie.Id.ToString()}";
            this.labelId.AutoSize = true;

            this.labelTitle.Text = $"{Movie.Title}({Movie.ReleaseDate.Year})";
            this.labelTitle.AutoSize = true;

            this.labelOriginalTitle.Text = Movie.OriginalTitle;
            this.labelOriginalTitle.AutoSize = true;

            this.pictureBoxPoster.Load(Movie.PosterUrl(200));

            this.labelOverview.Text = Movie.Overview;
            this.labelOverview.AutoSize = true;
            this.labelVotesCount.Text = $"{Movie.VoteCount.ToString("N0", cultureInfo)} Votes";

            this.pictureBoxAvailable.BorderStyle = BorderStyle.None;

            this.circularProgressBarVotes.Value = (int)Math.Round(Movie.VoteAverage * 10);
            this.circularProgressBarVotes.Text = $"{Math.Round(Movie.VoteAverage * 10)}%";

            this.labelgenre.Location = new Point(this.labelTitle.Location.X, this.labelTitle.Bottom + 10);
            this.labelOriginalTitle.Location = new Point(this.labelgenre.Location.X, this.labelgenre.Bottom + 10);
            this.circularProgressBarVotes.Location = new Point(this.labelOriginalTitle.Location.X + 10, this.labelOriginalTitle.Bottom + 20);
            this.labelOverview.Location = new Point(this.circularProgressBarVotes.Right + 10, this.labelOriginalTitle.Bottom + 20);
            this.labelVotesCount.Location = new Point(this.circularProgressBarVotes.Location.X, this.circularProgressBarVotes.Bottom + 15);


            CheckAvailability(movie);
            this.Refresh();

            
        }

        private void CheckAvailability(Movie movie)
        {
            bool available = service.IsAvailable(movie);
            if (available)
            {
                this.pictureBoxAvailable.BackColor = Color.Green;
                this.labelAvailable.Text = "Ready to rent";
                this.buttonStartEndRent.Text = "Start Rent";

            }
            else
            {
                Customer movieRentedCustomer = CustomersRepository.Instance.GetById(service.GetMovieRental(movie.Id).CustomerId);
                this.buttonStartEndRent.Text = "End Rent";
                this.pictureBoxAvailable.BackColor = Color.Red;
                this.labelAvailable.Text = $"Rental not available, rented by: {movieRentedCustomer.Name} ({movieRentedCustomer.Id})";

            }
        }

        public override void Refresh()
        {
            base.Refresh();
            CheckAvailability(Movie);
        }

        private void buttonStartEndRent_Click(object sender, EventArgs e)
        {
            var rentalForm = new RentalForm(Movie, this);
            rentalForm.ShowDialog();
            _refreshAction?.Invoke();
            this.Refresh();
        }

        private void buttonImbID_Click(object sender, EventArgs e)
        {
            if (Movie != null)
            {
                ImbdForm imbdform = new ImbdForm(Movie.imdb_id, Movie.Title);
                imbdform.ShowDialog();
            }
            else
            {
                MessageBox.Show("No hay información de IMDB disponible para esta película.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

      
    }
}
