import { Component } from '@angular/core';

@Component({
  selector: 'app-hamburger',
  templateUrl: './hamburger.component.html',
  styleUrl: './hamburger.component.css'
})
export class HamburgerComponent {
  
    menuOpen = false;
    categories: string[] = ['Category1', 'Category2', 'Category3'];
  
    toggleMenu() {
      this.menuOpen = !this.menuOpen;
     
    }
    openAddCategoryDialog() {
      
      console.log("Add Category dialog opened");
    }
}




