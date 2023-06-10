import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Student } from '../models/ui-models/student.model';           
import { StudentService } from './student.service';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css'],
})
export class StudentsComponent implements OnInit {
students: Student[] = [];
displayedColumns: string[] = ['firstName', 'lastName', 'dateOfBirth', 'email', 'mobile','gender','edit'];

dataSource: MatTableDataSource<Student> = new MatTableDataSource<Student>();
@ViewChild(MatPaginator) matPaginator!:MatPaginator;
@ViewChild(MatSort) matSort!:MatSort;

filterString = '';

  constructor(private studentService: StudentService) {}

  ngOnInit(): void {                                        //<--  //ngOnInit is a life cycle hook called by Angular to indicate that the Angular is done creating the component.

    //<-- //
    //Fetch Students
    this.studentService.getStudents().subscribe(           //<--  //.is a method in Angular that connects the observer to observable events.It is a method from the rxjs library, used internally by Angular
      (successResponse) => {
        this.students = successResponse;
        this.dataSource = new MatTableDataSource<Student>(this.students);
        if(this.matPaginator){
        this.dataSource.paginator = this.matPaginator;
        }

        if(this.matSort){

this.dataSource.sort = this.matSort;

        }

      },
      (errorResponse) => {
        console.log(errorResponse);
      }
    );
  }
  filterStudents(){

 this.dataSource.filter = this.filterString.trim().toLowerCase();
  }
}
