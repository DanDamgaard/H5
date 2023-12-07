using Microsoft.Maui.Controls.Shapes;
using MobileApp.Classes;
using MobileApp.Services;
using System.Drawing;

namespace MobileApp.Pages;

public partial class BookHistoryPage : ContentPage
{
    private readonly Book _book;
    private Api api = new Api();
    List<RentedBook> bookList = new List<RentedBook>();

    public BookHistoryPage(Book book)
	{
		InitializeComponent();
        _book = book;
        createHistory();
    }

    private async void createHistory()
    {
        bookImage.Source = _book.Image;

        BookTitelLabel.Text = _book.Title;

        List<RentedBook> rentedBooks = await api.getRentedBooks();
        foreach(RentedBook book in rentedBooks)
        {
            if(book.Bookid == _book.Id)
            {
                bookList.Add(book);

            }
        }

        bookList = bookList.OrderByDescending(X =>  X.StartDate).ToList();


        createTable(bookList);
       

    }

    private void createTable(List<RentedBook> list)
    {
        RoundRectangle borderStrokeShape = new RoundRectangle
        {
            CornerRadius = new CornerRadius(0, 0, 0, 0)
        };


        HorizontalStackLayout header = new HorizontalStackLayout();
        header.Spacing = 0;
        header.Padding = 0;

        Border headerBorder1 = new Border();
        headerBorder1.Stroke = Colors.Black;
        headerBorder1.StrokeThickness = 2;
        headerBorder1.StrokeShape = borderStrokeShape;
        headerBorder1.WidthRequest = 150;

        Button hb1Btn = new Button();
        hb1Btn.Text = "Bruger";
        hb1Btn.TextColor = Colors.Black;
        hb1Btn.Background = Colors.Transparent;
        headerBorder1.Content = hb1Btn;

        Border headerBorder3 = new Border();
        headerBorder3.Stroke = Colors.Black;
        headerBorder3.StrokeThickness = 2;
        headerBorder3.StrokeShape = borderStrokeShape;
        headerBorder3.WidthRequest = 80;
        headerBorder1.Padding = 0;

        Button hb3Btn = new Button();
        hb3Btn.Text = "Lånt";
        hb3Btn.TextColor = Colors.Black;
        hb3Btn.Background = Colors.Transparent;
        headerBorder3.Content = hb3Btn;

        Border headerBorder4 = new Border();
        headerBorder4.Stroke = Colors.Black;
        headerBorder4.StrokeThickness = 2;
        headerBorder4.StrokeShape = borderStrokeShape;
        headerBorder3.WidthRequest = 80;
        headerBorder1.Padding = 0;

        Button hb4Btn = new Button();
        hb4Btn.Text = "Afleveret";
        hb4Btn.TextColor = Colors.Black;
        hb4Btn.Background = Colors.Transparent;
        headerBorder4.Content = hb4Btn;


        header.Children.Add(headerBorder1);
        header.Children.Add(headerBorder3);
        header.Children.Add(headerBorder4);
        tableStack.Add(header);

        foreach(RentedBook book in list)
        {
            HorizontalStackLayout hs = new HorizontalStackLayout();
            hs.Spacing = 0;
            hs.Padding = 0;

            Border userBorder = new Border();
            userBorder.Stroke = Colors.Transparent;
            userBorder.WidthRequest = 150;

            Label userLabel = new Label();
            userLabel.HorizontalOptions = LayoutOptions.Center;
            userLabel.Text = book.User.Name;
            userLabel.TextColor = Colors.Black;
            userBorder.Content = userLabel;

            hs.Children.Add(userBorder);

            Border startBorder = new Border();
            startBorder.Stroke = Colors.Transparent;
            startBorder.WidthRequest = 80;

            Label startLabel = new Label();
            startLabel.HorizontalOptions = LayoutOptions.Center;
            startLabel.Text = book.StartDate.ToString("dd/MM/yyyy");
            startLabel.TextColor = Colors.Black;
            startBorder.Content = startLabel;

            hs.Children.Add(startBorder);

            Border endBorder = new Border();
            endBorder.Stroke = Colors.Transparent;
            endBorder.WidthRequest = 80;

            Label endLabel = new Label();
            endLabel.HorizontalOptions = LayoutOptions.Center;
            endLabel.Text = book.StartDate.ToString("dd/MM/yyyy");
            endLabel.TextColor = Colors.Black;
            endBorder.Content = endLabel;

            hs.Children.Add(endBorder);

            tableStack.Children.Add(hs);
            
        }
    }

}
