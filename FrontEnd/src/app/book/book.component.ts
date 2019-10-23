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

  ngOnInit() {
    this.getBooks();
  }

  getBooks(): void {
    this.bookService.getBooks()
      .subscribe(books => {
        this.data = books;
        console.log(this.data);
        this.isLoadingResults = false;
      }, err => {
        console.log(err);
        this.isLoadingResults = false;
      });
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['login']);
  }

}
