import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Contributor } from 'app/models/contributor';
import { TaskDialogData } from 'app/models/task-dialog-data';

@Component({
  selector: 'table-list-dialog',
  templateUrl: './table-list-dialog.component.html',
  styleUrls: ['./table-list-dialog.component.scss']
})
export class TableListDialogComponent implements OnInit {
  dialogData : TaskDialogData;

  constructor(
    public dialogRef: MatDialogRef<TableListDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: TaskDialogData) { }

  ngOnInit(): void {
  }

  onClick(): void {
    this.dialogRef.close();
  }

}
