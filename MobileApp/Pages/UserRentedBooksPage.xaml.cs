using MobileApp.Classes;
using MobileApp.Services;

namespace MobileApp.Pages;

public partial class UserRentedBooksPage : ContentPage
{
    private readonly User _user;
    private List<RentedBook> rentedBooks = new List<RentedBook>();
    private Api api = new Api();
    public UserRentedBooksPage(User user)
	{
		InitializeComponent();
        _user = user;
        getRentedBooks();
    }

    private async void getRentedBooks()
    {
        List<RentedBook> books = await api.getRentedBooks();


        foreach (RentedBook book in books)
        {
            if (book.UserId == _user.Id)
            {
                rentedBooks.Add(book);
            }
        }

        if (rentedBooks.Count > 0)
        {
            createBookList();
        }
        else
        {
            Label label = new Label();
            label.HorizontalOptions = LayoutOptions.Center;
            label.VerticalOptions = LayoutOptions.Center;
            label.FontSize = 18;
            label.Text = "Bruger har ingen udlejninger";
            UserStack.Add(label);
        }

    }

    private async void createBookList()
    {
        foreach (RentedBook book in rentedBooks)
        {
            HorizontalStackLayout hs = new HorizontalStackLayout();
            hs.Spacing = 5;

            VerticalStackLayout vs1 = new VerticalStackLayout();
            vs1.WidthRequest = 150;

            VerticalStackLayout vs2 = new VerticalStackLayout();
            vs2.Spacing = 5;

            Image image = new Image();
            image.Source = book.Book.Image;
            vs1.Children.Add(image);
            hs.Children.Add(vs1);

            Label titel = new Label();
            titel.Text = book.Book.Title;
            titel.LineBreakMode = LineBreakMode.WordWrap;
            titel.MaxLines = 3;
            titel.WidthRequest = 200;
            titel.HeightRequest = 60;
            vs2.Children.Add(titel);

            Label rentDate = new Label();
            Label returnDate = new Label();
            rentDate.Text = "L�nt: " + book.StartDate.ToString("dd/MM/yyyy");
            string returnStr = book.Book.Status == 1 ? "Aflereret: " : "Afleveres: ";
            returnDate.Text = returnStr + book.EndDate.ToString("dd/MM/yyyy");
            vs2.Children.Add(rentDate);
            vs2.Children.Add(returnDate);

            hs.Children.Add(vs2);
            Button button = new Button();

            if(book.Book.Status == 1)
            {
                button.Text = "Aflevere bog";
                button.Clicked += async (s, arg) =>
                {
                    if(await api.returnBook(book))
                    {
                        await DisplayAlert("Succes", "Bogen blev afleveret", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Fejl", "N�et gik galt pr�v igen senere", "OK");
                    }
                };
            }
            else
            {
                Book testBook = await api.getBook(book.Bookid);
                if(testBook.Status == 0)
                {
                    button.Text = "Udl�nd bog Igen";
                    button.Clicked += async (s, arg) =>
                    {
                        if (await api.reRentBook(book))
                        {
                            await DisplayAlert("Succes", "Du har udl�nt bogen", "OK");
                        }
                        else
                        {
                            await DisplayAlert("Fejl", "N�et gik galt pr�v igen senere", "OK");
                        }
                    };

                }
                else
                {
                    button.IsVisible = false;
                }
            }
            
            vs2.Children.Add(button);

            UserStack.Add(hs);
        }
    }
}