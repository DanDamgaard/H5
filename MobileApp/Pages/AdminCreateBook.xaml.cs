using Microsoft.Maui.Storage;
using MobileApp.Classes;
using MobileApp.Services;

namespace MobileApp.Pages;

public partial class AdminCreateBook : ContentPage
{
    private Image _image = new Image();
    private string imageTitle = "";
    private Api api = new Api();
    public AdminCreateBook()
    {
        InitializeComponent();
    }

    private async void createBookBtn_Clicked(object sender, EventArgs e)
    {
        if (titleValidator.IsNotValid)
        {
            await DisplayAlert("Fejl", "Du skal skrive en bog title", "Ok");
            return;
        }

        if (descValidator.IsNotValid)
        {
            await DisplayAlert("Fejl", "Du skal skrive en bog beskrivelse", "Ok");
            return;
        }

        if (genreValidator.IsNotValid)
        {
            await DisplayAlert("Fejl", "Du skal skrive en genre til bogen", "Ok");
            return;
        }

        if (autherValidator.IsNotValid)
        {
            await DisplayAlert("Fejl", "Du skal skrive navnet på forfatter på bogen", "Ok");
            return;
        }

        if (publisherValidator.IsNotValid)
        {
            await DisplayAlert("Fejl", "Du skal skrive bogens forlag", "Ok");
            return;
        }

        if (imageTitle == "")
        {
            await DisplayAlert("Fejl", "Du skal vælge en forside billede", "Ok");
            return;
        }

        Author author = new Author(0, AutherBox.Text);

        BookUpload book = new BookUpload(0, TitleBox.Text, GenreBox.Text, DescBox.Text, TitleBox.Text + ".jpeg", author, PublisherBox.Text, 0);

        if (await api.createBook(book))
        {
            await DisplayAlert("Succes", "Bogen blev oprettet", "OK");
            await Navigation.PopModalAsync();
        }
        else
        {
            await DisplayAlert("Fejl", "Bogen blev ikke oprettet", "OK");
        }

    }

    private async void PickIamegBtn_Clicked(object sender, EventArgs e)
    {

        var result = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images
        });

        if (result == null) return;

        var stream = await result.OpenReadAsync();

        _image.Source = ImageSource.FromStream(() => stream);

        imageTitle = result.FileName;

        PicNameLabel.Text = imageTitle;

    }

    private async void cancelBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void TitleBox_Completed(object sender, EventArgs e)
    {
        DescBox.Focus();
    }

    private void DescBox_Completed(object sender, EventArgs e)
    {
        GenreBox.Focus();
    }

    private void GenreBox_Completed(object sender, EventArgs e)
    {
        AutherBox.Focus();
    }

    private void AutherBox_Completed(object sender, EventArgs e)
    {
        PublisherBox.Focus();
    }

    private void PublisherBox_Completed(object sender, EventArgs e)
    {
        PublisherBox.Unfocus();
    }
}