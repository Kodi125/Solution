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
using System.IO;

using Newtonsoft.Json;

namespace LabSheet1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Band[] bandsList;
        public MainWindow()
        {
            InitializeComponent();
        }

        //Get the bands, sort them and list them in the bands listbox
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Add all bands to a single array then copy to globally declared array
            bandsList = GetBands();

            //Sort bands
            Array.Sort(bandsList);

            //Populate bands listbox with all bands
            LbxBands.ItemsSource = bandsList;

            //Populate genre combo box
            string[] genres = { "All", "Rock", "Pop", "Indie" };
            CbxGenre.ItemsSource = genres;
        }
        //Create all bands and albums needed
        private Band[] GetBands()
        {
            ////Create bands
            //Rock
            RockBand r1 = new RockBand() { Name = "The Foo Fighters", Formed = 1994, Members = "Dave Grohl, Nate Mendel, Pat Smear, Taylor Hawkins, Chris Shifflett, Rami Jafee" };
            RockBand r2 = new RockBand() { Name = "The Rolling Stones", Formed = 1962, Members = "Mick Jagger, Ian Stewart, Dick Taylor, Bill Wyman, Mick Taylor" };

            //Pop
            PopBand p1 = new PopBand() { Name = "The Beatles", Formed = 1960, Members = "John Lennon, Paul McCartney, George Harrison, Ringo Starr" };
            PopBand p2 = new PopBand() { Name = "Green Day", Formed = 1986, Members = "Billie Joe Armstrong, Mike Dirnt, Tre Cool" };

            //Indie
            IndieBand i1 = new IndieBand() { Name = "Arctic Monkeys", Formed = 2002, Members = "Alex Turner, Matt Heldens, Jamie Cook, Nick O'Malley" };
            IndieBand i2 = new IndieBand() { Name = "The Strokes", Formed = 1998, Members = "Julian Casablancas, Nick Valensi, Albert Hammond Jr, Nikolai Fraiture, Fabrizio Moretti" };

            #region albums

            ////Create and assign albums to appropriate artists
            //Random Declaration
            Random random = new Random();

            //Foo fighters albums
            Album a1 = new Album() { Name = "Greatest Hits", Released = GetRandomDate(r1, random), Sales = random.Next(1000000, 10000000) };
            Album a2 = new Album() { Name = "One by One", Released = GetRandomDate(r1, random), Sales = random.Next(1000000, 10000000) };
            r1.Albums[0] = a1;
            r1.Albums[1] = a2;

            //Rolling stones albums
            Album a3 = new Album() { Name = "Beggars Banquet", Released = GetRandomDate(r2, random), Sales = random.Next(1000000, 10000000) };
            Album a4 = new Album() { Name = "Blue & Lonesome", Released = GetRandomDate(r2, random), Sales = random.Next(1000000, 10000000) };
            r2.Albums[0] = a3;
            r2.Albums[1] = a4;

            //The Beatles albums
            Album a5 = new Album() { Name = "Let It Be", Released = GetRandomDate(p1, random), Sales = random.Next(1000000, 10000000) };
            Album a6 = new Album() { Name = "Sgt. Pepper's Lonely Heart Club Band", Released = GetRandomDate(p1, random), Sales = random.Next(1000000, 10000000) };
            p1.Albums[0] = a5;
            p1.Albums[1] = a6;

            //Green Day albums
            Album a7 = new Album() { Name = "Dookie", Released = GetRandomDate(p2, random), Sales = random.Next(1000000, 10000000) };
            Album a8 = new Album() { Name = "American Idiot", Released = GetRandomDate(p2, random), Sales = random.Next(1000000, 10000000) };
            p2.Albums[0] = a7;
            p2.Albums[1] = a8;

            //Arctic Monkeys albums
            Album a9 = new Album() { Name = "Whatever People Say I am, That's what I'm not", Released = GetRandomDate(i1, random), Sales = random.Next(1000000, 10000000) };
            Album a10 = new Album() { Name = "Favourite Worst Nightmare", Released = GetRandomDate(i1, random), Sales = random.Next(1000000, 10000000) };
            i1.Albums[0] = a9;
            i1.Albums[1] = a10;

            //The Strokes albums
            Album a11 = new Album() { Name = "Room on Fire", Released = GetRandomDate(i2, random), Sales = random.Next(1000000, 10000000) };
            Album a12 = new Album() { Name = "The Modern Age", Released = GetRandomDate(i2, random), Sales = random.Next(1000000, 10000000) };
            i2.Albums[0] = a11;
            i2.Albums[1] = a12;

            #endregion albums

            Band[] bandsCreated = { r1, r2, p1, p2, i1, i2 };
            return bandsCreated;
        }

        //When the selection of the bands listbox is changed, update the albums list and band information
        private void LbxBands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Null Check
            if (LbxBands.SelectedItem != null)
            {
                //Get selection
                Band selectedBand = LbxBands.SelectedItem as Band;

                //Set albums listbox to display right information
                LbxAlbums.ItemsSource = selectedBand.Albums;

                //Display bands information
                DisplayFormed.Text = "Formed: " + selectedBand.Formed;
                DisplayMembers.Text = "Members: " + selectedBand.Members;
            }
        }

        private void LbxAlbums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CbxGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbxGenre.SelectedItem != null)
            {
                //Get selection
                string selectedGenre = CbxGenre.SelectedItem as string;

                //Array to hold filtered items
                Band[] filteredBands = new Band[2];
                int counter = 0;

                switch (selectedGenre)
                {
                    case "All":
                        LbxBands.ItemsSource = GetBands();
                        break;
                    case "Rock":
                        foreach (Band currentBand in bandsList)
                        {
                            if (currentBand is RockBand)
                            {
                                filteredBands[counter] = currentBand;
                                counter++;
                            }
                        }
                        LbxBands.ItemsSource = filteredBands;
                        break;
                    case "Pop":
                        foreach (Band currentBand in bandsList)
                        {
                            if (currentBand is PopBand)
                            {
                                filteredBands[counter] = currentBand;
                                counter++;
                            }
                        }
                        LbxBands.ItemsSource = filteredBands;
                        break;
                    case "Indie":
                        foreach (Band currentBand in bandsList)
                        {
                            if (currentBand is IndieBand)
                            {
                                filteredBands[counter] = currentBand;
                                counter++;
                            }
                        }
                        LbxBands.ItemsSource = filteredBands;
                        break;
                }
            }
        }

        //Generate random date within range
        private DateTime GetRandomDate(Band band, Random randomFactory)
        {

            //Calculates a time range given two integers
            DateTime startDate = new DateTime(band.Formed, 01, 01);
            DateTime endDate = new DateTime(2018, 09, 01);
            TimeSpan timeSpan = endDate - startDate;
            TimeSpan newSpan = new TimeSpan(0, randomFactory.Next(0, (int)timeSpan.TotalMinutes), 0);
            DateTime newDate = startDate + newSpan;

            return newDate;

        }

        //When the save button is clicked, the information is written to the bandInfo.txt file
        private void button_Click(object sender, RoutedEventArgs e)
        {
            #region RegularFileWrite
            ////Create streamWriter
            //StreamWriter sw = new StreamWriter("bandInfo.txt");

            ////Null check
            //if (LbxBands.SelectedItem != null)
            //{
            //    //Get selected band
            //    Band selectedBand = LbxBands.SelectedItem as Band;

            //    //Write the data to file
            //    sw.WriteLine("Name: " + selectedBand.Name + ", Formed: " + selectedBand.Formed.ToString() + ", Members: " + selectedBand.Members);
            //}

            ////Close streamWriter
            //sw.Close();

            #endregion RegularFileWrite

            #region JSON_FileWrite


            Band selectedBand = LbxBands.SelectedItem as Band;
            if (selectedBand != null)
            {
                string bandData = JsonConvert.SerializeObject(selectedBand, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(@"c:\temp\band.json"))
                {
                    sw.Write(bandData);
                }

            }

            #endregion JSON_FileWrite

        }

    }
}
