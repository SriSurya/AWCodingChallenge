
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Movie } from '../model/movie';

@Injectable()
export class MoviesService {

 

  constructor(private http: HttpClient) {


   }

  getAllMovies() {
    return this.http.get<Movie[]>("http://localhost:62926/api/v1/Movies");
  }

  getMoviesByTitle(title: string){
    return this.http.get<Movie[]>("http://localhost:62926/api/v1/Movies/" + title);
  }

  
}

