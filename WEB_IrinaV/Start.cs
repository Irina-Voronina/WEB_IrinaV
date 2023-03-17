using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WEB_IrinaV
{
    public class Start : ContentPage
    {
        public List<Button> buttons { get; set; }
        List<ContentPage> pages { get; set; }
        Picker pk;
        public Start()
        {
            StackLayout st = new StackLayout();

            buttons = new List<Button>();
            pages = new List<ContentPage>();

            List<string> files = new List<string> { "a.jpg", "i.jfif", "l.jpg", "m.jfif", "s.jpg", "w.jpg"};
            List<string> dirs = new List<string> { "Смайлик 1", "Смайлик 2", "Смайлик 3", "Смайлик 4", "Смайлик 5", "Смайлик 6" };
            Random rnd = new Random();  

            for (int i = 0; i < files.Count; i++) 
            {
                Button b = new Button
                {
                    Text = dirs[i],
                    TabIndex = i
                };
                buttons.Add(b);
                st.Children.Add(b);
                b.Clicked += B_Clicked;

                AlamLeht p=new AlamLeht(dirs[i], files[i]);
                pages.Add(p);

                //ContentPage p = new ContentPage
                //{
                //    BackgroundColor=Color.FromRgb(rnd.Next(0,255), rnd.Next(0,255), rnd.Next(0,255))
                //};

            }

            pk = new Picker
            {
                ItemsSource = dirs,
                Title = "Tee valik",
                TitleColor = Color.YellowGreen
            };
            pk.SelectedIndexChanged += Pk_SelectedIndexChanged;
            st.Children.Add(pk);

            Content = st;
        }

        private async void Pk_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Navigation.PushAsync(pages[pk.SelectedIndex]);
        }

        private async void B_Clicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            await Navigation.PushAsync(pages[b.TabIndex]);
        }
    }
}
