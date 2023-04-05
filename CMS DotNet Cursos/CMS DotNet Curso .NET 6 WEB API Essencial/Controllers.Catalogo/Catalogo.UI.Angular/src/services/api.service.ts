import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { catchError, tap, map } from 'rxjs/operators';
import { Categoria } from 'src/model/categoria';
import { Usuario } from 'src/model/usuario';

const apiUrl = 'https://localhost:7176/api/v1/categorias';
const apiLoginUrl = 'https://localhost:7176/api/autoriza/login';
var token = '';
var httpOptions = { headers: new HttpHeaders({ "Content-Type": "application/json" }) };

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  public montaHeaderToken() {
    token = localStorage.getItem("jwt") || '';
    //console.log('jwt header token ' + token);
    httpOptions = { headers: new HttpHeaders({ "Authorization": "Bearer " + token, "Content-Type": "application/json" }) };
  }

  public Login(dados): Observable<Usuario> {
    return this.http.post<Usuario>(apiLoginUrl, dados).pipe(
      tap((response: any) => console.log(`Login usuario com Token = ${dados.email} -  Token = ${response.Token}`)),
      catchError(this.handleError<Usuario>('Login'))
    );
  }

  public getCategorias(): Observable<Categoria[]> {
    this.montaHeaderToken();
    console.log(httpOptions.headers);
    return this.http.get<Categoria[]>(apiUrl, httpOptions)
      .pipe(
        tap((response: any) => console.log('leu as Categorias')),
        catchError(this.handleError('getCategorias', []))
      );
  }

  public getCategoria(id: any): Observable<Categoria> {
    this.montaHeaderToken();
    //console.log(httpOptions.headers);
    const url = `${apiUrl}/${id}`;
    //console.log(`url=${url}`);
    return this.http.get<Categoria>(url, httpOptions).pipe(
      tap((response: any) => console.log(`leu a Categoria id=${id}`)),
      catchError(this.handleError<Categoria>(`getCategoria id=${id}`))
    );
  }

  public addCategoria(dados): Observable<Categoria> {
    this.montaHeaderToken();
    return this.http.post<Categoria>(apiUrl, dados, httpOptions).pipe(
      tap((response: any) => console.log(`adicionou a Categoria com w/ id=${response.Id}`)),
      catchError(this.handleError<Categoria>('addCategoria'))
    );
  }

  public updateCategoria(id: any, dados): Observable<any> {
    const url = `${apiUrl}/${id}`;
    console.log(`url=${url}`);
    console.log(`dados=${dados}`);
    this.montaHeaderToken();
    return this.http.put(url, dados, httpOptions).pipe(
      tap(_ => console.log(`atualiza o produco com id=${id}`)),
      catchError(this.handleError<any>('updateCategoria'))
    );
  }

  public deleteCategoria(id: any): Observable<Categoria> {
    const url = `${apiUrl}/${id}`;
    return this.http.delete<Categoria>(url, httpOptions).pipe(
      tap(_ => console.log(`remove o Categoria com id=${id}`)),
      catchError(this.handleError<Categoria>('deleteCategoria'))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }

}
