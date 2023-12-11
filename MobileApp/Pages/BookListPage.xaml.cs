using Microsoft.Maui.Controls;
using Microsoft.Maui;
using MobileApp.Classes;
using MobileApp.Services;
using System.Diagnostics;
namespace MobileApp.Pages;

public partial class BookListPage : ContentPage
{
    private List<Book> bookList = new List<Book>();
    private Api api = new Api();
    public BookListPage()
    {
        InitializeComponent();
        GetBooks();

    }

    private void createBookList(List<Book> books)
    {
        BookStack.Children.Clear();

        foreach (Book book in books)
        {
            VerticalStackLayout stack = new VerticalStackLayout();
            stack.HorizontalOptions = LayoutOptions.Center;
            stack.WidthRequest = 300;
            stack.Spacing = 10;

            Image img = new Image();
            img.Source = book.Image;
            stack.Children.Add(img);

            Label lbl = new Label();
            lbl.Text = book.Title;
            stack.Children.Add(lbl);

            Button button = new Button();
            button.Text = "Se detaljer";
            button.Clicked += (s, arg) =>
            {
                Navigation.PushModalAsync(new BookDetailPage(book));
            };

            if(Global.User.Roles == 1)
            {
                Button editBtn = new Button();
                editBtn.Text = "Ret bog";
                editBtn.Clicked += async (s, arg) =>
                {
                    await Navigation.PushModalAsync(new AdminEditBook(book));
                };

                Button historybtn = new Button();
                historybtn.Text = "Se historik";
                historybtn.Clicked += async (s, arg) =>
                {
                   await Navigation.PushModalAsync(new BookHistoryPage(book));
                };

                Button deleteBtn = new Button();
                deleteBtn.Text = "Slet bog";
                deleteBtn.Background = Colors.Red;
                deleteBtn.Clicked += async (s, arg) =>
                {
                    bool answer = await DisplayAlert("Advarsel", "Er du sikker på at du vil slette denne bog", "Ja", "Nej");
                    if(answer)
                    {
                        if(await api.deleteBook(book.Id))
                        {
                            await DisplayAlert("Succes", "Bogen blev slettet", "OK");
                            GetBooks();
                        }
                        else
                        {
                            await DisplayAlert("Fejl", "Kunne ikke slette bogen", "OK");
                        }
                    }
                    else
                    {
                        return;
                    }
                };

                stack.Children.Add(editBtn);
                stack.Children.Add(historybtn);
                stack.Children.Add(deleteBtn);
            }

            stack.Children.Add(button);

            BookStack.Children.Add(stack);
        }
    }

    private async void GetBooks()
    {
        bookList = await api.GetBooks();

        createBookList(bookList);
        createFilter();
    }

    private void createFilter()
    {
        genreStack1.Clear();
        genreStack2.Clear();

        List<string> genre = new List<string>();
        List<string> auther = new List<string>();
        List<string> publisher = new List<string>();

        foreach (Book book in bookList)
        {
            if (!genre.Contains(book.Category))
            {
                genre.Add(book.Category);
            }

            if (!auther.Contains(book.AuthorName))
            {
                auther.Add(book.AuthorName);
            }

            if (!publisher.Contains(book.Publisher))
            {
                publisher.Add(book.Publisher);
            }
        }



        bool changeStack = true;

        foreach (string s in genre)
        {
            HorizontalStackLayout stack = new HorizontalStackLayout();


            Button btn = new Button();
            btn.Text = s;
            btn.Padding = 0;
            btn.TextColor = Colors.Black;
            btn.HeightRequest = 20;
            btn.Background = Colors.Transparent;

            CheckBox box = new CheckBox();


            btn.Clicked += (s, arg) =>
            {
                box.IsChecked = !box.IsChecked;
            };

            stack.Children.Add(btn);
            stack.Children.Add(box);


            if (changeStack)
            {
                genreStack1.Children.Add(stack);
                changeStack = false;
            }
            else
            {
                genreStack2.Children.Add(stack);
                changeStack = true;
            }
        }

        changeStack = true;

        foreach (string s in auther)
        {
            HorizontalStackLayout stack = new HorizontalStackLayout();


            Button btn = new Button();
            btn.Text = s;
            btn.Padding = 0;
            btn.TextColor = Colors.Black;
            btn.HeightRequest = 20;
            btn.Background = Colors.Transparent;

            CheckBox box = new CheckBox();

            btn.Clicked += (s, arg) =>
            {
                box.IsChecked = !box.IsChecked;
            };

            stack.Children.Add(btn);
            stack.Children.Add(box);


            if (changeStack)
            {
                autherStack1.Children.Add(stack);
                changeStack = false;
            }
            else
            {
                autherStack2.Children.Add(stack);
                changeStack = true;
            }
        }

        foreach (string s in publisher)
        {
            HorizontalStackLayout stack = new HorizontalStackLayout();


            Button btn = new Button();
            btn.Text = s;
            btn.Padding = 0;
            btn.TextColor = Colors.Black;
            btn.HeightRequest = 20;
            btn.Background = Colors.Transparent;

            CheckBox box = new CheckBox();

            btn.Clicked += (s, arg) =>
            {
                box.IsChecked = !box.IsChecked;
            };

            stack.Children.Add(btn);
            stack.Children.Add(box);


            if (changeStack)
            {
                publisherStack1.Children.Add(stack);
                changeStack = false;
            }
            else
            {
                publisherStack2.Children.Add(stack);
                changeStack = true;
            }
        }
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        filterBooks();
    }

