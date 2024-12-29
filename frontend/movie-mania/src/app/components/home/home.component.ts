import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  movies = [
    {
      poster: 'https://via.placeholder.com/150',
      name: 'Inception',
      year: 2010,
      imdb: 8.8,
    },
    {
      poster: 'https://via.placeholder.com/150',
      name: 'The Dark Knight',
      year: 2008,
      imdb: 9.0,
    },
    {
      poster: 'https://via.placeholder.com/150',
      name: 'Interstellar',
      year: 2014,
      imdb: 8.6,
    },
  ];
}
