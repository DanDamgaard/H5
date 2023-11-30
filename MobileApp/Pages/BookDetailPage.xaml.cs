using MobileApp.Classes;

namespace MobileApp.Pages;

public partial class BookDetailPage : ContentPage
{
    private readonly Book _book;

    public BookDetailPage(Book book)
	{
		InitializeComponent();
        _book = book;
    }
}