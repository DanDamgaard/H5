using MobileApp.Classes;
using MobileApp.Services;

namespace MobileApp.Pages;

public partial class UserRentHistoryPage : ContentPage
{
    private readonly User _user;
    private List<RentedBook> rentedBooks = new List<RentedBook>();
    private Api api = new Api();

    public UserRentHistoryPage(User user)
	{
		InitializeComponent();
        _user = user;
        getRentedBooks();
    }

    private async void getRentedBooks()
    {
        List<RentedBook> books = await api.getUsersAndRentedBooks();

        
        foreach (RentedBook book in books)
        {
            if (book.UserId == _user.Id)
            {
                rentedBooks.Add(book);
            }
        }

        if(rentedBooks.Count > 0)
        {
            createBookList();
        }
        else
        {
            Label label = new Label();
            label.HorizontalOptions = LayoutOptions.Center;
            label.VerticalOptions = LayoutOptions.Center;
            label.FontSize = 18;
            label.Text = Global.User == _user ? "Du har ingen udlejninger" : "Bruger har ingen udlejninger";
            UserStack.Add(label);
        }
        
    }

    private async void createBookList()
    {
        foreach(RentedBook book in rentedBooks)
        {
            Book selectedBook = await api.getBook(book.BookId);
            HorizontalStackLayout hs = new HorizontalStackLayout();
            hs.Spacing = 5;

            VerticalStackLayout vs1 = new VerticalStackLayout();
            vs1.WidthRequest = 150;

            VerticalStackLayout vs2 = new VerticalStackLayout();
            vs2.Spacing = 5;

            Image image = new Image();
            image.Source = selectedBook.Image;
            vs1.Children.Add(image);
            hs.Children.Add(vs1);

            Label titel = new Label();
            titel.Text = selectedBook.Title;
            titel.LineBreakMode = LineBreakMode.WordWrap;
            titel.MaxLines = 3;
            titel.WidthRequest = 200;
            titel.HeightRequest = 60;
            vs2.Children.Add(titel);

            Label rentDate = new Label();
            Label returnDate = new Label();
            rentDate.Text = "Lånt: " + book.StartDate.ToString("dd/MM/yyyy");
            returnDate.Text = "Afleverings dato: " + book.EndDate.ToString("dd/MM/yyyy");
            vs2.Children.Add(rentDate);
            vs2.Children.Add(returnDate);

            hs.Children.Add(vs2);
            Button button = new Button();
            button.Text = "Se bog";
            button.Clicked += (s, arg) =>
            {
                Navigation.PushModalAsync(new BookDetailPage(selectedBook));
            };
            vs2.Children.Add(button);

            UserStack.Add(hs);
        }
    }
}