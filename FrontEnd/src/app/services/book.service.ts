import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Book } from '../book/book';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  apiUrl = 'https://localhost:44348/api/book';

  constructor(private http: HttpClient) { }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl)
      .pipe(
        tap(_ => this.log('Livros retornados com sucesso!')),
        catchError(this.handleError('Erro ao retornar livros', []))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error);

      this.log(`${operation} failed: ${error.message}`);

      return of(result as T);
    };
  }

  private log(message: string) {
    console.log(message);
  }
}