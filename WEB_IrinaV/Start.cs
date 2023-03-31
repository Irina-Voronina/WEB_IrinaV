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
        Image imgs;
        List<string> files;

        public Start()
        {
            // StackLayout st = new StackLayout();

            Grid grid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto }, // кнопки будут в первой строке
                new RowDefinition { Height = GridLength.Auto }, // вторая строка для Picker
                new RowDefinition { Height = GridLength.Star } // оставшееся место займет картинка
            },
                ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star } // кнопки займут все доступное место по ширине
            }
            };

            buttons = new List<Button>();
            pages = new List<ContentPage>();

            files = new List<string> { "a.jpg", "i.jfif", "l.jpg", "m.jfif", "s.jpg", "w.jpg" };
            List<string> dirs = new List<string> { "1", "2", "3", "4", "5", "6" };

            Random rnd = new Random();  

            for (int i = 0; i < files.Count; i++) 
            {
                Button b = new Button
                {
                    Text = dirs[i],
                    TabIndex = i,
                    TextColor = Color.White,
                    BackgroundColor = Color.DodgerBlue,
                    FontSize = 38,
                    FontAttributes = FontAttributes.Bold
                };
                buttons.Add(b);
                //st.Children.Add(b);
                grid.Children.Add(b, i, 0); //добавляем расположение кнопок в первую строку сетки
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
                TitleColor = Color.HotPink,
                FontSize = 28,
                FontAttributes = FontAttributes.Bold
            };

            pk.SelectedIndexChanged += Pk_SelectedIndexChanged;
            //st.Children.Add(pk);
            grid.Children.Add(pk, 0, 1); // добавляем picker во вторую строку сетки
            Grid.SetColumnSpan(pk, buttons.Count); // устанавливаем ширину picker, равную количеству кнопок и находиться будет под кнопками

            imgs = new Image
            {
                Source = files[0]
            };

            //st.Children.Add(imgs);

            grid.Children.Add(imgs, 0, 2); // добавляем картинку в третью строку сетки
            Grid.SetRowSpan(imgs, 3); // картинка займет все оставшееся место в сетке
            Grid.SetColumnSpan(imgs, buttons.Count); // картинка будет находиться под кнопками и по ширине всех кнопок

            SwipeGestureRecognizer swipeLeft = new SwipeGestureRecognizer //добавляем жест для сдвига картинки влево
            {
                Direction = SwipeDirection.Left
            };
            swipeLeft.Swiped += Swipe_Swiped_Left;
            imgs.GestureRecognizers.Add(swipeLeft);

            SwipeGestureRecognizer swipeRight = new SwipeGestureRecognizer  //вправо
            {
                Direction = SwipeDirection.Right
            };
            swipeRight.Swiped += Swipe_Swiped_Right;
            imgs.GestureRecognizers.Add(swipeRight);


            Content = grid;
            //Content = st;
        }

        int i = 0;
        private void Swipe_Swiped_Left(object sender, SwipedEventArgs e) //влево
        {
            if (files != null && files.Count > 0) //добавить проверку наличия элементов в списке files, чтобы избежать попытки доступа к несуществующим элементам
            {
                if (i < files.Count - 1)
                {
                    i++;
                }
                else if (i == files.Count - 1)
                {
                    i = 0;
                }
                imgs.Source = files[i];

                // Изменяем цвет кнопок
                for (int j = 0; j < buttons.Count; j++)
                {
                    if (j == i)
                    {
                        buttons[j].BackgroundColor = Color.Orange;
                    }
                    else
                    {
                        buttons[j].BackgroundColor = Color.DodgerBlue;
                    }
                }

            }
        }

        private void Swipe_Swiped_Right(object sender, SwipedEventArgs e) //вправо
        {
            if (files != null && files.Count > 0) 
            {
                if (i > 0)
                {
                    i--;
                }
                else if (i == 0)
                {
                    i = files.Count - 1;
                }
                imgs.Source = files[i];

                // Изменяем цвет кнопок
                for (int j = 0; j < buttons.Count; j++)
                {
                    if (j == i)
                    {
                        buttons[j].BackgroundColor = Color.Orange;
                    }
                    else
                    {
                        buttons[j].BackgroundColor = Color.DodgerBlue;
                    }
                }
            }
        }


        private async void Pk_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pk.SelectedIndex != -1)
            {
                await Navigation.PushAsync(pages[pk.SelectedIndex]);
                pk.SelectedIndex = -1; // сбросить выбор в строке в пикере
            }
        }

        private async void B_Clicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            await Navigation.PushAsync(pages[b.TabIndex]);
        }
    }
}
