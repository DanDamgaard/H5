using MobileApp.Classes;

namespace MobileApp.Pages;

public partial class BookHistoryPage : ContentPage
{
    private readonly Book _book;

    public BookHistoryPage(Book book)
	{
		InitializeComponent();
        _book = book;
    }
}