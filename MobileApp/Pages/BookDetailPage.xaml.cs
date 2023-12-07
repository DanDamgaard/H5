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
            RentBookBtn.IsVisible = true;
        }
        descBookLabel.Text = book.Description;
    }

    private void RentBookBtn_Clicked(object sender, EventArgs e)
    {
        statusLabel.Text = "Udlånt";
        statusLabel.TextColor = Colors.Red;
        RentBookBtn.IsVisible = false;
    }

    private async void BookHistoryBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new BookHistoryPage(_book));
    }
}