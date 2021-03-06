import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  apiUrl = 'https://localhost:44348/api/auth';

  constructor(private http: HttpClient) { }

  login(data: any): Observable<any> {
    return this.http.post<any>(this.apiUrl + '/login', data)
      .pipe(
        tap(_ => this.log('Login realizado com sucesso!')),
        catchError(this.handleError('Erro ao realizar login!', []))
      );
  }

  register(data: any): Observable<any> {
    return this.http.post<any>(this.apiUrl + '/register', data)
      .pipe(
        tap(_ => this.log('Registro realizado com sucesso!')),
        catchError(this.handleError('Erro ao tentar realizar registro!', []))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);

      this.login(`${operation} failed: ${error.message}`);

      return of(result as T);
    };
  }

  private log(message: string) {
    console.log(message);
  }
}
