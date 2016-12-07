using System;
using Xamarin.Forms;

namespace xBountyHunter.Views
{
    public class acercaDePage : ContentPage
    {
        private Label ldevelopedby;
        private Label ldevelopername;
        private Label lyear;
        private Label ltrainigsr;
        private Label lratingvalor;
        private Slider srating;
        private StackLayout verticalStackLayout;

        public acercaDePage()
        {
            ldevelopedby = new Label
            {
                Text = "Desarrollado por",
                FontSize = 15,
                HorizontalTextAlignment = 
                TextAlignment.Center
            };

            ldevelopername = new Label
            {
                Text = "Daniel Villalba",
                FontSize = 12,
                HorizontalTextAlignment = TextAlignment.Center
            };

            lyear = new Label
            {
                Text = "2016", FontSize = 8,
                HorizontalTextAlignment = TextAlignment.Center
            };

            ltrainigsr = new Label
            {
                Text = "DW. TRA",
                FontSize = 12,
                HorizontalTextAlignment = TextAlignment.Center
            };

            lratingvalor = new Label
            {
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Center
            };

            srating = new Slider
            {
                Maximum = 5,
                Minimum = 0,
                Value = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            verticalStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Title = "Acerca de";
            if (xBountyApp.Current.Properties.ContainsKey("Rating"))
                srating.Value = (double)xBountyApp.Current.Properties["Rating"];
            lratingvalor.Text = srating.Value.ToString();
            verticalStackLayout.Children.Add(ldevelopedby);
            verticalStackLayout.Children.Add(ldevelopername);
            verticalStackLayout.Children.Add(lyear);
            verticalStackLayout.Children.Add(ltrainigsr);
            verticalStackLayout.Children.Add(srating);
            verticalStackLayout.Children.Add(lratingvalor);

            srating.ValueChanged += (sender, args) =>
            {
                double value = srating.Value;
                value = Math.Round(value * 2) / 2;
                lratingvalor.Text = value.ToString();
                xBountyApp.Current.Properties["Rating"] = value;
            };

            Content = verticalStackLayout;
        }
    }
}
