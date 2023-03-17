using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WEB_IrinaV
{
    internal class AlamLeht : ContentPage
    {
        Label lbl;
        Image img;
        Switch sw;

        public AlamLeht(string title, string file)
        {
            lbl = new Label
            {
                Text = title,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),
                TextColor = Color.YellowGreen,
                HorizontalOptions = LayoutOptions.Center,
            };

            sw = new Switch
            {
                HorizontalOptions = LayoutOptions.Center,
                IsToggled = true,
            };
            sw.Toggled += Sw_Toggled;

            img = new Image { Source = file };
            Content = new StackLayout { Children = {lbl,sw,img} };
        }

        private void Sw_Toggled(object sender, ToggledEventArgs e)
        {
            if(e.Value==true)
            {
                img.IsVisible = true;

            }
            else
            {
                img.IsVisible = false;
            }
        }
    }
}
