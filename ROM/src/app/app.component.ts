import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Single Slice';

  constructor(
    private router: Router) { }

  redirectToMain() {
    this.router.navigate(['/order']);
  }
}
