import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { User } from '../auth/user';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    apiUrl = 'https://localhost:44348/api/user';

    constructor(private http: HttpClient) { }

    getUser(id: number): Observable<User[]> {
        return this.http.get<User[]>(this.apiUrl + '/' + id)
            .pipe(
                tap(_ => this.log('user')),
                catchError(this.handleError('getUser', []))
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