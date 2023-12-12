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
    private bool userSort = false;
    private bool startSort = true;
    private bool endSort = false;

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

        List<RentedBook> rentedBooks = await api.getUsersAndRentedBooks();
        foreach(RentedBook book in rentedBooks)
        {
            if(book.BookId == _book.Id)
            {
                bookList.Add(book);

            }
        }

        bookList = bookList.OrderByDescending(X =>  X.StartDate).ToList();

        createHeader();

        createTable(bookList);
       

    }

    private void createHeader()
    {
        RoundRectangle borderStrokeShape = new RoundRectangle
        {
            CornerRadius = new CornerRadius(0, 0, 0, 0)
        };

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
        

        Border headerBorder2 = new Border();
        headerBorder2.Stroke = Colors.Black;
        headerBorder2.StrokeThickness = 2;
        headerBorder2.StrokeShape = borderStrokeShape;
        headerBorder2.WidthRequest = 80;
        headerBorder2.Padding = 0;

        Button hb2Btn = new Button();
        hb2Btn.Text = "Lånt ↓";
        hb2Btn.TextColor = Colors.Black;
        hb2Btn.Background = Colors.Transparent;
        headerBorder2.Content = hb2Btn;
        

        Border headerBorder3 = new Border();
        headerBorder3.Stroke = Colors.Black;
        headerBorder3.StrokeThickness = 2;
        headerBorder3.StrokeShape = borderStrokeShape;
        headerBorder3.WidthRequest = 100;
        headerBorder3.Padding = 0;

        Button hb3Btn = new Button();
        hb3Btn.Text = "Afleveret";
        hb3Btn.TextColor = Colors.Black;
        hb3Btn.Background = Colors.Transparent;
        headerBorder3.Content = hb3Btn;



        hb1Btn.Clicked += (sender, e) =>
        {
            if (userSort)
            {
                userSort = false;
                hb1Btn.Text = "Bruger ↑";
                bookList = bookList.OrderBy(X => X.UserName).ToList();
                createTable(bookList);
            }
            else
            {
                userSort = true;
                hb1Btn.Text = "Bruger ↓";
                bookList = bookList.OrderByDescending(X => X.UserName).ToList();
                createTable(bookList);
            }
            startSort = false;
            endSort = false;
            hb2Btn.Text = "Lånt";
            hb3Btn.Text = "Afleveret";

        };

        hb2Btn.Clicked += (s, arg) =>
        {
            if (startSort)
            {
                startSort = false;
                hb2Btn.Text = "Lånt ↑";
                bookList = bookList.OrderBy(X => X.StartDate).ToList();
                createTable(bookList);
            }
            else
            {
                startSort = true;
                hb2Btn.Text = "Lånt ↓";
                bookList = bookList.OrderByDescending(X => X.StartDate).ToList();
                createTable(bookList);
            }
            userSort = false;
            endSort = false;
            hb1Btn.Text = "Bruger";
            hb3Btn.Text = "Afleveret";
        };

        hb3Btn.Clicked += (s, arg) =>
        {
            if(endSort)
            {
                endSort = false;
                hb3Btn.Text = "Afleveret ↑";
                bookList = bookList.OrderBy(X => X.EndDate).ToList();
                createTable(bookList);
            }
            else
            {
                endSort = true;
                hb3Btn.Text = "Afleveret ↓";
                bookList = bookList.OrderByDescending(X => X.EndDate).ToList();
                createTable(bookList);
            }
            userSort = false;
            startSort = false;
            hb1Btn.Text = "Bruger";
            hb2Btn.Text = "Lånt";
        };


        headerStack.Children.Add(headerBorder1);
        headerStack.Children.Add(headerBorder2);
        headerStack.Children.Add(headerBorder3);
    }

    private void createTable(List<RentedBook> list)
    {
        tableStack.Clear();
        

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
            userLabel.Text = book.UserName;
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
