using MobileApp.Classes;
using MobileApp.Services;

namespace MobileApp.Pages;

public partial class BookDetailPage : ContentPage
{
    private readonly Book _book;
    private Api api = new Api();

    public BookDetailPage(Book book)
	{
		InitializeComponent();
        _book = book;
        getBook();
    }

    private async void getBook()
    {
        Book book = await api.getBook(_book.Id);
        bookTitleLabel.Text = book.Title;
        BookImage.Source = book.Image;
        AutherLabel.Text = "Forfatter: " + book.AuthorName;
        if (book.Status == 1)
        {
            statusLabel.Text = "Udlånt";
            statusLabel.TextColor = Colors.Red;
        }
        else
        {
            statusLabel.Text = "Ledig";
            statusLabel.TextColor = Colors.Green;
            if(Global.User.Roles == 0) RentBookBtn.IsVisible = true;
        }
        descBookLabel.Text = book.Description;
    }

    private async void RentBookBtn_Clicked(object sender, EventArgs e)
    {
        DateTime date = DateTime.Now;
        RentedBook rentedBook = new RentedBook(0 ,_book.Id, Global.User.Id, _book.Title, Global.User.Name, date, date, true);

        if(await api.rentBook(rentedBook))
        {
            statusLabel.Text = "Udlånt";
            statusLabel.TextColor = Colors.Red;
            RentBookBtn.IsVisible = false;
            await DisplayAlert("Sucess", "Du har lånt bogen", "OK");
        }
        else
        {
            await DisplayAlert("Fejl", "Nået gik galt", "OK");
        }
        
    }

    private async void BookHistoryBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new BookHistoryPage(_book));
    }
}