    private bool isFilterUsed()
    {
        foreach (HorizontalStackLayout h in genreStack1)
        {
            CheckBox box = (CheckBox)h.Children[1];
            if (box.IsChecked == true)
            {
                return true;
            }
        }

        foreach (HorizontalStackLayout h in genreStack2)
        {
            CheckBox box = (CheckBox)h.Children[1];
            if (box.IsChecked == true)
            {
                return true;
            }
        }

        foreach (HorizontalStackLayout h in autherStack1)
        {
            CheckBox box = (CheckBox)h.Children[1];
            if (box.IsChecked == true)
            {
                return true;
            }
        }

        foreach (HorizontalStackLayout h in autherStack2)
        {
            CheckBox box = (CheckBox)h.Children[1];
            if (box.IsChecked == true)
            {
                return true;
            }
        }

        foreach (HorizontalStackLayout h in publisherStack1)
        {
            CheckBox box = (CheckBox)h.Children[1];
            if (box.IsChecked == true)
            {
                return true;
            }
        }

        foreach (HorizontalStackLayout h in publisherStack2)
        {
            CheckBox box = (CheckBox)h.Children[1];
            if (box.IsChecked == true)
            {
                return true;
            }
        }

        return false;
    }

    private void FilterBtn_Clicked(object sender, EventArgs e)
    {
        ListStack.IsVisible = false;
        FilterStack.IsVisible = true;

    }

    private void filterBooks()
    {
        List<Book> data = bookList;
        List<Book> newBookList = new List<Book>();
        List<Book> searchBookList = new List<Book>();
        List<string> genreFilterList = new List<string>();
        List<string> autherFilterList = new List<string>();
        List<string> publisherFilterList = new List<string>();


        

        if(!String.IsNullOrEmpty(searchBar.Text))
        {
            foreach(Book book in bookList)
            {
                if (book.Title.ToLower().Contains(searchBar.Text))
                {
                    searchBookList.Add(book);
                }
            }
        }

        if (isFilterUsed())
        {
            foreach (HorizontalStackLayout h in genreStack1)
            {
                Button btn = (Button)h.Children[0];
                CheckBox box = (CheckBox)h.Children[1];
                string genre = btn.Text;

                if (box.IsChecked == true)
                {
                    genreFilterList.Add(genre);
                }
            }

            foreach (HorizontalStackLayout h in genreStack2)
            {
                Button btn = (Button)h.Children[0];
                CheckBox box = (CheckBox)h.Children[1];
                string genre = btn.Text;

                if (box.IsChecked == true)
                {
                    genreFilterList.Add(genre);
                }
            }

            foreach (HorizontalStackLayout h in autherStack1)
            {
                Button btn = (Button)h.Children[0];
                CheckBox box = (CheckBox)h.Children[1];
                string genre = btn.Text;

                if (box.IsChecked == true)
                {
                    autherFilterList.Add(genre);
                }
            }

            foreach (HorizontalStackLayout h in autherStack2)
            {
                Button btn = (Button)h.Children[0];
                CheckBox box = (CheckBox)h.Children[1];
                string genre = btn.Text;

                if (box.IsChecked == true)
                {
                    autherFilterList.Add(genre);
                }
            }

            foreach (HorizontalStackLayout h in publisherStack1)
            {
                Button btn = (Button)h.Children[0];
                CheckBox box = (CheckBox)h.Children[1];
                string genre = btn.Text;

                if (box.IsChecked == true)
                {
                    publisherFilterList.Add(genre);
                }
            }

            foreach (HorizontalStackLayout h in publisherStack2)
            {
                Button btn = (Button)h.Children[0];
                CheckBox box = (CheckBox)h.Children[1];
                string genre = btn.Text;

                if (box.IsChecked == true)
                {
                    publisherFilterList.Add(genre);
                }
            }

            if (searchBookList.Count > 0)
            {
                data = searchBookList;
            }

            foreach (Book book in data)
            {
                newBookList.Add(book);
            }

            foreach (Book book in data)
            {
                
                if (!genreFilterList.Contains(book.Category) && genreFilterList.Count > 0)
                {
                    newBookList.Remove(book);
                }


                if (!autherFilterList.Contains(book.AuthorName) && autherFilterList.Count > 0)
                {
                    newBookList.Remove(book);
                }

                
                if (!publisherFilterList.Contains(book.Publisher) && publisherFilterList.Count > 0)
                {
                    newBookList.Remove(book);
                }

            }


        }
        
        if (isFilterUsed())
        {
            createBookList(newBookList);
        }
        else if(!isFilterUsed() && searchBookList.Count > 0)
        {
            createBookList(searchBookList);
        }
        else
        {
            createBookList(bookList);
        }

    }


    private void CloseFilter_Clicked(object sender, EventArgs e)
    {
        filterBooks();
        
        ListStack.IsVisible = true;
        FilterStack.IsVisible = false;
    }

    private void searchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        searchBar.Unfocus();
    }

    private void ClearFilterBtn_Clicked(object sender, EventArgs e)
    {
        foreach (HorizontalStackLayout h in genreStack1)
        {
            CheckBox box = (CheckBox)h.Children[1];
            box.IsChecked = false;
        }

        foreach (HorizontalStackLayout h in genreStack2)
        {
            CheckBox box = (CheckBox)h.Children[1];
            box.IsChecked = false;
        }

        foreach (HorizontalStackLayout h in autherStack1)
        {
            CheckBox box = (CheckBox)h.Children[1];
            box.IsChecked = false;
        }

        foreach (HorizontalStackLayout h in autherStack2)
        {
            CheckBox box = (CheckBox)h.Children[1];
            box.IsChecked = false;
        }

        foreach (HorizontalStackLayout h in publisherStack1)
        {
            CheckBox box = (CheckBox)h.Children[1];
            box.IsChecked = false;
        }

        foreach (HorizontalStackLayout h in publisherStack2)
        {
            CheckBox box = (CheckBox)h.Children[1];
            box.IsChecked = false;
        }
    }
}