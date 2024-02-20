import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Contributor } from 'app/models/contributor';
import { Task } from 'app/models/task';
import { ContributorsService } from 'app/services/contributors/contributors.service';
import { TasksService } from 'app/services/tasks/tasks.service';
import { TableListDialogComponent } from 'app/table-list-dialog/table-list-dialog.component';

@Component({
  selector: 'app-table-list',
  templateUrl: './table-list.component.html',
  styleUrls: ['./table-list.component.css']
})

// Komponenta TableListComponent je deo koda koji cemo na vezbama menjati
export class TableListComponent implements OnInit {

  // Nizovi koji ce se koristiti za prikazivanje podataka. Moraju biti tipizirani kako bismo na jednostavan nacin pristupali njihovim obelezjima
  tasks: Task[] = [];
  contributors: Contributor[] = [];

  // Kroz konstruktor uvek zelimo da injektujemo sve potrebne servise, koje kasnije mozemo slobodno koristiti u kodu.
  constructor(private taskService: TasksService,
              private contributorsService: ContributorsService,
              private dialogModel: MatDialog) { }

  // Kod u metodi ngOnInit ima potencijal da poprilicno naraste. Kreirana je novu metoda startSubscription koja se poziva iz nje
  ngOnInit() {
    this.startSubscription();
  }

  // Prilikom ucitavanja stranice, pozivaju se metode iz prethodno kreiranih servisa. Neophodno je za ekstraktovanje njihovog rezultata (tj. response)
  // pozvati metodu subscribe kojom ce se objekat res (tj. prakticno body odgovora) dodeliti odgovarajucem nizu.
  startSubscription() {
    this.contributorsService.getContributors().subscribe(res => {
      this.contributors = res;
    });

    this.taskService.getTasks().subscribe(res => {
      res.forEach( item => {
        if (item.assignee !== "") {
          const contributor = this.contributors.find( con => con.userName == item.assignee );
          if (contributor) {
            this.tasks.push({ id: item.id, title: item.title, description: item.description, assignee: contributor });
          }
        } else {
          this.tasks.push({ id: item.id, title: item.title, description: item.description, assignee: new Contributor() });
        }
      })
    });
  }

  deleteTask(e : Task) {
    const taskIndex = this.tasks.findIndex( (obj: Task) => obj.id == e.id);
    this.tasks.splice(taskIndex, 1);
  }

  openDialog(e: Task) {
    const dialog = this.dialogModel.open(TableListDialogComponent, {
      width: '600px',
      data: { title: e.title, description: e.description, assignee: e.assignee, contributors: this.contributors }
    });
      
    dialog.afterClosed().subscribe(result => {
      if (result) {
        const taskIndex = this.tasks.findIndex( (obj: Task) => obj.id == e.id);
        this.tasks[taskIndex].title = result.title;
        this.tasks[taskIndex].description = result.description;
        this.tasks[taskIndex].assignee = result.assignee;
      }
    });
  }
}
