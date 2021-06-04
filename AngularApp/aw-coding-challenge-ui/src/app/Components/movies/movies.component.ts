import { Component, OnInit } from '@angular/core';
import { Movie } from '../model/movie';
import { MoviesService } from './movies.service';
import { map,catchError  } from 'rxjs/operators';
import { DatePipe } from '@angular/common'

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css'],
  providers: [MoviesService]
})
export class MoviesComponent implements OnInit {

movies: Movie[] = [];

  constructor(private service: MoviesService, private datepipe: DatePipe) { 

    //this.movies = [];
  }

  ngOnInit(): void {
      this.service.getAllMovies()
      .pipe(map((data: any) => {
       
         data.forEach((element: { releaseDate: any; }) => {
          element.releaseDate = element.releaseDate !== null ? this.datepipe.transform(new Date(element.releaseDate), 'yyyy-MM-dd') : '';
           
         });
              
      return data;
      
                }))
      .subscribe(data => {
          this.movies = data;

        console.log(data);
      })
  }

  getMoviesByTitle(title: string): void {
    this.service.getMoviesByTitle(title)
    .pipe(map((data: any) => {
       
      data.forEach((element: { releaseDate: any; }) => {
       element.releaseDate = element.releaseDate !== null ? this.datepipe.transform(new Date(element.releaseDate), 'yyyy-MM-dd') : '';
        
      });
           
          return data;
   
    }))
  
    .subscribe(data => {
        this.movies = data;
      },
      error => {

        console.log(error.message);
      }
      
      );
  }

}
