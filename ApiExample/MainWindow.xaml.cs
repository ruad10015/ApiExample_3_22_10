using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace ApiExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string ImagePath { get; set; }
        public string Minute { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public dynamic Data { get; set; }
        public dynamic SingleData { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }
        HttpClient client = new HttpClient();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var name = movieTextbox.Text;
            HttpResponseMessage response = new HttpResponseMessage();
            response=client.GetAsync($@"http://www.omdbapi.com/?apikey=8c4eb5a6&s={name}&plot=full").Result;

            var str=response.Content.ReadAsStringAsync().Result;
            Data = JsonConvert.DeserializeObject(str);

            var firstMovieTitle = Data.Search[0].Title;

            response = client.GetAsync($@"http://www.omdbapi.com/?apikey=8c4eb5a6&t={firstMovieTitle}&plot=full").Result;

            str = response.Content.ReadAsStringAsync().Result;
            SingleData = JsonConvert.DeserializeObject(str);

            movieLabel1.Content = SingleData.Title + " " + SingleData.RunTime + " " + SingleData.Description;
            movieImage1.Source=SingleData.Poster;
            movieImage2.Source=SingleData.Poster;


            var secondMovieTitle = Data.Search[1].Title;

            response = client.GetAsync($@"http://www.omdbapi.com/?apikey=8c4eb5a6&t={secondMovieTitle}&plot=full").Result;

            str = response.Content.ReadAsStringAsync().Result;
            SingleData = JsonConvert.DeserializeObject(str);

            movieLabel2.Content = SingleData.Title + " " + SingleData.RunTime + " " + SingleData.Description;
            movieImage12.Source = SingleData.Poster;
            movieImage22.Source = SingleData.Poster;


        }
    }
}
