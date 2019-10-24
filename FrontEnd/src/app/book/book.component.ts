import { Component, OnInit } from '@angular/core';
import { BookService } from '../services/book.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { Book } from './book';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})
export class BookComponent implements OnInit {

  constructor(private bookService: BookService, private authService: AuthService, private router: Router) { }

  data: Book[] = [];
  displayedColumns: string[] = ['bookId', 'isbn', 'title'];
  isLoadingResults = true;
  is: string;
  role: string = localStorage.getItem('role');

  //Lifecycle primeiro carregamento da página
  ngOnInit() {
  }

  // //Lifecyle carregamento de componentes internos
  ngAfterContentInit() {
    this.getBooks();
  }

  //Retorna se sou administrador ou não
  get isAdmin() {
    return this.role == "Admin";
  }

  //Chama o serviço que retorna a lista de livros
  async getBooks() {
    this.bookService.getBooks()
      .subscribe(books => {
        this.data = books;
        this.isLoadingResults = false;
      }, err => {
        console.log(err);
        this.isLoadingResults = false;
      });
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    this.router.navigate(['login']);
  }

  controllAccess() {
    this.router.navigate(['controllAccess']);
  }

}
