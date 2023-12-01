using MobileApp.Classes;

namespace MobileApp.Pages;

public partial class BookDetailPage : ContentPage
{
    private readonly Book _book;

    public BookDetailPage(Book book)
	{
		InitializeComponent();
        _book = book;
        bookTitleLabel.Text = _book.Title;
        BookImage.Source = book.Image;
        if(book.Status == 1)
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
        descBookLabel.Text = _book.Description;

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