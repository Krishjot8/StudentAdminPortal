import { Component } from '@angular/core';        // The import of the Component class that is defined in the module @angular/core is being done in this line.



@Component({
  selector: 'app-root',                            //The reference to the selector, templateUrl, and styleUrls are given in the declarator. The selector is a tag that is used to be placed in the index.html file.
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
                                              //The AppComponent class has a title variable, which is used to display the application’s title in the browser. In this case, angular will be shown in the browser.
}





