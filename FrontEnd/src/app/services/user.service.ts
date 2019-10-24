import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    apiUrl = 'https://localhost:44348/api/user';

    constructor(private http: HttpClient) { }

    getUser(email: any): Observable<any> {
        return this.http.get<any>(this.apiUrl, {
            params: {
                email
            }
        })
            .pipe(
                tap(_ => this.log('Usuário recuperado com sucesso!')),
                catchError(this.handleError('Erro ao recuperar usuário!', []))
            );
    }

    alterar(data: any): Observable<any> {
        return this.http.put<any>(this.apiUrl + '/alterar', data)
            .pipe(
                tap(_ => this.log('Alterado role com sucesso!')),

                catchError(this.handleError('Erro ao alterar role!', []))
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